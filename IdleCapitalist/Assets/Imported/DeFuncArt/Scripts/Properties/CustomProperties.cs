/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Included in the DeFuncArt.CustomProperties namespace.</summary>
namespace DeFuncArt.CustomProperties
{
	/// <summary>An attribute to hide a property declared in a base class in the inspector of derived classes.</summary>
	public sealed class HideInDerivedClassesAttribute : PropertyAttribute {}
}