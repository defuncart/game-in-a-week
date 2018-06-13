/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A struct of game constants.</summary>
public struct Constants
{
    /// <summary>The number of stone types.</summary>
	public const int NUMBER_STONE_TYPES = 6;
    /// <summary>The minimum number of stones for a match.</summary>
	public const int MINIMUM_STONES_FOR_MATCH = 3;

    /// <summary>The initial and default volume multiplier for music.</summary>
    public const float INITIAL_MUSIC_VOLUME_MULTIPLIER = 0.8f;
    /// <summary>The initial and default volume multiplier for sfx.</summary>
    public const float INITIAL_SFX_VOLUME_MULTIPLIER = 0.8f;
}

/// <summary>A struct of constants for the inspector. Ideally I would like these to be in a UNITY_EDITOR #IF but then the game wouldn't be able to compile.</summary>
public struct InspectorConstants
{
    /// <summary>[Level SO, maximumNumberMoves Property] minimum value for the range attribute.</summary>
    public const int LEVEL_MOVES_MINIMUM_NUMBER_OF = 1;
    /// <summary>[Level SO, maximumNumberMoves Property] maximum value for the range attribute.</summary>
    public const int LEVEL_MOVES_MAXIMUM_NUMBER_OF = 99;
    /// <summary>[Level SO, scoreToAchieveXStar Property] minimum value for the range attribute.</summary>
    public const int LEVEL_SCORE_MINIMUM_TO_OBTAIN_STARS = 5;
    /// <summary>[Level SO, scoreToAchieveXStar Property] maximum value for the range attribute.</summary>
    public const int LEVEL_SCORE_MAXIMUM_TO_OBTAIN_STARS = 999;
}

/// <summary>A struct of animation duration variables.</summary>
public struct AnimationDuration
{
	public const float SWAP = 0.25f;
	public const float COLUMN_COLLAPSE = 0.1f;
}

/// <summary>A struct of scene build indeces.</summary>
public struct SceneBuildIndeces
{
	/// <summary>The loading scene.</summary>
	public const int LoadingScene = 0;
	/// <summary>The menu scene.</summary>
	public const int MenuScene = 1;
	/// <summary>The game scene.</summary>
	public const int GameScene = 2;
}
 