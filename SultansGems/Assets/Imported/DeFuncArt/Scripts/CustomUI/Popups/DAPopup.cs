/*
 *	Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A base class from which popups can inherit from.</summary>
	public abstract class DAPopup : DACanvas
	{
		/// <summary>An event which occurs when the popup closes.</summary>
		public event EventHandler OnPopupClose;

        /// <summary>Callback when the canvas is initialized.</summary>
        protected override sealed void InitializeCanvas()
        {
            InitializePopup();
        }

        /// <summary>Can be called by a subclass in place of Awake.</summary>
        protected virtual void InitializePopup() {}

        /// <summary>Callback before the canvas is destroyed.</summary>
        protected override sealed void DestroyCanvas()
        {
            OnPopupClose = null;
            DestroyPopup();
        }

        /// <summary>Can be called by a subclass in place of OnDestroy.</summary>
        protected virtual void DestroyPopup() {}

		/// <summary>Trigger the popup to be displayed on the screen.
		public abstract void Display();

		/// <summary>Trigger the popup to be hidden from the screen.
		/// Each derived popup needs to implement this function.</summary>
		public abstract void Hide();

		/// <summary>Each popup has an ExitButton and this is the resulting callback.</summary>
		public void ExitButtonPressed()
		{
			Hide();
		}

		/// <summary>Raises the OnPopupClose event (for subclasses).</summary>
		protected void RaiseOnPopupCloseEvent()
		{
			if(OnPopupClose != null) { OnPopupClose(); }
			#if UNITY_EDITOR
			if(OnPopupClose == null) { Debug.LogWarningFormat("No listeners for OnPopupClose for {0}", name); }
			#endif
		}
	}
}
