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

	/// <summary>The player's current cash.</summary>
	public float cash { get; private set; }
	/// <summary>The player's lifetimeCash at the last reset (prestige).</summary>
	private float lifetimeCashLastReset;
	/// <summary>The cash the player has made over the game lifetime.</summary>
	public float lifetimeCash { get { return lifetimeCashLastReset + cash; } }
	/// <summary>An array of businesses.</summary>
	private Business[] businesses;
	/// <summary>An array denoting which managers have been bought.</summary>
	private bool[] managerIsBought;
	/// <summary>An array denoting which upgrades have been bought.</summary>
	private bool[] upgradeIsBought;
	/// <summary>The date that the player last closed the app and stopped playing the game.</summary>
	private System.DateTime dateLastPlayed;

	#endregion

	#region Initialization

	/// <summary>Initializes an instance of the class.</summary>
	protected PlayerManager()
	{
		lifetimeCashLastReset = 0;
		businesses = new Business[GameData.instance.numberOfBusinesses];
		for(int i=0; i < businesses.Length; i++)
		{
			businesses[i] = new Business( GameData.instance.GetDataForBusiness(i).type );
//			businesses[i] = new Business( (Business.Type)i );
		}
		Reset();
	}

	/// <summary>Resets players variables. Used on first load and for each prestige.</summary>
	private void Reset()
	{
		cash = 0;
		managerIsBought = new bool[GameData.instance.numberOfManagers]; //defaults to false
		upgradeIsBought = new bool[GameData.instance.numberOfUpgrades];
		dateLastPlayed = System.DateTime.MinValue;

		Save();
	}

	#endregion

	#region Methods

	/// <summary>Gets a business.</summary>
	/// <returns>The business.</returns>
	/// <param name="businessIndex">The business' index.</param>
	public Business GetBusiness(int businessIndex)
	{
		Assert.IsTrue(businessIndex >= 0 && businessIndex < businesses.Length);
		return businesses[businessIndex];
	}

	/// <summary>The player's total cash per second.</summary>
	public float totalCashPerSecond
	{
		get
		{
			float totalCashPerSecond = 0;
			for(int i=0; i < businesses.Length; i++)
			{
				if(businesses[i].isUnlocked && businesses[i].hasManager)
				{
					totalCashPerSecond += businesses[i].cashPerSecond;
				}
			}
			return totalCashPerSecond;
		}
	}

	/// <summary>Determines the player's earnings since they last played the game (via managers running the businesses). This is vastly less than if they would have played online (i.e. the app open).</summary>
	/// <returns>The earnings since last play.</returns>
	public float DetermineEarningsSinceLastPlay()
	{
		Assert.AreNotEqual(dateLastPlayed, System.DateTime.MinValue);

		//determine the time since the player last played
		float timeSince = (float)(Device.GetCurrentTime (verifyWithServer: false) - dateLastPlayed).TotalSeconds;
		if (timeSince > Constants.MAX_BACKGROUND_TIME) { timeSince = Constants.MAX_BACKGROUND_TIME; }
		//calculate the earnings
		float earnings = totalCashPerSecond * timeSince * Constants.BACKGROUND_PROFIT_RETAIN_MULTIPLIER;
		cash += earnings;
		//return the earnings (for a popup etc.).
		return earnings;
	}

	/// <summary>Whether the player should consider prestiging.</summary>
	public bool shouldConsiderPrestige
	{
		get
		{
			for(int i=0; i < businesses.Length; i++)
			{
				if(!businesses[i].isUnlocked || businesses[i].level < 5 || !businesses[i].costsAreHigherThanProfit) { return false; }
				else { Debug.Log("business " + businesses[i].name + " is unprofitable"); }
			}
			return true;
		}
	}			

	/// <summary>Resets the player's data and awards them a prestige multiplier.</summary>
	public void Prestige()
	{
		for(int i=0; i < businesses.Length; i++)
		{
			businesses[i].UpdatePrestigeProfitMultiplier(Constants.PRESTIGE_BONUS_MULTIPLIER); //x2
			businesses[i].Reset();
		}
		Reset();
		dateLastPlayed = System.DateTime.UtcNow;
	}

	/// <summary>Updates that the player has bought a specified manager.</summary>
	/// <param name="managerIndex">The bought manager (as an index).</param>
	public void BoughtManager(int managerIndex)
	{
		Assert.IsTrue(managerIndex >= 0 && managerIndex < managerIsBought.Length);
		managerIsBought[managerIndex] = true; //Save();
	}

	/// <summary>Determines whether the player has previously bought a specified manager.</summary>
	/// <returns><c>true</c> if the player has previously bought the specified manager; otherwise, <c>false</c>.</returns>
	/// <param name="managerIndex">The manager (as an index).</param>
	public bool HasBoughtManager(int managerIndex)
	{
		Assert.IsTrue(managerIndex >= 0 && managerIndex < managerIsBought.Length);
		return managerIsBought[managerIndex];
	}

	/// <summary>Updates that the player has bought a given upgrade.</summary>
	/// <param name="upgradeIndex">The bought upgrade (as an index).</param>
	public void BoughtUpgrade(int upgradeIndex)
	{
		Assert.IsTrue(upgradeIndex >= 0 && upgradeIndex < upgradeIsBought.Length);
		upgradeIsBought[upgradeIndex] = true; //Save();
	}

	/// <summary>Determines whether the player has previously bought a specified upgrade.</summary>
	/// <returns><c>true</c> if the player has previously bought the specified upgrade; otherwise, <c>false</c>.</returns>
	/// <param name="upgradeIndex">The upgrade (as an index).</param>
	public bool HasBoughtUpgrade(int upgradeIndex)
	{
		Assert.IsTrue(upgradeIndex >= 0 && upgradeIndex < upgradeIsBought.Length);
		return upgradeIsBought[upgradeIndex];
	}

	/// <summary>Increments the player's cash by a specified amount.</summary>
	/// <param name="amount">The amount to increment the player's cash by.</param>
	public void IncrementCashBy(float amount)
	{
		cash += amount; //Save();
	}

	/// <summary>Decrements the player's cash by a specified amount.</summary>
	/// <param name="amount">The amount to decrement the player's cash by.</param>
	public void DecrementCashBy(float amount)
	{
		cash -= amount; //Save();
	}

	/// <summary>Saves the player's data to disk.</summary>
	public void OnSaveToDisk()
	{
		dateLastPlayed = System.DateTime.UtcNow;
		Save();
	}

	/// <summary>Sets whether the game is paused. Start/Stop business managers and save to disk.</summary>
	/// <param name="gamePaused">Whether game should be paused.</param>
	public void SetGamePaused(bool gamePaused)
	{
		//either start or stop the business managers
		for(int i=0; i < businesses.Length; i++)
		{
			if(gamePaused) { businesses[i].ManagerStopsWorking(); }
			else if(!gamePaused && businesses[i].hasManager) { businesses[i].ManagerStartsWorking(); }
		}
		//save
		OnSaveToDisk();
	}

	#endregion
}
