/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.CustomProperties;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArt.CustomProperties namespace.</summary>
namespace DeFuncArtEditor
{
    /// <summary>A custom property drawer for the ReadOnly attribute.</summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        /// <summary>Callback to make a custom GUI for the property.</summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI..</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for..</param>
        /// <param name="label">The label of this property..</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        /// <summary>Callback to specify how tall the GUI for this field in pixels is. Default is 1 line high.</summary>
        /// <returns>The height in pixels.</returns>
        /// <param name="property">The SerializedProperty to make the custom GUI for..</param>
        /// <param name="label">The label of this property..</param>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
            //return base.GetPropertyHeight(property, label);
        }
    }
}
#endif