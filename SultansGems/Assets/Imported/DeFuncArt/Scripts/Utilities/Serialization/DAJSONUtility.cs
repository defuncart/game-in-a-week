/*
 *  Written by James Leahy. (c) 2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>Part of the DeFuncArt.Serialization namespace.</summary>
namespace DeFuncArt.Serialization
{
    /// <summary>Custom JSON utility methods.</summary>
    public class DAJSONUtility
    {
        /// <summary>Returns an array of objects of type T from a JSON string.</summary>
        /// <returns>An array of objects of type T from a JSON string.</returns>
        /// <param name="json">The JSON string.</param>
        /// <typeparam name="T">The generic type.</typeparam>
        public static T[] ArrayFromJSON<T>(string json)
        {
            string modifiedJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(modifiedJson);
            return wrapper.array;
        }

        /// <summary>A serializable Wrapper with a given type.</summary>
        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array = null;
        }

        /*
        public static Dictionary<string, T> DictionaryFromJSON<T>(string json)
        {
            string modifiedJson = "{ \"items\": " + json + "}";
            DictionaryArray<T> loadedData = JsonUtility.FromJson<DictionaryArray<T>>(modifiedJson);
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            for(int i=0; i < loadedData.items.Length; i++)
            {
                dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            return dictionary;
        }


        private static Dictionary<string, string> FromJsonStringStringDictionary(string json)
        {
            string modifiedJson = "{ \"items\": " + json + "}";
            DictionaryArray<string> loadedData = JsonUtility.FromJson<DictionaryArray<string>>(modifiedJson);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            for(int i=0; i < loadedData.items.Length; i++)
            {
                dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            return dictionary;
        }

        [System.Serializable]
        private class DictionaryArray<T>
        {
            public DictionaryElement<T>[] items = null;
        }

        [System.Serializable]
        private class DictionaryElement<T>
        {
            public string key = null;
            public T value = default(T);
        }
        */
    }
}