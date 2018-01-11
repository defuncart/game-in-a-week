/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of transport extention methods.</summary>
	public static class DATransformExtensions
	{
		/// <summary>Extension Method. Determine the children of the specified transform.</summary>
		/// <returns>The transform's children.</returns>
		public static IEnumerable<Transform> Children(this Transform transform)
		{
			foreach (Transform child in transform)
			{
				yield return child;
			}
		}

		/// <summary>Sets a Transform's x-position to a given value.</summary>
		/// <param name="x">The x-position.</param>
		public static void SetX(this Transform transform, float x)
		{
			Vector3 position = transform.position;
			position.x = x;
			transform.position = position;	
		}

		/// <summary>Resets a Transform's x-position to origin.</summary>
		public static void ResetX(this Transform transform)
		{
			transform.SetX(0);	
		}

		/// <summary>Sets a Transform's local x-position to a given value.</summary>
		/// <param name="x">The local x-position.</param>
		public static void SetLocalX(this Transform transform, float x)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.x = x;
			transform.localPosition = localPosition;	
		}

		/// <summary>Resets a Transform's local x-position to origin.</summary>
		public static void ResetLocalX(this Transform transform)
		{
			transform.SetLocalX(0);	
		}

		/// <summary>Sets a Transform's y-position to a given value.</summary>
		/// <param name="x">The y-position.</param>
		public static void SetY(this Transform transform, float y)
		{
			Vector3 position = transform.position;
			position.y = y;
			transform.position = position;	
		}

		/// <summary>Resets a Transform's y-position to origin.</summary>
		public static void ResetY(this Transform transform)
		{
			transform.SetY(0);	
		}

		/// <summary>Sets a Transform's local y-position to a given value.</summary>
		/// <param name="x">The local y-position.</param>
		public static void SetLocalY(this Transform transform, float y)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.y = y;
			transform.localPosition = localPosition;	
		}

		/// <summary>Resets a Transform's local y-position to origin.</summary>
		public static void ResetLocalY(this Transform transform)
		{
			transform.SetLocalY(0);	
		}

		/// <summary>Moves a Transform to a given position over a given duration.</summary>
		/// <param name="targetPosition">The target position.</param>
		/// <param name="seconds">The time in seconds.</param>
		public static IEnumerator MoveToInTime(this Transform transform, Vector3 targetPosition, float seconds)
		{
			float elapsedTime = 0;
			Vector3 startingPosition = transform.position;

			while(elapsedTime <= seconds && transform != null)
			{
				transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime/seconds);
				elapsedTime += Time.deltaTime;
				yield return null; //yield until after Update of next frame
			}
			if(transform != null) { transform.position = targetPosition; }
		}

		/// <summary>Moves a Transform to a given local position over a given duration.</summary>
		/// <param name="targetPosition">The target local position.</param>
		/// <param name="seconds">The time in seconds.</param>
		public static IEnumerator MoveLocallyToInTime(this Transform transform, Vector3 targetPosition, float seconds)
		{
			float elapsedTime = 0;
			Vector3 startingPosition = transform.localPosition;

			while(elapsedTime <= seconds && transform != null)
			{
				transform.localPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime/seconds);
				elapsedTime += Time.deltaTime;
				yield return null; //yield until after Update of next frame
			}
			if(transform != null) { transform.localPosition = targetPosition; }
		}

		/// <summary>Sets a Transform's Euler x-angle to a given value.</summary>
		/// <param name="z">The Euler x-angle.</param>
		public static void SetEulerX(this Transform transform, float x)
		{
			Vector3 eulerAngles = transform.eulerAngles;
			eulerAngles.x = x;
			transform.eulerAngles = eulerAngles;	
		}

		/// <summary>Sets a Transform's Euler x-angle to a given value.</summary>
		/// <param name="z">The Euler y-angle.</param>
		public static void SetEulerY(this Transform transform, float y)
		{
			Vector3 eulerAngles = transform.eulerAngles;
			eulerAngles.y = y;
			transform.eulerAngles = eulerAngles;	
		}

		/// <summary>Sets a Transform's Euler z-angle to a given value.</summary>
		/// <param name="z">The Euler z-angle.</param>
		public static void SetEulerZ(this Transform transform, float z)
		{
			Vector3 eulerAngles = transform.eulerAngles;
			eulerAngles.z = z;
			transform.eulerAngles = eulerAngles;	
		}

		/// <summary>Sets a Transform's localeScale (x, y) to a given value.</summary>
		/// <param name="scaleValue">The scale value.</param>
		public static void SetScale(this Transform transform, float scaleValue)
		{
			Vector3 localScale = transform.localScale;
			localScale.x = scaleValue; localScale.y = scaleValue;
			transform.localScale = localScale;	
		}

		/// <summary>Scales a Transform x, y to a given percentage over a given duration.</summary>
		/// <param name="targetScale">The target scale value.</param>
		/// <param name="seconds">The time in seconds.</param>
		public static IEnumerator ScaleToInTime(this Transform transform, float targetScale, float seconds)
		{
			float elapsedTime = 0;
			Vector3 startingPosition = transform.position;
			float startingLocalScale = transform.localScale.x;

			while(elapsedTime <= seconds && transform != null)
			{
				float scale = Mathf.Lerp(startingLocalScale, targetScale, elapsedTime/seconds);
				transform.SetScale(scale);

				elapsedTime += Time.deltaTime;
				yield return null; //yield until after Update of next frame
			}
			if(transform != null) { transform.SetScale(targetScale); }
		}
	}
}
