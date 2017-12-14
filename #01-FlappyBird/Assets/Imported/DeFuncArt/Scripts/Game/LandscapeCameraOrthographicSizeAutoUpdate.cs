/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Included in the DeFuncArt.Game namespace.</summary>
namespace DeFuncArt.Game
{
	[ExecuteInEditMode]
	/// <summary>A script which auto updates the camera's orthographic size.</summary>
	public class LandscapeCameraOrthographicSizeAutoUpdate : MonoBehaviour
	{
		/// <summary>The target screen width.</summary>
		public int targetWidth = 1024;
		/// <summary>The number of pixels to units.</summary>
		public float pixelsToUnits = 1;

		#if UNITY_EDITOR
		private int screenWidth, screenHeight;
		#endif

		/// <summary>Awake the instance.</summary>
		private void Awake()
		{
			UpdateOrthographicSize();
			#if UNITY_EDITOR
			UpdateScreenVariables();
			#endif
		}

		/// <summary>Updates the camera's orthographic size.</summary>
		private void UpdateOrthographicSize()
		{
			int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
			Camera.main.orthographicSize = height / pixelsToUnits / 2;
		}

		#if UNITY_EDITOR
		/// <summary>Updates the instance.</summary>
		private void Update()
		{
			if(screenWidth != Screen.width || screenHeight != Screen.height)
			{
				UpdateOrthographicSize();
				UpdateScreenVariables();
			}
		}

		/// <summary>Updates the current screen variables.</summary>
		private void UpdateScreenVariables()
		{
			screenWidth = Screen.width; screenHeight = Screen.height;
		}
		#endif
	}
}