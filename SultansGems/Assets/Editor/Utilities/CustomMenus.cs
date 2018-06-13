/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>A class of custom menu items.</summary>
public class CustomMenus
{
	/// <summary>Opens the scene with index 0 (LoadingScene) using a menu item or SHIFT-ALT-0.</summary>
	[MenuItem("Tools/Open Scene/Loading #&0")]
	public static void LoadScene0()
	{
		EditorSceneManager.OpenScene("Assets/Imported/DeFuncArt/Scenes/LoadingScene.unity");
	}

	/// <summary>Opens the scene with index 1 (MenuScene) using a menu item or SHIFT-ALT-1.</summary>
	[MenuItem("Tools/Open Scene/Menu #&1")]
	public static void LoadScene1()
	{
		EditorSceneManager.OpenScene("Assets/Scenes/MenuScene.unity");
	}

	/// <summary>Opens the scene with index 2 (GameScene) using a menu item or SHIFT-ALT-2.</summary>
	[MenuItem("Tools/Open Scene/Game #&2")]
	public static void LoadScene2()
	{
		EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity");
	}
}
#endif
