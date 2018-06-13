/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>An abstract board component which features funcionality common to BoardCell and BoardPiece, 
/// both of which extend from BoardComponent.</summary>
public abstract class BoardComponent : MonoBehaviour
{
	/// <summary>The x-value.</summary>
	public int x { get; private set; }
	/// <summary>The y-value.</summary>
	public int y { get; private set; }

	/// <summary>Sets the x and y values.</summary>
	/// <param name="x">The x value.</param>
	/// <param name="y">The y value.</param>
	/// <param name="automaticallyUpdateTransform">Whether the transform's position should be updated. Defaults to false.</param>
	public virtual void SetXY(int x, int y, bool automaticallyUpdateTransform = false)
	{
        Assert.IsTrue(x >= 0 && x <= GameBoard.COLUMNS, string.Format("{0}: {1} is an invalid x-value", name, x));
        Assert.IsTrue(y >= 0 && y <= GameBoard.ROWS, string.Format("{0}: {1} is an invalid y-value", name, y));

		this.x = x; this.y = y;
		if(automaticallyUpdateTransform) { transform.localPosition = new Vector3(x, y, 0); }
	}
}

