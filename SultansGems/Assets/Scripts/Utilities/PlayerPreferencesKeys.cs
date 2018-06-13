/*
 * 	Written by James Leahy. (c) 2018 DeFunc Art.
 * 	https://github.com/defuncart/
 */
using DeFuncArt.Serialization;

/// <summary>Here the project's PlayerPreferences' keys are declared and initialized.</summary>
public static class PlayerPreferencesKeys//: PlayerPreferences.Keys
{
	/// <summary>Whether the game was previously launched.</summary>
	public const string previouslyLaunched = "previouslyLaunched";

	/// <summary>Initializes required key-value pairs to their initial values.</summary>
	public static void Initialize()
	{
		PlayerPreferences.SetBool(previouslyLaunched, true);
	}
}
