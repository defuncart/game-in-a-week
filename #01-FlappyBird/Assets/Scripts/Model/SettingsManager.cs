/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
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

	/// <summary>Whether music is enabled.</summary>
	public bool musicEnabled { get; private set; }
	/// <summary>Whether sfx is enabled.</summary>
	public bool sfxEnabled { get; private set; }
	/// <summary>A volume multiplier for music.</summary>
	public float musicVolumneMultiplier { get; private set; }
	/// <summary>A volume multiplier for sfx.</summary>
	public float sfxVolumneMultiplier { get; private set; }
	/// <summary>The localizated language index.</summary>
	public int localizedLanguage { get; private set; }

	#endregion

	#region Initialization

	/// <summary>Initializes an instance of the class.</summary>
	protected SettingsManager()
	{
		musicEnabled = sfxEnabled = true;
		musicVolumneMultiplier = sfxVolumneMultiplier = 1;
		localizedLanguage = -1;
		Save();
	}

	#endregion

	#region Methods

	/// <summary>DEBUG ONLY. Overriding ToString to return something meaningful.</summary>
	/// <returns>A String representation of the object.</returns>
	override public string ToString()
	{
		return string.Format("musicEnabled={0}, sfxEnabled={1}, musicVolumneMultiplier={2}, sfxVolumneMultiplier={3}, localizedLanguage={4}", 
			musicEnabled, sfxEnabled, musicVolumneMultiplier, sfxVolumneMultiplier, localizedLanguage);
	}

	/// <summary>Setter for musicEnabled. Saves setting and updates AudioManager.</summary>
	public void SetMusicEnabled(bool enabled)
	{
		musicEnabled = enabled; Save();
		AudioManager.instance.ToggleBackgroundMusic(enabled);
	}

	/// <summary>Toggles whether the music is enabled.</summary>
	public void ToggleMusicEnabled()
	{
		SetMusicEnabled(!musicEnabled);
	}

	/// <summary>Setter for sfxEnabled.</summary>
	public void SetSFXEnabled(bool enabled)
	{
		sfxEnabled = enabled; Save();
	}

	/// <summary>Toggle whether sfx is enabled.</summary>
	public void ToggleSFXEnabled()
	{
		SetSFXEnabled(!sfxEnabled);
	}

	/// <summary>Sets the localized language.</summary>
	public void SetLocalizedLanguage(int localizedLanguage)
	{
		this.localizedLanguage = localizedLanguage; Save();
	}

	#endregion

	#region Booleans

	/// <summary>An enum of the SettingManager's various boolean variables.</summary>
	public enum BooleanVariable
	{
		MusicEnabled, SFXEnabled
	}

	/// <summary>Gets the value of a BooleanVariable.</summary>
	/// <param name="variable">The boolean variable.</param>
	public bool GetBooleanVariableValue(BooleanVariable variable)
	{
		switch(variable)
		{
		case BooleanVariable.MusicEnabled:
			return musicEnabled;
		case BooleanVariable.SFXEnabled:
			return sfxEnabled;
		default:
			Debug.LogErrorFormat("{0} is not cated for in the swich statement", variable); return false;
		}
	}

	/// <summary>Set a BooleanVariable to a given value.</summary>
	/// <param name="variable">The boolean variable.</param>
	/// <param name="value">The value to set.</param>
	public void SetBooleanVariable(BooleanVariable variable, bool value)
	{
		switch(variable)
		{
		case BooleanVariable.MusicEnabled:
			SetMusicEnabled(value);
			break;
		case BooleanVariable.SFXEnabled:
			SetSFXEnabled(value);
			break;
		}
	}

	/// <summary>Toggles a BooleanVariable.</summary>
	/// <param name="variable">The boolean variable.</param>
	public void ToggleBooleanVariable(BooleanVariable variable)
	{
		switch(variable)
		{
		case BooleanVariable.MusicEnabled:
			ToggleMusicEnabled();
			break;
		case BooleanVariable.SFXEnabled:
			ToggleSFXEnabled();
			break;
		}
	}

	#endregion
}
