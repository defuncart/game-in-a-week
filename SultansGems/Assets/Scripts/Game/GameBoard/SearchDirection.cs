/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

//basically vector 1

public struct SearchDirection
{
	public int x { get; private set; }
	public int y { get; private set; }

	public static SearchDirection left
	{
		get { return new SearchDirection { x = -1, y = 0 }; }
	}

	public static SearchDirection right
	{
		get { return new SearchDirection { x = 1, y = 0 }; }
	}

	public static SearchDirection up
	{
		get { return new SearchDirection { x = 0, y = 1 }; }
	}

	public static SearchDirection down
	{
		get { return new SearchDirection { x = 0, y = -1 }; }
	}

//	private SearchDirection(int x, int y)
//	{
//		this.x = x; this.y = y;
//	}

	public bool isNormal
	{
		get { return Mathf.Abs(x) == 1 || Mathf.Abs(y) == 1; }
	}
}

