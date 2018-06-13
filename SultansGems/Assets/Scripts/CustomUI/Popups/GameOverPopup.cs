/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.UI;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>A game over popup.</summary>
public class GameOverPopup : DAPopupStatic
{
    /// <summary>An event which occurs when the player wants to continue.</summary>
    public event EventHandler OnContinue;
    /// <summary>An event which occurs when the player wants to restart.</summary>
    public event EventHandler OnRestart;
    /// <summary>An event which occurs when the player wants to quit.</summary>
    public event EventHandler OnQuit;

    /// <summary>The game won panel.</summary>
    [Tooltip("The game won panel.")]
    [SerializeField] private GameObject gameWonPanel = null;
    /// <summary>The game lost panel.</summary>
    [Tooltip("The game lost panel.")]
    [SerializeField] private GameObject gameLostPanel = null;
    /// <summary>Game Won Panel: The three stars.</summary>
    [Tooltip("Game Won Panel: The three stars.")]
    [SerializeField] private Image[] stars = null;
    /// <summary>The star sprites asset.</summary>
    [Tooltip("The star sprites asset.")]
    [SerializeField]
    private UIStarSpritesAsset starSprites = null;

    #if UNITY_EDITOR
    /// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
    private void OnValidate()
    {
        Assert.IsNotNull(gameWonPanel);
        Assert.IsNotNull(gameLostPanel);
        Assert.IsTrue(stars.Length == 3);
        Assert.IsNotNull(starSprites);
    }
    #endif

    /// <summary>Setups the Popup.</summary>
    /// <param name="gameWon">Whether the game was won.</param>
    /// <param name="starsAttained">The number of stars attained (0, 1 or 2).</param>
    public void Setup(bool gameWon, int starsAttained=0)
    {
        gameWonPanel.SetActive(gameWon); gameLostPanel.SetActive(!gameWon);
        if(gameWon)
        {
            Assert.IsTrue(starsAttained >= 0 && starsAttained <= 2);

            for(int i = 0; i < stars.Length; i++)
            {
                stars[i].sprite = i <= starsAttained ? starSprites.filled : starSprites.unfilled;
            }
        }
    }

    /// <summary>Callback when the continue button is pressed.</summary>
    public void ContinueButtonPressed()
    {
        Assert.IsNotNull(OnContinue);
        OnContinue();
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
