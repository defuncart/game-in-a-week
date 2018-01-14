/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of image extention methods.</summary>
	public static class DAImageExtensions
	{
		/// <summary>Set the alpha of a Image.</summary>
		public static void SetAlpha(this Image image, float alpha)
		{
			Color temp = image.color;
			temp.a = alpha;
			image.color = temp;
		}
	}
}
