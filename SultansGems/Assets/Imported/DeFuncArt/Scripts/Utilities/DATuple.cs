/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>A model used to represent tuples (which are not available in C# 4.5).</summary>
public class DATuple<T1, T2> 
{
    /// <summary>The tuple's first value.</summary>
	public T1 first;
    /// <summary>The tuple's second value.</summary>
    public T2 second;

    /// <summary>Initializes a new DATuple instance.</summary>
    /// <param name="first">The tuple's first value.</param>
    /// <param name="second">The tuple's second value.</param>
	public DATuple(T1 first, T2 second)
	{
		this.first = first;
		this.second = second;
	}
}
