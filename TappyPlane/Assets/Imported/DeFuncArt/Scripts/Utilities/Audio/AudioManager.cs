/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>An AudioManager which supports the playing of audio files by filename.</summary>
	public class AudioManager : MonoSingleton<AudioManager>
	{
		/// <summary>A music audio database.</summary>
		[Tooltip("A music audio database.")]
		[SerializeField] private AudioDatabase musicDatabase;
		/// <summary>A sound effects audio database.</summary>
		[Tooltip("A sound effects audio database.")]
		[SerializeField] private AudioDatabase sfxDatabase;
		/// <summary>An AudioSource used to play background music.</summary>
		private AudioSource background;
		/// <summary>An AudioSource used to play sfx.</summary>
		private AudioSource sfx;

		#if UNITY_EDITOR
		/// <summary>EDITOR ONLY: An array of the database keys.</summary>
		public string[] keys
		{
			get
			{
				Assert.IsNotNull(musicDatabase); Assert.IsNotNull(sfxDatabase);
				return (musicDatabase.keys.Union(sfxDatabase.keys)).ToArray();
			}
		}
		#endif

		/// <summary>Callback when the instance is started.</summary>
		private void Start()
		{
			//get references to the background and sfx players
			AudioSource[] audioSources = GetComponents<AudioSource>();
			Assert.IsTrue(audioSources.Length == 2);
			background = audioSources[0]; sfx = audioSources[1];
		}

		/// <summary>Play a SFX file by key on the SFX AudioSource.</summary>
		public void PlayMusic(AudioManagerKeys key)
		{
			PlayMusic(key.ToString());
		}

		/// <summary>Play a music file by key on the Background AudioSource.</summary>
		public void PlayMusic(string key)
		{
			if(SettingsManager.instance.musicEnabled)
			{
				AudioDatabase.AudioFile audioFile = musicDatabase.GetAudioFileForKey(key);
				Assert.IsNotNull(audioFile);

				if(background.clip == audioFile.clip) { return; } //no need to load
				background.volume = audioFile.volume * SettingsManager.instance.musicVolumneMultiplier;
				background.clip = audioFile.clip;
				background.loop = true;
				background.Play();
			}
		}

		/// <summary>Play a SFX file by key on the SFX AudioSource.</summary>
		public void PlaySFX(AudioManagerKeys key)
		{
			PlaySFX(key.ToString());
		}

		/// <summary>Play a SFX file by name on the SFX AudioSource.</summary>
		public void PlaySFX(string filename)
		{
			if(SettingsManager.instance.sfxEnabled)
			{
				AudioDatabase.AudioFile audioFile = sfxDatabase.GetAudioFileForKey(filename);
				Assert.IsNotNull(audioFile);

				sfx.volume = audioFile.volume * SettingsManager.instance.sfxVolumneMultiplier;
				sfx.PlayOneShot(audioFile.clip);
			}
		}

		/// <summary>Toggles the background music.</summary>
		public void ToggleBackgroundMusic(bool shouldEnabled)
		{
			if(!background.isPlaying && shouldEnabled)
			{
				background.Play();
			}
			else if(background.isPlaying && !shouldEnabled)
			{
				background.Stop();
			}
		}

		/// <summary>Stops any audio on SFX and voice AudioSources.</summary>
		public void StopSFX()
		{
			if(sfx.isPlaying) { sfx.Stop(); }
		}
	}
}