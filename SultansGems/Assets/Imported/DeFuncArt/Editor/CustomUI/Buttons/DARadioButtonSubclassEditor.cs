/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
    /// <summary>Custom Editor which subclasses of DAButton can extend from.</summary>
    public abstract class DARadioButtonSubclassEditor : DARadioButtonEditor
    {
        /// <summary>An array of property names.</summary>
        protected abstract string[] propertyNames { get; }
        /// <summary>An array of properties.</summary>
        protected SerializedProperty[] properties;

        /// <summary>Callback when script was enabled.</summary>
        protected override void OnEnable()
        {
            //firstly call base implementation
            base.OnEnable();
            //then get references to the class's properties
            properties = new SerializedProperty[propertyNames.Length];
            for(int i=0; i < properties.Length; i++)
            {
                properties[i] = serializedObject.FindProperty(propertyNames[i]);
            }
        }

        /// <summary>Callback to draw the inspector.</summary>
        public override void OnInspectorGUI()
        {
            //draw base properties and update the serialized object
            base.OnInspectorGUI();
            serializedObject.Update();

            //draw properties
            for(int i=0; i < properties.Length; i++)
            {
                EditorGUILayout.PropertyField(properties[i]);
            }

            //apply any changes to the serialized object
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
