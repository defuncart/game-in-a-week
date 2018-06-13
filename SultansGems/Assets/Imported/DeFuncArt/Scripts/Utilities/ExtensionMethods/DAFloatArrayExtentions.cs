/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Linq;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of float array extention methods.</summary>
	public static class DAFloatArrayExtensions
	{
		/// <summary>Changes every element in an array to a given value.</summary>
		public static float[] Normalize(this float[] array)
		{
			if(array == null){ return array; }
			float sum = array.Sum();
			for(int i=0; i < array.Length; i++)
			{
				array[i] /= sum;
			}
			return array;
		}
	}
}
