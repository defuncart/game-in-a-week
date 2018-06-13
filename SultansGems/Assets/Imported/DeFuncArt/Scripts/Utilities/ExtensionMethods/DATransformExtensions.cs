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

		/// <summary>Sets a Transform's local x-position to a given value.</summary>
		/// <param name="x">The local x-position.</param>
		public static void SetLocalX(this Transform transform, float x)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.x = x;
			transform.localPosition = localPosition;	
		}

		/// <summary>Sets a Transform's y-position to a given value.</summary>
		/// <param name="y">The y-position.</param>
		public static void SetY(this Transform transform, float y)
		{
			Vector3 position = transform.position;
			position.y = y;
			transform.position = position;	
		}

		/// <summary>Sets a Transform's local y-position to a given value.</summary>
		/// <param name="x">The local y-position.</param>
		public static void SetLocalY(this Transform transform, float y)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.y = y;
			transform.localPosition = localPosition;	
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

        /// <summary>Moves a Transform by a given x-distance over a given duration.</summary>
        /// <param name="dx">The distance to move in the x-direction.</param>
        /// <param name="seconds">The time in seconds.</param>
        public static IEnumerator MoveXByInTime(this Transform transform, float dx, float seconds)
        {
            float elapsedTime = 0;
            float startingX = transform.position.x;
            float targetX = startingX + dx;

            while(elapsedTime <= seconds && transform != null)
            {
                float x = Mathf.Lerp(startingX, targetX, elapsedTime/seconds);
                transform.SetX(x);
                elapsedTime += Time.deltaTime;
                yield return null; //yield until after Update of next frame
            }
            if(transform != null) { transform.SetX(targetX); }
        }

        /// <summary>Moves a Transform by a given y-distance over a given duration.</summary>
        /// <param name="dy">The distance to move in the y-direction.</param>
        /// <param name="seconds">The time in seconds.</param>
        public static IEnumerator MoveYByInTime(this Transform transform, float dy, float seconds)
        {
            float elapsedTime = 0;
            float startingY = transform.position.y;
            float targetY = startingY + dy;

            while(elapsedTime <= seconds && transform != null)
            {
                float y = Mathf.Lerp(startingY, targetY, elapsedTime/seconds);
                transform.SetY(y);
                elapsedTime += Time.deltaTime;
                yield return null; //yield until after Update of next frame
            }
            if(transform != null) { transform.SetY(targetY); }
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
