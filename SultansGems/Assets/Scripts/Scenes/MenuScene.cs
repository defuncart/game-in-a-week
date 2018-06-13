/*
 *  Written by James Leahy. (c) 2017-2018 DeFunc Art.
 *  https://github.com/defuncart/
 */
using DeFuncArt.Utilities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>A script used to control the Menu scene.</summary>
public class MenuScene : BaseScene
{
    /// <summary>An array of level buttons.</summary>
    [Tooltip("An array of level buttons.")]
    [SerializeField] private LevelButton[] levelButtons = null;

    /// <summary>Callback when the instance starts.</summary>
    protected override void StartImplementation()
    {
        Assert.IsTrue(levelButtons.Length == LevelsManager.instance.NUMBER_LEVELS);

        //delegates
        for(int i=0; i < levelButtons.Length; i++)
        {
            int temp = i;
            levelButtons[i].onClick.AddListener(() => {
                if(levelButtons[temp].interactable)
                {
                    SettingsManager.instance.level = temp;
                    SceneManager.LoadScene(SceneBuildIndeces.GameScene);
                }
            });
        }

        //assign buttons
        Assert.IsTrue(levelButtons.Length == LevelsManager.instance.NUMBER_LEVELS);
        for(int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].Initialize(text: (i+1).ToString(), isUnlocked: PlayerManager.instance.LevelisUnlocked(levelIndex: i), starsObtained: PlayerManager.instance.StarsObtainedForLevel(levelIndex: i)); 
        }

        //start music
        AudioManager.instance.PlayMusic(MusicDatabaseKeys.main);
    }

    /// <summary>Callback before the instance is destroyed.</summary>
    private void OnDestroy()
    {
        //delegates
        for(int i=0; i < levelButtons.Length; i++)
        {
            levelButtons[i].onClick.RemoveAllListeners();
        }
    }
}
