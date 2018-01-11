/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>An popup which presents a scrollview of UpgradePanels to the player.</summary>
public class UpgradesPopup : DAPopup
{
	/// <summary>The Transform of the ScrollView's content (parent of all instantiated upgrade panels).</summary>
	[Tooltip("The Transform of the ScrollView's content (parent of all instantiated upgrade panels).")]
	[SerializeField] RectTransform scrollViewContent;
	/// <summary>The upgrade panel prefab.</summary>
	[Tooltip("The upgrade panel prefab.")]
	[SerializeField] UpgradePanel upgradePanelPrefab;
	/// <summary>An array of instantiated panels.</summary>
	private List<UpgradePanel> panels;

	/// <summary>Displays the popup.</summary>
	public override void Display()
	{
		//instantiate a list of the upgrade panels which the player hasn't yet bought
		if(panels == null) { panels = new List<UpgradePanel>(); }
		for(int upgradeIndex = 0; upgradeIndex < GameData.instance.numberOfUpgrades; upgradeIndex++)
		{
			if(!PlayerManager.instance.HasBoughtUpgrade(upgradeIndex))
			{
				UpgradePanel panel = Instantiate(upgradePanelPrefab, scrollViewContent);
				panel.Initialize(upgradeIndex);
				panels.Add(panel);
			}
		}
		//and set the popup to be visible
		SetVisibleInteractable(true);
	}

	/// <summary>Hides the popup.</summary>
	public override void Hide()
	{
		Assert.IsNotNull(panels);

		//set the popup to be invisible
		SetVisibleInteractable(false);
		//and destroy all instantiated panels and clear the list
		for(int i=0; i < panels.Count; i++)
		{
			if(panels[i] != null) { Destroy(panels[i].gameObject); } //panel could have deleted in UpgradePanel
		}
		panels.Clear();
	}
}
