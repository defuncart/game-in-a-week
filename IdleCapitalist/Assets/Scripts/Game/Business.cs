/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A model representing a business which can be serialized to disk.</summary>
[System.Serializable]
public class Business
{
	#region Properties

	/// <summary>Callback when a unit has been produced by the business.</summary>
	[field: System.NonSerialized] public EventHandler OnUnitProduced;
	// <summary>Callback when a manager was hired for the business.</summary>
	[field: System.NonSerialized] public EventHandler OnManagerHired;

	/// <summary>An enum denoting the type of the business.</summary>
	public enum Type
	{
		/// <summary>The Lemonade Stand business.</summary>
		LemonadeStand,
		/// <summary>The Newspaper Delivery business.</summary>
		NewspaperDelivery,
		/// <summary>The Car Wash business.</summary>
		CarWash,
		/// <summary>The Pizza Delivery business.</summary>
		PizzaDelivery,
		/// <summary>The Fish Boat business.</summary>
		FishBoat,
		/// <summary>The Beer Brewery business.</summary>
		BeerBrewery 
	}
	/// <summary>The business' type.</summary>
	public Type type { get; private set; }
	/// <summary>The business' name.</summary>
	public string name
	{ 
		get { return LocalizationManager.instance.StringForKey(type.ToString()); }
	}
	/// <summary>The business' index.</summary>
	public int index { get { return (int)type; } }
	/// <summary>The business' data.</summary>
	private BusinessData data
	{
		get { return GameData.instance.GetDataForBusiness(index); }
	}
	/// <summary>The business' image.</summary>
	public Sprite image { get { return data.image; } }
	/// <summary>The business' level (from 1 to maxLevel).</summary>
	public int level { get; private set; }
	/// <summary>"The maximum level the business can be upgrade to.</summary>
	public int maxLevel { get { return data.maxLevel; } }
	/// <summary>Whether the business is unlocked (i.e. player has bought it).</summary>
	public bool isUnlocked { get; private set; }
	/// <summary>The cost to unlock (i.e. buy) the business.</summary>
	public float costToUnlock { get { return data.initialCost; } }
	/// <summary>An enum denoting the type of the business manager.</summary>
	public enum ManagerType 
	{ 
		/// <summary>No manager. This is the default on business purchase.</summary>
		None, 
		/// <summary>A standard manager which runs the business.</summary>
		RunsBusiness, 
		/// <summary>An efficient manager who reduces the business' costs.</summary>
		ReducesCost
	}
	/// <summary>The business' manager type.</summary>
	private ManagerType managerType;
	/// <summary>Whether the business has a manager.</summary>
	public bool hasManager { get { return managerType != ManagerType.None; } }

	#endregion

	#region Time

	/// <summary>The time to produce a unit. Decreases exponentialy as level increases.</summary>
	public float timeToProduce
	{
		get
		{ 
			float temp = data.initialTime * Mathf.Pow(data.timeMultiplier, level);
			return temp > 0 ? temp : 0.01f; //TO DO
		}
	}
	/// <summary>The time to produce this unit. As timeToProduce can change over the course of a unit's production, this cached value is instead used.</summary>
	private float timeToProduceThisUnit;
	/// <summary>A timer to countdown the time required to produce the next unit.</summary>
	[System.NonSerialized] private float timer = 0;
	/// <summary>The time until next unit is produced.</summary>
	private float timeUntilNextUnitIsProduced
	{
		get { return timeToProduceThisUnit - timer; }
	}
	/// <summary>How far the completion of the current unit is (from 0 to 1).</summary>
	public float unitCompletePercentage
	{
		get { return timer > 0 ? (timeToProduceThisUnit - timer)/timeToProduceThisUnit : 0; }
	}
	/// <summary>A string to display in HH:MM:SS the time required to produce the current unit. Returns 00:00:00 if business isn't currently producing.</summary>
	public string timeDisplayString
	{
		get
		{
			//System.TimeSpan t = System.TimeSpan.FromSeconds(Mathf.Ceil(timer));
			System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(timer);
			return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}
	}

	#endregion

	#region Cost

	/// <summary>A multiplier which can reduce the cost of upgrade the business to subsequent levels.</summary>
	private float costReductionMultiplier;
	/// <summary>Sets the costReductionMultiplier. Should be >= 0 && < 1.</summary>
	public void SetCostReductionMultiplier(float costReductionMultiplier)
	{
		Assert.IsTrue(costReductionMultiplier >= 0 && costReductionMultiplier < 1, string.Format("costReductionMultiplier: {0} should be >= 0 && < 1", costReductionMultiplier));
		this.costReductionMultiplier = costReductionMultiplier;
	}
	/// <summary>The cost reduction factor (simply 1 minus costReductionMultiplier).</summary>
	private float costReductionFactor
	{
		get { return 1 - costReductionMultiplier; }
	}

	/// <summary>Determines the cost to upgrade by a given number of levels.</summary>
	/// <returns>The cost of the potential upgrade.</returns>
	/// <param name="numberOfLevels">The number of levels to upgrade by.</param>
	private float CostToUpgradeByLevels(int numberOfLevels)
	{
		return data.initialCost * ((Mathf.Pow(data.costMultiplier, level) * (Mathf.Pow(data.costMultiplier, numberOfLevels) - 1))/(data.costMultiplier - 1)) * costReductionFactor;
	}

	/// <summary>Determines the maximum number of levels that the player can upgrade by.</summary>
	private int DetermineMaximumNumberOfLevelsPlayerCanUpgrade()
	{
		return Mathf.FloorToInt(Mathf.Log((((PlayerManager.instance.cash*(data.costMultiplier-1))/(data.initialCost*costReductionFactor*Mathf.Pow(data.costMultiplier, level))) +1), data.costMultiplier));
	}

	#endregion

	#region Profit

	/// <summary>The milestone profit multiplier.</summary>
	private float milestoneProfitMultiplier;
	/// <summary>Determines the milestoneProfitMultiplier for a specified level.</summary>
	/// <returns>The milestoneProfitMultiplier for level.</returns>
	/// <param name="level">The specified level.</param>
	private float MilestoneProfitMultiplierForLevel(int level)
	{
		float milestoneProfitMultiplier = 1;
		for(int i=0; i < data.milestoneMultipliers.Length; i++)
		{
			if(level >= data.milestoneMultipliers[i].level)
			{
				milestoneProfitMultiplier *= data.milestoneMultipliers[i].multiplier;
			}
		}
		return milestoneProfitMultiplier;
	}
	/// <summary>Updates the milestoneProfitMultiplier to a given amount (cumulative). Should be >= 1.</summary>
	private void UpdateMilestoneProfitMultiplier(float amount)
	{
		Assert.IsTrue(amount >= 1, string.Format("amount: {0} should be >= 1", amount));
		milestoneProfitMultiplier = amount; //non-cumulative
	}
	/// <summary>The upgrade profit multiplier.</summary>
	private float upgradeProfitMultiplier;
	/// <summary>Updates the upgradeProfitMultiplier by a given amount (cumulative). Should be >= 1.</summary>
	public void UpdateUpgradeProfitMultiplier(float amount)
	{
		Assert.IsTrue(amount >= 1, string.Format("amount: {0} should be >= 1", amount));
		upgradeProfitMultiplier *= amount;
	}
	/// <summary>The prestige profit multiplier.</summary>
	private float prestigeProfitMultiplier;
	/// <summary>Updates the prestigeProfitMultiplier by a given amount (cumulative). Should be >= 1.</summary>
	public void UpdatePrestigeProfitMultiplier(float amount)
	{
		Assert.IsTrue(amount >= 1, string.Format("amount: {0} should be >= 1", amount));
		prestigeProfitMultiplier *= amount;
	}

	/// <summary>The business' profit per unit.</summary>
	public float profit
	{
		get {  return data.initialProfit * level * milestoneProfitMultiplier * upgradeProfitMultiplier * prestigeProfitMultiplier; }
	}
	/// <summary>Determines the profit for a specified level.</summary>
	/// <returns>The profit.</returns>
	/// <param name="level">The specified level.</param>
	private float ProfitForLevel(int level)
	{
		return data.initialProfit * level * MilestoneProfitMultiplierForLevel(level) * upgradeProfitMultiplier * prestigeProfitMultiplier;
	}

	/// <summary>The business' cash per second ratio.</summary>
	public float cashPerSecond
	{
		get { return profit / timeToProduce; }
	}
	/// <summary>Whether the cash per second should be displayed to the player.</summary>
	public bool shouldShowCashPerSecond { get; private set; }
	/// <summary>Sets whether the cash per second should be displayed to the player.</summary>
	public void SetShouldShowCashPerSecond(bool shouldShowCashPerSecond) { this.shouldShowCashPerSecond = shouldShowCashPerSecond; }

	#endregion

	#region Producing

	/// <summary>Whether the business is currently producing a unit.</summary>
	public bool isProducing { get; private set; } //timer > 0

	/// <summary>Starts producing a unit.</summary>
	public void StartProducing()
	{
		Assert.IsFalse(isProducing);

		MS.instance.StartCoroutine(ProduceUnit());
	}

	/// <summary>A coroutine which produces a unit for the business.</summary>
	private IEnumerator ProduceUnit()
	{
		//note the intial time to produce the unit (this may change as the player levels up, so this cached version is used instead)
		timeToProduceThisUnit = timeToProduce;
		//start timer
		timer = timeToProduceThisUnit;
		while((timer -= Time.deltaTime) > 0)
		{
			GameManager.instance.OnUpdateUI();
			yield return null;
		}
		//unit produced
		if(OnUnitProduced != null) { OnUnitProduced(); }
		PlayerManager.instance.IncrementCashBy(profit);
		GameManager.instance.OnUpdateUI();
		timer = 0;
	}

	#endregion

	#region Manager

	/// <summary>A reference to the ManagerStartsWorking coroutine.</summary>
	[System.NonSerialized] private Coroutine managerStartsWorkingCoroutine;

	/// <summary>Assigns a manager to the business.</summary>
	/// <param name="managerType">The manager type to assign.</param>
	public void AssignManager(ManagerType managerType)
	{
		if(this.managerType == ManagerType.None && managerType == ManagerType.RunsBusiness)
		{
			if(OnManagerHired != null) { OnManagerHired(); } //send an event that the manager was hired - player cannot manually produce a unit
		}

		this.managerType = managerType;
		if(isUnlocked) //technically one can buy a manager before owning the business...
		{
			if(managerType == ManagerType.RunsBusiness)
			{
				managerStartsWorkingCoroutine = MS.instance.StartCoroutine(ManagerStartsWorkingCoroutine());
			}
		}
	}

	/// <summary>Manager begins working (i.e. producing units for the business.</summary>
	public void ManagerStartsWorking()
	{
		Assert.IsTrue(hasManager);

		managerStartsWorkingCoroutine = MS.instance.StartCoroutine(ManagerStartsWorkingCoroutine());
	}

	/// <summary>A coroutine in which the business indefiently produces a unit.</summary>
	private IEnumerator ManagerStartsWorkingCoroutine()
	{
		while(true)
		{
			yield return ProduceUnit();
		}
	}

	/// <summary>Manager stops working.</summary>
	public void ManagerStopsWorking()
	{
		if(managerStartsWorkingCoroutine != null) { MS.instance.StopCoroutine(managerStartsWorkingCoroutine); }
	}

	#endregion

	#region Level

	/// <summary>Whether there is a level higher to which the business can upgrade to.</summary>
	public bool upgradeLevelExists { get { return level < maxLevel; } }
	/// <summary>Cached value. The number of levels to upgrade by.</summary>
	private int numberOfLevelsToUpgradeBy;
	/// <summary>Cached value. The cost to upgrade by numberOfLevelsToUpgradeBy levels.</summary>
	private float costToUpgradeByXLevels;
	/// <summary>Determine the cost to upgrade by X levels.</summary>
	public float UpgradeXLevelsCost(int numberOfLevels)
	{
		numberOfLevelsToUpgradeBy = (level + numberOfLevels > maxLevel ? maxLevel - level : numberOfLevels);

		if(numberOfLevelsToUpgradeBy > 0)
		{
			costToUpgradeByXLevels = CostToUpgradeByLevels(numberOfLevelsToUpgradeBy);
			return costToUpgradeByXLevels;
		}
		else
		{
			Debug.LogWarningFormat("{0} numberOfLevelsToUpgradeBy == 0", name);
			return -1;
		}
	}
	/// <summary>Determine the cost to upgrade by the maximum number of levels possible.</summary>
	public float UpgradeMaxLevelsCost()
	{
		int numberOfLevels = DetermineMaximumNumberOfLevelsPlayerCanUpgrade();
		return numberOfLevels == 0 && upgradeLevelExists ? UpgradeXLevelsCost(1) : UpgradeXLevelsCost(numberOfLevels);
	}
	/// <summary>Upgrades the business to last calculated values.</summary>
	public void UpgradeToLastCalculatedLevel()
	{
		if(numberOfLevelsToUpgradeBy >0 && PlayerManager.instance.cash >= costToUpgradeByXLevels)
		{
			UpdateLevelBy(numberOfLevelsToUpgradeBy);
			PlayerManager.instance.DecrementCashBy(costToUpgradeByXLevels);
			GameManager.instance.OnUpdateUI();
			//reset cached data
			costToUpgradeByXLevels = -1; numberOfLevelsToUpgradeBy = -1;
		}
	}

	/// <summary>Updates the level by a given amount. </summary>
	/// <param name="amount">The amount to increase level by.</param>
	private void UpdateLevelBy(int amount)
	{
		Assert.IsTrue(level + amount <= maxLevel, string.Format("level {0} + amount {1} should be <= maxLevel {2}", level, amount, maxLevel));
		level += amount;

		//check if the business has reached a milestone
		for(int i=0; i < hasReachedMilestone.Length; i++)
		{
			if(!hasReachedMilestone[i])
			{
				if(level >= data.milestoneMultipliers[i].level)
				{
					hasReachedMilestone[i] = true;
					UpdateMilestoneProfitMultiplier(data.milestoneMultipliers[i].multiplier);
				}
			}
		}
	}

	/// <summary>Whether the business has reached a set of milestones.</summary>
	private bool[] hasReachedMilestone;
	/// <summary>A value from 0 to 1 denoting the percentage complete until next milestone is awarded.</summary>
	public float nextMilestonePercentage
	{
		get
		{
			for(int i=0; i < data.milestoneMultipliers.Length - 1; i++)
			{
				int j = i + 1;
				if(!hasReachedMilestone[i])
				{
					return 1 - (data.milestoneMultipliers[i].level - level*1f) / (data.milestoneMultipliers[i].level - 1);
				}
				else if(!hasReachedMilestone[j])
				{
					return 1 - (data.milestoneMultipliers[j].level - level*1f) / (data.milestoneMultipliers[j].level - data.milestoneMultipliers[i].level);
				}
			}
			return 0; //all milestones reached
		}
	}

	/// <summary>Sets the business to be unlocked (i.e. bought).</summary>
	public void SetUnlocked()
	{
		isUnlocked = true;
		if(hasManager) { AssignManager(managerType); } //incase they bought a manager before a business...
	}

	/// <summary>Determine whether costs are higher than profit (for the next level).</summary>
	public bool costsAreHigherThanProfit
	{
		get
		{
			if(level < maxLevel) { return CostToUpgradeByLevels(1) > ProfitForLevel(level + 1); }
			else { return true; } //this is only used for prestige - already maxed out? good time to prestige!
		}
	}

	#endregion

	#region Initialization

	/// <summary>Initializes a new instance of the <see cref="Business"/> class.</summary>
	/// <param name="type">The business type.</param>
	public Business(Type type)
	{
		this.type = type;
		milestoneProfitMultiplier = upgradeProfitMultiplier = prestigeProfitMultiplier = 1;
		Reset();
	}

	/// <summary>Resets the business (initially or after a prestige).</summary>
	public void Reset()
	{
		level = 1;

		milestoneProfitMultiplier = upgradeProfitMultiplier = 1;
		SetCostReductionMultiplier(0);

		isUnlocked = data.initiallyUnlocked;
		hasReachedMilestone = new bool[data.milestoneMultipliers.Length]; //defaults to false

		ManagerStopsWorking(); //prestige
		shouldShowCashPerSecond = false;
		AssignManager(ManagerType.None);
		timeToProduceThisUnit = -1;
		timer = 0;
		isProducing = false;
	}

	#endregion
}
