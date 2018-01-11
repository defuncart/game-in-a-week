﻿/*
 * 	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 * 	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Part of the DeFuncArt.Serialization namespace.</summary>
namespace DeFuncArt.Serialization
{
	/// <summary>A simple extention of PlayerPrefs which allows the saving of boolean values. 
	/// Extension methods are only valid on instances, hence the creation of this class.</summary>
	public static class PlayerPreferences
	{
		/// <summary>Determines whether a preference with the specified key exists.</summary>
		public static bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		/// <summary>Sets a new (or overwrites a previous) integer preference key-value pair.</summary>
		public static void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value); PlayerPrefs.Save();
		}

		/// <summary>Gets the value of a integer preference for a given key.</summary>
		public static int GetInt(string key)
		{
			if(!HasKey(key)) { Debug.LogError(string.Format("Key \"{0}\" not found!", key)); }
			return PlayerPrefs.GetInt(key);
		}

		/// <summary>Sets a new (or overwrites a previous) boolean preference key-value pair.</summary>
		public static void SetBool(string key, bool value)
		{
			SetInt(key, value ? 1 : 0);
		}

		/// <summary>Gets the value of a boolean preference for a given key.</summary>
		public static bool GetBool(string key)
		{
			if(!HasKey(key)) { Debug.LogError(string.Format("Key \"{0}\" not found!", key)); }
			return GetInt(key) == 1;
		}

		/// <summary>Sets a new (or overwrites a previous) floating point preference key-value pair.</summary>
		public static void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value); PlayerPrefs.Save();
		}

		/// <summary>Gets the value of a floating point preference for a given key.</summary>
		public static float GetFloat(string key)
		{
			if(!HasKey(key)) { Debug.LogError(string.Format("Key \"{0}\" not found!", key)); }
			return PlayerPrefs.GetFloat(key);
		}

		/// <summary>Sets a new (or overwrites a previous) string preference key-value pair.</summary>
		public static void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value); PlayerPrefs.Save();
		}

		/// <summary>Gets the value of a string preference for a given key.</summary>
		public static string GetString(string key)
		{
			if(!HasKey(key)) { Debug.LogError(string.Format("Key \"{0}\" not found!", key)); }
			return PlayerPrefs.GetString(key);
		}

		/// <summary>Removes all preferences.</summary>
		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		/// <summary>Removes a preference by key.</summary>
		public static void DeleteKey(string key)
		{
			if(!HasKey(key)) { Debug.LogError(string.Format("Key \"{0}\" not found!", key)); }
			PlayerPrefs.DeleteKey(key);
		}
	}
}
