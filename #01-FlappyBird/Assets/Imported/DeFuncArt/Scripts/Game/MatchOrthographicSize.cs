/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using UnityEngine;

/// <summary>Included in the DeFuncArt.Game namespace.</summary>
namespace DeFuncArt.Game
{
	/// <summary>Transform's y-positions matches the camera's orthographic size. Useful for ceiling gameobject colliders.</summary>
	public class MatchOrthographicSize : MonoBehaviour
	{
		/// <summary>Callback when the object starts.</summary>
		private void Start()
		{
			transform.SetY(Camera.main.orthographicSize);
		}
	}
}
