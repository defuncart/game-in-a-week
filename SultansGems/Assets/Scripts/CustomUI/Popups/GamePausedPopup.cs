/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.UI;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A game paused popup.</summary>
public class GamePausedPopup : DAPopupStatic
{
    /// <summary>An event which occurs when the player wants to restart.</summary>
    public event EventHandler OnRestart;
    /// <summary>An event which occurs when the player wants to quit.</summary>
    public event EventHandler OnQuit;

    /// <summary>The sfx toggle button.</summary>
    [Tooltip("The sfx toggle button.")]
    [SerializeField] private DAToggleButton sfxToggleButton = null;
    /// <summary>The music toggle button.</summary>
    [Tooltip("The music toggle button.")]
    [SerializeField] private DAToggleButton musicToggleButton = null;

    ///<summary>Callback when the popup is initialized.</summary>
    protected override void InitializePopup()
    {
        //delegates
        sfxToggleButton.OnToggled += (bool value) => {
            SettingsManager.instance.sfxEnabled = value;
        };
        musicToggleButton.OnToggled += (bool value) => {
            SettingsManager.instance.musicEnabled = value;
            if(value){ AudioManager.instance.PlayMusic(MusicDatabaseKeys.main); }
        };

        sfxToggleButton.InitializeToggled(SettingsManager.instance.sfxEnabled);
        musicToggleButton.InitializeToggled(SettingsManager.instance.musicEnabled);
    }

    /// <summary>Callback when the restart button is pressed.</summary>
    public void RestartButtonPressed()
    {
        Assert.IsNotNull(OnRestart);
        Hide();
        OnRestart();
    }

    /// <summary>Callback when the quit button is pressed.</summary>
    public void QuitButtonPressed()
    {
        Assert.IsNotNull(OnQuit);
        OnQuit();
    }
}
