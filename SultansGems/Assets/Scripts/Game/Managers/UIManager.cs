/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>A ui manager which controls onscreen user interface elements.</summary>
public class UIManager : DACanvas
{
    /// <summary>A text component denoting the number of remaining moves.</summary>
    [Tooltip("A text component denoting the number of remaining moves.")]
    [SerializeField] private Text movesText = null;
    /// <summary>A text component denoting the player's score.</summary>
    [Tooltip("A text component stating the player's score.")]
    [SerializeField] private Text scoreText = null;
    /// <summary>An array of stars denoting the what rating the player has already received.</summary>
    [Tooltip("An array of stars denoting the what rating the player has already received.")]
    [SerializeField] private Image[] stars = null;
    /// <summary>The star sprites asset.</summary>
    [Tooltip("The star sprites asset.")]
    [SerializeField] private UIStarSpritesAsset starSprites = null;

    #if UNITY_EDITOR
    /// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
    private void OnValidate()
    {
        Assert.IsNotNull(movesText);
        Assert.IsNotNull(scoreText);
        Assert.AreEqual(stars.Length, 3);
        Assert.IsNotNull(starSprites);
    }
    #endif

    /// <summary>Sets the score.</summary>
    /// <param name="score">The score.</param>
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
        UpdateStarsForScore(score);
    }

    /// <summary>Updates the onscreen stars for a given score.</summary>
    /// <param name="score">The score.</param>
    private void UpdateStarsForScore(int score)
    {
        if(score > LevelsManager.instance.currentLevel.scoreToAchieve3Star) //three stars
        {
            stars[0].sprite = stars[1].sprite = stars[2].sprite = starSprites.filled;
        }
        else if(score > LevelsManager.instance.currentLevel.scoreToAchieve2Star) //2 stars (index 0, 1)
        {
            stars[0].sprite = stars[1].sprite = starSprites.filled; stars[2].sprite = starSprites.unfilled;
        }
        else if(score > LevelsManager.instance.currentLevel.scoreToAchieve1Star) //1 star (index 0)
        {
            stars[0].sprite = starSprites.filled; stars[1].sprite = stars[2].sprite = starSprites.unfilled;
        }
        else //0 stars
        {
            stars[0].sprite = stars[1].sprite = stars[2].sprite = starSprites.unfilled;
        }
    }

    /// <summary>Sets the moves left.</summary>
    /// <param name="moves">The number of moves left.</param>
    public void SetMoves(int moves)
    {
        movesText.text = moves.ToString();
    }
}
