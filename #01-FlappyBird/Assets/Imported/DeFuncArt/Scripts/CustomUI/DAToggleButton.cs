/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A subclass of Button which adds some additional functionality.</summary>
	[RequireComponent(typeof(Image))]
	//[AddComponentMenu("UI/DAToggleButton", 30)]
	public class DAToggleButton : DAButton
	{
		/// <summary>A sprite for the off state.</summary>
		[SerializeField] private Sprite offSprite;
		/// <summary>A sprite for the on state.</summary>
		[SerializeField] private Sprite onSprite;

		/// <summary>A backing variable for selected.</summary>
		private bool _selected;
		/// <summary>Whether the toggle is selected (i.e. on).</summary>
		public bool selected
		{
			get { return _selected; }
			set { _selected = value; Refresh(); }
		}

		/// <summary>Callback when the instance is started.</summary>
		new private void Start()
		{
			//call base implementation and refresh the toggle
			base.Start();
			Refresh();
		}

		/// <summary>Refreshes the toggle.</summary>
		private void Refresh()
		{
			image.sprite = selected ? onSprite : offSprite;
		}

		/// <summary>A callback when a touch down event has been registered.</summary>
		public override void OnPointerDown(PointerEventData eventData)
		{
			//pass event data onto base class
			base.OnPointerDown(eventData);
			//if the button is interactable, toggle selected state
			if(interactable) { selected = !selected; }
		}
	}
}
