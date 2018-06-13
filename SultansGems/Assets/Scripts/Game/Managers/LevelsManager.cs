/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.CustomProperties;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using DeFuncArtEditor;
using UnityEditor;
#endif

/// <summary>A mono singleton levels manager.</summary>
public class LevelsManager : MonoSingleton<LevelsManager>
{
    /// <summary>The levels.</summary>
    [Tooltip("The Levels.")]
    [SerializeField] private Level[] levels;

    /// <summary>The number of levels.</summary>
    public int NUMBER_LEVELS
    {
        get { return levels.Length; }
    }

    /// <summary>The current level.</summary>
    public Level currentLevel
    {
        get { return levels[SettingsManager.instance.level]; }
    }

    /// <summary>The number of points (for the current level) for a given stone.</summary>
    /// <returns>The number of points.</returns>
    /// <param name="stoneType">THe stone's type.</param>
    public int PointsForStone(int stoneType)
    {
        Assert.IsTrue(stoneType >= 0 && stoneType < Constants.NUMBER_STONE_TYPES);
        return currentLevel.stonesPoints[stoneType];
    }

    #if UNITY_EDITOR

    /// <summary>EDITOR ONLY: Callback when the script is update or a value is changed in the inspector.</summary>
    private void OnValidate()
    {
        Assert.IsTrue(levels.Length > 0, "Expected levels for Level Manager.");
        for(int i=0; i < levels.Length; i++)
        {
            Assert.IsNotNull(levels[i], string.Format("Levels Manager: Expected level at index {0} to be not null.", i));
        }
    }

    /// <summary>EDITOR ONLY: Imports level assets from Assets/Levels.</summary>
    public void EDITOR_Import()
    {
        //get levels
        levels = FolderManager.GetAssetsAtPath<Level>("Levels");
        //save assets
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    /// <summary>An inspector button which imports assets.</summary>
    [Tooltip("Import level assets from Assets/Levels.")]
    [SerializeField] DAInspectorButton importButton = new DAInspectorButton(methodName: "EDITOR_Import", buttonText: "Import");

    #endif
}
