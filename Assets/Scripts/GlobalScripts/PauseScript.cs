using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    //Bool that tracks if game is paused or not.
    private bool gameIsPaused = true;

    //Cahce the level manager, and the tutorial.
    levelManager lvlManager;
    Level1TutorialText tutorialtext1;

    //Cache the KeeperOfVariables.
    mainMenuVariables varKeeper;

    void Start()
    {
        //Reference the lvlmanager.
        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();

        //If this is level 01, cache the tutorial.
        if (lvlManager.currentLevel == 1)
            tutorialtext1 = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();

        //Reference the keeperOfVariables.
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
    }

    public void PausePlay()
    {
        if (lvlManager.currentLevel == 1)
        {
            if (tutorialtext1.currentTutorialPage == 8  || tutorialtext1.currentTutorialPage == 18)
            {
                tutorialtext1.currentTutorialPage++;
            }
        }  
        gameIsPaused = !gameIsPaused;
    }

    public bool GetPauseStatus()
    {
        return gameIsPaused;
    }
}
