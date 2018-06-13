/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Serialization;
using UnityEngine;

/// <summary>DataManager is responsible for maintaining the project's data files.</summary>
public static class DataManager
{
	/// <summary>Initializes the project's data files.</summary>
	public static void Initialize()
	{
		PlayerManager.Create();
		SettingsManager.Create();
		PlayerPreferencesKeys.Initialize();
	}

	/// <summary>Deletes the project's data files.</summary>
	public static void Delete()
	{
		PlayerPreferences.DeleteAll();
		BinarySerializer.DeleteFile(typeof(PlayerManager).Name);
		BinarySerializer.DeleteFile(typeof(SettingsManager).Name);
	}

	/// <summary>Verifies the project's data files, re-creating them if necessary.</summary>
	public static void Verify()
	{
		if(!BinarySerializer.FileExists(typeof(PlayerManager).Name))
		{ 
			Debug.LogWarningFormat("{0} does not exist, re-creating.", typeof(PlayerManager).Name);
			PlayerManager.Create();
		}
		if(!BinarySerializer.FileExists(typeof(SettingsManager).Name))
		{ 
			Debug.LogWarningFormat("{0} does not exist, re-creating.", typeof(SettingsManager).Name);
			SettingsManager.Create();
		}
	}

	/// <summary>Reloads the project's data files.</summary>
	public static void ReloadData()
	{
		Delete();
		Initialize();
	}
}
