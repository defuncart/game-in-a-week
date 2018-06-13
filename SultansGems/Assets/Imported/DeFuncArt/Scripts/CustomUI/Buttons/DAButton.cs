/*
 *	Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A subclass of Button which adds some additional functionality.</summary>
	[RequireComponent(typeof(Image))]
	//	[AddComponentMenu("UI/DAButton", 30)]
	public class DAButton : Button
	{
		/// <summary>Whether the button will scale on touch down events.</summary>
		[Tooltip("Whether the button will scale on touch down events.")]
		public bool scaleOnTouch = true;
		/// <summary>The button's scale percentage.</summary>
		[Tooltip("The button's scale percentage.")]
		[Range(0.5f, 1.5f)] public float scalePercentage = 1.05f;
		/// <summary>The duration for a scale animation.</summary>
		private const float SCALE_DURATION = 0.05f;
		/// <summary>Whether the button will play a sound on touch down events.</summary>
		[Tooltip("Whether the button will play a sound on touch down events.")]
		public bool soundOnTouch = true;
		/// <summary>The button's sfx key.</summary>
		[Tooltip("The button's sfx key.")]
		public SFXDatabaseKeys soundOnTouchKey = SFXDatabaseKeys.buttonGeneric;

		/// <summary>The button's alpha value.</summary>
		public float alpha
		{
			get { return this.image.color.a; }
			set { this.image.SetAlpha(value); }
		}

		/// <summary>The button's color.</summary>
		public Color color
		{
			get { return this.image.color; }
			set { this.image.color = value; }
		}

		/// <summary>Sets whether the button is active in the scene.</summary>
		public void SetActive(bool active)
		{
			this.gameObject.SetActive(active);
		}

		/// <summary>A callback when a touch down event has been registered.</summary>
		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);

			if(interactable)
			{
				if(scaleOnTouch) { StartCoroutine(transform.ScaleToInTime(scalePercentage, SCALE_DURATION)); }
				if(soundOnTouch) { AudioManager.instance.PlaySFX(soundOnTouchKey); }
			}
		}

		/// <summary>A callback when a touch up event has been registered.</summary>
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);

			if(interactable)
			{
				if(scaleOnTouch) { StartCoroutine(transform.ScaleToInTime(1.0f, SCALE_DURATION)); }
			}
		}

		/*public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
		}*/
	}
}
