/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>A custom editor script for the AudioDatabase object.</summary>
	[CustomEditor(typeof(AudioDatabase), true)]
	public class AudioDatabaseEditor : Editor
	{
		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw the default inspector
			DrawDefaultInspector();
			//draws a button which updates the database's keys
			if(GUILayout.Button(new GUIContent(text: "Update keys", tooltip: "Updates the database's keys.")))
			{
				(target as AudioDatabase).UpdateKeys();
			}
		}
	}
}
#endif
