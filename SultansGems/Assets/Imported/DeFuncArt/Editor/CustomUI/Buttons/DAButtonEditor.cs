/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
	/// <summary>Custom Editor for the DAButton component.</summary>
	[CustomEditor(typeof(DAButton), true)]
	[CanEditMultipleObjects]
	public class DAButtonEditor : ButtonEditor//UnityEditor.UI.SelectableEditor
	{
		/// <summary>The scaleOnTouch property.</summary>
		protected SerializedProperty scaleOnTouchProperty;
		/// <summary>The scalePercentage property.</summary>
		protected SerializedProperty scalePercentageProperty;
		/// <summary>The soundOnTouch property.</summary>
		protected SerializedProperty soundOnTouchProperty;
		/// <summary>The soundOnTouchKey property.</summary>
		protected SerializedProperty soundOnTouchKeyProperty;

		/// <summary>Callback when script was enabled.</summary>
		protected override void OnEnable()
		{
			//firstly call base implementation
			base.OnEnable();
			//then get references to private variables
			scaleOnTouchProperty = serializedObject.FindProperty("scaleOnTouch");
			scalePercentageProperty = serializedObject.FindProperty("scalePercentage");
			soundOnTouchProperty = serializedObject.FindProperty("soundOnTouch");
			soundOnTouchKeyProperty = serializedObject.FindProperty("soundOnTouchKey");
		}

		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw base properties and update the serialized object
			base.OnInspectorGUI();
			serializedObject.Update();

			//draw scale on touch boolean - if selected, draw scale percentage silder
			EditorGUILayout.PropertyField(scaleOnTouchProperty);
			if(scaleOnTouchProperty.boolValue)
			{
				EditorGUILayout.PropertyField(scalePercentageProperty);
			}
			//draw sound on touch boolean - if selected, draw soundOnTouchKey property (enum)
			EditorGUILayout.PropertyField(soundOnTouchProperty);
			if(soundOnTouchProperty.boolValue)
			{
				EditorGUILayout.PropertyField(soundOnTouchKeyProperty);
			}

			//apply any changes to the serialized object
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif
