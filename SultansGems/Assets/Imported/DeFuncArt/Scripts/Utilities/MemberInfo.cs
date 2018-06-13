/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using System;
using System.Linq.Expressions;
using UnityEngine;

// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>Performs operations on files and folders in the editor.</summary>
	public class MemberInfo
	{
		/// <summary>Determines the name of a given member.</summary>
		/// <returns>The member name.</returns>
		/// <param name="memberExpression">Member expression.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
			return expressionBody.Member.Name;
		}
	}
}