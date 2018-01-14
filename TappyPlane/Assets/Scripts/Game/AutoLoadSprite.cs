/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>A script which auto loads on awake the sprite for a given level.</summary>
[RequireComponent(typeof(SpriteRenderer))]
public class AutoLoadSprite : MonoBehaviour
{
	/// <summary>An array of sprites for the various levels.</summary>
	[Tooltip("An array of sprites for the various levels")]
	[SerializeField] private Sprite[] sprites;
	/// <summary>The sprite renderer.</summary>
	private SpriteRenderer spriteRenderer;

	/// <summary>Callback when the object awakes.</summary>
	private void Awake()
	{
		Assert.IsTrue(sprites.Length == Constants.NUMBER_OF_LEVELS);

		spriteRenderer = this.GetComponent<SpriteRenderer>();
		Refresh();
	}

	/// <summary>Refreshes the sprite.</summary>
	public void Refresh()
	{
		spriteRenderer.sprite = sprites[PlayerManager.instance.currentLevel];
	}
}
