/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>An popup which presents a scrollview of ManagerPanels to the player.</summary>
public class ManagersPopup : DAPopup
{
	/// <summary>The Transform of the ScrollView's content (parent of all instantiated upgrade panels).</summary>
	[Tooltip("The Transform of the ScrollView's content (parent of all instantiated upgrade panels).")]
	[SerializeField] RectTransform scrollViewContent;
	/// <summary>The manager panel prefab.</summary>
	[Tooltip("The manager panel prefab.")]
	[SerializeField] ManagerPanel managerPanelPrefab;
	/// <summary>An array of instantiated panels.</summary>
	private List<ManagerPanel> panels;

	/// <summary>Displays the popup.</summary>
	public override void Display()
	{
		//instantiate a list of the upgrade panels which the player hasn't yet bought
		if(panels == null) { panels = new List<ManagerPanel>(); }
		for(int managerIndex = 0; managerIndex < GameData.instance.numberOfManagers; managerIndex++)
		{
			if(!PlayerManager.instance.HasBoughtManager(managerIndex))
			{
				ManagerPanel panel = Instantiate(managerPanelPrefab, scrollViewContent);
				panel.Initialize(managerIndex);
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
			if(panels[i] != null) { Destroy(panels[i].gameObject); } //panel could have deleted in ManagerPanel
		}
		panels.Clear();
	}
}
