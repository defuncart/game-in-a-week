/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArtEditor;
using System.IO;
using System.Linq;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
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

            #if UNITY_EDITOR
            /// <summary>Constructs a new AudioFile instance.</summary>
            /// <param name="clip">The AudioFile's clip.</param>
            /// <param name="volume">The AudioFile's volume.</param>
            public AudioFile(AudioClip clip, float volume = 1f)
            {
                this.clip = clip;
                this.volume = volume;
                key = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(clip));
            }
            #endif
        }
		/// <summary>An array of audiofiles.</summary>
		[Tooltip("An array of audiofiles.")]
		[SerializeField] protected AudioFile[] audioFiles;

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
		public void EDITOR_UpdateKeys()
		{
			foreach(AudioFile audioFile in audioFiles)
			{
				audioFile.key = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(audioFile.clip));
				if(audioFile.volume > 1) { audioFile.volume = 1; } else if(audioFile.volume < 0) { audioFile.volume = 0; }
			}
		}

		/// <summary>EDITOR ONLY: Returns an array of the the database's keys.</summary>
		public string[] keys
		{
			get
			{
				Assert.IsNotNull(audioFiles); Assert.IsTrue(audioFiles.Length > 0);

				return audioFiles.Select(x => x.key).ToArray();
			}
		}

        public bool EDITOR_HasFolder(string folfer)
        {
            return true;
        }

        /// <summary>
        /// Imports from folder.
        /// </summary>
        /// <param name="folder">Folder.</param>
        public void EDITOR_ImportFromFolder(string folder)
        {
            AudioClip[] clips = FolderManager.GetAssetsAtPath<AudioClip>("Audio/" + folder);
            audioFiles = new AudioFile[clips.Length];
            for(int i = 0; i < audioFiles.Length; i++)
            {
                audioFiles[i] = new AudioFile(clips[i]);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

		#endif
	}
}
