/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.CustomProperties;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArt.CustomProperties namespace.</summary>
//namespace DeFuncArt
//{
	/// <summary>A custom property drawer for the HideInDerivedClasses attribute.</summary>
	[CustomPropertyDrawer(typeof(HideInDerivedClassesAttribute))]
	public class HideInDerivedClassesAttributeDrawer : PropertyDrawer
	{
		/// <summary>Determines if a given property should be shown.</summary>
		private bool ShouldShow(SerializedProperty property)
		{
			//get the class type of the object we are trying to render to the inspector
			Type type = property.serializedObject.targetObject.GetType();
			//get field info for the property
			FieldInfo fieldInfo = type.GetField(property.name, BindingFlags.NonPublic | BindingFlags.Instance);
			//if the field is part of the class we are trying to render, test if it is where the property was originally declared, if so, return true
			if(fieldInfo != null)
			{
				return type == fieldInfo.DeclaringType;
			}
			//otherwise return false
			return false;
		}

		/// <summary>Callback to make a custom GUI for the property.</summary>
		/// <param name="position">Rectangle on the screen to use for the property GUI..</param>
		/// <param name="property">The SerializedProperty to make the custom GUI for..</param>
		/// <param name="label">The label of this property..</param>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			if(ShouldShow(property))
			{
				EditorGUI.PropertyField(position, property);
			}
		}

		/// <summary>Callback to specify how tall the GUI for this field in pixels is. Default is 1 line high.</summary>
		/// <returns>The height in pixels.</returns>
		/// <param name="property">The SerializedProperty to make the custom GUI for..</param>
		/// <param name="label"></param>The label of this property..</param>
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if(ShouldShow(property))
			{ 
				return base.GetPropertyHeight(property, label);
			} 
			else
			{ 
				return 0;
			}
		}
	}
//}
#endif