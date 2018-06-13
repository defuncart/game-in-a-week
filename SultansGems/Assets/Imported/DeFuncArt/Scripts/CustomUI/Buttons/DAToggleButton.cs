/*
 *  Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
    public class DAToggleButton : DAButton
    {
        public EventHandlerBool OnToggled;

        /// <summary>Whether the buttons is toggled on.</summary>
        public bool isToggled { get; private set; }

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

        /// <summary>The toggle button's type.</summary>
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

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Toggle);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnToggled = null;
        }

        public virtual void InitializeToggled(bool toggled)
        {
            isToggled = toggled;
            UpdateButton();
        }

        /// <summary>Sets whether the button is selected.</summary>
        public virtual void Toggle()
        {
            isToggled = !isToggled;
            OnToggled(isToggled);
            //update button
            UpdateButton();
        }

        /// <summary>Updates the button.</summary>
        protected virtual void UpdateButton()
        {
            if (type == Type.Alpha) { alpha = isToggled ? selectedAlpha : unselectedAlpha; }
            else if (type == Type.Color) { image.color = isToggled ? selectedColor : unselectedColor; }
            else if (type == Type.Sprite) { image.sprite = isToggled ? selectedSprite : unselectedSprite; }
        }
    }
}
