/*
 *  Written by James Leahy (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
    /// <summary>Utilities for the console.</summary>
    public static class DAConsole
    {
        #if UNITY_EDITOR
        /// <summary>Clears the console.</summary>
        public static void Clear()
        {
            System.Type logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
            System.Reflection.MethodInfo clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }
        #else
        /// <summary>Clears the console.</summary>
        public static void Clear() {}
        #endif
    }
}
