/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	/// <summary>A custom component added to a UI Canvas which allows the canvas to be faded in and out, interactability updated etc.</summary>
	[RequireComponent(typeof(CanvasGroup))]
	public class DACanvas : MonoBehaviour
	{
		/// <summary>The canvas group.</summary>
		private CanvasGroup canvasGroup;

		/// <summary>Callback when the object is awoken.</summary>
		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>(); //get a reference to the GameObject's CanvasGroup
			SetVisibleInteractable(false);
			canvasGroup.ignoreParentGroups = false;
		}

		/// <summary>Sets whether the canvas is interactable.</summary>
		public void SetInteractable(bool interactable)
		{
			canvasGroup.interactable = interactable; //interactability of all children
			canvasGroup.blocksRaycasts = interactable; //whether touches will be processed
		}

		/// <summary>Determine if the canvas is visible.</summary>
		public bool isVisible
		{
			get { return canvasGroup.alpha != 0; } 
		}

		/// <summary>Sets whether the panel is interactable.</summary>
		public void SetVisibility(float alpha)
		{
			Assert.IsTrue(alpha >= 0 && alpha <= 1);

			canvasGroup.alpha = alpha;
		}

		/// <summary>Sets whether the panel is visible.</summary>
		public void SetVisible(bool isVisible)
		{
			canvasGroup.alpha = (isVisible ? 1f : 0f);
		}

		/// <summary>Sets whether the panel is visible and interactable.</summary>
		public void SetVisibleInteractable(bool isVisibleInteractable)
		{
			SetVisible(isVisibleInteractable);
			SetInteractable(isVisibleInteractable);
		}

		/// <summary>Fade the canvas in with a given duration.</summary>
		/// <param name="duration">Duration.</param>
		public void FadeInWithDuration(float duration)
		{
			canvasGroup.alpha = 0; 
			StartCoroutine(FadeToAlphaWithDuration(1f, duration));
		}

		/// <summary>Fade the canvas out with a given duration.</summary>
		/// <param name="duration">Duration.</param>
		public void FadeOutWithDuration(float duration)
		{
			StartCoroutine(FadeToAlphaWithDuration(0f, duration));
		}

		/// <summary>Fade the Panel to a given alpha value over a given duration.</summary>
		/// <param name="alpha">Alpha.</param>
		/// <param name="duration">Duration.</param>
		private IEnumerator FadeToAlphaWithDuration(float alpha, float duration)
		{
			Assert.IsTrue(alpha >= 0 && alpha <= 1);
			Assert.IsNotNull(canvasGroup);

			SetInteractable(false); //canvas is not interactable for duration of fading

			float speed = 1f / duration;
			float fadeUpDown = alpha > canvasGroup.alpha ? 1 : -1;
			while(canvasGroup.alpha != alpha)
			{
				canvasGroup.alpha += fadeUpDown * Time.deltaTime * speed;
				yield return null; //yield until after Update of next frame
			}

			if(isVisible) { SetInteractable(true); } //if canvas is visible, it is once-again interactable
		}
	}
}