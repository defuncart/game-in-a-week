/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.UI;
using UnityEditor;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
    /// <summary>Custom Editor for the DARadioButton component.</summary>
    [CustomEditor(typeof(DAToggleButton), true)]
    [CanEditMultipleObjects]
    public class DAToggleButtonEditor : DAButtonEditor
    {
        /// <summary>The type property.</summary>
        protected SerializedProperty typeProperty;
        /// <summary>The selectedAlpha property.</summary>
        protected SerializedProperty selectedAlphaProperty;
        /// <summary>The unselectedAlpha property.</summary>
        protected SerializedProperty unselectedAlphaProperty;
        /// <summary>The selectedColor property.</summary>
        protected SerializedProperty selectedColorProperty;
        /// <summary>The unselectedColor property.</summary>
        protected SerializedProperty unselectedColorProperty;
        /// <summary>The selectedSprite property.</summary>
        protected SerializedProperty selectedSpriteProperty;
        /// <summary>The unselectedSprite property.</summary>
        protected SerializedProperty unselectedSpriteProperty;

        /// <summary>Callback when script was enabled.</summary>
        protected override void OnEnable()
        {
            //firstly call base implementation
            base.OnEnable();
            //then get references to private variables
            typeProperty = serializedObject.FindProperty("type");
            selectedAlphaProperty = serializedObject.FindProperty("selectedAlpha");
            unselectedAlphaProperty = serializedObject.FindProperty("unselectedAlpha");
            selectedColorProperty = serializedObject.FindProperty("selectedColor");
            unselectedColorProperty = serializedObject.FindProperty("unselectedColor");
            selectedSpriteProperty = serializedObject.FindProperty("selectedSprite");
            unselectedSpriteProperty = serializedObject.FindProperty("unselectedSprite");
        }

        /// <summary>Callback to draw the inspector.</summary>
        public override void OnInspectorGUI()
        {
            //draw base properties and update the serialized object
            base.OnInspectorGUI();
            serializedObject.Update();

            //draw the type property
            EditorGUILayout.PropertyField(typeProperty);
            //if index 0 enum, then draw alpha properties, otherwise draw sprite properties
            if(typeProperty.intValue == 0)
            {
                EditorGUILayout.PropertyField(selectedAlphaProperty);
                EditorGUILayout.PropertyField(unselectedAlphaProperty);
            }
            else if(typeProperty.intValue == 1)
            {
                EditorGUILayout.PropertyField(selectedColorProperty);
                EditorGUILayout.PropertyField(unselectedColorProperty);
            }
            else
            {
                EditorGUILayout.PropertyField(selectedSpriteProperty);
                EditorGUILayout.PropertyField(unselectedSpriteProperty);
            }

            //apply any changes to the serialized object
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
