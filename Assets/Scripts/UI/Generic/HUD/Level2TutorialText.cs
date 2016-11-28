using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level2TutorialText : MonoBehaviour {

    bool drawStartInfo;

    public int currentTutorialPage = 0;

    public bool requiresNextClickToProgress;

    Rect    TutorialBox;
    float   TutorialBoxStartPosX,
            TutorialBoxStartPosY,
            TutorialBoxHeight,
            TutorialBoxWidth;

    string currentTutorialText;

    PauseScript pauseScript;

    //Cache 'Next' button dimensions
    Rect    nextButton;
    float   nextButtonX,
            nextButtonY,
            nextButtonHeight,
            nextButtonWidth;


    string  tutorialPage1,
            tutorialPage2,
            tutorialPage3,
            tutorialPage4,
            tutorialPage5,
            tutorialPage6,
            tutorialPage7,
            tutorialPage8;

    public bool produceWasAddedText = false;

    string dndTutorialPage1    = "Good job on making it this far! Now, I have a surprise for you.\n\n" +
                                 "Open the command list again.",

            dndTutorialPage2   = "Look! A new command has been added. The \"Split\" command.\n" +
                                 "This command is useful for splitting your troops between destinations, instead of sending them all to a single point.\n\n" +
                                 "Drag the command into the castle's editor window.",

            dndTutorialPage3   = "The \"Spilt\" command needs a variable. Right now, \"Every Other\" is available.\n\n" +
                                 "Drag it into the variable-slot.",

			dndTutorialPage4   = "Now, if you enter two Attack commands with different destinations in the two next command slots, the split variable will make newly spawned giraffes move to one or the other, alternately.\n" +
                                 "Do that, then click \"Play\" and see what happens!",

            dndTutorialPage5   = "Did you forget the \"Produce Units\" command? You better restart, and try again.",

            dndTutorialPage6   = "Seems you did not enter two attack commands with different variables after the split command. Try to fix that!",

			dndTutorialPage7   = "See? Your giraffes are splitting their numbers between the two checkpoints. You can now defend your base from two fronts!\n\n" +
                                 "Click \"Next\" to continue.",

            dndTutorialPage8   = "In later levels, \"Every Third\" will be available for the \"Split\" command as well, which will then send every third giraffe to one location, and the other two to the other location.\n\n" +
                                 "Click \"Next\" to end the tutorial, and go forth to victory!";

    string textTutorialPage1   = "Good job on making it this far! Now, I have a surprise for you.\n\n" +
                                 "Open the command list again.",

            textTutorialPage2  = "Look! A new command has been added. The \"splitAt(X)\" command.\n" +
                                 "This command is useful for splitting your troops between destinations, instead of sending them all to a single point.\n\n" +
                                 "Enter the \"splitAt(X)\" command in the castle's editor and click \"Compile\".",

            textTutorialPage3  = "The \"splitAt(X)\" command needs a variable. Right now, \"2\" is available.\n\n" +
                                 "Replace the X with that, if you didn't already.",

			textTutorialPage4  = "Now, if you enter two Attack commands with different destinations on the next two lines, the split variable will make newly spawned giraffes move to one or the other, alternately.\n\n" +
                                 "Do that, then click \"Play\" and see what happens!",

            textTutorialPage5  = "Did you forget the \"produceUnits\" command? You better restart, and try again.",

            textTutorialPage6  = "Seems you did not enter two attack commands with different variables after the split command. Try to fix that!",

			textTutorialPage7  = "See? Your giraffes are splitting their numbers between the two checkpoints. You can now defend your base from two fronts!\n\n" +
                                 "Click \"Next\" to continue.",

            textTutorialPage8  = "In later levels, \"3\" will be available for the \"splitAt(X)\" command as well, which will then send every third giraffe to one location, and the other two to the other location.\n\n" +
                                 "Click \"Next\" to end the tutorial, and go forth to victory!";

    public GUISkin commandSkin;

    levelManager lvlManager;
    mainMenuVariables varKeeper;

    // Use this for initialization
    void Start()
    {
        requiresNextClickToProgress = false;
        TutorialBoxStartPosX = (Screen.width / 2) - (Screen.width / 6);
        TutorialBoxStartPosY = (Screen.height / 2) + (Screen.height / 6);
        TutorialBoxHeight = (Screen.height / 4) + (Screen.height / 16);
        TutorialBoxWidth = Screen.width / 3;
        drawStartInfo = true;
        TutorialBox = new Rect(TutorialBoxStartPosX, TutorialBoxStartPosY, TutorialBoxWidth, TutorialBoxHeight);

        pauseScript = GameObject.Find("UIButtons").GetComponent<PauseScript>();

        nextButtonX = TutorialBoxStartPosX + Screen.width / 4;
        nextButtonY = TutorialBoxStartPosY - Screen.height / 40;
        nextButtonWidth = Screen.width / 23;
        nextButtonHeight = Screen.height / 22;
        nextButton = new Rect(nextButtonX, nextButtonY, nextButtonWidth, nextButtonHeight);

        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();

        if (varKeeper.useDragonDrop == true) // check if it is the drag and drop or text game mode, then fill the tutorial text correctly.
        {
            tutorialPage1 = dndTutorialPage1;
            tutorialPage2 = dndTutorialPage2;
            tutorialPage3 = dndTutorialPage3;
            tutorialPage4 = dndTutorialPage4;
            tutorialPage5 = dndTutorialPage5;
            tutorialPage6 = dndTutorialPage6;
            tutorialPage7 = dndTutorialPage7;
            tutorialPage8 = dndTutorialPage8;
        }
        else
        {
            tutorialPage1 = textTutorialPage1;
            tutorialPage2 = textTutorialPage2;
            tutorialPage3 = textTutorialPage3;
            tutorialPage4 = textTutorialPage4;
            tutorialPage5 = textTutorialPage5;
            tutorialPage6 = textTutorialPage6;
            tutorialPage7 = textTutorialPage7;
            tutorialPage8 = textTutorialPage8;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Update is called once per frame
        if (lvlManager.currentLevel == 2)
        {
            switch (currentTutorialPage)
            {
                case 0:
                    currentTutorialText = tutorialPage1;
                    break;
                case 1:
                    currentTutorialText = tutorialPage2;
                    break;
                case 2:
                    currentTutorialText = tutorialPage3;
                    break;
                case 3:
                    currentTutorialText = tutorialPage4;
                    break;
                case 4:
                    currentTutorialText = tutorialPage5;
                    if (produceWasAddedText && !pauseScript.GetPauseStatus())
                    {
                        currentTutorialPage++;
                    }
                    break;
                case 5:
                    currentTutorialText = tutorialPage6;
                    break;
                case 6:
                    currentTutorialText = tutorialPage7;
                    requiresNextClickToProgress = true;
                    break;
                case 7:
                    currentTutorialText = tutorialPage8;
                    requiresNextClickToProgress = true;
                    break;
                case 8:
                    drawStartInfo = false;
                    break;
                default:
                    break;
            }
        }


    }

    void OnGUI()
    {
        GUI.skin = commandSkin;
        commandSkin.GetStyle("tutorialBoundingBoxBackground").wordWrap = true;

        commandSkin.GetStyle("tutorialBoundingBoxBackground").fontSize = Screen.width / 90;
        commandSkin.GetStyle("tutorialBoundingBoxBackground").fontStyle = FontStyle.Bold;

        if (drawStartInfo && lvlManager.currentLevel == 2)
       {
            if (requiresNextClickToProgress)
            {
                commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.top = 0;
                commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.bottom = Screen.height / 200;
                commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.left = 0;
                commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.right = 0;
                if (GUI.Button(nextButton, "Next", commandSkin.GetStyle("tutorialBoundingBoxBackground")))
                {
                    currentTutorialPage++;
                    requiresNextClickToProgress = false;
                }
            }
            commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.top = Screen.height / 28;
            commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.bottom = Screen.height / 25;
            commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.left = Screen.width / 20;
            commandSkin.GetStyle("tutorialBoundingBoxBackground").padding.right = Screen.width / 20;
            GUI.Box(new Rect(TutorialBox), currentTutorialText, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
        }
    }
}
