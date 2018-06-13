/*
 *	Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A base class from which animating popups can inherit from.</summary>
	//[RequireComponent(typeof(Animator))]
	public abstract class DAPopupAnimated : DAPopup
	{
		/// <summary>A class of Animator triggers for DAPopup.</summary>
		protected struct DAPopupAnimatorTrigger
		{
			/// <summary>Popup starts offscreen and above.</summary>
			public const string OffscreenTop = "OffscreenTop";
			/// <summary>Popup starts offscreen and left.</summary>
			public const string OffscreenLeft = "OffscreenLeft";

			/// <summary>Popup bounces in from the left.</summary>
			public const string BounceInFromLeft = "BounceInFromLeft";
			/// <summary>Popup bounces out to the left.</summary>
			public const string BounceOutToLeft = "BounceOutToLeft";
			/// <summary>Popup bounces in from the top.</summary>
			public const string BounceInFromTop = "BounceInFromTop";
			/// <summary>Popup bounces out to the top.</summary>
			public const string BounceOutToTop = "BounceOutToTop";
		}

		/// <summary>An enum of the various offscreen positions.</summary>
		private enum OffscreenPosition
		{
			/// <summary>Offscreen and above.</summary>
			OffscreenTop, 
			/// <summary>Offscreen and left.</summary>
			OffscreenLeft
		}
		/// <summary>The popup's initial offscreen position. Defaults to OffscreenTop.</summary>
		[Tooltip("The popup's initial offscreen position. Defaults to OffscreenTop.")]
		[SerializeField] private OffscreenPosition offscreenPosition = OffscreenPosition.OffscreenTop;

		/// <summary>The popups animator, used to animate the popup onto and out of the screen.</summary>
		protected Animator animator;
		/// <summary>The popup's bounce in trigger.</summary>
		private string bounceInTrigger;
		/// <summary>The popup's bounce out trigger.</summary>
		private string bounceOutTrigger;

		/// <summary>Callback when the popup is initialized.</summary>
		protected override void InitializePopup()
		{
			//get a reference to the GameObject's Animator
			animator = GetComponentInChildren<Animator>();
			if(animator == null) { Debug.LogErrorFormat("{0} has no animator", name); }
			//set up the popups initial position
			animator.SetTrigger( offscreenPosition == OffscreenPosition.OffscreenTop ? DAPopupAnimatorTrigger.OffscreenTop : DAPopupAnimatorTrigger.OffscreenLeft );
			//set up the popups bounce in and bounce out triggers
			DetermineBounceInOutTriggers();
		}

		/// <summary>Trigger the popup to be displayed on screen.
		/// Each derived popup needs to implement this function.</summary>
		override public void Display()
		{
			SetVisible(true);
			animator.SetTrigger(bounceInTrigger);
		}

		/// <summary>Trigger the popup to be hidden from the screen.
		/// Each derived popup needs to implement this function.</summary>
		override public void Hide()
		{
			animator.SetTrigger(bounceOutTrigger);
		}

		/// <summary>Callback from Animator once a Bounced-In state has completed.</summary>
		protected void BounceInCompleted()
		{
			SetInteractable(true);
		}

		/// <summary>Callback from Animator once a Bounced-In state has completed.</summary>
		protected void BounceOutCompleted()
		{
			SetInteractable(false);
			SetVisible(false);
			RaiseOnPopupCloseEvent();
		}

		/// <summary>Determines the corresponding bounce in and bounce out triggers for the popup's initial position.</summary>
		private void DetermineBounceInOutTriggers()
		{	
			switch (offscreenPosition)
			{
			case OffscreenPosition.OffscreenTop:
				bounceInTrigger = DAPopupAnimatorTrigger.BounceInFromTop;
				bounceOutTrigger = DAPopupAnimatorTrigger.BounceOutToTop;
				break;
			}
		}
	}
}
