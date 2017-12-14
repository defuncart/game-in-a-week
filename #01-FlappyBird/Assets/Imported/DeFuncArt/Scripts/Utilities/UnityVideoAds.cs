/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_ADS
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Advertisements;
#endif
using UnityEngine.Assertions;

/// <summary>A manager for the Unity Ads.</summary>
public static class UnityAdsManager
{
	/// <summary>Determines whether ads can be shown.</summary>
	public static bool canShowAds
	{
		get
		{
			#if UNITY_ADS
			return Advertisement.isSupported && Device.hasInternetConnection;
			#else
			return false;
			#endif
		}
	}

	#if UNITY_ADS
	/// <summary>A callback when the player should be rewarded.</summary>
	private static System.Action rewardCallback;
	#endif

	/// <summary>Shows the player a video ad by which they'll receive a reward afterwards.</summary>
	public static void ShowRewardVideoWithCallback(System.Action callback)
	{
		Assert.IsTrue(canShowAds);

		#if UNITY_ADS
		rewardCallback = callback;
		Advertisement.Show("rewardedVideo", new ShowOptions{ resultCallback = HandleShowResult });
		#endif
	}

	#if UNITY_ADS
	/// <summary>The result callback after showing an ad.</summary>
	private static void HandleShowResult(Advertisement.ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			rewardCallback();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	#endif
}
