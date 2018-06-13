/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A board cell of the game board.</summary>
public class BoardCell : BoardComponent
{
	/// <summary>Sets the cell's x and y values.</summary>
	/// <param name="x">The x value.</param>
	/// <param name="y">The y value.</param>
	/// <param name="automaticallyUpdateTransform">Whether the transform's position should be updated. Defaults to true.</param>
	public override void SetXY(int x, int y, bool automaticallyUpdateTransform = true)
	{
		base.SetXY(x, y, automaticallyUpdateTransform);
		name = string.Format("BoardCell ({0}, {1})", x, y);
	}

	/// <summary>Compares if two GameObjects are adjoining.</summary>
	/// <param name="go1">The first GameObject.</param>
	/// <param name="go2">The second GameObject.</param>
	/// <returns>Boolean whether both GameObjects are adjoining.</returns>
	public static bool AreAdjoining(GameObject go1, GameObject go2)
	{
		BoardCell cell1 = go1.GetComponent<BoardCell>(); BoardCell cell2 = go2.GetComponent<BoardCell>();
		Assert.IsNotNull(cell1); Assert.IsNotNull(cell2);

		return (Mathf.Abs(cell1.x - cell2.x) == 1 && cell1.y == cell2.y) ||
			(Mathf.Abs(cell1.y - cell2.y) == 1 && cell1.x == cell2.x);
	}
}
