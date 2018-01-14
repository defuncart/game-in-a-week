/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of debug string extention methods.</summary>
	public static class DADebugString
	{
		/// <summary>DEBUG ONLY. An override of ToString to return something meaningful.
		/// Extension methods cannot override existing methods, hence the need for a new function.
		/// Loop through the array and construct a string of its contents.</summary>
		/// <returns>A string representation of the array.</returns>
		public static string DebugString<T>(this T[] array) where T: class
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("[");
			for (int i=0; i < array.Length; i++) {
				if (i != 0) {
					sb.Append (", ");
				}
				sb.Append ("{");
				sb.Append (array [i].ToString ());
				sb.Append ("}");
			}
			sb.Append ("]");
			return sb.ToString (); //[{object1}, {object2}, ... ]
		}

		//specific implementations for primative types

		public static string DebugString(this bool[] array)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i=0; i < array.Length; i++)
			{
				if(i != 0) { sb.Append (", "); }
				sb.Append(array[i].ToString());
			}
			sb.Append("}");
			return sb.ToString();
		}

		public static string DebugString(this int[] array)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i=0; i < array.Length; i++)
			{
				if(i != 0) { sb.Append(", "); }
				sb.Append(array[i].ToString());
			}
			sb.Append("}");
			return sb.ToString();
		}

		public static string DebugString(this int[][] array)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i=0; i < array.Length; i++)
			{
				if(i != 0) { sb.Append(", "); }

				sb.Append("[");
				for(int j=0; j < array[i].Length; j++)
				{
					if(j != 0) { sb.Append(", "); }
					sb.Append(array[i][j].ToString());
				}
				sb.Append("]");
			}
			sb.Append("}");
			return sb.ToString();
		}

		public static string DebugString(this float[] array)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i=0; i < array.Length; i++)
			{
				if(i != 0) { sb.Append(", "); }
				sb.Append(array[i].ToString());
			}
			sb.Append("}");
			return sb.ToString();
		}

		public static string DebugString(this byte[] array)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i=0; i < array.Length; i++)
			{
				if(i != 0) { sb.Append(", "); }
				sb.Append(array[i]);
			}
			sb.Append("}");
			return sb.ToString();
		}

		public static string DebugString(this string[] array)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i=0; i < array.Length; i++)
			{
				if(i != 0) { sb.Append(", "); }
				sb.Append(array[i]);
			}
			sb.Append("}");
			return sb.ToString();
		}

		public static string DebugString<T>(this IEnumerable<T> collection)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			bool firstElement = true;

			foreach(T item in collection)
			{
				if(!firstElement){ sb.Append(", "); }
				sb.Append(item.ToString());
				if(firstElement) { firstElement = false; }
			}
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>DEBUG ONLY. An override of ToString to return something meaningful.
		/// Extension methods cannot override existing methods, hence the need for a new function.
		/// Loop through the dictionary and construct a string of its KeyValuePairs.</summary>
		/// <returns>A string representation of the array.</returns>
		public static string DebugString<T>(this Dictionary<string, T> dictionary) where T: class
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");

			bool firstElement = true;
			foreach(var kvp in dictionary)
			{
				if(!firstElement){ sb.Append(", "); }
				sb.Append (kvp.Key + " : " + kvp.Value.ToString());
				if(firstElement) { firstElement = false; }
			}
			sb.Append("}");
			return sb.ToString();
		}

		//specific implementations for dictionaries with primative types

		public static string DebugString(this Dictionary<string, float> dictionary)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");

			bool firstElement = true;
			foreach(var kvp in dictionary)
			{
				if(!firstElement){ sb.Append(", "); }
				sb.Append (kvp.Key + " : " + kvp.Value);
				if(firstElement) { firstElement = false; }
			}
			sb.Append("}");
			return sb.ToString();
		}
	}
}
