using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    //Bool that tracks if game is paused or not.
    private bool gameIsPaused = true;

    //Cahce the level manager, and the tutorial.
    levelManager lvlManager;
    Level1TutorialText tutorial1;
    Level2TutorialText tutorial2;

    //Cache the KeeperOfVariables.
    mainMenuVariables varKeeper;

    void Start()
    {
        //Reference the lvlmanager.
        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();

        //If this is level 01, cache the tutorial.
        if (lvlManager.currentLevel == 1)
            tutorial1 = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();
        if (lvlManager.currentLevel == 2)
            tutorial2 = GameObject.Find("UIManager").GetComponent<Level2TutorialText>();

        //Reference the keeperOfVariables.
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
    }

    public void PausePlay()
    {
        if (lvlManager.currentLevel == 1)
        {
            if (tutorial1.currentTutorialPage == 8  || tutorial1.currentTutorialPage == 18)
            {
                tutorial1.currentTutorialPage++;
            }
        }
        if (lvlManager.currentLevel == 2)
        {
            if (tutorial2.currentTutorialPage == 3)
            {
                tutorial2.currentTutorialPage++;
            }
        }
        gameIsPaused = !gameIsPaused;
    }

    public bool GetPauseStatus()
    {
        return gameIsPaused;
    }
}
