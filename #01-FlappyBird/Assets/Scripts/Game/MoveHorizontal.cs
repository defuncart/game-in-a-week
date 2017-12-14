/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A script that moves a GameObject horizontally.</summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveHorizontal : MonoBehaviour
{
	/// <summary>A enum representing the possible directions.</summary>
	public enum Direction
	{
		/// <summary>Left direction.</summary>
		Left, 
		/// <summary>Right direction.</summary>
		Right
	}
	/// <summary>The move direction.</summary>
	[Tooltip("The move direction.")]
	[SerializeField] private Direction direction;
	/// <summary>The move speed.</summary>
	[Tooltip("The move speed.")]
	[SerializeField] private float speed = 1;

	public float speedMultiplier = 1f;
	Rigidbody2D rb;

	/// <summary>Callback when the object awakes.</summary>
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		speedMultiplier = GameScene.gameSpeed;
		UpdateVelocity();
//		GetComponent<Rigidbody2D>().velocity = new Vector2(direction == Direction.Right ? speed : -speed, 0);
//		currentSpeed = speed;
	}

	public void SetSpeedMultiplier(float speedMultiplier)
	{
		Assert.IsTrue(speedMultiplier > 0 && speedMultiplier < 10);

		this.speedMultiplier = speedMultiplier; UpdateVelocity();
	}

	private void UpdateVelocity()
	{
		rb.velocity = Vector2.zero;
		rb.velocity = (direction == Direction.Left ? Vector2.left : Vector2.right) * speed * speedMultiplier;
	}
}
