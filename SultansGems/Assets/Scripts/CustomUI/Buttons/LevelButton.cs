/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>A button used to select a level.</summary>
public class LevelButton : DAButton
{
    /// <summary>The button's text.</summary>
    [Tooltip("The button's text.")]
    [SerializeField] private Text buttonText = null;
    /// <summary>The button's locked image.</summary>
    [Tooltip("The button's locked image.")]
    [SerializeField] private GameObject lockedImage = null;
    /// <summary>The button's stars.</summary>
    [Tooltip("The button's stars.")]
    [SerializeField] private Image[] stars = null;
    /// <summary>The star sprites asset.</summary>
    [Tooltip("The star sprites asset.")]
    [SerializeField] private UIStarSpritesAsset starSprites = null;

    /// <summary>Initializes the button.</summary>
    /// <param name="text">The level's text (i.e. 1, 2 etc.).</param>
    /// <param name="isUnlocked">Whether the level is unlocked.</param>
    /// <param name="starsObtained">The number of stars obtained for the level (0, 1 or 2).</param>
    public void Initialize(string text, bool isUnlocked, int starsObtained)
    {
        Assert.IsFalse(text.IsNullOrEmpty());

        buttonText.text = text;
        lockedImage.SetActive(!isUnlocked);
        interactable = isUnlocked;
        if(isUnlocked && starsObtained > -1)
        {
            Assert.IsTrue(starsObtained < 3);

            for(int i=0; i < stars.Length; i++)
            {
                if(i <= starsObtained) { stars[i].sprite = starSprites.filled; }
            }
        }
    }
}
