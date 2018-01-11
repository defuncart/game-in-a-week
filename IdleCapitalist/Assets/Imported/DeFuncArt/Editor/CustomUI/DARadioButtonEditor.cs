/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.UI;
using UnityEngine;
using UnityEditor;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>Custom Editor for the DARadioButton component.</summary>
	[CustomEditor(typeof(DARadioButton))]
	public class DARadioButtonEditor : DAButtonEditor
	{
		/// <summary>The selectedSprite property.</summary>
		private SerializedProperty selectedSpriteProperty;
		/// <summary>The unselectedSprite property.</summary>
		private SerializedProperty unselectedSpriteProperty;

		/// <summary>Callback when script was enabled.</summary>
		protected override void OnEnable()
		{
			//firstly call base implementation
			base.OnEnable();
			//then get references to private variables
			selectedSpriteProperty = serializedObject.FindProperty("selectedSprite");
			unselectedSpriteProperty = serializedObject.FindProperty("unselectedSprite");
		}

		/// <summary>Callback to draw the inspector.</summary>
		override public void OnInspectorGUI()
		{
			//draw base properties and update the serialized object
			base.OnInspectorGUI();
			serializedObject.Update();
			//draw the private properties
			EditorGUILayout.PropertyField(selectedSpriteProperty);
			EditorGUILayout.PropertyField(unselectedSpriteProperty);
			//apply any changes to the serialized object
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif
