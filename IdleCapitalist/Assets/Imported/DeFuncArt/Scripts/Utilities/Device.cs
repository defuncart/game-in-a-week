/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using System;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>Included in the DeFuncArt.Utilities namespace.</summary>
namespace DeFuncArt.Utilities
{
	/// <summary>A static class containing helper functions to determine particulars of the Device.</summary>
	public static class Device
	{
		/// <summary>Determine if the device has internet connectivity.</summary>
		public static bool hasInternetConnection
		{
			get { return Application.internetReachability != NetworkReachability.NotReachable; }
		}

		/// <summary>Determine if the device has a wifi connectivity.</summary>
		public static bool hasWifiConnection
		{
			get { return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork; }
		}

		public static DateTime GetCurrentTime(bool verifyWithServer = false)
		{
//			Debug.Log ("Device time: " + DateTime.UtcNow);
//
//
//			Debug.Log ("GetNistTime: " + GetNistTime ());
//
//			DateTime d = GetNistTime ().ToUniversalTime ();
//			Debug.Log (d);

			if(verifyWithServer && hasInternetConnection)
			{
				return GetNistTime();
			}
			else
			{
				return DateTime.UtcNow;
			}
		}


		/// <summary>Get the current Nist time.</summary>
		private static DateTime GetNistTime()
		{
			/*using(WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())‌
		{
			return DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", 
				CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
		}*/

			Assert.IsTrue(hasInternetConnection);

			//var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
			HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.google.com");
			WebResponse response = myHttpWebRequest.GetResponse();
			return DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", 
//			return DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'UTC'", 
				CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal).ToUniversalTime();
		}
	}
}
