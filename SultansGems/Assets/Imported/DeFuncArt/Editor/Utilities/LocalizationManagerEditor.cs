/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArt.Utilities;
using UnityEditor;
using UnityEngine;

/// <summary>Included in the DeFuncArtEditor namespace.</summary>
namespace DeFuncArtEditor
{
    /// <summary>Custom Editor for the AudioDatabase component.</summary>
    [CustomEditor(typeof(LocalizationManager), true)]
    public class LocalizationManagerEditor : CustomKeysEditor
    {
        /// <summary>The asset's name.</summary>
        protected override string assetName
        {
            get { return "LocalizationManager"; }
        }
        /// <summary>The filepath to save $ASSETNAME$Keys.cs.</summary>
        protected override string filepath
        {
            get { return Application.dataPath + "/Scripts/Utilities/Localization/" + assetName + "Keys.cs"; }
        }
        /// <summary>An array of keys which should be saved in $ASSETNAME$Keys.cs.</summary>
        protected override string[] keys
        {
            get { return (target as LocalizationManager).keys; }
        }

        /// <summary>Callback to draw the inspector.</summary>
        public override void OnInspectorGUI()
        {
            //draw the default inspector
            DrawDefaultInspector();
            //draw an import button
            if (GUILayout.Button(new GUIContent(text: "Import", tooltip: "Imports an array of TextAssets from Assets/Localization")))
            {
                (target as LocalizationManager).EDITOR_Import();
            }
            //draw the export button, if applicable
            DrawExportButton();
        }
    }
}
#endif
