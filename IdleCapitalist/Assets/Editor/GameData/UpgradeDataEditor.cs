/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>Custom Editor for the UpgradeData asset.</summary>
[CustomEditor(typeof(UpgradeData))]
[CanEditMultipleObjects]
public class UpgradeDataEditor : Editor
{
	
	/// <summary>The image property.</summary>
	protected SerializedProperty imageProperty;
	/// <summary>The nameKey property.</summary>
	protected SerializedProperty nameKeyProperty;
	/// <summary>The type property.</summary>
	protected SerializedProperty typeProperty;
	/// <summary>The business property.</summary>
	protected SerializedProperty businessProperty;
	/// <summary>The profitMultiplier property.</summary>
	protected SerializedProperty profitMultiplierProperty;
	/// <summary>The cost property.</summary>
	protected SerializedProperty costProperty;

	/// <summary>Callback when script was enabled.</summary>
	private void OnEnable()
	{
		//get references to private variables
		imageProperty = serializedObject.FindProperty("image");
		nameKeyProperty = serializedObject.FindProperty("nameKey");
		typeProperty = serializedObject.FindProperty("type");
		businessProperty = serializedObject.FindProperty("business");
		profitMultiplierProperty = serializedObject.FindProperty("profitMultiplier");
		costProperty = serializedObject.FindProperty("cost");
	}

	/// <summary>Callback to draw the inspector.</summary>
	public override void OnInspectorGUI()
	{
		//update the serialized object
		serializedObject.Update();

		//draw properties
		EditorGUILayout.PropertyField(imageProperty);
		EditorGUILayout.PropertyField(nameKeyProperty);
		EditorGUILayout.PropertyField(typeProperty);
		if(typeProperty.enumValueIndex == 0) //upgrade is for a single business
		{
			EditorGUILayout.PropertyField(businessProperty);
		}
		EditorGUILayout.PropertyField(profitMultiplierProperty);
		EditorGUILayout.PropertyField(costProperty);

		//apply any changes to the serialized object
		serializedObject.ApplyModifiedProperties();
	}
}
#endif
