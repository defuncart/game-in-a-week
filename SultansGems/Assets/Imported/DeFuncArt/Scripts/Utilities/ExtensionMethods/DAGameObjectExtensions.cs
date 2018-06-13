/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of GameObject extention methods.</summary>
	public static class DAGameObjectExtensions
	{
		/// <summary>Gets the first component of type T from the GameObject's children (ignoring root, the object itself).</summary>
		public static T GetComponentInChildrenNoRoot<T>(this GameObject gameObject) where T:Component
		{
			T[] components = gameObject.GetComponentsInChildren<T>();
			foreach(T component in components)
			{
				if(component.gameObject != gameObject )
				{
					return component;
				}
			}
			return null;
		}
	}
}
