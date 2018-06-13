/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.Serialization;
using System.Collections;
using UnityEngine;

/// <summary>A base scene class from which scenes can derived from.</summary>
public class BaseScene : MonoBehaviour
{
    #if UNITY_EDITOR
    /// <summary>Callback when the instance starts.</summary>
    private IEnumerator Start()
    {
        //wait until LevelsManager singleton is loaded
        while(LevelsManager.instance == null)
        {
            yield return null;
        }
        //if the game hasn't been previously launced, create and set initial data
        if (!PlayerPreferences.HasKey(PlayerPreferencesKeys.previouslyLaunched))
        {
            DataManager.Initialize();
        }
        //verify that data is okay
        DataManager.Verify();

        //call subclass's Start
        StartImplementation();
    }
    #else
    /// <summary>Callback when the instance starts.</summary>
    private void Start()
    {
        StartImplementation();
    }
    #endif

    /// <summary>A Start implementation for baseclasses.</summary>
    protected virtual void StartImplementation() {}
}
