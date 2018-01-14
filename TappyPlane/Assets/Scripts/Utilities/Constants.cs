/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A struct of game constants.</summary>
public struct Constants
{
	/// <summary>The number of levels.</summary>
	public const int NUMBER_OF_LEVELS = 4;
	/// <summary>The number of planes.</summary>
	public const int NUMBER_OF_PLANES = 4;
}

/// <summary>A struct of game store variables.</summary>
public struct GameStore
{
	/// <summary>An array of prices for the planes.</summary>
	private static readonly int[] planePrices = {0, 100, 200, 300};
	/// <summary>An array of prices for the levels.</summary>
	private static readonly int[] levelPrices = {0, 100, 200, 300};

	/// <summary>Returns the price for a given plane.</summary>
	/// <returns>The price for the plane.</returns>
	/// <param name="plane">The plane's index.</param>
	public static int GetPriceForPlane(int plane)
	{
		Assert.IsTrue(plane >= 0 && plane < Constants.NUMBER_OF_PLANES);
		return planePrices[plane];
	}

	/// <summary>Returns the price for a given level.</summary>
	/// <returns>The price for the level.</returns>
	/// <param name="level">The level's index.</param>
	public static int GetPriceForLevel(int level)
	{
		Assert.IsTrue(level >= 0 && level < Constants.NUMBER_OF_LEVELS);
		return levelPrices[level];
	}
}

/// <summary>A struct of scene build indeces.</summary>
public struct SceneBuildIndeces
{
	/// <summary>The loading scene.</summary>
	public const int LoadingScene = 0;
	/// <summary>The game scene.</summary>
	public const int GameScene = 1;
}

/// <summary>A struct of Tags.</summary>
public struct Tags
{
	/// <summary>The Ground Tag (used for both parts of the ground).</summary>
	public const string Ground = "Ground";
	/// <summary>The Obstacle Tag (used for the various types of obstacles).</summary>
	public const string Obstacle = "Obstacle";
}
