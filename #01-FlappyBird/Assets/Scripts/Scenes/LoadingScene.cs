/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 */
using DeFuncArt.Serialization;
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>This script controls the loading scene.</summary>
public class LoadingScene : MonoBehaviour
{
	///DEBUG
	[SerializeField] private GameObject resetButton;
	///DEBUG

	/// <summary>Callback when the instance is awoken.</summary>
	private void Awake()
	{
		//if the game hasn't been previously launced, create and set initial data
		if(!PlayerPreferences.HasKey(PlayerPreferencesKeys.previouslyLaunched))
		{
			DataManager.Initialize();
		}
		DataManager.Verify();
		//load game scene - doesn't use cached yields
		this.Invoke(action: () => {
				SceneManager.LoadScene(SceneBuildIndeces.GameScene);
		}, time: AnimationDuration.LOADING_SCENE, useCachedWaits: false);
	}

	///DEBUG
	public void ResetButtonPressed()
	{
		DataManager.ReloadData();
		resetButton.SetActive(false);
	}
	///DEBUG
}
