/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>Custom Editor for the AudioDatabase component.</summary>
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
