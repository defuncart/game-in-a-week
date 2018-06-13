/*
 *	Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
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
		[SerializeField] private AudioDatabase musicDatabase = null;
        /// <summary>A sound effects audio database.</summary>
        [Tooltip("A sound effects audio database.")]
        [SerializeField] private AudioDatabase sfxDatabase = null;
        /// <summary>An AudioSource used to play music.</summary>
        private AudioSource music;
        /// <summary>An AudioSource used to play sfx.</summary>
		private AudioSource sfx;

		/// <summary>Callback when the instance is started.</summary>
		private void Start()
		{
			//get references to the sfx and voice players
			AudioSource[] audioSources = GetComponents<AudioSource>();
			Assert.IsTrue(audioSources.Length == 2);
            music = audioSources[0]; sfx = audioSources[1];
		}

        /// <summary>Play a music file by key on the music AudioSource.</summary>
        public void PlayMusic(MusicDatabaseKeys key)
        {
            PlayMusic(key.ToString());
        }

        /// <summary>Play a music file by name on the music AudioSource.</summary>
        public void PlayMusic(string filename)
        {
            if(SettingsManager.instance.musicEnabled)
            {
                AudioDatabase.AudioFile audioFile = musicDatabase.GetAudioFileForKey(filename);
                Assert.IsNotNull(audioFile);

                //if the music audio source is currently playing the same audioclip, ignore
                if(music.isPlaying && music.clip == audioFile.clip) { return; }

                //otherwise set the volume, load and loop the clip
                music.volume = audioFile.volume * SettingsManager.instance.musicVolumeMultiplier;
                music.loop = true;
                music.clip = audioFile.clip;
                music.Play();
            }
        }

        /// <summary>Stops any audio on music AudioSources.</summary>
        public void StopMusic()
        {
            if(music.isPlaying) { music.Stop(); }
        }

		/// <summary>Play a SFX file by key on the SFX AudioSource.</summary>
		public void PlaySFX(SFXDatabaseKeys key)
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

				sfx.volume = audioFile.volume * SettingsManager.instance.sfxVolumeMultiplier;
				sfx.PlayOneShot(audioFile.clip);
			}
		}

		/// <summary>Stops any audio on SFX and voice AudioSources.</summary>
		public void StopSFX()
		{
			if(sfx.isPlaying) { sfx.Stop(); }
		}
	}
}
