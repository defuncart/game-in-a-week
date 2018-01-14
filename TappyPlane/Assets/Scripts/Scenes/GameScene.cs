/*
 *	Written by James Leahy. (c) 2017 DeFunc Art.
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.UI;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.UI;

/// <summary>This script controls the game scene.</summary>
public class GameScene : MonoBehaviour
{
	#region Properties

	/// <summary>A reference to the player.</summary>
	[Tooltip("A reference to the player.")]
	[SerializeField] private Player player;
	/// <summary>A reference to the object spawner.</summary>
	[Tooltip("A reference to the object spawner.")]
	[SerializeField] private ObjectSpawner objectSpawner;

	/// <summary>The main menu canvas.</summary>
	[Tooltip("The main menu canvas.")]
	[SerializeField] private DACanvas mainMenuCanvas;
	/// <summary>The settings canvas.</summary>
	[Tooltip("The settings canvas.")]
	[SerializeField] private DACanvas settingsCanvas;
	/// <summary>An array of language buttons on the settings canvas.</summary>
	[Tooltip("An array of language buttons on the settings canvas.")]
	[SerializeField] private DAButton[] settingsCanvasLanguageButtons;
	/// <summary>The main menu canvas.</summary>
	[Tooltip("The main menu canvas.")]
	[SerializeField] private DACanvas gameCanvas;
	/// <summary>The score text on the game canvas.</summary>
	[Tooltip("The score text on the game canvas.")]
	[SerializeField] private Text gameCanvasScoreText;
	/// <summary>The results canvas.</summary>
	[Tooltip("The results canvas.")]
	[SerializeField] private DACanvas resultsCanvas;
	/// <summary>The score text on the game canvas.</summary>
	[Tooltip("The score text on the results canvas.")]
	[SerializeField] private Text resultsCanvasScoreText;
	/// <summary>The ad button on the game canvas.</summary>
	[Tooltip("The ad button on the results canvas.")]
	[SerializeField] private GameObject resultsCanvasAdButton;
	/// <summary>The play again button on the game canvas.</summary>
	[Tooltip("The play again button on the results canvas.")]
	[SerializeField] private Transform resultsCanvasPlayAgainButton;
	/// <summary>The main menu canvas.</summary>
	[Tooltip("The main menu canvas.")]
	[SerializeField] private DACanvas shopCanvas;
	/// <summary>The coins value text on the shop canvas.</summary>
	[Tooltip("The coins value text on the shop canvas.")]
	[SerializeField] private Text shopCanvasCoinValueText;

	/// <summary>How often the game speed should be updated.</summary>
	[Tooltip("How often the game speed should be updated.")]
	[SerializeField] private float gameSpeedUpdateInterval = 1.5f;
	/// <summary>How much the game speed should be updated by (per update interval).</summary>
	[Tooltip("How much the game speed should be updated by (per update interval).")]
	[SerializeField] private float gameSpeedUpdateAmount = 0.25f;
	/// <summary>The game's max speed.</summary>
	[Tooltip("The game's max speed.")]
	[SerializeField] private float maxGameSpeed = 5f;
	/// <summary>The game's current speed.</summary>
	public static float gameSpeed { get; private set; }
	/// <summary>The player's score for the current game.</summary>
	private int score;
	/// <summary>A reference to a cooroutine which updates the score every second.</summary>
	private Coroutine updateScoreCoroutine;
	/// <summary>A reference to a cooroutine which updates the speed every gameSpeedUpdateInterval seconds.</summary>
	private Coroutine updateSpeedCoroutine;

	#endregion

	#region Methods

	/// <summary>Callback when the instance awakes.</summary>
	private void Awake()
	{
		//delegates
		player.OnPlayerDie += Game_GameOver;
		//initialize variables
		gameSpeed = 1;
	}

	/// <summary>Callback when the instance is destroyed.</summary>
	private void OnDestroy()
	{
		//delegates
		player.OnPlayerDie -= Game_GameOver;
	}

	/// <summary>Callback when the instance starts.</summary>
	private void Start()
	{
		ShowMainMenu();
		AudioManager.instance.PlayMusic(AudioManagerKeys.music);
	}

	/// <summary>Shows the main menu canvas.</summary>
	private void ShowMainMenu()
	{
		player.SetPlayable(false); player.Reset();
		objectSpawner.SetSpawnable(false);

		mainMenuCanvas.SetVisibleInteractable(true); settingsCanvas.SetVisible(false); resultsCanvas.SetVisible(false);
	}

	/// <summary>Starts the game and shows the game canvas.</summary>
	private void StartGame()
	{
		//reset the score
		score = 0; gameCanvasScoreText.text = ""; //text is initially displayed as an empty string instead of 0

		mainMenuCanvas.SetVisible(false); resultsCanvas.SetVisible(false); gameCanvas.SetVisible(true);
		player.SetPlayable(true); player.Reset();
		objectSpawner.SetSpawnable(true);

		//start an invoking coroutine that'll update the score every second after one second
		updateScoreCoroutine = this.InvokeRepeating(() => {
			score++; gameCanvasScoreText.text = score.ToString();
		}, AnimationDuration.ONE_SECOND, AnimationDuration.ONE_SECOND);
		//start an invoking coroutine that'll update the speed every gameSpeedUpdateInterval seconds after gameSpeedUpdateInterval seconds
		updateSpeedCoroutine = this.InvokeRepeating(() => {
			if(gameSpeed < maxGameSpeed)
			{
				gameSpeed += gameSpeedUpdateAmount;
				SetSpeedMultiplierMoveHorizontalScripts();
			}
		}, gameSpeedUpdateInterval, gameSpeedUpdateInterval);
	}

	/// <summary>Sets the speed multiplier move horizontal scripts.</summary>
	private void SetSpeedMultiplierMoveHorizontalScripts()
	{
		MoveHorizontal[] scripts = FindObjectsOfType<MoveHorizontal>();
		foreach(MoveHorizontal script in scripts)
		{
			script.SetSpeedMultiplier(gameSpeed);
		}
	}

	/// <summary>Shows the results canvas.</summary>
	private void ShowResults()
	{
		if(UnityAdsManager.canShowAds && score > 0)
		{
			resultsCanvasAdButton.SetActive(true);
			resultsCanvasPlayAgainButton.SetLocalX(150);
		}
		else
		{
			resultsCanvasAdButton.SetActive(false);
			resultsCanvasPlayAgainButton.SetLocalX(0);
		}
		resultsCanvasScoreText.text = score.ToString(); resultsCanvas.SetVisibleInteractable(true);
	}

	/// <summary>Shows the shop canvas.</summary>
	private void ShowShop()
	{
		Shop_UpdateCoinsText();
		mainMenuCanvas.SetVisible(false); resultsCanvas.SetVisible(false); shopCanvas.SetVisibleInteractable(true);
	}

	#endregion

	#region MainMenuCanvas

	/// <summary>Callback when the PlayButton on the MainMenu is pressed.</summary>
	public void MainMenu_PlayButtonPressed()
	{
		mainMenuCanvas.SetInteractable(false);
		StartGame();
	}

	/// <summary>Callback when the ShopButton on the MainMenu is pressed.</summary>
	public void MainMenu_ShopButtonPressed()
	{
		mainMenuCanvas.SetInteractable(false);
		ShowShop();
	}

	/// <summary>Callback when the SettingsButton on the MainMenu is pressed.</summary>
	public void MainMenu_SettingsButtonPressed()
	{
		mainMenuCanvas.SetVisibleInteractable(false); settingsCanvas.SetVisibleInteractable(true);
		SettingsCanvas_RefreshLanguageButtons();
	}

	#endregion

	#region SettingsCanvas

	/// <summary>Callback when the HomeButton on the SettingsCanvas is pressed.</summary>
	public void SettingsCanvas_HomeButtonPressed()
	{
		settingsCanvas.SetVisibleInteractable(false); mainMenuCanvas.SetVisibleInteractable(true);
	}

	/// <summary>Callback when the MusicButton on the SettingsCanvas is toggled.</summary>
	public void SettingsCanvas_MusicButtonToggled()
	{
		SettingsManager.instance.ToggleMusicEnabled();
	}

	/// <summary>Callback when the SFXButton on the SettingsCanvas is toggled.</summary>
	public void SettingsCanvas_SFXButtonToggled()
	{
		SettingsManager.instance.ToggleSFXEnabled();
	}

	/// <summary>Callback when the LanguageButton on the SettingsCanvas is pressed.</summary>
	/// <param name="language">The language index.</param>
	public void SettingsCanvas_LanguageButtonPressed(int language)
	{
		if(language != SettingsManager.instance.localizedLanguage)
		{
			LocalizationManager.instance.SetLanguage(language);
			SettingsCanvas_RefreshLanguageButtons();
		}
	}

	/// <summary>Refreshes the language buttons on the SettingsCanvas.</summary>
	private void SettingsCanvas_RefreshLanguageButtons()
	{
		for(int i=0; i < settingsCanvasLanguageButtons.Length; i++)
		{
			settingsCanvasLanguageButtons[i].alpha = 
				(i == SettingsManager.instance.localizedLanguage ? 1.0f : 0.6f);
		}
	}

	#endregion

	#region GameCanvas

	/// <summary>Proceeds a game over state.</summary>
	private void Game_GameOver()
	{
		//stop the update score coroutine
		StopCoroutine(updateScoreCoroutine);
		//stop the update speed coroutine, reset speed for all game objects
		StopCoroutine(updateSpeedCoroutine); gameSpeed = 1f; SetSpeedMultiplierMoveHorizontalScripts();
		//set player to be unplayable, spawner to stop spawning and play sfx
		player.SetPlayable(false); objectSpawner.SetSpawnable(false);
		AudioManager.instance.PlaySFX(AudioManagerKeys.explosion);
		//display results
		gameCanvas.SetVisibleInteractable(false); ShowResults();
	}

	#endregion

	#region ResultsCanvas

	/// <summary>Callback when the PlayButton on the ResultsCanvas is pressed.</summary>
	public void Results_PlayButtonPressed()
	{
		PlayerManager.instance.IncrementCoins(score);
		resultsCanvas.SetInteractable(false);
		StartGame();
	}

	/// <summary>Callback when the AdButton on the ResultsCanvas is pressed.</summary>
	public void Results_AdButtonPressed()
	{
		resultsCanvas.SetInteractable(false);
		//display a video ad - if the player watches it, reward them
		UnityAdsManager.ShowRewardVideoWithCallback(() => {
			//play sfx
			AudioManager.instance.PlaySFX(AudioManagerKeys.doubleCoins);
			//double the player's score
			score *= 2;resultsCanvasScoreText.text = score.ToString();
			//remove the ad button and center play again button
			resultsCanvasAdButton.SetActive(false);
			resultsCanvasPlayAgainButton.SetLocalX(0);
			resultsCanvas.SetInteractable(true);
		});
	}
		
	/// <summary>Callback when the MainMenuButton on the ResultsCanvas is pressed.</summary>
	public void Results_MainMenuButtonPressed()
	{
		PlayerManager.instance.IncrementCoins(score);
		resultsCanvas.SetInteractable(false);
		ShowMainMenu();
	}

	#endregion

	#region ShopCanvas

	/// <summary>Callback when the ExitButton on the ShopCanvas is pressed.</summary>
	public void Shop_ExitButtonPressed()
	{
		shopCanvas.SetVisibleInteractable(false); mainMenuCanvas.SetVisibleInteractable(true);
	}

	/// <summary>Updates the coin text on the ShopCanvas.</summary>
	public void Shop_UpdateCoinsText()
	{
		shopCanvasCoinValueText.text = PlayerManager.instance.coins.ToString();
	}

	#endregion
}
