/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
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
	}

	/// <summary>Reloads the editor's game managers using a menu item or ALT-CMD-C.</summary>
	[MenuItem("Tools/Data/Reload #&r")]
	public static void ReloadData()
	{
		Debug.Log("Reloading Data...");
		DataManager.ReloadData();
	}
}
#endif
