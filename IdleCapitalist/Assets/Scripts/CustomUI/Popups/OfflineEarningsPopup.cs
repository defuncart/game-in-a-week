/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.UI;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>An popup which presents an overview of the player's earnings while they were offline.</summary>
public class OfflineEarningsPopup : DAPopup
{
	/// <summary>The popup's description.</summary>
	[Tooltip("The popup's description.")]
	[SerializeField] Text descriptionText;

	/// <summary>Initializes the popup.</summary>
	/// <param name="earningsToDisplay">The earnings to display (string).</param>
	public void Initialize(string earningsToDisplay)
	{
		descriptionText.text = string.Format(LocalizationManager.instance.StringForKey(LocalizationManagerKeys.OfflineEarningsPopupDescriptionText), earningsToDisplay);
	}

	/// <summary>Displays the popup.</summary>
	public override void Display()
	{
		//and set the popup to be visible
		SetVisibleInteractable(true);
	}

	/// <summary>Hides the popup.</summary>
	public override void Hide()
	{
		//set the popup to be invisible
		SetVisibleInteractable(false);
	}
}
