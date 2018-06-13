/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.UI;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
    /// <summary>Custom Editor for the DARadioButtonGroup component.</summary>
    [CustomEditor(typeof(DARadioButtonGroup), true)]
    [CanEditMultipleObjects]
    public class DARadioButtonGroupEditor : Editor
    {
        /// <summary>The manuallyAssign property.</summary>
        protected SerializedProperty manuallyAssignProperty;
        /// <summary>The radioButtons property.</summary>
        protected SerializedProperty radioButtonsProperty;

        /// <summary>Callback when script was enabled.</summary>
        protected void OnEnable()
        {
            //get references to private variables
            manuallyAssignProperty = serializedObject.FindProperty("manuallyAssign");
            radioButtonsProperty = serializedObject.FindProperty("radioButtons");
        }

        /// <summary>Callback to draw the inspector.</summary>
        public override void OnInspectorGUI()
        {
            //update the serialized object
            serializedObject.Update();

            //draw the manuallyAssign property
            EditorGUILayout.PropertyField(manuallyAssignProperty);

            //if manuallyAssign is true, then draw the radioButtonsProperty array
            if(manuallyAssignProperty.boolValue)
            {
                EditorGUILayout.PropertyField(property: radioButtonsProperty, includeChildren: true);

                //draw a button which allows the automatic setting of the radio buttons
                if (GUILayout.Button(new GUIContent(text: "Set Radio Buttons", tooltip: "Automatically sets the group's radio buttons from children active in the hierarchy.")))
                {
                    (target as DARadioButtonGroup).AutomaticallySetRadioButtons();
                }
            }

            //apply any changes to the serialized object
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
