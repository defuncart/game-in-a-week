/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 */
using DeFuncArt.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>A simple Left, Right Swipe recognizer.</summary>
public class SwipeRecognizer : MonoBehaviour
{
	/// <summary>An event when the user swipes left.</summary>
	public event EventHandler OnSwipeLeft;
	/// <summary>An event when the user swipes right.</summary>
	public event EventHandler OnSwipeRight;
	/// <summary>Whether the recognizer is active or not.</summary>
	public bool active = true;

	private const float maxTime = 1.0f; //max time between touch down and touch up
	private const float maxAngle = 30; //minimum angle range
	private const float minDistance = 50; //minimum number of pixels
	private const float minVelocity = 500; //minimum velocity of the swipe
	private Vector2 startPosition; //touch down start position
	private float swipeStartTime; //touch down start time

	/// <summary> On every frame update, check if there is a swipe.</summary>
	private void Update()
	{
		if(!active) { return; }

		if(Input.GetMouseButtonDown(0)) //on touch down, save position and starttime
		{
			startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			swipeStartTime = Time.time;
		}

		if(Input.GetMouseButtonUp(0)) //on touch up, test if there was a swipe
		{
			float deltaTime = Time.time - swipeStartTime;
			Vector2 endPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 swipeVector = endPosition - startPosition;
			float velocity = swipeVector.magnitude/deltaTime;

			if(deltaTime < maxTime && velocity >= minVelocity && swipeVector.magnitude > minDistance)
			{
				swipeVector.Normalize();

				float angleOfSwipe = Vector2.Dot(swipeVector, Vector2.right);
				angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

				if(angleOfSwipe < maxAngle)
				{
					OnSwipeRight();
				}
				else if((180f - angleOfSwipe) < maxAngle)
				{
					OnSwipeLeft();
				}
			}
		}
	}
}
