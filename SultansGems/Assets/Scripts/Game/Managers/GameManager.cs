/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.ExtensionMethods;
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>The Game Manager.</summary>
public class GameManager : MonoBehaviour
{
    /// <summary>Reference to the gameboad.</summary>
    [Tooltip("Reference to the gameboad.")]
    [SerializeField]
    private GameBoard gameboard = null;
    /// <summary>Reference to the touch manager.</summary>
    [Tooltip("Reference to the touch manager.")]
    [SerializeField]
    private TouchManager touchManager = null;
    /// <summary>Reference to the ui manager.</summary>
    [Tooltip("Reference to the ui manager.")]
    [SerializeField]
    private UIManager uiManager = null;

    /// <summary>The game paused popup.</summary>
    [Tooltip("The game paused popup.")]
    [SerializeField]
    private GamePausedPopup gamePausedPopup = null;
    /// <summary>The game over popup.</summary>
    [Tooltip("The game over popup.")]
    [SerializeField]
    private GameOverPopup gameOverPopup = null;

    /// <summary>The current level.</summary>
    private Level currentLevel
    {
        get { return LevelsManager.instance.currentLevel; }
    }

    /// <summary>A backing variable for moves.</summary>
    private int _moves = 0;
    /// <summary>The number of remaining moves.</summary>
    private int moves
    {
        get { return _moves; }
        set { Assert.IsTrue(value >= 0); _moves = value; uiManager.SetMoves(moves); }
    }
    /// <summary>A backing variable for score.</summary>
    private int _score = 0;
    /// <summary>The player's score.</summary>
    private int score
    {
        get { return _score; }
        set { Assert.IsTrue(value >= 0); _score = value; uiManager.SetScore(score); }
    }

    /// <summary>Callback when the manager awakes.</summary>
    private void Awake()
    {
        //delegates
        gamePausedPopup.OnPopupClose += GamePausedPopup_OnPopupClose;
        gamePausedPopup.OnRestart += OnRestart;
        gamePausedPopup.OnQuit += OnQuit;

        gameboard.OnPointsScored += (int value) => {
            score += value;
        };
        gameboard.OnSuccessfulMove += () => {
            moves--;
            if(moves == 0) { GameOver(); }
        };
        gameboard.AnimatingFinished += () => {
            touchManager.ResetSelectionState();
        };

        touchManager.OnStonesSelected += (selectedStone1, selectedStone2) => {
            StartCoroutine(gameboard.Swap(selectedStone1, selectedStone2));
        };

        gameOverPopup.OnContinue += OnQuit;
        gameOverPopup.OnRestart += OnRestart;
        gameOverPopup.OnQuit += OnQuit;
    }

    /// <summary>Callback before the manager is destroyed.</summary>
    private void OnDestroy()
    {
        //delegates
        gamePausedPopup.OnPopupClose -= GamePausedPopup_OnPopupClose;
        gamePausedPopup.OnRestart -= OnRestart;
        gamePausedPopup.OnQuit -= OnQuit;
        gameOverPopup.OnContinue -= OnQuit;
        gameOverPopup.OnRestart -= OnRestart;
        gameOverPopup.OnQuit -= OnQuit;
    }

    /// <summary>Callback when the manager starts.</summary>
    private void Start()
    {
        //create the gameboard and restart the game
        gameboard.Create();
        OnRestart();

        //start music
        AudioManager.instance.PlayMusic(MusicDatabaseKeys.main);
    }

    #region Callbacks

    /// <summary>Callback when the player wants to quit.</summary>
    public void OnQuit()
    {
        touchManager.acceptInput = false;
        uiManager.SetInteractable(false);

        SceneManager.LoadScene(SceneBuildIndeces.MenuScene);
    }

    /// <summary>Callback when the pause button is pressed.</summary>
    public void PauseButtonPressed()
    {
        touchManager.acceptInput = false;
        SetGameElementsVisible(false);
        gamePausedPopup.Display();
    }

    /// <summary>Sets whether the game elements should be visible.</summary>
    private void SetGameElementsVisible(bool visible, bool pauseGame = false)
    {
        if(pauseGame) { Time.timeScale = visible ? 1 : 0; }
        gameboard.visible = visible;
        touchManager.acceptInput = visible;
        uiManager.SetVisibleInteractable(visible);
    }

    /// <summary>Callback when the game paused popup is closed.</summary>
    private void GamePausedPopup_OnPopupClose()
    {
        touchManager.acceptInput = true;
        SetGameElementsVisible(true);
    }

    /// <summary>Callback when the player wants to restart.</summary>
    private void OnRestart()
    {
        gameboard.visible = true;
        gameboard.Reset();
        touchManager.acceptInput = true;
        moves = currentLevel.maximumNumberMoves; score = 0;
        uiManager.SetVisibleInteractable(true);
    }

    #endregion

    #region Methods

    private void GameOver()
    {
        bool gameWon = score >= currentLevel.scoreToAchieve1Star;

        //SetGameElementsVisible(false);
        touchManager.acceptInput = false;
        uiManager.SetInteractable(false);

        if (gameWon)
        {
            int starsObtained = (score > currentLevel.scoreToAchieve3Star ? 2 : score > currentLevel.scoreToAchieve2Star ? 1 : 0);
            gameOverPopup.Setup(gameWon, starsObtained);
            PlayerManager.instance.SetScoreStarsObtainedForCurrentLevel(score, starsObtained);
        }
        else
        {
            gameOverPopup.Setup(gameWon);
        }

        this.Invoke(() => {
            SetGameElementsVisible(false);
            gameOverPopup.Display();
        }, Duration.HALF_SECOND);
    }

    #endregion
}
