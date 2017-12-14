/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Game;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/// <summary>A script controlling the player.</summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	/// <summary>An event which occurs when the player dies.</summary>
	public EventHandler OnPlayerDie;

	/// <summary>The tap impulse force.</summary>
	[Tooltip("The tap impulse force.")]
	[SerializeField] private float tapForce = 300f;
	/// <summary>An explosion prefab for when the player dies.</summary>
	[Tooltip("An explosion prefab for when the player dies.")]
	[SerializeField] private GameObject explosionPrefab;

	/// <summary>Whether the play can current play.</summary>
	private bool isPlayable = false;
	/// <summary>Whether user is currently tapping.</summary>
	private bool isTapping = false;
	/// <summary>The player's rigidbody.</summary>
	private Rigidbody2D rb;
	/// <summary>The player's start position.</summary>
	private Vector3 startPosition;
	/// <summary>The y-position on the last frame.</summary>
	private float previousY;
	/// <summary>A struct of the player's animator parameters.</summary>
	private struct AnimatorParameter
	{
		/// <summary>plane is an int animator parameter denoting which plane should be displayed.</summary>
		public const string plane = "plane";
	}
	/// <summary>The player's animator.</summary>
	private Animator animator;

	/// <summary>Callback when the object awakes.</summary>
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		startPosition = transform.position;
		previousY = startPosition.y;

		#if UNITY_EDITOR
		KeyboardController.instance.OnSpacebar += EDITORONLY_TapReceived;
		#endif
	}

	/// <summary>Callback when the object is about to be destroyed.</summary>
	private void OnDestroy()
	{
		#if UNITY_EDITOR
		KeyboardController.instance.OnSpacebar -= EDITORONLY_TapReceived;
		#endif
	}

	#if UNITY_EDITOR
	/// <summary>An editor only method which is used to triggered a received tap.</summary>
	private void EDITORONLY_TapReceived()
	{
		isTapping = true;
	}
	#endif

	/// <summary>Callback when the object updates.</summary>
	private void Update()
	{
		if(isPlayable && !isTapping)
		{
			isTapping = Input.GetButtonDown("Fire1"); //read the tap input in Update so button presses aren't missed.
		}
	}

	/// <summary>Callback when the object updates (physics).</summary>
	private void FixedUpdate()
	{
		if(isTapping)
		{
			//reset the velocity and add an upwards force
			rb.velocity = Vector2.zero; rb.AddForce(Vector2.up * tapForce);
			isTapping = false;
			AudioManager.instance.PlaySFX(AudioManagerKeys.tap);
		}

		if(isPlayable) //called after physics calculations
		{
			//set the plane to tilt slightly upwards/downwards matching upwards/downwards movement
			transform.SetEulerZ( transform.position.y > previousY ? 5 : -5 );
			previousY = transform.position.y;
		}
	}

	/// <summary>Sets the player to be playable or not.</summary>
	public void SetPlayable(bool playable)
	{
		isPlayable = playable;
		rb.simulated = playable;
	}

	/// <summary>Resets the player.</summary>
	public void Reset()
	{
		transform.position = startPosition; transform.SetEulerZ(0);
		rb.velocity = Vector2.zero;
		animator.SetInteger(AnimatorParameter.plane, PlayerManager.instance.currentPlane);
		//if the explosion animation is still running, destroy it
		if(transform.childCount > 0)
		{
			Transform explosion = transform.GetChild(0); Destroy(explosion.gameObject);
		}
	}
		
	/// <summary>Callback for the OnCollisionEnter2D event.</summary>
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == Tags.Ground || collision.gameObject.tag == Tags.Obstacle)
		{
			SetPlayable(false);
			Instantiate(explosionPrefab, this.transform);
			animator.SetInteger(AnimatorParameter.plane, -1); //fades the plane out
			OnPlayerDie();
		}
	}
}
