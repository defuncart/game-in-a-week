/*
 *  Written by James Leahy. (c) 2016-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
    /// <summary>A static popup with a single Text component.</summary>
    public class DAPopupStaticSingleText : DAPopupStatic
    {
        /// <summary>The Text component.</summary>
        [Tooltip("The Text component.")]
        [SerializeField] private Text text = null;

        /// <summary>Sets the popup's Text component text.</summary>
        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}
