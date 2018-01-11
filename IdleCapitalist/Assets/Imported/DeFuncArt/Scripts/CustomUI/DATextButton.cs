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
	/// <summary>A subclass of DAButton which has a Text component as a child.</summary>
	public class DATextButton : DAButton
	{
		/// <summary>The button's text component (as a child).</summary>
		private Text textComponent;

		/// <summary>Callback when the component awakes.</summary>
		protected override void Awake()
		{
			base.Awake();

			textComponent = GetComponentInChildren<Text>();
			Assert.IsNotNull(textComponent, "Expected a Text component.");
		}

		/// <summary>The button's text.</summary>
		public string text
		{
			get { return textComponent.text; }
			set { textComponent.text = value; }
		}
	}
}
