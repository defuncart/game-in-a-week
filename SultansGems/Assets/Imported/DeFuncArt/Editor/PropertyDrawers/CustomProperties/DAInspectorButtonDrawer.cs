/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.CustomProperties;
using System.Reflection;
using UnityEngine;
using UnityEditor;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
    /// <summary>A custom property drawer for the DAInspectorButton.</summary>
    [CustomPropertyDrawer(typeof(DAInspectorButton))]
    public class DAInspectorButtonDrawer : PropertyDrawer
    {
        /// <summary>Callback to make a custom GUI for the property.</summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //if the button is pressed
            if(GUI.Button(position, property.FindPropertyRelative("buttonText").stringValue))
            {
                //get a reference to the target object
                Object target = property.serializedObject.targetObject;
                //and find methodinfo for the button's method
                MethodInfo mInfo = target.GetType().GetMethod(property.FindPropertyRelative("methodName").stringValue);
                //if the method exists, invoke the method, otherwise log an error
                if(mInfo != null)
                {
                    mInfo.Invoke(target, null);
                }
                else
                {
                    Debug.LogErrorFormat("Method <color=red>{0}</color> was not found in class <color=green>{1}</color> on object <color=blue>{2}</color>. Ensure that the desired method is correctly spelled and declared public.",
                                         property.FindPropertyRelative("methodName").stringValue, "", target.name);
                }
            }
        }
    }
}
#endif