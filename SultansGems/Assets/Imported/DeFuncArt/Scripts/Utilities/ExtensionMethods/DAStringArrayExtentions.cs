/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of string array extention methods.</summary>
	public static class DAStringArrayExtensions
	{
		/// <summary>Determines if the string array is sorted alphabetically.</summary>
		public static bool IsSorted(this string[] arr)
		{
			for(int i=1; i < arr.Length; i++)
			{
				//if the previous element is bigger, return false
				if(arr[i - 1].CompareTo(arr[i]) > 0)  { return false; }
			}
			return true;
		}
	}
}
