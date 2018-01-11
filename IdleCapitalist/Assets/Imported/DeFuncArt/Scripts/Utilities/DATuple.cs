/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */

/// <summary>Part of the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>As System.Tuple is only available in .Net 4.0 and above, this is a very simple custom implementation.</summary>
	public class DATuple<T1, T2> 
	{
		/// <summary>The first value.</summary>
		public T1 first;
		/// <summary>The second value.</summary>
		public T2 second;

		/// <summary>Initializes a new instance of the <see cref="DATuple"/> class.</summary>
		/// <param name="first">The first value of the tuplet.</param>
		/// <param name="second">The second value of the tuplet.</param>
		public DATuple(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}
	}
}
