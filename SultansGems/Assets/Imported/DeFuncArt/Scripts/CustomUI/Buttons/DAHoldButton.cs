/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 */
using DeFuncArt.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DAHoldButton : Button
{
	public event EventHandler OnButtonHeld;

	public const float HOLD_DURATION = 0.75f;

	private bool isHolding = false;
	private float downTime;

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);

		isHolding = true;
		downTime = Time.time;

		StartCoroutine(HoldConditionTest());
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);

		/*if(Time.time - downTime >= HOLD_DURATION)
		{
			if(OnButtonHeld != null) { OnButtonHeld(); }
		}*/

		isHolding = false;
	}

	private IEnumerator HoldConditionTest()
	{
		while(isHolding)
		{
			if(Time.time - downTime >= HOLD_DURATION)
			{
				if(OnButtonHeld != null) { OnButtonHeld(); }
			}
			yield return null;
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
	}
}
