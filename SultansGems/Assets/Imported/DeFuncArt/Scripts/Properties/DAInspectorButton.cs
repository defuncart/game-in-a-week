/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

/// <summary>Included in the DeFuncArt.CustomProperties namespace.</summary>
namespace DeFuncArt.CustomProperties
{
    [System.Serializable]
    public class DAInspectorButton
    {
        /// <summary>The button's method name.</summary>
        public string methodName;
        /// <summary>The button's text.</summary>
        public string buttonText;

        /// <summary>Initializes a DAInspectorButton.</summary>
        /// <param name="methodName">The button's method name.</param>
        /// <param name="buttonText">The button's text.</param>
        public DAInspectorButton(string methodName, string buttonText = "Button Text")
        {
            this.methodName = methodName;
            this.buttonText = buttonText;
        }
    }
}
#endif