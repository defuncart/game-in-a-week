/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Spawns an object A every B seconds with vertical variation C.</summary>
public class ObjectSpawner : MonoBehaviour
{
	/// <summary>An array of objects to spawn.</summary>
	[Tooltip("An array of objects to spawn.")]
	[SerializeField] private GameObject[] objectsToSpawn;
	/// <summary>The probabilities of each object to be spawned.</summary>
	[Tooltip("The probabilities of each object to be spawned.")]
	[SerializeField] private float[] spawnProbabilities;
	/// <summary>A struct denoting a (min, max) pair.</summary>
	[System.Serializable]
	private struct Range
	{
		/// <summary>The minimum value.</summary>
		public float min;
		/// <summary>The minimum value.</summary>
		public float max;
	}
	/// <summary>An array of (min, max) pairs to denote vertical spawn range.</summary>
	[Tooltip("An array of (min, max) pairs to denote vertical spawn range.")]
	[SerializeField] private Range[] verticalSpawnRange;
	/// <summary>The minimum duration between spawned objects.</summary>
	[Tooltip("The minimum duration between spawned objects.")]
	[Range(0.5f, 5f)] [SerializeField] private float spawnMin = 1f;
	/// <summary>The maximum duration between spawned objects.</summary>
	[Tooltip("The maximum duration between spawned objects")]
	[Range(0.5f, 5f)] [SerializeField] private float spawnMax = 2f;

	#if UNITY_EDITOR
	/// <summary>Callback when the object awakes.</summary>
	private void Awake()
	{
		Assert.IsTrue(objectsToSpawn.Length == spawnProbabilities.Length);
		Assert.IsTrue(spawnProbabilities.Sum() == 1f);
		Assert.IsTrue(spawnMax > spawnMin);
	}
	#endif

	/// <summary>Sets whether objects can be spawned.</summary>
	public void SetSpawnable(bool spawnable)
	{
		if(spawnable) { DestroySpawnedObjects(); SpawnObject(); }
		else { StopAllCoroutines(); }
	}

	/// <summary>Spawns the object.</summary>
	private void SpawnObject()
	{
		//determine an object to spawn
		GameObject objectToSpawn = objectsToSpawn.RandomObjectWithProbabilityDistribution(spawnProbabilities);

		//get the object's index
		int index = System.Array.IndexOf(objectsToSpawn, objectToSpawn);

		//set the position to be the spawners x and object's y value (with variation)
		Vector3 position = new Vector3(transform.position.x, objectToSpawn.transform.position.y, 0);
		position.y += Random.Range(verticalSpawnRange[index].min, verticalSpawnRange[index].max);

		//instantiate a new object and parent it to the spawner
		GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity) as GameObject;
		spawnedObject.transform.parent = this.transform;

		//determine adjusted (min, max) spawn intervals depending on the game's speed
		float adjustedMin = spawnMin / GameScene.gameSpeed, adjustedMax = spawnMax / GameScene.gameSpeed;

		//spawn another object in B seconds - doesn't use cached yields
		this.Invoke(action: () => {
			SpawnObject();
		}, time: Random.Range(adjustedMin, adjustedMax), useCachedWaits: false);
	}

	/// <summary>Destroys the spawned objects.</summary>
	private void DestroySpawnedObjects()
	{
		IEnumerable<Transform> objects = transform.Children();
		foreach(Transform transform in objects) { Destroy(transform.gameObject); }
	}
}
