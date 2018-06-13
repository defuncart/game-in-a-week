/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>Custom Editor for the Level component.</summary>
[CustomEditor(typeof(Level), true)]
[CanEditMultipleObjects]
public class LevelEditor : Editor
{
    /// <summary>A reference to the target script;</summary>
    private Level targetScript;

    /// <summary>The rows property.</summary>
    private SerializedProperty rowsProperty;
    /// <summary>The stonesDistribution property.</summary>
    private SerializedProperty stonesDistributionProperty;
    /// <summary>The stonesPoints property.</summary>
    private SerializedProperty stonesPointsProperty;

    /// <summary>A static float array used for player input for the stonesDistributionProperty.</summary>
    private static float[] stonesDistribution;
    /// <summary>A static int array used for player input for the stonesPointsProperty.</summary>
    private static int[] stonesPoints;

	/// <summary>Callback when script was enabled.</summary>
	protected void OnEnable()
	{
        //get references to private variables
        targetScript = target as Level;
        rowsProperty = serializedObject.FindProperty("rows");
        stonesDistributionProperty = serializedObject.FindProperty("stonesDistribution");
        stonesPointsProperty = serializedObject.FindProperty("stonesPoints");

        //construct arrays
        stonesPoints = new int[Constants.NUMBER_STONE_TYPES];
        stonesDistribution = new float[Constants.NUMBER_STONE_TYPES];
        for (int i = 0; i < Constants.NUMBER_STONE_TYPES; i++)
        {
            stonesPoints[i] = stonesPointsProperty.GetArrayElementAtIndex(i).intValue;
            stonesDistribution[i] = stonesDistributionProperty.GetArrayElementAtIndex(i).floatValue;
        }
	}

	/// <summary>Callback to draw the inspector.</summary>
	public override void OnInspectorGUI()
	{
        //draw the default inspector and update the serialized object
        DrawDefaultInspector();
        serializedObject.Update();

        //draw a space and label
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Initial Board", EditorStyles.boldLabel);

        //get a drawable rect
        Rect fullRect = GUILayoutUtility.GetRect(Screen.width, Screen.width * (GameBoard.ROWS > GameBoard.COLUMNS ? GameBoard.COLUMNS / (GameBoard.ROWS * 1f) : GameBoard.ROWS / (GameBoard.COLUMNS * 1f)));
        //and deterine the rectangular size
        float size = fullRect.width / (Mathf.Max(GameBoard.COLUMNS, GameBoard.ROWS));

        //next get a rect at the current position with width and height equal to this
        Rect elementRect = new Rect(fullRect.x, fullRect.y, size, size);

        //draw serialized string array as 2d array
        for (int j = 0; j < GameBoard.ROWS; j++)
        {
            SerializedProperty row = rowsProperty.GetArrayElementAtIndex(j).FindPropertyRelative("row");

            for(int i=0; i < GameBoard.COLUMNS; i++)
            {
                EditorGUI.PropertyField(elementRect, row.GetArrayElementAtIndex(i), GUIContent.none);
                elementRect.x += size;
            }

            elementRect.x = fullRect.x;
            elementRect.y += size;
        }

        //next draw a stones table

        //the header labels
        string[] labelTexts = { "Stones", "Points", "Probability" };
        int numberOfColumns = 3;
        //determines a rows full width & height
        fullRect = GUILayoutUtility.GetRect(Screen.width, 20);

        //draw the table's header row
        EditorGUILayout.BeginHorizontal();
        Rect tempRect = new Rect(fullRect.x, fullRect.y, fullRect.width / 3f, fullRect.height);
        for(int j = 0; j < numberOfColumns; j++)
        {
            EditorGUI.LabelField(tempRect, labelTexts[j], EditorStyles.boldLabel);
            tempRect.x += tempRect.width;
        }
        EditorGUILayout.EndHorizontal();

        //draw the other rows
        for(int i=0; i < Constants.NUMBER_STONE_TYPES; i++)
        {
            EditorGUILayout.BeginHorizontal();

            tempRect = GUILayoutUtility.GetRect(fullRect.width / 3f, 20);
            EditorGUI.LabelField(tempRect, i.ToString());
            tempRect = GUILayoutUtility.GetRect(fullRect.width / 3f, 20);
            stonesPoints[i] = EditorGUI.IntField(tempRect, stonesPoints[i]);
            tempRect = GUILayoutUtility.GetRect(fullRect.width / 3f, 20);
            stonesDistribution[i] = EditorGUI.FloatField(tempRect, stonesDistribution[i]);

            EditorGUILayout.EndHorizontal();
        }

        //determine if any table elements should be updated
        bool updatedAnElement = false;
        for (int i = 0; i < Constants.NUMBER_STONE_TYPES; i++)
        {
            if(stonesPoints[i] != stonesPointsProperty.GetArrayElementAtIndex(i).intValue)
            {
                targetScript.stonesPoints[i] = stonesPoints[i];
                updatedAnElement = true;
            }

            if(stonesDistribution[i] != stonesDistributionProperty.GetArrayElementAtIndex(i).floatValue)
            {
                targetScript.stonesDistribution[i] = stonesDistribution[i];
                updatedAnElement = true;
            }
        }

        if(updatedAnElement) { targetScript.EDITOR_ManualOnValidate(); }

        //apply any changes to the serialized object
        serializedObject.ApplyModifiedProperties();
	}
}
#endif
