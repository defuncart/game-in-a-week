/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of string extention methods.</summary>
	public static class DAStringExtensions
	{
		/// <summary>Determines if the string is null or empty.</summary>
		/// <returns><c>true</c> if the string is null or empty, otherwise <c>false</c>.</returns>
		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}
	}
}
