/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.IO;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>An asset used as a database of AudioFiles.</summary>
[CreateAssetMenu(fileName = "AudioDatabase", menuName = "AudioDatabase", order = 1000)]
public class AudioDatabase : ScriptableObject
{
	/// <summary>A model representing an AudioFile object.</summary>
	[System.Serializable]
	public class AudioFile
	{
		/// <summary>The audiofile's clip.</summary>
		public AudioClip clip;
		/// <summary>The audiofile's volume.</summary>
		public float volume;
		/// <summary>The audiofile's key.</summary>
		public string key;
	}
	/// <summary>An array of audiofiles.</summary>
	[Tooltip("An array of audiofiles.")]
	[SerializeField] private AudioFile[] audioFiles;

	/// <summary>Returns the audio file for a given key.</summary>
	/// <returns>The audio file for a given key.</returns>
	/// <param name="key">The key.</param>
	public AudioFile GetAudioFileForKey(string key)
	{
		foreach(AudioFile audioFile in audioFiles)
		{
			if(audioFile.key == key) { return audioFile; }
		}
		Debug.LogError(string.Format("{0} is not valid for {1}", key, name)); return null;
	}

	#if UNITY_EDITOR
	/// <summary>EDITOR ONLY: Updates the database's keys from clip filenames.</summary>
	public void UpdateKeys()
	{
		foreach(AudioFile audioFile in audioFiles)
		{
			audioFile.key = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(audioFile.clip));
			if(audioFile.volume > 1) { audioFile.volume = 1; } else if(audioFile.volume < 0) { audioFile.volume = 0; }
		}
	}

	/// <summary>EDITOR ONLY: Returnd an array of the the database's keys.</summary>
	public List<string> keys
	{
		get
		{
			Assert.IsNotNull(audioFiles); Assert.IsTrue(audioFiles.Length > 0);

			List<string> returnList = new List<string>();
			foreach(AudioFile audioFile in audioFiles)
			{
				returnList.Add(audioFile.key);
			}
			return returnList;
		}
	}
	#endif
}
