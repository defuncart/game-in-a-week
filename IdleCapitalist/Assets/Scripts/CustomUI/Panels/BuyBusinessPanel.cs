/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.UI;

/// <summary>A panel which displays information about a business, and the possibility to buy that business.</summary>
public class BuyBusinessPanel : MonoBehaviour
{
	/// <summary>An event when the player presses the buy button.</summary>
	public EventHandler OnBuyBusinessButtonPressed;

	/// <summary>The panel's name text.</summary>
	[Tooltip("The panel's name text.")]
	[SerializeField] private Text nameText;
	/// <summary>The panel's cost text.</summary>
	[Tooltip("The panel's cost text.")]
	[SerializeField] private Text costText;
	/// <summary>The panel's buy button.</summary>
	[Tooltip("The panel's buy button.")]
	[SerializeField] private Button buyButton;

	/// <summary>Whether the panel (i.e. the buy button) is interactable. Automatically sets the button's color.</summary>
	public bool interactable
	{
		get { return buyButton.interactable; }
		set { buyButton.interactable = value; buyButton.image.color = (value ? GameColors.purple : GameColors.gray); }
	}

	/// <summary>Initializes the panel for a given business.</summary>
	/// <param name="name">The business' name.</param>
	/// <param name="cost">The business' cost.</param>
	public void Initialize(string name, float cost)
	{
		nameText.text = name;
		costText.text = NumberFormatter.ToString(cost, showDecimalPlaces: false);
		buyButton.onClick.AddListener(() => {
			OnBuyBusinessButtonPressed();
		});
	}

	/// <summary>Callback before the component (or gameobject) is destroyed.</summary>
	private void OnDestroy()
	{
		OnBuyBusinessButtonPressed = null; //remove all listeners
	}
}
