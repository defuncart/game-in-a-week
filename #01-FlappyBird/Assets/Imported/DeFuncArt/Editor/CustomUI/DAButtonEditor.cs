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
	/// <summary>A custom editor script for the DAButton object.</summary>
	[CustomEditor(typeof(DAButton), true)]
	[CanEditMultipleObjects]
	public class DAButtonEditor : UnityEditor.UI.SelectableEditor
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
		/// <summary>The minimum scale percentage.</summary>
		protected const float SCALEPERCENTAGE_MIN = 0.5f;
		/// <summary>The maximum scale percentage.</summary>
		protected const float SCALEPERCENTAGE_MAX = 1.5f;
		/// <summary>The onClick property.</summary>
		protected SerializedProperty m_OnClickProperty;
		/// <summary>The soundOnTouchKey property.</summary>
		protected SerializedProperty soundOnTouchKeyProperty;
		/// <summary>A reference to the DAButton being inspected.</summary>
		private DAButton button;

		/// <summary>Callback when script was enabled.</summary>
		protected override void OnEnable()
		{
			//firstly call base implementation
			base.OnEnable();
			//then get references to private variables
			m_OnClickProperty = serializedObject.FindProperty("m_OnClick");
			soundOnTouchKeyProperty = serializedObject.FindProperty("soundOnTouchKey");
			button = (DAButton)target;
		}

		/// <summary>Callback to draw the inspector.</summary>
		public override void OnInspectorGUI()
		{
			//draw base properties and update the serialized object
			base.OnInspectorGUI();
			serializedObject.Update();

			//draw space
			EditorGUILayout.Space();
			//draw the on click property
			EditorGUILayout.PropertyField(m_OnClickProperty);
			//draw scale on touch boolean - if selected, draw scale percentage silder
			button.scaleOnTouch = EditorGUILayout.Toggle(new GUIContent(LabelText.SCALEONTOUCH, ToolTip.SCALEONTOUCH), button.scaleOnTouch);
			if(button.scaleOnTouch)
			{
				button.scalePercentage = EditorGUILayout.Slider(new GUIContent(LabelText.SCALEPERCENTAGE, ToolTip.SCALEPERCENTAGE), 
					button.scalePercentage, SCALEPERCENTAGE_MIN, SCALEPERCENTAGE_MAX);
			}
			//draw sound on touch boolean - if selected, draw soundOnTouchKey property (enum)
			button.soundOnTouch = EditorGUILayout.Toggle(new GUIContent(LabelText.SOUNDONTOUCH, ToolTip.SOUNDONTOUCH), button.soundOnTouch);
			if(button.soundOnTouch)
			{
				EditorGUILayout.PropertyField(soundOnTouchKeyProperty, new GUIContent(LabelText.SOUNDONTOUCHFILE, ToolTip.SOUNDONTOUCHFILE));
			}

			//apply any changes to the serialized object
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif