/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using System;
using System.Collections;
using UnityEngine;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.ExtensionMethods
{
	/// <summary>A collection of mono behaviour extention methods.</summary>
	public static class DAMonoBehaviourExtensions
	{
		/// <summary>Invokes an action after a given time.</summary>
		/// <param name="action">The action to invoke.</param>
		/// <param name="time">The time in seconds.</param>
		/// <param name="useCachedWaits">Whether cached wait values should be used. Defaults to true.</param>
		public static Coroutine Invoke(this MonoBehaviour monoBehaviour, Action action, float time, bool useCachedWaits = true)
		{
			return monoBehaviour.StartCoroutine(InvokeImplementation(action, time, useCachedWaits));
		}

		/// <summary>Coroutine which waits time seconds and then invokes the given action.</summary>
		private static IEnumerator InvokeImplementation(Action action, float time, bool useCachedWaits)
		{
			//wait for time seconds then invoke the action. if useCachedYields is true, uses a cached WaitForSeconds, otherwise creates a new one
			yield return (useCachedWaits ? WaitFor.Seconds(time) : new WaitForSeconds(time));
			action();
		}

		/// <summary>Invokes an action after a given time, then repeatedly every repeateRate seconds.</summary>
		/// <param name="action">The action to invoke.</param>
		/// <param name="time">The time in seconds.</param>
		/// <param name="repeatRate">The repeat rate in seconds.</param>
		/// <param name="useCachedWaits">Whether cached wait values should be used. Defaults to true.</param>
		public static Coroutine InvokeRepeating(this MonoBehaviour monoBehaviour, Action action, float time, float repeatRate, bool useCachedWaits = true)
		{
			return monoBehaviour.StartCoroutine(InvokeRepeatingImplementation(action, time, repeatRate, useCachedWaits));
		}

		/// <summary>The coroutine implementation of InvokeRepeating.
		private static IEnumerator InvokeRepeatingImplementation(Action action, float time, float repeatRate, bool useCachedWaits)
		{
			//wait for a given time then indefiently loop - if useCachedYields is true, uses a cached WaitForSeconds, otherwise creates a new one
			yield return (useCachedWaits ? WaitFor.Seconds(time) : new WaitForSeconds(time));
			while(true)
			{
				//invokes the action then waits repeatRate seconds - if useCachedYields is true, uses a cached WaitForSeconds, otherwise creates a new one
				action();
				yield return (useCachedWaits ? WaitFor.Seconds(repeatRate) : new WaitForSeconds(repeatRate));
			}
		}
	}
}
