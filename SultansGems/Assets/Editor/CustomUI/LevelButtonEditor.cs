/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
#if UNITY_EDITOR
using DeFuncArtEditor;
using UnityEditor;

/// <summary>Custom Editor which subclasses of DAButton can extend from.</summary>
[CustomEditor(typeof(LevelButton), true)]
[CanEditMultipleObjects]
public class LevelButtonEditor : DAButtonSubclassEditor
{
    /// <summary>An array of property names.</summary>
    protected override string[] propertyNames
    {
        get { return new string[] { "buttonText", "lockedImage", "stars", "starSprites" }; }
    }
}
#endif
