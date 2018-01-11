/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A struct of game constants.</summary>
public struct Constants
{
	/// <summary>A string array of the various bulk upgrade options.</summary>
	public static readonly string[] BULK_UPGRADE_OPTIONS = { "x 1", "x 5", "x 10", "MAX" };
	/// <summary>The number of bulk updrade options.</summary>
//	public static int NUMBER_BULK_UPGRADE_OPTIONS
//	{
//		get { return BULK_UPGRADE_OPTIONS.Length; }
//	}
	public const int NUMBER_BULK_UPGRADE_OPTIONS = 4;
	/// <summary>An array of the integer bulk upgrade options. This array is purposely of length NUMBER_BULK_UPGRADE_OPTIONS-1 as max has no integer equivelent. Could have 999 I suppose.</summary>
	public static readonly int[] BULK_UPGRADE_LEVELS = { 1, 5, 10 };
	/// <summary>Business panel: A minimum tolerance under which it makes no sense to update the profit fill.</summary>
	public const float MINIMUM_TIME_TO_UPDATE_PRODUCTION_FILL = 0.1f;
	/// <summary>The maximum time that the game will consider for background profits.</summary>
	public const float MAX_BACKGROUND_TIME = 60 * 60 * 2; //2 hrs
	/// <summary>The percentage of profits the player earns when the app is in background. Vastly lower than when played in the foreground.</summary>
	public const float BACKGROUND_PROFIT_RETAIN_MULTIPLIER = 0.05f;
	/// <summary>The multiplier receive on each prestige.</summary>
	public const float PRESTIGE_BONUS_MULTIPLIER = 2;
}

/// <summary>A struct of animation duration variables.</summary>
public struct AnimationDuration {}

/// <summary>A struct of game store variables.</summary>
public struct GameStore {}

/// <summary>A struct of scene build indeces.</summary>
public struct SceneBuildIndeces
{
	/// <summary>The loading scene.</summary>
	public const int LoadingScene = 0;
	/// <summary>The game scene.</summary>
	public const int GameScene = 1;
}

/// <summary>A struct of Tags.</summary>
public struct Tags {}

/// <summary>A struct of Game colors.</summary>
public struct GameColors
{
	/// <summary>A purple color used on price tags and buy buttons.</summary>
	public static readonly Color purple = HexToColor("#8E44AD");
	/// <summary>A gray color used when the player has insufficient cash for a given object.</summary>
	public static readonly Color gray = HexToColor("#BDC3C7");
	/// <summary>A black color used for text (Profit or Cash Per Sec).</summary>
	public static readonly Color black = HexToColor("#323232");
	/// <summary>A green color used for text (Profit or Cash Per Sec).</summary>
	public static readonly Color green = HexToColor("#16A085");

	/// <summary>Converts a Hex string into a Color.</summary>
	/// <returns>The converted color.</returns>
	/// <param name="hex">The color as a Hex string.</param>
	private static Color HexToColor(string hex)
	{
		Color color = Color.white;
		ColorUtility.TryParseHtmlString(hex, out color);

		return color;
	}
}
