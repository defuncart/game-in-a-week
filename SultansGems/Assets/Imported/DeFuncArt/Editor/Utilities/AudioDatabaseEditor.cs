/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.Utilities;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>Custom Editor for the AudioDatabase component.</summary>
	[CustomEditor(typeof(AudioDatabase), true)]
	public class AudioDatabaseEditor : CustomKeysEditor
	{
		/// <summary>The asset's name.</summary>
		protected override string assetName
		{
			//get { return "SFXDatabase"; }
            get { return target.name; }
		}
		/// <summary>The filepath to save $ASSETNAME$Keys.cs.</summary>
		protected override string filepath
		{
			get { return Application.dataPath + "/Scripts/Utilities/Audio/" + assetName + "Keys.cs"; }
		}
		/// <summary>An array of keys which should be saved in $ASSETNAME$Keys.cs.</summary>
		protected override string[] keys
		{
			get { return (target as AudioDatabase).keys; }
		}

		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw the default inspector
			DrawDefaultInspector();
			//draws a button which updates the database's keys
			if(GUILayout.Button(new GUIContent(text: "Update keys", tooltip: "Updates the database's keys.")))
			{
				(target as AudioDatabase).EDITOR_UpdateKeys();
			}
			//draw the export button, if applicable
			DrawExportButton();
		}
	}
}
#endif
