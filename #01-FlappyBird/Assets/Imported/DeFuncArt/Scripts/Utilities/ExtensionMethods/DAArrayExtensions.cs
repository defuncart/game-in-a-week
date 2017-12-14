/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Linq;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of array extention methods.</summary>
	public static class DAArrayExtensions
	{
		/// <summary>Determine whether an array contains a given value.</summary>
		public static bool Contains<T>(this T[] array, T value) where T : class
		{
			//return System.Array.IndexOf(array, value) >= 0; //if index is >= zero, then array contains object
			return array.IndexOf<T>(value) >= 0;
		}

		/// <summary>Returns the index of a given value in a given array.</summary>
		public static int IndexOf<T>(this T[] array, T value) where T : class
		{
			return System.Array.IndexOf(array, value);
		}

		/// <summary>Changes every element in an array to a given value.</summary>
		public static T[] Repeat<T>(this T[] array, T value)
		{
			if(array == null){ return array; }
			for(int i=0; i < array.Length; i++)
			{
				array[i] = value;
			}
			return array;
		}

		/// <summary>Returns a random element from the array.</summary>
		public static T RandomElement<T>(this T[] array) where T : class
		{
			int index  = Random.Range(0, array.Length);
			return array[index];
		}

		/// <summary>Returns a random object from the array depending on a given probability distribution.</summary>
		public static T RandomObjectWithProbabilityDistribution<T>(this T[] array, float[] probabilityDistribution) where T : class
		{
			Assert.IsTrue(array.Length == probabilityDistribution.Length);
			Assert.IsTrue(probabilityDistribution.Sum() == 1);

			float sum = 0;
			float randomValue = Random.value;
			for(int i=0; i < probabilityDistribution.Length; i++)
			{
				sum += probabilityDistribution[i];
				if(sum >= randomValue) { return array[i]; }
			}
			return array.Last();
		}
	}
}
