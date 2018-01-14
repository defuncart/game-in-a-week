/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.Utilities;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>A custom editor script for the AudioManager object.</summary>
	[CustomEditor(typeof(AudioManager))]
	public class AudioManagerEditor : ManagerWithKeysEditor
	{
		/// <summary>The filepath to save $MANAGERNAME$Keys.cs.</summary>
		protected override string filepath
		{
			get { return Application.dataPath + "/Imported/DeFuncArt/Scripts/Utilities/Audio/AudioManagerKeys.cs"; }
		}

		/// <summary>An array of keys which should be saved in $MANAGERNAME$Keys.cs.</summary>
		protected override string[] keys
		{
			get { return (target as AudioManager).keys; }
		}
	}
}
#endif