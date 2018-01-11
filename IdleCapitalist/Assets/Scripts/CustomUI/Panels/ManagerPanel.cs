/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>A panel which displays information about a manager, and the possibility to buy that manager, in ManagersPopup.</summary>
public class ManagerPanel : MonoBehaviour
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

	/// <summary>Initializes the panel for a given manager.</summary>
	/// <param name="managerIndex">The manager (as an index).</param>
	public void Initialize(int managerIndex)
	{
		Assert.IsFalse(PlayerManager.instance.HasBoughtManager(managerIndex));

		//get a reference to the upgrade's data
		ManagerData data = GameData.instance.GetDataForManager(managerIndex);
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
				PlayerManager.instance.BoughtManager(managerIndex);
				PlayerManager.instance.DecrementCashBy(data.cost);
				int businessIndex = (int)data.business;
				if(data.type == ManagerData.ManagerType.Standard) //manager starts running the business
				{
					Assert.IsFalse(PlayerManager.instance.GetBusiness(businessIndex).hasManager);
					PlayerManager.instance.GetBusiness(businessIndex).AssignManager(Business.ManagerType.RunsBusiness);
				}
				else if(data.type == ManagerData.ManagerType.Efficient) //manager reduces costs, shows cash per second
				{
					PlayerManager.instance.GetBusiness(businessIndex).AssignManager(Business.ManagerType.ReducesCost);
					PlayerManager.instance.GetBusiness(businessIndex).SetCostReductionMultiplier(data.costReductionMultiplier);
					if(data.showCashPerSecond) { PlayerManager.instance.GetBusiness(businessIndex).SetShouldShowCashPerSecond(true); }
				}
				//finally destroy the panel
				Destroy(gameObject);
			});
		}
	}
}
