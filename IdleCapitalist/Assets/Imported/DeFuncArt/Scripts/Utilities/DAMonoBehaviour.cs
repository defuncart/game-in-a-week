//using DeFuncArt.Utilities;
using System;
using System.Collections;
using UnityEngine;

/// <summary>Included in the DeFuncArt.ExtensionMethods namespace.</summary>
namespace DeFuncArt.Utilities
{
	public class DAMonoBehaviour : MonoBehaviour
	{
		public new void Invoke(string methodName, float time)
		{
			throw new Exception();
		}

		public new void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			throw new Exception();
		}

		public new bool IsInvoking()
		{
			throw new Exception();
		}

		public new void CancelInvoke()
		{
			throw new Exception();
		}

		/// <summary>Invokes an action after a given time.</summary>
		/// <param name="action">The action to invoke.</param>
		/// <param name="time">The time in seconds.</param>
		/// <param name="useCachedWaits">Whether cached wait values should be used. Defaults to true.</param>
		public Coroutine Invoke(Action action, float time, bool useCachedWaits = true)
		{
			return StartCoroutine(InvokeImplementation(action, time, useCachedWaits));
		}

		/// <summary>Coroutine which waits time seconds and then invokes the given action.</summary>
		private IEnumerator InvokeImplementation(Action action, float time, bool useCachedWaits)
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
		public Coroutine InvokeRepeating(Action action, float time, float repeatRate, bool useCachedWaits = true)
		{
			return StartCoroutine(InvokeRepeatingImplementation(action, time, repeatRate, useCachedWaits));
		}

		/// <summary>The coroutine implementation of InvokeRepeating.
		private IEnumerator InvokeRepeatingImplementation(Action action, float time, float repeatRate, bool useCachedWaits)
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

		public void D()
		{
			Invoke (() => {
				Debug.Log("okay");
			}, 2f);

			CancelInvoke();
		}
	}
}
