/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A base class from which popups can extend from, itself derived from DACanvas and inteneted to be added to a UI.Canvas.</summary></summary>
	public abstract class DAPopup : DACanvas
	{
		/// <summary>An event which occurs when the popup is closed.</summary>
		public EventHandler OnPopupClose;

		/// <summary>A custom method which subclasses must implement to configure when the popup should be displayed.</summary>
		public abstract void Display();

		/// <summary>A custom method which subclasses must implement to configure when the popup should be hidden.</summary>
		public abstract void Hide();

		/// <summary>Callback when the popup's ExitButton is pressed.</summary>
		public void ExitButtonPressed()
		{
			Hide();
			if(OnPopupClose != null) { OnPopupClose(); }
		}
	}
}
