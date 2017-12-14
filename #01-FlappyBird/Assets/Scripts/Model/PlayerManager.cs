/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
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

	/// <summary>The number of coins.</summary>
	public int coins { get; private set; }
	/// <summary>An array denoting which levels are unlocked.</summary>
	private bool[] levelsUnlocked;
	/// <summary>An array denoting which planes are unlocked.</summary>
	private bool[] planesUnlocked;
	/// <summary>The current level.</summary>
	public int currentLevel = 0;
	/// <summary>The current plane.</summary>
	public int currentPlane = 0;

	#endregion

	#region Initialization

	/// <summary>Initializes an instance of the class.</summary>
	protected PlayerManager()
	{
		coins = 0;
		levelsUnlocked = new bool[Constants.NUMBER_OF_LEVELS].Repeat(false); levelsUnlocked[0] = true;
		planesUnlocked = new bool[Constants.NUMBER_OF_PLANES].Repeat(false); planesUnlocked[0] = true;
		Save();
	}

	#endregion

	#region Methods

	/// <summary>Determines whether a given level is unlocked.</summary>
	public bool LevelIsUnlocked(int level)
	{
		Assert.IsTrue(level >= 0 && level < Constants.NUMBER_OF_LEVELS);
		return levelsUnlocked[level];
	}

	/// <summary>Sets a given level to be unlocked.</summary>
	public void SetLevelUnlocked(int level)
	{
		Assert.IsTrue(level >= 0 && level < Constants.NUMBER_OF_LEVELS);
		levelsUnlocked[level] = true; Save();
	}

	/// <summary>Determines whether a given plane is unlocked.</summary>
	public bool PlaneIsUnlocked(int plane)
	{
		Assert.IsTrue(plane >= 0 && plane < Constants.NUMBER_OF_PLANES);
		return planesUnlocked[plane];
	}

	/// <summary>Sets a given plane to be unlocked.</summary>
	public void SetPlaneUnlocked(int plane)
	{
		Assert.IsTrue(plane >= 0 && plane < Constants.NUMBER_OF_LEVELS);
		planesUnlocked[plane] = true; Save();
	}

	/// <summary>Increments the player's coins by a given amount.</summary>
	public void IncrementCoins(int amount)
	{
		coins += amount; Save();
	}

	/// <summary>Decrements the player's coins by a given amount.</summary>
	public void DecrementCoins(int amount)
	{
		coins -= amount; Save();
	}

	#endregion
}
