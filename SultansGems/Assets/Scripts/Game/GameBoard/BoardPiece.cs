/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A gameboard piece from which other game pieces can inherit from.</summary>
[RequireComponent(typeof(SpriteRenderer))]
public abstract class BoardPiece : BoardComponent
{
    /// <summary>Whether the board piece is a stone.</summary>
	public bool isStone
	{
		get { return GetComponent<Stone>() != null; }
	}
	/// <summary>DEBUG ONLY. Overriding ToString to return something meaningful.</summary>
	/// <returns>A String representation of the object.</returns>
	override public string ToString()
	{
		return string.Format("{0}: ({1}, {2})", gameObject.name, x, y);
	}
}
