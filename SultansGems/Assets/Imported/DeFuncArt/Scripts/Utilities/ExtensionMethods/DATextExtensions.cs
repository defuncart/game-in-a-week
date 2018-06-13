/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of UI.Text extention methods.</summary>
	public static class DATextExtensions
	{
		/// <summary>Set the alpha of a Image.</summary>
		public static void SetAlpha(this Text text, float alpha)
		{
			Color temp = text.color;
			temp.a = alpha;
			text.color = temp;
		}

		/// <summary>Fades the alpha of a Text field to a given value over a given duration.</summary>
		public static IEnumerator FadeToAlphaWithDuration(this Text text, float alpha, float duration)
		{
			float speed = 1f / duration;
			//		float fadeUpDown = alpha > text.color.a ? 1 : -1;
			Color temp = text.color;
			float amount = alpha - temp.a;
			while(temp.a != alpha)
			{
				temp.a += amount * Time.deltaTime * speed;
				text.color = temp;
				yield return null;
			}
		}
	}
}
