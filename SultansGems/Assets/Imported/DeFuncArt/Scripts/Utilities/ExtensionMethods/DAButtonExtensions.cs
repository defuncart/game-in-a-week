/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of UI.Button extention methods.</summary>
	public static class DAButtonExtensions
	{
		/// <summary>Set the alpha of a Button.</summary>
		public static void SetAlpha(this Button button, float alpha)
		{
			//set the alpha channel of the button's image
			//TO DO
			//button.image.SetAlpha(alpha);
			//button.image returns the wrong image (onboarding)
			button.GetComponent<Image>().SetAlpha(alpha);
			//TO DO
			//and set the alpha of any children images too
			IEnumerable<Transform> children = button.gameObject.transform.Children();
			foreach(Transform child in children)
			{
				Image childImage = child.GetComponent<Image>();
				if(childImage != null) { childImage.SetAlpha(alpha); }
			}
		}
	}
}
