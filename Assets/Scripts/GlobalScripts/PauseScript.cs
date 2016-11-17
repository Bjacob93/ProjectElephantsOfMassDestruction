using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    HomebaseGUI homebasePauseSetting;

    GameObject resetButton;
    GameObject playButton;
    Button play;
    public Sprite playSprite;

    public bool gameIsPaused = true;

    levelManager lvlManager;
    Level1TutorialText tutorialtext1;

    mainMenuVariables varKeeper;

    void Start()
    {
        homebasePauseSetting = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<HomebaseGUI>();
        resetButton = GameObject.Find("Reset");

        playButton = GameObject.FindGameObjectWithTag("PlayButton");

        if (resetButton == null) Debug.Log("didn't find button");

        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
        if (lvlManager.currentLevel == 1)
        tutorialtext1 = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();

        play = GetComponent<Button>();
        play.image.sprite = playSprite;

        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
    }

    public void Pause()
    {
        if (lvlManager.currentLevel == 1)
        {
            if (tutorialtext1.currentTutorialPage == 12 && !varKeeper.useDragonDrop)
            {
                tutorialtext1.currentTutorialPage++;
            }
            if (tutorialtext1.currentTutorialPage == 13 && varKeeper.useDragonDrop)
            {
                tutorialtext1.currentTutorialPage++;
            }
        }
            

        if (resetButton.activeSelf)
        {
            resetButton.SetActive(false);
        }
        else
        {
            resetButton.SetActive(true);
        }

        if (playButton.activeSelf)
        {
            playButton.SetActive(false);
        }
        else
        {
            playButton.SetActive(true);
        }

        gameIsPaused = !gameIsPaused;

        homebasePauseSetting.gameIsPaused = !homebasePauseSetting.gameIsPaused;
    }
}
