/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;

/// <summary>A class which converts numbers to strings. Determines the correct divider (, or .) based on the device's locale.</summary>
public class NumberFormatter
{
	/// <summary>One million as a floating point number.</summary>
	private const float ONE_MILLION = 1000000; //10^6
	/// <summary>One billion as a floating point number.</summary>
	private const float ONE_BILLION = 1000000000; //10^9
	/// <summary>One trillion as a floating point number.</summary>
	private const float ONE_TRILLION = 1000000000000; //10^12
	/// <summary>One quadrillion as a floating point number.</summary>
	private const float ONE_QUADRILLION = 1000000000000000; //10^15

	/*
	Quintillion			10^18
	Sextillion			10^21
	Septillion			10^24
	Octillion			10^27
	Nonillion			10^30
	Decillion			10^33
	Undecillion			10^36
	Duodecillion		10^39
	Tredecillion		10^42
	Quatttuor-decillion	10^45
	Quindecillion		10^48
	Sexdecillion		10^51
	Septen-decillion	10^54
	Octodecillion		10^57
	Novemdecillion		10^60
	Vigintillion		10^63
	Centillion			10^303
	*/

	/// <summary>Converts a number to a string.</summary>
	/// <returns>A string representation of the number.</returns>
	/// <param name="number">The number to convert.</param>
	/// <param name="showDecimalPlaces">If two decimal places should be shown.</param>
	/// <param name="showDollarSign">If the dollar sign should be shown.</param>
	public static string ToString(float number, bool showDecimalPlaces = false, bool showDollarSign = true)
	{
		if(showDollarSign) { return string.Format("${0}", ToString(number, showDecimalPlaces)); }
		else { return ToString(number, showDecimalPlaces); }
	}

	/// <summary>Converts a number to a string.</summary>
	/// <returns>A string representation of the number.</returns>
	/// <param name="number">The number to convert.</param>
	/// <param name="showDecimalPlaces">If two decimal places should be shown.</param>
	private static string ToString(float number, bool showDecimalPlaces = false)
	{
		float numberToDisplay = number;
		string largeNumberText = "";
		//firstly determine if the number is a large number
		if(number >= ONE_QUADRILLION)
		{
			numberToDisplay = number / ONE_QUADRILLION; largeNumberText = LocalizationManager.instance.StringForKey(LocalizationManagerKeys.Quadrillion);
		}
		else if(number >= ONE_TRILLION)
		{
			numberToDisplay = number / ONE_TRILLION; largeNumberText = LocalizationManager.instance.StringForKey(LocalizationManagerKeys.Trillion);
		}
		else if(number >= ONE_BILLION)
		{
			numberToDisplay = number / ONE_BILLION; largeNumberText = LocalizationManager.instance.StringForKey(LocalizationManagerKeys.Billion);
		}
		//format the string depending on if the number to display is whole or not
		bool isWholeNumber = (numberToDisplay == Mathf.Floor(numberToDisplay));
		if(showDecimalPlaces && !isWholeNumber) { return string.Format("{0:n2} {1}", numberToDisplay, largeNumberText); } //if it is a whole number, ignore decimal places i.e. $6, not $6.00
		else { return string.Format("{0:n0} {1}", numberToDisplay, largeNumberText); }
	}
}
