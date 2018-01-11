/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A MonoSingleton which is used to store BusinessData, ManagerData and UpgradeData assets.</summary>
public class GameData : MonoSingleton<GameData>
{
	/// <summary>An array of assets for each business.</summary>
	[Tooltip("An array of assets for each business.")]
	[SerializeField] private BusinessData[] businesses;
	/// <summary>An array of assets for each manager.</summary>
	[Tooltip("An array of assets for each manager.")]
	[SerializeField] private ManagerData[] managers;
	/// <summary>An array of assets for each upgrade.</summary>
	[Tooltip("An array of assets for each upgrade.")]
	[SerializeField] private UpgradeData[] upgrades;

	#if UNITY_EDITOR
	/// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
	private void OnValidate()
	{
		Assert.IsTrue(businesses.Length > 0, "Expected at least one business.");
		Assert.IsTrue(managers.Length > 0, "Expected at least one manager.");
		Assert.IsTrue(upgrades.Length > 0, "Expected at least one upgrade.");
	}
	#endif

	/// <summary>Gets the data for a specific business.</summary>
	/// <returns>The data for the specific business.</returns>
	/// <param name="businessIndex">The business (as an index).</param>
	public BusinessData GetDataForBusiness(int businessIndex)
	{
		Assert.IsTrue(businessIndex >= 0 && businessIndex < businesses.Length);

		return businesses[businessIndex];
	}

	/// <summary>The number of businessses.</summary>
	public int numberOfBusinesses
	{
		get { return businesses.Length; }
	}

	/// <summary>Gets the data for a specific manager.</summary>
	/// <returns>The data for the specific manager.</returns>
	/// <param name="managerIndex">The manager (as an index).</param>
	public ManagerData GetDataForManager(int managerIndex)
	{
		Assert.IsTrue(managerIndex >= 0 && managerIndex < managers.Length);

		return managers[managerIndex];
	}

	/// <summary>The number of managers.</summary>
	public int numberOfManagers
	{
		get { return managers.Length; }
	}

	/// <summary>Gets the data for a specific upgrade.</summary>
	/// <returns>The data for the specific upgrade.</returns>
	/// <param name="upgradeIndex">The upgrade (as an index).</param>
	public UpgradeData GetDataForUpgrade(int upgradeIndex)
	{
		Assert.IsTrue(upgradeIndex >= 0 && upgradeIndex < upgrades.Length);

		return upgrades[upgradeIndex];
	}

	/// <summary>The number of upgrades.</summary>
	public int numberOfUpgrades
	{
		get { return upgrades.Length; }
	}
}
