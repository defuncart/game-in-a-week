/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Included in the DeFuncArt.Game namespace.</summary>
namespace DeFuncArt.Game
{
	/// <summary>This script wraps the position of an object to its start position after moving a given width.</summary>
	public class WrapHorizontalPosition : MonoBehaviour
	{
		/// <summary>The object's total width.</summary>
		[Tooltip("The object's total width.")]
		[SerializeField] private float totalWidth;
		/// <summary>The object's start position.</summary>
		private Vector3 startPosition;

		/// <summary>Callback when the object awakes.</summary>
		private void Awake()
		{
			startPosition = transform.position;
		}

		/// <summary>Callback once per frame when the object updates.</summary>
		private void Update()
		{
			//if object is offscreen, wrap back to start position
			if(Mathf.Abs(transform.position.x - startPosition.x) >= totalWidth)
			{
				transform.position = startPosition;
			}
		}
	}
}
