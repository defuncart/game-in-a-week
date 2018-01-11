/*
 *	Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;

/// <summary>Once attached to an animating object, this component can destroy the object once an animation is completed.</summary>
public class SelfDestroyingAnimationObject : MonoBehaviour
{
	/// <summary>Callback from Animator once the animation is completed.</summary>
	public void AnimationCompleted()
	{
		Destroy(gameObject);
	}
}
