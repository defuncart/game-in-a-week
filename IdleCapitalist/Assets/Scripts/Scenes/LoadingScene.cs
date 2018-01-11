/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Serialization;
using DeFuncArt.ExtensionMethods;
using System.Collections;
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
		//load game scene - doesn't use cached waits
		this.Invoke(action: () => {
			SceneManager.LoadScene(SceneBuildIndeces.GameScene);
		}, time: DeFuncArt.Utilities.Duration.DA_LOGO_LOADING_SCENE, useCachedWaits: false);
		//initialize game data
		StartCoroutine(Initialize());
	}

	/// <summary>Initializes the game data.</summary>
	private IEnumerator Initialize()
	{
		//wait until GameData singleton is loaded
		while(GameData.instance == null)
		{
			yield return null;
		}
		//if the game hasn't been previously launced, create and set initial data
		if(!PlayerPreferences.HasKey(PlayerPreferencesKeys.previouslyLaunched))
		{
			DataManager.Initialize();
		}
		//verify that data is okay
		DataManager.Verify();
	}

	///DEBUG
	public void ResetButtonPressed()
	{
		DataManager.ReloadData();
		resetButton.SetActive(false);
	}
	///DEBUG
}
