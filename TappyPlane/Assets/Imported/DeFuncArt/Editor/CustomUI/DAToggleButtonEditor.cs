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
	/// <summary>A custom editor script for the DAToggleButton object.</summary>
	[CustomEditor(typeof(DAToggleButton), true)]
	[CanEditMultipleObjects]
	public class DAToggleButtonEditor : DAButtonEditor
	{
		/// <summary>The offSprite property.</summary>
		private SerializedProperty offSpriteProperty;
		/// <summary>The onSprite property.</summary>
		private SerializedProperty onSpriteProperty;

		/// <summary>Callback when script was enabled.</summary>
		protected override void OnEnable()
		{
			//firstly call base implementation
			base.OnEnable();
			//then get references to private variables
			offSpriteProperty = serializedObject.FindProperty("offSprite");
			onSpriteProperty = serializedObject.FindProperty("onSprite");
		}

		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw base properties
			base.OnInspectorGUI();
			//draw offSprite property
			EditorGUILayout.PropertyField(offSpriteProperty);
			//draw onSprite property
			EditorGUILayout.PropertyField(onSpriteProperty);

			//apply any changes to the serialized object
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif