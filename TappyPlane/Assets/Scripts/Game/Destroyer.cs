/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using UnityEngine;

/// <summary>Used by the Destroyer game object to destroy certain objects that collide with it.</summary>
public class Destroyer : MonoBehaviour
{
	/// <summary>An array of object tags that the destroyer should destroy.</summary>
	[Tooltip("An array of object tags that the destroyer should destroy")]
	[SerializeField] private string[] tags;

	/// <summary>Callback when the OnTriggerEnter2D event is called.</summary>
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(tags.Contains(collider.tag))
		{
			Destroy(collider.gameObject);
		}
	}
}
