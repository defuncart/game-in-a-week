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
	/// <summary>A collection of UI.Image extention methods.</summary>
	public static class DAImageExtensions
	{
		/// <summary>Set the alpha of a Image.</summary>
		public static void SetAlpha(this Image image, float alpha)
		{
			Color temp = image.color;
			temp.a = alpha;
			image.color = temp;
		}

//		/// <summary>Sets the locale scale of an image.</summary>
//		public static void SetLocaleScale(this Image image, float x, float y)
//		{
//			RectTransform rt = image.rectTransform;
//			Vector3 temp = rt.localScale;
//			temp.x = x; temp.y = y;
//			rt.localScale = temp;
//		}
//
//		/// <summary>Sets the locale scale of an image.</summary>
//		public static IEnumerator SetLocaleXScaleToIn(this Image image, float x, float time)
//		{
//			RectTransform rt = image.rectTransform;
//			float elapsedTime = 0;
//			Vector3 startingScale = rt.localScale;
//			Vector3 targetScale = new Vector3(x, startingScale.y, startingScale.z);
//			while(elapsedTime <= time && rt != null)
//			{
//				rt.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime/time);
//				elapsedTime += Time.deltaTime;
//				yield return null;
//			}
//			if(rt != null) { rt.localScale = targetScale; }
//		}

		/// <summary>Sets an image's filled amount to a given value over a given duration.</summary>
		public static IEnumerator SetFillAmountToIn(this Image image, float fillAmount, float time)
		{
			Assert.IsTrue(image.type == Image.Type.Filled);

			float elapsedTime = 0;
			float startingFillAmount = image.fillAmount;
			while(image.fillAmount != fillAmount)
			{
				image.fillAmount = Mathf.Lerp(startingFillAmount, fillAmount, elapsedTime/time);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
	}
}
