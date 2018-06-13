/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A serializable singleton manager which is responsible for saving the player's data to disk.</summary>
[System.Serializable]
public class PlayerManager : SerializableSingleton<PlayerManager>
{
    #region Properties

    /// <summary>An array denoting which levels are unlocked.</summary>
    private bool[] levelUnlocked;
    /// <summary>An array denoting each levels scores.</summary>
    private int[] levelScores;
    /// <summary>An array denoting which rating was obtained for each levels.</summary>
    private int[] starsObtained;

	#endregion

	#region Initialization

	/// <summary>Initializes an instance of the class.</summary>
	protected PlayerManager()
	{
        levelUnlocked = new bool[LevelsManager.instance.NUMBER_LEVELS];
        levelUnlocked[0] = true;
        levelScores = new int[LevelsManager.instance.NUMBER_LEVELS];
        starsObtained = new int[LevelsManager.instance.NUMBER_LEVELS].Repeat(-1);
		Save();
	}

	#endregion

	#region Methods

    /// <summary>Determine if a given level is unlocked.</summary>
    public bool LevelisUnlocked(int levelIndex)
    {
        Assert.IsTrue(levelIndex >= 0 && levelIndex < LevelsManager.instance.NUMBER_LEVELS);
        return levelUnlocked[levelIndex];
    }

    /// <summary>Determine the score for a given level.</summary>
    public int ScoreForLevel(int levelIndex)
    {
        Assert.IsTrue(levelIndex >=0 && levelIndex < LevelsManager.instance.NUMBER_LEVELS);
        return levelScores[levelIndex];
    }

    /// <summary>Determine the rating for a given level.</summary>
    public int StarsObtainedForLevel(int levelIndex)
    {
        Assert.IsTrue(levelIndex >= 0 && levelIndex < LevelsManager.instance.NUMBER_LEVELS);
        return starsObtained[levelIndex];
    }

    /// <summary>Sets the score and stars obtained for the current level.</summary>
    public void SetScoreStarsObtainedForCurrentLevel(int score, int starsObtained)
    {
        if(score > levelScores[SettingsManager.instance.level]) { levelScores[SettingsManager.instance.level] = score; }
        if(starsObtained > this.starsObtained[SettingsManager.instance.level]) { this.starsObtained[SettingsManager.instance.level] = starsObtained; }
        if(SettingsManager.instance.level < LevelsManager.instance.NUMBER_LEVELS - 1)
        {
            SettingsManager.instance.level++;
            levelUnlocked[SettingsManager.instance.level] = true;
        }
        Save();
    }

	#endregion
}
