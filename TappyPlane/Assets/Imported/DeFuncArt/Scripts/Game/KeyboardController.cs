/*
 * 	Written by James Leahy (c) 2017 DeFunc Art.
 * 	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.Utilities;
using UnityEngine;

/// <summary>Included in the DeFuncArt.Game namespace.</summary>
namespace DeFuncArt.Game
{
	/// <summary>A Keyboard controller used in Editor mode to listen for certain keypressed.
	/// Objects (i.e. PlayerController) can suscribe to these events.</summary>
	public class KeyboardController : MonoSingleton<KeyboardController>
	{
		/// <summary>An event which is triggered when the spacebar is pressed.</summary>
		public EventHandler OnSpacebar;

		/// <summary>Callback once per frame.</summary>
		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(OnSpacebar != null) { OnSpacebar(); }
			}
		}
	}
}
#endif
