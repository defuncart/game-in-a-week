/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A data model for the particulars of a business stored as an asset.</summary>
[CreateAssetMenu(fileName="UpgradeData", menuName="Game Data/Upgrade Data", order=1)]
public class UpgradeData : ScriptableObject
{
	/// <summary>The upgrade's image.</summary>
	[Tooltip("The upgrade's image.")]
	public Sprite image;
	/// <summary>A locailization key for the upgrade's name.</summary>
	[Tooltip("A locailization key for the upgrade's name.")]
	[SerializeField] private string nameKey;
	/// <summary>An enum denoting the type of the upgrade (whether it is applicable to a single business or all businesses).</summary>
	public enum UpgradeType
	{
		/// <summary>An upgrade applicable to a single business.</summary>
		Business, 
		/// <summary>An upgrade applicable to all businesses.</summary>
		Monopoly
	}
	/// <summary>The upgrade's type (whether it is applicable to a single business or all businesses).</summary>
	[Tooltip("The upgrade's type (whether it is applicable to a single business or all businesses).")]
	public UpgradeType type;
	/// <summary>If the upgrade is application to a single business, then the relevant business.</summary>
	[Tooltip("If the upgrade is application to a single business, then the relevant business.")]
	public Business.Type business;
	/// <summary>The upgrade's profit multiplier.</summary>
	[Tooltip("The upgrade's profit multiplier.")]
	public float profitMultiplier;
	/// <summary>The upgrade's one-time buy cost.</summary>
	[Tooltip("The upgrade's one-time buy cost.")]
	public float cost;

	#if UNITY_EDITOR
	/// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
	private void OnValidate()
	{
		Assert.IsNotNull(image, "Need to supply an image for the upgrade.");
		Assert.IsTrue(nameKey != "", "Need to supply a string key for the upgrade's name");
		Assert.IsTrue(profitMultiplier > 0, "Profit Multiplier should be greater than 0.");
		Assert.IsTrue(cost > 0, "Cost should be greater than 0.");
	}
	#endif

	/// <summary>The upgrade's localized name.</summary>
	new public string name
	{
		get { return LocalizationManager.instance.StringForKey(nameKey); }
	}

	/// <summary>A localized description of the upgrade.</summary>
	public string description
	{
		get
		{
			if(type == UpgradeType.Business) //{0} profit x{1}
			{
				return string.Format(LocalizationManager.instance.StringForKey(LocalizationManagerKeys.UpgradeBusinessProfitsXTimes), LocalizationManager.instance.StringForKey(business.ToString()), profitMultiplier.ToString());
			}
			else //All profits x{0}
			{
				return string.Format(LocalizationManager.instance.StringForKey(LocalizationManagerKeys.UpgradeAllProfitsXTimes), profitMultiplier.ToString());
			}
		}
	}
}
