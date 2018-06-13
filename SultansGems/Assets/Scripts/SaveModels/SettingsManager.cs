/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A serializable singleton manager which is responsible for game settings.</summary>
[System.Serializable]
public class SettingsManager : SerializableSingleton<SettingsManager>
{
	#region Properties

    /// <summary>A backing variable for localizedLanguage.</summary>
    private int _localizedLanguage = -1;
    /// <summary>The localizated language index.</summary>
    public int localizedLanguage
    {
        get { return _localizedLanguage; }
        set
        {
            if (value >= 0 && value <= LocalizationManager.instance.NUMBER_LOCALIZED_LANGUAGES)
            {
                _localizedLanguage = value;
                LocalizationManager.instance.Restart(); LocalizationManager.RefreshCurrentSceneLocalizedText(); Save();
            }
        }
    }

    /// <summary>A backing variable for localizedLanguage.</summary>
    private float _musicVolumeMultiplier = Constants.INITIAL_MUSIC_VOLUME_MULTIPLIER;
    /// <summary>The music volume multiplier (between 0 and 1).</summary>
    public float musicVolumeMultiplier
    {
        get { return _musicVolumeMultiplier; }
        set { Assert.IsTrue(value >= 0 && value <= 1); if (value >= 0 && value <= 1) { _musicVolumeMultiplier = value; Save(); } }
    }
    /// <summary>Whether music is enabled.</summary>
    public bool musicEnabled
    {
        get { return _musicVolumeMultiplier > 0; }
        set { _musicVolumeMultiplier = value == true ? Constants.INITIAL_MUSIC_VOLUME_MULTIPLIER : 0; Save(); if(!value){ AudioManager.instance.StopMusic(); } }
    }

    /// <summary>A backing variable for localizedLanguage.</summary>
    private float _sfxVolumeMultiplier = Constants.INITIAL_SFX_VOLUME_MULTIPLIER;
    /// <summary>The sfx volume multiplier (between 0 and 1).</summary>
    public float sfxVolumeMultiplier
    {
        get { return _sfxVolumeMultiplier; }
        set { Assert.IsTrue(value >= 0 && value <= 1); if (value >= 0 && value <= 1) { _sfxVolumeMultiplier = value; Save(); } }
    }
    /// <summary>Whether sfx is enabled.</summary>
    public bool sfxEnabled
    {
        get { return _sfxVolumeMultiplier > 0; }
        set { _sfxVolumeMultiplier = value == true ? Constants.INITIAL_MUSIC_VOLUME_MULTIPLIER : 0; Save(); if(!value){ AudioManager.instance.StopSFX(); } }
    }

    /// <summary>A backing variable for level.</summary>
    private int _level = 0;
    /// <summary>The current level.</summary>
    public int level
    {
        get { return _level; }
        set { Assert.IsTrue(value >= 0 && value < LevelsManager.instance.NUMBER_LEVELS); if (value >= 0 && value < LevelsManager.instance.NUMBER_LEVELS) { _level = value; Save(); } }
    }

    #endregion

	#region Initialization

	/// <summary>Initializes an instance of the class.</summary>
	protected SettingsManager()
	{
		Save();
	}

	#endregion

	#region Methods

	/// <summary>DEBUG ONLY. Overriding ToString to return something meaningful.</summary>
	/// <returns>A String representation of the object.</returns>
	override public string ToString()
	{
        return string.Format("localizedLanguage: {0}, musicVolumeMultiplier: {1}, sfxVolumeMultiplier: {2}, level: {3}", localizedLanguage, musicVolumeMultiplier, sfxVolumeMultiplier, level);
	}

	#endregion
}
