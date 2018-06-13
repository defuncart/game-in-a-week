/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections.Generic;
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
        /// <typeparam name="T">The type.</typeparam>
        public static string DebugString<T>(this T[] array)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("[");
			for(int i=0; i < array.Length; i++)
            {
				if(i != 0) { sb.Append (", "); }
				sb.Append (array [i].ToString ());
			}
			sb.Append ("]");
			return sb.ToString (); //[{object1}, {object2}, ... ]
		}

        /// <summary>DEBUG ONLY. An override of ToString to return something meaningful.
        /// Extension methods cannot override existing methods, hence the need for a new function.
        /// Loop through the jagged array and construct a string of its contents.</summary>
        /// <returns>A string representation of the jagged array.</returns>
        /// <typeparam name="T">The type.</typeparam>
        public static string DebugString<T>(this T[][] array)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for(int i = 0; i < array.Length; i++)
            {
                if (i != 0) { sb.Append(", "); }
                sb.Append(array[i].DebugString<T>());
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>DEBUG ONLY. An override of ToString to return something meaningful.
        /// Extension methods cannot override existing methods, hence the need for a new function.
        /// Loop through the multidimensional array and construct a string of its contents.</summary>
        /// <returns>A string representation of the multidimensional array.</returns>
        /// <typeparam name="T">The type.</typeparam>
        public static string DebugString<T>(this T[,] array)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (i != 0) { sb.Append(", "); }

                sb.Append("[");
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (j != 0) { sb.Append(", "); }
                    sb.Append(array[i, j].ToString());
                }
                sb.Append("]");
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>DEBUG ONLY. An override of ToString to return something meaningful.
        /// Extension methods cannot override existing methods, hence the need for a new function.
        /// Loop through the collection and construct a string of its contents.</summary>
        /// <returns>A string representation of the collection.</returns>
        /// <typeparam name="T">The type.</typeparam>
		public static string DebugString<T>(this IEnumerable<T> collection)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			bool firstElement = true;

			foreach(T item in collection)
			{
				if(!firstElement){ sb.Append(", "); }
				sb.Append(item.ToString());
				if(firstElement) { firstElement = false; }
			}
			sb.Append("]");
			return sb.ToString();
		}

        /// <summary>DEBUG ONLY. An override of ToString to return something meaningful.
        /// Extension methods cannot override existing methods, hence the need for a new function.
        /// Loop through the dictionary and construct a string of its contents.</summary>
        /// <returns>A string representation of the dictionary.</returns>
        /// <typeparam name="T">The type.</typeparam>
		public static string DebugString<T>(this Dictionary<string, T> dictionary)
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
	}
}
