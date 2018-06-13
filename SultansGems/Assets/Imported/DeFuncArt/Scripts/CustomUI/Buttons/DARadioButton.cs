/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
    /// <summary>A subclass of DAButton which adds radio group functionality.</summary>
    public class DARadioButton : DAButton
	{
		/// <summary>Whether the button is selected or not.</summary>
        public bool isSelected { get; protected set; }

        /// <summary>An enum denoting the various radio button types.</summary>
        public enum Type
        {
            /// <summary>Changes alpha when selected/unselected.</summary>
            Alpha,
            /// <summary>Changes color when selected/unselected.</summary>
            Color,
            /// <summary>Changes sprites when selected/unselected.</summary>
            Sprite
        }
        /// <summary>The radio button's type.</summary>
        [Tooltip("The radio button's type.")]
        [SerializeField] private Type type = Type.Alpha;

        /// <summary>An alpha value when the button is selected.</summary>
        [Tooltip("An alpha value when the button is selected.")]
        [Range(0, 1), SerializeField] private float selectedAlpha = 1f;
        /// <summary>An alpha value when the button is unselected.</summary>
        [Tooltip("An alpha value when the button is unselected.")]
        [Range(0, 1), SerializeField] private float unselectedAlpha = 0.6f;

        /// <summary>A color to display when the button is selected.</summary>
        [Tooltip("An sprite to display when the button is selected.")]
        [SerializeField] private Color selectedColor = Color.black;
        /// <summary>A color to display when the button is unselected.</summary>
        [Tooltip("An sprite to display when the button is unselected.")]
        [SerializeField] private Color unselectedColor = Color.black;

        /// <summary>An sprite to display when the button is selected.</summary>
        [Tooltip("An sprite to display when the button is selected.")]
		[SerializeField] private Sprite selectedSprite = null;
        /// <summary>An sprite to display when the button is unselected.</summary>
        [Tooltip("An sprite to display when the button is unselected.")]
        [SerializeField] private Sprite unselectedSprite = null;

        /// <summary>Sets whether the button is selected.</summary>
		public virtual void SetSelected(bool selected)
		{
			isSelected = selected;
            //update button
            UpdateButton(selected);
		}

        /// <summary>Updates the button.</summary>
        protected virtual void UpdateButton(bool selected)
        {
            if (type == Type.Alpha) { alpha = selected ? selectedAlpha : unselectedAlpha; }
            else if (type == Type.Color) { image.color = selected ? selectedColor : unselectedColor; }
            else if (type == Type.Sprite) { image.sprite = selected ? selectedSprite : unselectedSprite; }
        }
	}
}
