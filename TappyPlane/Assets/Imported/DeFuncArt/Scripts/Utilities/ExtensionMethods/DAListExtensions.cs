/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of list extention methods.</summary>
	public static class DAListExtensions
	{
		/// <summary>Shuffle a list.</summary>
		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while(n > 1)
			{
				n--;
				int k = Random.Range(0, n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}
