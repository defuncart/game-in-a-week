/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
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
		/// <summary>A struct of the Label values for DAButton's serialized properties.</summary>
		protected struct LabelText
		{
			/// <summary>The label text for variable scaleOnTouch text.</summary>
			public const string SCALEONTOUCH = "Scale on Touch";
			/// <summary>The label text for variable scalePercentage.</summary>
			public const string SCALEPERCENTAGE = "Scale Percentage";
			/// <summary>The label text for variable soundOnTouchFile.</summary>
			public const string SOUNDONTOUCHFILE = "Soundfile";
			/// <summary>The label text for variable soundOnTouch.</summary>
			public const string SOUNDONTOUCH = "Sound on Touch";
		}
		/// <summary>A struct of the Tooltip values for DAButton's serialized properties.</summary>
		protected struct ToolTip
		{
			/// <summary>The tooltip for variable scaleOnTouch.</summary>
			public const string SCALEONTOUCH = "Whether the button will scale on touch down events.";
			/// <summary>The tooltip for variable scalePercentage.</summary>
			public const string SCALEPERCENTAGE = "The button's scale percentage";
			/// <summary>The tooltip for variable soundOnTouch.</summary>
			public const string SOUNDONTOUCH = "Whether the button will play a sound on touch down events.";
			/// <summary>The tooltip for variable soundOnTouchFile.</summary>
			public const string SOUNDONTOUCHFILE = "The button's soundfile.";
		}
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
			EditorGUILayout.PropertyField(scaleOnTouchProperty, new GUIContent(LabelText.SCALEONTOUCH, ToolTip.SCALEONTOUCH));
			if(scaleOnTouchProperty.boolValue)
			{
				EditorGUILayout.PropertyField(scalePercentageProperty, new GUIContent(LabelText.SCALEPERCENTAGE, ToolTip.SCALEPERCENTAGE));
			}
			//draw sound on touch boolean - if selected, draw soundOnTouchKey property (enum)
			EditorGUILayout.PropertyField(soundOnTouchProperty, new GUIContent(LabelText.SOUNDONTOUCH, ToolTip.SOUNDONTOUCH));
			if(soundOnTouchProperty.boolValue)
			{
				EditorGUILayout.PropertyField(soundOnTouchKeyProperty, new GUIContent(LabelText.SOUNDONTOUCHFILE, ToolTip.SOUNDONTOUCHFILE));
			}

			//apply any changes to the serialized object
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif
