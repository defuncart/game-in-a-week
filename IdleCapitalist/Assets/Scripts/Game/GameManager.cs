/*
 *	Written by James Leahy. (c) 2018 DeFunc Art.
 *	https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Serialization;
using DeFuncArt.UI;
using DeFuncArt.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>The game manager.</summary>
public class GameManager : MonoSingleton<GameManager>
{
	/// <summary>The main canvas.</summary>
	[Tooltip("The main canvas.")]
	[SerializeField] private DACanvas mainCanvas;
	/// <summary>The upgrades popup.</summary>
	[Tooltip("The upgrades popup.")]
	[SerializeField] private UpgradesPopup upgradesPopup;
	/// <summary>The managers popup.</summary>
	[Tooltip("The managers popup.")]
	[SerializeField] private ManagersPopup managersPopup;
	/// <summary>The offline earnings popup.</summary>
	[Tooltip("The offline earnings popup.")]
	[SerializeField] private OfflineEarningsPopup offlineEarningsPopup;

	/// <summary>The content of the scroll view. Used to house BusinessPanels.</summary>
	[Tooltip("The content of the scroll view. Used to house BusinessPanels.")]
	[SerializeField] private RectTransform scrollViewContent;
	/// <summary>The business panel prefab.</summary>
	[Tooltip("The business panel prefab.")]
	[SerializeField] private BusinessPanel businessPanelPrefab;
	/// <summary>The buy business panel prefab.</summary>
	[Tooltip("The buy business panel prefab.")]
	[SerializeField] private BuyBusinessPanel buyBusinessPanelPrefab;

	/// <summary>A text for the player's cash.</summary>
	[Tooltip("A text for the player's cash.")]
	[SerializeField] private Text playerCashText;
	/// <summary>The bulkLevelUp button text.</summary>
	[Tooltip("The bulkLevelUp button text.")]
	[SerializeField] private Text bulkLevelUpButtonText;
	/// <summary>The prestige panel.</summary>
	[Tooltip("The prestige panel.")]
	[SerializeField] private GameObject prestigePanel;

	public int bulkLevelUpIndex { get; private set; }
	public int bulkLevelUpAmount { get { return Constants.BULK_UPGRADE_LEVELS[bulkLevelUpIndex]; } }

	/// <summary>An array of BusinessPanels and BuyBusinessPanels.</summary>
	private MonoBehaviour[] panels;
	/// <summary>Whether the UI should be updated this frame.</summary>
	private bool shouldUpdateUIThisFrame = false;
	/// <summary>A coroutine used to automatically save the game's data to disk.</summary>
	private Coroutine GameSaveCoroutine;

	/// <summary>Callback when the component awakes.</summary>
	override protected void Init()
	{
		//delegates
		upgradesPopup.OnPopupClose += OnPopupClose;
		managersPopup.OnPopupClose += OnPopupClose;
		offlineEarningsPopup.OnPopupClose += OnPopupClose;

		//variables
		bulkLevelUpIndex = 0;
	}

	/// <summary>Callback before the component (or game object) is destroyed.</summary>
	private void OnDestory()
	{
		//delegates
		upgradesPopup.OnPopupClose -= OnPopupClose;
		managersPopup.OnPopupClose -= OnPopupClose;
		offlineEarningsPopup.OnPopupClose -= OnPopupClose;
	}

	#if UNITY_EDITOR

	/// <summary>EDITOR ONLY: Callback when the application will quit. Isn't triggered on Android or iOS.</summary>
	private void OnApplicationQuit()
	{
		Debug.Log("OnApplicationQuit");

		StartOrStopGameSaveCoroutine(false);
		PlayerManager.instance.SetGamePaused(true);
	}

	/// <summary>Callback when the component starts.</summary>
	private IEnumerator Start()
	{
		//verify data saved to disk
		DataManager.Verify();
		//wait until GameData singleton is loaded
		while(GameData.instance == null)
		{
			yield return null;
		}
		//wait until LocalizationManager singleton is loaded
		while(LocalizationManager.instance == null)
		{
			yield return null;
		}
		//wait until LocalizationManager database is loaded
		while(!LocalizationManager.instance.isLoaded)
		{
			yield return null;
		}

		//finally initialize the game
		Initialize();
	}

	#elif UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL

	/// <summary>EDITOR ONLY: Callback when the application will pause. Triggered on Android or iOS.</summary>
	private void OnApplicationPause(bool gamePaused)
	{
		Debug.Log("OnApplicationPause: " + gamePaused);

		StartOrStopGameSaveCoroutine(!gamePaused);
		PlayerManager.instance.SetGamePaused(gamePaused);
	}

	/// <summary>Callback when the component starts.</summary>
	private void Start()
	{
		Initialize();
	}
	#endif

	/// <summary>Initializes the game on startup.</summary>
	private void Initialize()
	{
		if(PlayerPreferences.GetBool(PlayerPreferencesKeys.hasSeenTutorial))
		{
			//determine how much money the player has earned since last play
			float offlineEarnings = PlayerManager.instance.DetermineEarningsSinceLastPlay();
			if(offlineEarnings > 0)
			{
				mainCanvas.SetInteractable(false);
				offlineEarningsPopup.Initialize(NumberFormatter.ToString (number: offlineEarnings, showDecimalPlaces: true, showDollarSign: true));
				offlineEarningsPopup.Display();
			}

			Debug.Log("offlineEarnings: " + offlineEarnings);

		}
		else
		{
			//show tutorial
			PlayerPreferences.SetBool(PlayerPreferencesKeys.hasSeenTutorial, true);
		}

		//create business panels
		CreateBusinessPanels();
		//initialize specific ui elements
		bulkLevelUpButtonText.text = Constants.BULK_UPGRADE_OPTIONS[bulkLevelUpIndex];
		prestigePanel.SetActive(false);
		//inialize dynamic ui elements
		UpdateUI();
		//finally start a game save coroutine
		StartOrStopGameSaveCoroutine(true);
	}

	/// <summary>Destroys the business panels.</summary>
	private void DestroyBusinessPanels()
	{
		for(int i=0; i < panels.Length; i++)
		{
			if(panels[i] != null) { Destroy(panels[i].gameObject); }
		}
		panels = null;
	}

	/// <summary>Creates the business panels.</summary>
	private void CreateBusinessPanels()
	{
		//create business panels
		panels = new MonoBehaviour[GameData.instance.numberOfBusinesses];
		for(int i=0; i < panels.Length; i++)
		{
			Business business = PlayerManager.instance.GetBusiness(i);
			if(business.isUnlocked)
			{
				BusinessPanel panel = Instantiate(businessPanelPrefab, scrollViewContent);
				panel.Initialize(business);
				panels[i] = panel;
			}
			else
			{
				BuyBusinessPanel panel = Instantiate(buyBusinessPanelPrefab, scrollViewContent);
				panel.Initialize(business.name, business.costToUnlock);
				panels[i] = panel;

				//set up delegate
				int temp = i; //a temporary value for the closure
				panel.OnBuyBusinessButtonPressed += (() => {  OnBuyBusinessButtonPressed(temp); });
			}
		}
	}

	/// <summary>Starts (true) or stops (false) the automatic game save coroutine.</summary>
	/// <param name="shouldStart">If set to <c>true</c> should start, otherwise should stop.</param>
	private void StartOrStopGameSaveCoroutine(bool shouldStart)
	{
		if(shouldStart)
		{
			Assert.IsNull(GameSaveCoroutine);

			GameSaveCoroutine = this.InvokeRepeating(() => {
				PlayerManager.instance.OnSaveToDisk();
			}, Duration.ONE_MINUTE, Duration.ONE_MINUTE);
		}
		else
		{
			Assert.IsNotNull(GameSaveCoroutine);

			if(GameSaveCoroutine != null) { StopCoroutine (GameSaveCoroutine); }
		}
	}

	/// <summary>Callback when the component updates.</summary>
	private void Update()
	{
		if(shouldUpdateUIThisFrame)
		{
			UpdateUI();
			shouldUpdateUIThisFrame = false;
		}
	}

	/// <summary>A method used as an event. GameManager should update the UI this frame.</summary>
	public void OnUpdateUI()
	{
		shouldUpdateUIThisFrame = true;
	}

	/// <summary>Updates the UI.</summary>
	private void UpdateUI()
	{
		//update player's cash
		playerCashText.text = NumberFormatter.ToString(number: PlayerManager.instance.cash, showDecimalPlaces: true);
		//update business panels
		for(int i=0; i < panels.Length; i++)
		{
			if(panels[i].GetComponent<BuyBusinessPanel>() != null)
			{
				(panels[i] as BuyBusinessPanel).interactable = PlayerManager.instance.cash >= PlayerManager.instance.GetBusiness(i).costToUnlock;
			}
			else if(panels[i].GetComponent<BusinessPanel>() != null)
			{
				(panels[i] as BusinessPanel).Refresh();
			}
		}
		//show the prestige panel if it'd be advantageous for the player to prestige
		prestigePanel.SetActive( PlayerManager.instance.shouldConsiderPrestige );
	}

	#region Callbacks

	/// <summary>Callback when a BuyBusinessButton is pressed.</summary>
	/// <param name="businessIndex">The business (as an index).</param>
	private void OnBuyBusinessButtonPressed(int businessIndex)
	{
		Business business = PlayerManager.instance.GetBusiness(businessIndex);
		if(PlayerManager.instance.cash >= business.costToUnlock)
		{
			//instantiate a new business panel and add it at position businessIndex in the scroll view hierarchy
			BusinessPanel panel = Instantiate(businessPanelPrefab, scrollViewContent);
			panel.transform.SetSiblingIndex(businessIndex);
			panel.Initialize(business);
			//delete the BuyBusinessPanel and update the array
			Destroy(panels[businessIndex].gameObject); //on destroy the delegate will be removed
			panels[businessIndex] = panel;
			//update player's cash
			PlayerManager.instance.DecrementCashBy(business.costToUnlock);
			business.SetUnlocked();
			UpdateUI();
		}
	}

	/// <summary>Callback when a popup is closed. Update MainCanvas.</summary>
	private void OnPopupClose()
	{
		mainCanvas.SetInteractable(true);
		shouldUpdateUIThisFrame = true;
	}

	/// <summary>Callback when the UpgradesPopupButton is pressed. Show UpgradesPopup.</summary>
	public void OnShowUpgradesPopupButtonPressed()
	{
		mainCanvas.SetInteractable(false);
		upgradesPopup.Display();
	}

	/// <summary>Callback when the ManagersPopupButton is pressed. Show ManagersPopup.</summary>
	public void OnShowManagersPopupButtonPressed()
	{
		mainCanvas.SetInteractable(false);
		managersPopup.Display();
	}

	/// <summary>Callback when the BulkLevelUpButton is pressed. Update bulkLevelUpIndex and bulkLevelUpButtonText.</summary>
	public void OnBulkLevelUpButtonPressed()
	{
		bulkLevelUpIndex = (bulkLevelUpIndex + 1) % Constants.NUMBER_BULK_UPGRADE_OPTIONS; //increment and clamp
		bulkLevelUpButtonText.text = Constants.BULK_UPGRADE_OPTIONS[bulkLevelUpIndex];
		shouldUpdateUIThisFrame = true;
	}

	/// <summary>Callback when the PrestigeButton is pressed. Perform a prestige.</summary>
	public void OnPrestigeButtonPressed()
	{
		prestigePanel.SetActive(false);
		//process the prestige
		PlayerManager.instance.Prestige();
		//destory and create the panels
		DestroyBusinessPanels(); CreateBusinessPanels();
		//reset scroll view to focus on lemonade stand (i.e. at origin)
		scrollViewContent.ResetY();
		//mark that the UI should be updated
		shouldUpdateUIThisFrame = true;
	}

	#endregion
}
