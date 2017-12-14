/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.UI;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>A custom editor script for the SettingsManagerBooleanToggleButton object.</summary>
	[CustomEditor(typeof(SettingsManagerBooleanToggleButton), true)]
	[CanEditMultipleObjects]
	public class SettingsManagerBooleanToggleButtonEditor : DAToggleButtonEditor
	{
		/// <summary>The settingsVariable property.</summary>
		private SerializedProperty settingsVariableProperty;

		/// <summary>Callback when script was enabled.</summary>
		protected override void OnEnable()
		{
			//firstly call base implementation
			base.OnEnable();
			//then get references to private variables
			settingsVariableProperty = serializedObject.FindProperty("settingsVariable");
		}

		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw base properties
			base.OnInspectorGUI();
			//draw settingsVariable property
			EditorGUILayout.PropertyField(settingsVariableProperty);

			//apply any changes to the serialized object
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif