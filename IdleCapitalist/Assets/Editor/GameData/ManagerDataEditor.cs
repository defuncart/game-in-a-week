/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>Custom Editor for the ManagerData asset.</summary>
[CustomEditor(typeof(ManagerData))]
[CanEditMultipleObjects]
public class ManagerDataEditor : Editor
{
	/// <summary>The image property.</summary>
	protected SerializedProperty imageProperty;
	/// <summary>The nameKey property.</summary>
	protected SerializedProperty nameKeyProperty;
	/// <summary>The type property.</summary>
	protected SerializedProperty typeProperty;
	/// <summary>The business property.</summary>
	protected SerializedProperty businessProperty;
	/// <summary>The showCashPerSecond property.</summary>
	protected SerializedProperty showCashPerSecondProperty;
	/// <summary>The costReductionMultiplier property.</summary>
	protected SerializedProperty costReductionMultiplierProperty;
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
		showCashPerSecondProperty = serializedObject.FindProperty("showCashPerSecond");
		costReductionMultiplierProperty = serializedObject.FindProperty("costReductionMultiplier");
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
		EditorGUILayout.PropertyField(businessProperty);
		if(typeProperty.enumValueIndex == 1) //efficient manager
		{
			EditorGUILayout.PropertyField(showCashPerSecondProperty);
			EditorGUILayout.PropertyField(costReductionMultiplierProperty);
		}
		EditorGUILayout.PropertyField(costProperty);

		//apply any changes to the serialized object
		serializedObject.ApplyModifiedProperties();
	}
}
#endif
