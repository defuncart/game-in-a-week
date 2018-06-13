/*
 *	Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A base class from which static popups (those that appear/disappear from the screen without animating) can inherit from.</summary>
	public abstract class DAPopupStatic : DAPopup
	{
		/// <summary>Displays the popup.</summary>
		public override void Display()
		{
			//set the popup to be visible
			SetVisibleInteractable(true);
		}

		/// <summary>Hides the popup.</summary>
		public override void Hide()
		{
			//set the popup to be invisible
			SetVisibleInteractable(false);
			//and trigger OnPopupClose event
			RaiseOnPopupCloseEvent();
		}
	}
}
