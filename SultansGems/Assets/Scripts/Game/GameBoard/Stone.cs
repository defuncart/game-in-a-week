/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A moveable and matchable board piece.</summary>
public class Stone : BoardPiece
{
	/// <summary>A backing variable for type.</summary>
    [Tooltip("The stone's type.")]
    [Range(0, Constants.NUMBER_STONE_TYPES - 1)] [SerializeField] protected int _type = 0;
	/// <summary>The stone's type.</summary>
	public int type { get { return _type; } }
}
