/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>A data model for the particulars of a business stored as an asset.</summary>
[CreateAssetMenu(fileName="BusinessData", menuName="Game Data/Business Data", order=1)]
public class BusinessData : ScriptableObject
{
	/// <summary>The business' image.</summary>
	[Tooltip("The business' image.")]
	public Sprite image;
	/// <summary>The business type.</summary>
	[Tooltip("The business type.")]
	public Business.Type type;
	/// <summary>Whether the business is initially unlocked.</summary>
	[Tooltip("Whether the business is initially unlocked.")]
	public bool initiallyUnlocked;
	/// <summary>The maximum level the business can be upgrade to.</summary>
	[Tooltip("The maximum level the business can be upgrade to.")]
	public int maxLevel;

	/// <summary>The initial cost to unlock the business.</summary>
	[Tooltip("The initial cost to unlock the business.")]
	public float initialCost;
	/// <summary>A multiplier (rased to the power of level) which increases the cost to upgrade the business to the next level.</summary>
	[Tooltip("A multiplier (rased to the power of level) which increases the cost to upgrade the business to the next level.")]
	public float costMultiplier;

	/// <summary>The time it initially takes to produce one unit.</summary>
	[Tooltip("The time it initially takes to produce one unit.")]
	public float initialTime;
	/// <summary>A multiplier (raised to the power of level) which reduces the time taken to produce a unit. 
	/// Defaults to 25 root of 0.5 (i.e. at level 25, multiplier is 0.5 (twice as fast), at level 50, multiplier is 0.25 (four times as fast).</summary>
	[Tooltip("A multiplier (raised to the power of level) which reduces the time taken to produce a unit. " +
		"Defaults to 25 root of 0.5 (i.e. at level 25, multiplier is 0.5 (twice as fast), at level 50, multiplier is 0.25 (four times as fast).")]
	public float timeMultiplier = 0.97264f;

	/// <summary>The initial profit from producing one unit.</summary>
	[Tooltip("The initial profit from producing one unit.")]
	public float initialProfit;

	[System.Serializable]
	/// <summary>A struct representing the structure of a Milestone Multiplier.</summary>
	public struct MilestoneMultiplier
	{
		/// <summary>The level the player should reach.</summary>
		[Tooltip("The level the player should reach.")]
		public int level;
		/// <summary>The multiplier to receive.</summary>
		[Tooltip("The multiplier to receive.")]
		public float multiplier;
	}
	/// <summary>An array of milestone pairs (level to reach, multiplier to receive).</summary>
	[Tooltip("An array of milestone pairs (level to reach, multiplier to receive).")]
	public MilestoneMultiplier[] milestoneMultipliers;
}
