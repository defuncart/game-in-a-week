/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>A component attached to an animating object that can destroy itself once the animation is completed.</summary>
public class SelfDestroyingAnimationObject : MonoBehaviour
{
	/// <summary>Callback from Animator once the animation is completed.</summary>
	public void AnimationCompleted()
	{
		Destroy(gameObject);
	}
}
