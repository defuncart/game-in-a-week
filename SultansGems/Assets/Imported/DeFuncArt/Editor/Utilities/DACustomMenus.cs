/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>A class of custom menu items.</summary>
public class DACustomMenus 
{
	/// <summary>Deletes the editor's game managers using a menu item or ALT-CMD-D.</summary>
	[MenuItem("Tools/Data/Delete #&d")]
	public static void DeleteData()
	{
		Debug.Log("Deleting Data..."); //Library/ApplicationSupport/DefuncArt/$PROJECT_NAME$
		DataManager.Delete();
		PlayerPreferencesKeys.Initialize();
	}

	/// <summary>Reloads the editor's game managers using a menu item or ALT-CMD-R.</summary>
	[MenuItem("Tools/Data/Reload #&r")]
	public static void ReloadData()
	{
		Debug.Log("Reloading Data...");
		DataManager.ReloadData();
	}

    /// <summary>Creates a Textfile.</summary>
    [MenuItem("Assets/Create/DeFunc Art/Textfile")]
    public static void CreateTextfile()
    {
        //get the path of the selected object (folder, file or nothing)
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //if no object is selected, set path as /Assets
        if(path == "") { path = Application.dataPath; }
        //if a file is selected (path has an extension), set path as the parent folder
        else if(Path.GetExtension(path) != "") { path = Path.GetDirectoryName(path); }

        //set the full path, create the file and refresh the asset database
        string fullPath = Path.Combine(Path.GetFullPath(path), "TextFile.txt");
        File.CreateText(fullPath);
        AssetDatabase.Refresh();
    }
}
#endif
