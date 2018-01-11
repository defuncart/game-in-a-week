/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>A panel which displays information about a business, used on the main canvas.</summary>
public class BusinessPanel : MonoBehaviour
{
	/// <summary>The panel's image and produce button.</summary>
	[Tooltip("The panel's produce button.")]
	[SerializeField] private Button produceButton;
	/// <summary>The panel's level fill.</summary>
	[Tooltip("The panel's level fill.")]
	[SerializeField] private Image levelFill;
	/// <summary>The panel's level text.</summary>
	[Tooltip("The panel's level text.")]
	[SerializeField] private Text levelText;
	/// <summary>The panel's progress bar fill.</summary>
	[Tooltip("The panel's progress bar fill.")]
	[SerializeField] private Image progressBarFill;
	/// <summary>The panel's profit text.</summary>
	[Tooltip("The panel's profit.")]
	[SerializeField] private Text profitText;
	/// <summary>The panel's upgrade button.</summary>
	[Tooltip("The panel's upgrade button.")]
	[SerializeField] private BuyButton upgradeButton;
	/// <summary>The panel's upgrade cost text.</summary>
	[Tooltip("The panel's upgrade cost text.")]
	[SerializeField] private Text upgradeCostText;
	/// <summary>The panel's time text.</summary>
	[Tooltip("The panel's time text.")]
	[SerializeField] private Text timeText;
	/// <summary>A reference to the panel's business.</summary>
	private Business business;

	/// <summary>Initializes the panel for a given business.</summary>
	/// <param name="business">The business.</param>
	public void Initialize(Business business)
	{
		//save a referene to the business
		this.business = business;
		//intialize UI
		produceButton.image.sprite = business.image;
		//if the business has a manager, start automatic production
		if(business.hasManager)
		{
			business.ManagerStartsWorking();
			produceButton.interactable = false;
		}
		else //otherwise allow the user to click to produce a unit. Subscribe to the BusinessProducedAUnit and BusinessHiredAManager events
		{
			business.OnUnitProduced += BusinessProducedAUnit;
			business.OnManagerHired += BusinessHiredAManager;
			produceButton.onClick.AddListener(() => {
				produceButton.interactable = false;
				business.StartProducing();
			});
		}
		//add a listener to the upgradeButton to facilitate an upgrade
		upgradeButton.onClick.AddListener(() => {
			business.UpgradeToLastCalculatedLevel();
			Refresh();
		});
		//refresh dynamic UI components
		Refresh();
	}

	/// <summary>Callback before the component (or gameobject) is destroyed.</summary>
	private void OnDestroy()
	{
		if(!business.hasManager)
		{
			business.OnUnitProduced -= BusinessProducedAUnit;
			business.OnManagerHired -= BusinessHiredAManager;
		}
	}

	/// <summary>Refreshes the panel.</summary>
	public void Refresh()
	{
		//firstly update the levelFill and levelText
		levelFill.fillAmount = business.nextMilestonePercentage;
		levelText.text = business.level.ToString();

		//if the business can be updated to a higher level, determine the cost for the selected bulkLeveUpIndex and set the button's interactibility and text
		if(business.upgradeLevelExists)
		{
			float costToUpgradeForBulkLevelUpIndex = (GameManager.instance.bulkLevelUpIndex < Constants.NUMBER_BULK_UPGRADE_OPTIONS - 1 ?
				business.UpgradeXLevelsCost( GameManager.instance.bulkLevelUpAmount ) : business.UpgradeMaxLevelsCost());
			bool canAffordUpgrade = PlayerManager.instance.cash >= costToUpgradeForBulkLevelUpIndex;
			upgradeButton.SetInteractableAndColor(canAffordUpgrade);
			upgradeCostText.text = NumberFormatter.ToString(costToUpgradeForBulkLevelUpIndex, showDecimalPlaces: true);
		}
		else //otherwise display maxed out
		{
			upgradeButton.SetInteractableAndColor(false);
			upgradeCostText.text = LocalizationManager.instance.StringForKey(LocalizationManagerKeys.Max);
		}
			
		//determine what to present in the profitText, profit per unit (black) or cash per second (green)
		if(business.shouldShowCashPerSecond)
		{
			progressBarFill.fillAmount = 1; //no fill
			profitText.text = string.Format("${0} /{1}", NumberFormatter.ToString(number: business.profit, showDecimalPlaces: true, showDollarSign: false), 
				LocalizationManager.instance.StringForKey(LocalizationManagerKeys.Sec));

		}
		else
		{
			progressBarFill.fillAmount = (business.timeToProduce >= Constants.MINIMUM_TIME_TO_UPDATE_PRODUCTION_FILL ? business.unitCompletePercentage : 1);
			profitText.text = NumberFormatter.ToString(number: business.profit, showDecimalPlaces: true);
		}

		//finally update the time to produce current unit (no unit in production returns 00:00:00)
		timeText.text = business.timeDisplayString;
	}

	/// <summary>Callback when the business has produced a unit.</summary>
	private void BusinessProducedAUnit()
	{
		Assert.IsFalse(business.hasManager);

		produceButton.interactable = true;
	}

	/// <summary>Callback when the business has hired a manager.</summary>
	private void BusinessHiredAManager()
	{
		Assert.IsFalse(business.hasManager);

		//business now has a manager who produces units - no need to listen for further events
		business.OnUnitProduced -= BusinessProducedAUnit;
		business.OnManagerHired -= BusinessHiredAManager;
		//player can no longer click the produce button
		produceButton.interactable = false;
		produceButton.onClick.RemoveAllListeners();
	}
}
