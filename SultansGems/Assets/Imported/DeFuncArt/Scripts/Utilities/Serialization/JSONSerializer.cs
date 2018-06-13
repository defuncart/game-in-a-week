/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Part of the DeFuncArt.Serialization namespace.</summary>
namespace DeFuncArt.Serialization
{
	/// <summary>A JSON Serializer which serializes and deserializes objects into JSON format.</summary>
	public class JSONSerializer
	{
		#region Array/Dictionary Serialization

		/// <summary>Serializes an object of type T to JSON.</summary>
		/// <returns>A JSON representation of the object T.</returns>
		/// <param name="data">The object T.</param>
		public static string ToJson<T>(T data) where T : class
		{
			if(typeof(T) == typeof(string[])) { return ToJsonStringArray(data as string[]); }
			else if(typeof(T) == typeof(int[])) { return ToJsonIntArray(data as int[]); }
			else if(typeof(T) == typeof(float[])) { return ToJsonFloatArray(data as float[]); }
			else if(typeof(T) == typeof(Dictionary<string, string>))
			{
				return ToJson(data as Dictionary<string, string>);
			}
			else if(typeof(T) == typeof(Dictionary<string, int>))
			{
				return ToJsonStringIntDictionary(data as Dictionary<string, int>);
			}
			else if(typeof(T) == typeof(Dictionary<string, float>))
			{
				return ToJsonStringFloatDictionary(data as Dictionary<string, float>);
			}
			else if(typeof(T) == typeof(Dictionary<string, string[]>))
			{
				return ToJsonStringStringArrayDictionary(data as Dictionary<string, string[]>);
			}
			else if(typeof(T) == typeof(Dictionary<string, int[][]>))
			{
				return ToJsonStringInt2DArrayDictionary(data as Dictionary<string, int[][]>);
			}
			else
			{
				Debug.LogError(string.Format("Type {0} is not supported.", typeof(T).Name)); return null;
			}
		}

		/// <summary>Deserializes an object of type T from JSON.</summary>
		/// <returns>The object T.</returns>
		/// <param name="json">A JSON representation of the object T.</param>
		public static T FromJson<T>(string json) where T : class
		{
			if(typeof(T) == typeof(string[])) { return FromJsonStringArray(json) as T; }
			if(typeof(T) == typeof(int[])) { return FromJsonIntArray(json) as T; }
			if(typeof(T) == typeof(float[])) { return FromJsonFloatArray(json) as T; }
			else if(typeof(T) == typeof(Dictionary<string, string>))
			{
				return FromJsonStringStringDictionary(json) as T;
			}
			else if(typeof(T) == typeof(Dictionary<string, int>))
			{
				return FromJsonStringIntDictionary(json) as T;
			}
			else if(typeof(T) == typeof(Dictionary<string, float>))
			{
				return FromJsonStringFloatDictionary(json) as T;
			}
			else if(typeof(T) == typeof(Dictionary<string, string[]>))
			{
				return FromJsonStringStringArrayDictionary(json) as T;
			}
			else if(typeof(T) == typeof(Dictionary<string, int[][]>))
			{
				return FromJsonStringInt2DArrayDictionary(json) as T;
			}
			else
			{
				Debug.LogError(string.Format("Type {0} is not supported.", typeof(T).Name)); return null;
			}
		}

		#endregion

		/*
		 * 		Presently array types for top-level JSON deserialization isn't natively supported. 
		 * 		However, by wrapping the array in an object, it can be deserialized.
		 * 		Each supported type implements ToJson and FromJson.
		 */

		#region Arrays

										/* 			string[]			*/

		[System.Serializable]
		private class TopLevelStringArray
		{
			public string[] array;
		}

		private static string ToJsonStringArray(string[] array)
		{
			TopLevelStringArray topLevelArray = new TopLevelStringArray() { array = array };
			return JsonUtility.ToJson(topLevelArray);
		}

		private static string[] FromJsonStringArray(string json)
		{
			TopLevelStringArray topLevelArray = JsonUtility.FromJson<TopLevelStringArray>(json);
			return topLevelArray.array;
		}

										/*			int[]				*/

		[System.Serializable]
		private class TopLevelIntArray
		{
			public int[] array;
		}

		private static string ToJsonIntArray(int[] array)
		{
			TopLevelIntArray topLevelArray = new TopLevelIntArray() { array = array };
			return JsonUtility.ToJson(topLevelArray);
		}

		private static int[] FromJsonIntArray(string json)
		{
			TopLevelIntArray topLevel = JsonUtility.FromJson<TopLevelIntArray>(json);
			return topLevel.array;
		}

										/*			float[]				*/

		[System.Serializable]
		private class TopLevelFloatArray
		{
			public float[] array;
		}

		private static string ToJsonFloatArray(float[] array)
		{
			TopLevelFloatArray topLevelArray = new TopLevelFloatArray() { array = array };
			return JsonUtility.ToJson(topLevelArray);
		}

		private static float[] FromJsonFloatArray(string json)
		{
			TopLevelFloatArray topLevel = JsonUtility.FromJson<TopLevelFloatArray>(json);
			return topLevel.array;
		}

		#endregion


		/* 
		 * 		Presently dictionary deserialization isn't natively support. However, by defining custom 
		 * 		objects (StringString, StringFloat, StringStringArray etc.), and objects contain arrays of 
		 * 		these objects, such an array can be natively deserialized as a custom object and then 
		 * 		converted into the required dictionary.
		 *  	Each supported type implements ToJson and FromJson.
		 */

		#region Dictionaries

									/* 			Dictionary<string, string>			*/

		[System.Serializable]
		private class StringStringDictionaryArray
		{
			public StringStringDictionary[] items;
		}

		[System.Serializable]
		private class StringStringDictionary
		{
			public string key;
			public string value;
		}

		private static string ToJsonStringStringDictionary(Dictionary<string, string> dictionary)
		{
			List<StringStringDictionary> dictionaryItemsList = new List<StringStringDictionary>();
			foreach(KeyValuePair<string, string> kvp in dictionary)
			{
				dictionaryItemsList.Add( new StringStringDictionary(){ key = kvp.Key, value = kvp.Value } );
			}

			StringStringDictionaryArray dictionaryArray = new StringStringDictionaryArray(){ items = dictionaryItemsList.ToArray() };
			return JsonUtility.ToJson(dictionaryArray);
		}

		private static string ToJson(Dictionary<string, string> dictionary)
		{
			List<StringStringDictionary> dictionaryItemsList = new List<StringStringDictionary>();
			foreach(KeyValuePair<string, string> kvp in dictionary)
			{
				dictionaryItemsList.Add( new StringStringDictionary(){ key = kvp.Key, value = kvp.Value } );
			}

			StringStringDictionaryArray dictionaryArray = new StringStringDictionaryArray(){ items = dictionaryItemsList.ToArray() };
			return JsonUtility.ToJson(dictionaryArray);
		}

		private static Dictionary<string, string> FromJsonStringStringDictionary(string json)
		{
			StringStringDictionaryArray loadedData = JsonUtility.FromJson<StringStringDictionaryArray>(json);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for(int i=0; i < loadedData.items.Length; i++)
			{
				dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
			}
			return dictionary;
		}

									/* 			Dictionary<string, float>			*/

		[System.Serializable]
		private class StringIntDictionaryArray
		{
			public StringIntDictionary[] items;
		}

		[System.Serializable]
		private class StringIntDictionary
		{
			public string key;
			public int value;
		}

		private static string ToJsonStringIntDictionary(Dictionary<string, int> dictionary)
		{
			List<StringIntDictionary> dictionaryItemsList = new List<StringIntDictionary>();
			foreach(KeyValuePair<string, int> kvp in dictionary)
			{
				dictionaryItemsList.Add( new StringIntDictionary(){ key = kvp.Key, value = kvp.Value } );
			}

			StringIntDictionaryArray dictionaryArray = new StringIntDictionaryArray(){ items = dictionaryItemsList.ToArray() };
			return JsonUtility.ToJson(dictionaryArray);
		}

		private static Dictionary<string, int> FromJsonStringIntDictionary(string json)
		{
			StringIntDictionaryArray loadedData = JsonUtility.FromJson<StringIntDictionaryArray>(json);
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for(int i=0; i < loadedData.items.Length; i++)
			{
				dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
			}
			return dictionary;
		}

									/* 			Dictionary<string, float>			*/

		[System.Serializable]
		private class StringFloatDictionaryArray
		{
			public StringFloatDictionary[] items;
		}

		[System.Serializable]
		private class StringFloatDictionary
		{
			public string key;
			public float value;
		}

		private static string ToJsonStringFloatDictionary(Dictionary<string, float> dictionary)
		{
			List<StringFloatDictionary> dictionaryItemsList = new List<StringFloatDictionary>();
			foreach(KeyValuePair<string, float> kvp in dictionary)
			{
				dictionaryItemsList.Add( new StringFloatDictionary(){ key = kvp.Key, value = kvp.Value } );
			}

			StringFloatDictionaryArray dictionaryArray = new StringFloatDictionaryArray(){ items = dictionaryItemsList.ToArray() };
			return JsonUtility.ToJson(dictionaryArray);
		}

		private static Dictionary<string, float> FromJsonStringFloatDictionary(string json)
		{
			StringFloatDictionaryArray loadedData = JsonUtility.FromJson<StringFloatDictionaryArray>(json);
			Dictionary<string, float> dictionary = new Dictionary<string, float>();
			for(int i=0; i < loadedData.items.Length; i++)
			{
				dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
			}
			return dictionary;
		}

									/* 			Dictionary<string, string[]>			*/ 

		[System.Serializable]
		private class StringStringArrayDictionaryArray
		{
			public StringStringArrayDictionary[] items;
		}

		[System.Serializable]
		private class StringStringArrayDictionary
		{
			public string key;
			public string[] value;
		}

		private static string ToJsonStringStringArrayDictionary(Dictionary<string, string[]> dictionary)
		{
			List<StringStringArrayDictionary> dictionaryItemsList = new List<StringStringArrayDictionary>();
			foreach(KeyValuePair<string, string[]> kvp in dictionary)
			{
				dictionaryItemsList.Add( new StringStringArrayDictionary(){ key = kvp.Key, value = kvp.Value } );
			}

			StringStringArrayDictionaryArray dictionaryArray = new StringStringArrayDictionaryArray(){ items = dictionaryItemsList.ToArray() };
			return JsonUtility.ToJson(dictionaryArray);
		}

		private static Dictionary<string, string[]> FromJsonStringStringArrayDictionary(string json)
		{
			StringStringArrayDictionaryArray loadedData = JsonUtility.FromJson<StringStringArrayDictionaryArray>(json);
			Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
			for(int i=0; i < loadedData.items.Length; i++)
			{
				dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
			}
			return dictionary;
		}

									/* 			Dictionary<string, int[][]>			*/ 

		[System.Serializable]
		private class StringInt2DArrayDictionaryArray
		{
			public StringInt2DArrayDictionary[] items;
		}

		[System.Serializable]
		private class StringInt2DArrayDictionary
		{
			public string key;
			public IntArray[] value;
		}

		private static string ToJsonStringInt2DArrayDictionary(Dictionary<string, int[][]> dictionary)
		{
			List<StringInt2DArrayDictionary> dictionaryItemsList = new List<StringInt2DArrayDictionary>();
			foreach(KeyValuePair<string, int[][]> kvp in dictionary)
			{
				List<IntArray> twoDArray = new List<IntArray>();
				foreach(int[] array in kvp.Value) { twoDArray.Add( new IntArray() { value = array } ); }

				dictionaryItemsList.Add( new StringInt2DArrayDictionary(){ key = kvp.Key, value = twoDArray.ToArray() } );
			}

			StringInt2DArrayDictionaryArray dictionaryArray = new StringInt2DArrayDictionaryArray(){ items = dictionaryItemsList.ToArray() };
			return JsonUtility.ToJson(dictionaryArray);
		}

		private static Dictionary<string, int[][]> FromJsonStringInt2DArrayDictionary(string json)
		{
			StringInt2DArrayDictionaryArray loadedData = JsonUtility.FromJson<StringInt2DArrayDictionaryArray>(json);
			Dictionary<string, int[][]> dictionary = new Dictionary<string, int[][]>();
			foreach(StringInt2DArrayDictionary dataDict in loadedData.items)
			{
				dictionary.Add(dataDict.key, FromIntArrayArrayTo2DIntArray(dataDict.value));
			}
			return dictionary;
		}

		#endregion

		#region Int[][]

		[System.Serializable]
		protected class IntArray
		{
			public int[] value;
		}

		protected static int[][] FromIntArrayArrayTo2DIntArray(IntArray[] intArrayArray)
		{
			List<int[]> twoDArray = new List<int[]>();
			foreach(IntArray array in intArrayArray) { twoDArray.Add(array.value); }
			return twoDArray.ToArray();
		}

		#endregion

	}
}
