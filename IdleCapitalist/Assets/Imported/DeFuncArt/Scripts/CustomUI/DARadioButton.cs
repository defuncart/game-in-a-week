/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A simple RadioButton which is extended from Unity's UI Button.
	/// This class's attidional properties are viewable in the inspector via DARadioButtonEditor script.</summary>
	public class DARadioButton : DAButton
	{
		/// <summary>Whether the button is selected or not.</summary>
		public bool isSelected { get; private set; }
//		private GameObject selectedGameObject; //a child object of the button which has a selected sprite outline.

		[SerializeField] private Sprite selectedSprite;
		[SerializeField] private Sprite unselectedSprite;

//		public enum Type
//		{
//			
//		}

		/// <summary>Initialize class members.</summary>
//		override protected void Awake()
//		{
//			Assert.IsTrue(transform.childCount == 1);
//
//			selectedGameObject = transform.GetChild(0).gameObject;
//			base.Awake();
//		}

		/// <summary>When the Button is selected, the sprite outline is displayed.</summary>
		public void SetSelected(bool selected)
		{
			isSelected = selected;
//			selectedGameObject.SetActive(selected);
			image.sprite = selected ? selectedSprite : unselectedSprite;
		}
	}
}
