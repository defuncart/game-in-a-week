/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>A panel which displays information about an upgrade, and the possibility to buy that upgrade, in UpgradesPopup.</summary>
public class UpgradePanel : MonoBehaviour
{
	/// <summary>The panel's image.</summary>
	[Tooltip("The panel's image.")]
	[SerializeField] private Image image;
	/// <summary>The panel's name text.</summary>
	[Tooltip("The panel's name text.")]
	[SerializeField] private Text nameText;
	/// <summary>The panel's description text.</summary>
	[Tooltip("The panel's description text.")]
	[SerializeField] private Text descripionText;
	/// <summary>The panel's cost text.</summary>
	[Tooltip("The panel's cost text.")]
	[SerializeField] private Text costText;
	/// <summary>The panel's buy button.</summary>
	[Tooltip("The panel's buy button.")]
	[SerializeField] private BuyButton buyButton;

	/// <summary>Initializes the panel for a given upgrade.</summary>
	/// <param name="upgradeIndex">The upgrade (as an index).</param>
	public void Initialize(int upgradeIndex)
	{
		Assert.IsFalse(PlayerManager.instance.HasBoughtUpgrade(upgradeIndex));

		//get a reference to the upgrade's data
		UpgradeData data = GameData.instance.GetDataForUpgrade(upgradeIndex);
		//and upgrade the UI
		image.sprite = data.image;
		nameText.text = data.name;
		descripionText.text = data.description;
		costText.text = NumberFormatter.ToString(number: data.cost, showDecimalPlaces: false);
		bool canAffordManager = PlayerManager.instance.cash >= data.cost;
		buyButton.SetInteractableAndColor(canAffordManager);
		if(canAffordManager)
		{
			//if the player can afford the upgrade, then add a callback
			buyButton.onClick.AddListener(() => {
				//firstly disable the button
				buyButton.interactable = false;
				//next process the purchase
				PlayerManager.instance.BoughtUpgrade(upgradeIndex);
				PlayerManager.instance.DecrementCashBy(data.cost);
				int businessIndex = (int)data.business; PlayerManager.instance.GetBusiness(businessIndex).UpdateUpgradeProfitMultiplier(data.profitMultiplier);
				//finally destroy the panel
				Destroy(gameObject);
			});
		}
	}
}
