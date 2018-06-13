/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>Included in the DeFuncArt.UI namespace.</summary>
namespace DeFuncArt.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	/// <summary>DAPanel is a custom component added to a UI Panel which allows the panel to be
	/// faded in and out, interactability updated etc.</summary>
	public class DAPanel : MonoBehaviour
	{
		/// <summary>The panel's canvas group.</summary>
		private CanvasGroup canvasGroup;

		/// <summary>Whether the panel is interactable on awake.</summary>
		[Tooltip("Whether the panel is interactable on awake.")]
		[SerializeField] private bool interactableOnAwake = true;

		/// <summary>Callback when the component awakes.</summary>
		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			SetInteractable(interactableOnAwake);
			InitializePanel();
		}

		/// <summary>Can be called by a subclass in place of awake.</summary>
		protected virtual void InitializePanel() {}

		/// <summary>Callback when the component awakes.</summary>
		private void OnDestroy()
		{
			DestroyPanel();
		}

		/// <summary>Can be called by a subclass in place of awake.</summary>
		protected virtual void DestroyPanel() {}

		#region Canvas

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

		#endregion

		#region OLD DAPanel

		/// <summary>Fade the panel in with a given duration.</summary>
		/// <param name="duration">Duration.</param>
		public void FadeInWithDuration(float duration)
		{
			canvasGroup.alpha = 0; 
			StartCoroutine(FadeToAlphaWithDuration(1f, duration));
		}

		/// <summary>Fade the panel out with a given duration.</summary>
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

			SetInteractable(false); //panel is not interactable for duration of fading

			float speed = 1f / duration;
			float fadeUpDown = alpha > canvasGroup.alpha ? 1 : -1;
			while(canvasGroup.alpha != alpha)
			{
				canvasGroup.alpha += fadeUpDown * Time.deltaTime * speed;
				yield return null;
			}

			if(isVisible) { SetInteractable(true); } //if panel is visible, it is once-again interactable
		}

		#endregion
	}
}
