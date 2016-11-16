using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level1TutorialText : MonoBehaviour {

    public bool commandListOpened = false;
    public bool editorHasBeenOpened = false;
    public bool enterProduceOrder = false;
    public bool enterAttackOrder = false;
    public bool enterAttackTarget = false;
    public bool chechpointEditorOpened = false;
    public bool enterDefendOrder = false;
    public bool pressPlay = false;
    bool drawStartInfo;

    int currentTutorialPage = 0;

    public bool requiresNextClickToProgress = true;

    Rect    TutorialBox;
    float   TutorialBoxStartPosX,
            TutorialBoxStartPosY,
            TutorialBoxHeight,
            TutorialBoxWidth;

    string currentTutorialText;

    //Cache 'Next' button dimensions
    Rect    nextButton;
    float   nextButtonX,
            nextButtonY,
            nextButtonHeight,
            nextButtonWidth;
    

    string  TutorialPage1,
            TutorialPage2,
            TutorialPage3,
            TutorialPage4,
            TutorialPage5,
            TutorialPage6,
            TutorialPage7,
            TutorialPage8,
            TutorialPage9,
            TutorialPage10,
            TutorialPage11,
            TutorialPage12,
            TutorialPage13,
            TutorialPage14,
            TutorialPage15;

    string dndTutorialPage1 =   "Lets begin! \n" +
                                "The Elephants attacks the homebase and we need to defend it, our goal is to eliminate all of the Elephants and Capture the flag \n" +
                                "In order to move around the map, you can use your mouse or WASD-buttons on the keyboard. \n  \n" +
                                "Press Tab-button on the keyboard or the show button on the screen to show the Command list.",

            dndTutorialPage2 = "This is the Command List, here are all of the commands which are available in the current level. To read more about the commands hover your curser over each command to read a description. \n You can close the Command list at any time by pressing tab or hide \n \n" +
                                "Click the castle to open up the sequence editor",

            dndTutorialPage3 = "This is the Editor here you place the commands from the command list. \n" +
                                "The editor is used to issue commands to the player giraffes, with the exception of produce which creates giraffes at the homebase. \n You can close the editor window at any time by right clicking. \n \n" +
                                "Drag a produce order to upper left in the editor to continue",

            dndTutorialPage4 = "The produce order creates giraffes at the player’s base, these giraffes receive the commands from the flag and homebase editors.\n \n" +
                                "Drag the “Attack” order onto the next line to continue",

            dndTutorialPage5 = "The attack command orders your giraffes at the editor's location to attack a set destination which is placed to the right of the attack order.\n \n" +
                                "Now give the Attack command the checkpoint A",

            dndTutorialPage6 = "The target checkpoint, in this case A is the place on the map where the “A” flag is placed. There can be multiple flags on a map, some of which have a capture point. The player needs to capture and hold these points in order to win. \n \n" +
                               "Now close the editor and scroll the camera left until you see a flag on the ground. click it to open its Editor.",

            dndTutorialPage7 = "This is the editor window for checkpoint A. Have you noticed that the commands you entered at the castle are not present here? This is because each location on the map has its own editor. \n \n" +
                                "Drag a defend command into the editor.",
        
            dndTutorialPage8 = "The Defend command will order any of your units that gets near the point to defend it. It also gives them a slight bonus to their armour. \n" + "Now you are ready to defend against the onslought of the savage elephants, go forth and be victorious  \n \n" +
                                "Press play to begin",

            dndTutorialPage9,
            dndTutorialPage10,
            dndTutorialPage11,
            dndTutorialPage12,
            dndTutorialPage13,
            dndTutorialPage14,
            dndTutorialPage15;

    string  textTutorialPage1 = "Lets begin! \n" +
                                "The Elephants attacks the homebase and we need to defend it, our goal is to eliminate all of the Elephants and Capture the flag \n" +
                                "In order to move around the map, you can use your mouse or WASD-buttons on the keyboard. \n  \n" +
                                "Press Tab-button on the keyboard or the show button on the screen to show the Command list.",

            textTutorialPage2 = "This is the Command List, here are all of the commands which are available in the current level. To read more about the commands hover your curser over each command to read a description. \n You can close the Command list at any time by pressing tab or hide \n \n" +
                                "Click the castle to open up the sequence editor",

            textTutorialPage3 = "This is the Editor here you write commands, only those availabe in the Command List can be written. \n" + 
                                "The Editor is used to issue orders to the gireffes, with the exception of the produce order, which creates giraffes at the homebase \n \n" +
                                "write produce order in the Editor to continue",

            textTutorialPage4 = "The produce order creates giraffes at our base, these giraffes recive commands from the homebase and flag Editor \n \n" + 
                                "Write attack order for checkpoint A one the line below produce to continue.",

            textTutorialPage5 = "The attack order commands your giraffes at the Editor's location to attack a set destination which is placed to the right of the attack order. \n" + 
                                "The target checkpoint, in this case A, is placed on the map where the A flag is. There can be multiple flags on a map, some of which have a capture point. \n" + 
                                "The player needs to capture and hold these points in order to win. \n \n" +
                                "Now give the attack command checkpoint A",

            textTutorialPage6 = "The target checkpoint in this case A is the place on the map where the A flag is placed. There can be multiple flags on a map, some of which have a capture point. As the player you need to capture and hold these points in order to win \n \n" + 
                                "Now close the editor an scroll the camera left until you see a flag on the ground. Click it to open its Editor",

            textTutorialPage7 = "This is the Editor window for checkpoint A. Have you noticed that the commands you entered at the castle are not present here? This is because each location on the map has its own Editor. \n \n" + 
                                "write defend in the editor to give your units a small advantage.",

            textTutorialPage8 = "The defend order will order any of your units that gets near the point to defend it. It also gives them a slight bonus to tehir armor. \n Now you are ready to defend against the onsloght of the savafe elephants, forth and be victorious \n \n" +
                                "Press play to begin",

            textTutorialPage9,
            textTutorialPage10,
            textTutorialPage11,
            textTutorialPage12,
            textTutorialPage13,
            textTutorialPage14,
            textTutorialPage15;

    public GUISkin commandSkin;

    levelManager lvlManager;
    mainMenuVariables varKeeper;

    // Use this for initialization
    void Start()
    {
        TutorialBoxStartPosX = (Screen.width / 2) - (Screen.width / 6);
        TutorialBoxStartPosY = (Screen.height / 2) + (Screen.height / 6);
        TutorialBoxHeight = Screen.height / 4;
        TutorialBoxWidth = Screen.width / 3;
        drawStartInfo = true;
        TutorialBox = new Rect(TutorialBoxStartPosX, TutorialBoxStartPosY, TutorialBoxWidth, TutorialBoxHeight);

        nextButtonX = TutorialBoxStartPosX + Screen.width / 25;
        nextButtonY = TutorialBoxStartPosY - Screen.height / 40;
        nextButtonWidth = 50;
        nextButtonHeight = 30;
        nextButton = new Rect(nextButtonX, nextButtonY, nextButtonWidth, nextButtonHeight);

        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();

        if (varKeeper.useDragonDrop == true) // check if it is the drag and drop or text game mode, then fill the tutorial text correctly.
        {
            TutorialPage1 = dndTutorialPage1;
            TutorialPage2 = dndTutorialPage2;
            TutorialPage3 = dndTutorialPage3;
            TutorialPage4 = dndTutorialPage4;
            TutorialPage5 = dndTutorialPage5;
            TutorialPage6 = dndTutorialPage6;
            TutorialPage7 = dndTutorialPage7;
            TutorialPage8 = dndTutorialPage8;
            TutorialPage9 = dndTutorialPage9;
            TutorialPage10 = dndTutorialPage10;
            TutorialPage11 = dndTutorialPage11;
            TutorialPage12 = dndTutorialPage12;
            TutorialPage13 = dndTutorialPage13;
            TutorialPage14 = dndTutorialPage14;
            TutorialPage15 = dndTutorialPage15;
        }
        else
        {
            TutorialPage1 = textTutorialPage1;
            TutorialPage2 = textTutorialPage2;
            TutorialPage3 = textTutorialPage3;
            TutorialPage4 = textTutorialPage4;
            TutorialPage5 = textTutorialPage5;
            TutorialPage6 = textTutorialPage6;
            TutorialPage7 = textTutorialPage7;
            TutorialPage8 = textTutorialPage8;
            TutorialPage9 = textTutorialPage9;
            TutorialPage10 = textTutorialPage10;
            TutorialPage11 = textTutorialPage11;
            TutorialPage12 = textTutorialPage12;
            TutorialPage13 = textTutorialPage13;
            TutorialPage14 = textTutorialPage14;
            TutorialPage15 = textTutorialPage15;
        }
    }
	
	// Update is called once per frame
	void Update () {
    // Update is called once per frame
        if (lvlManager.currentLevel == 1)
        {
            switch (currentTutorialPage)
            {
                case 0:
                    currentTutorialText = TutorialPage1;
                    break;
                case 1:
                    currentTutorialText = TutorialPage2;
                    break;
                case 2:
                    currentTutorialText = TutorialPage3;
                    break;
                case 3:
                    currentTutorialText = TutorialPage4;
                    break;
                case 4:
                    currentTutorialText = TutorialPage5;
                    break;
                case 5:
                    currentTutorialText = TutorialPage6;
                    break;
                case 6:
                    currentTutorialText = TutorialPage7;
                    break;
                case 7:
                    currentTutorialText = TutorialPage8;
                    break;
                case 8:
                    currentTutorialText = TutorialPage9;
                    break;
                case 9:
                    currentTutorialText = TutorialPage10;
                    break;
                case 10:
                    currentTutorialText = TutorialPage11;
                    break;
                case 11:
                    currentTutorialText = TutorialPage12;
                    break;
                case 12:
                    currentTutorialText = TutorialPage13;
                    break;
                case 13:
                    currentTutorialText = TutorialPage14;
                    break;
                case 14:
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
        if (drawStartInfo && lvlManager.currentLevel == 1)
       {
            if (requiresNextClickToProgress)
            {
                if (GUI.Button(nextButton, "Next"))
                {

                }
            }

            //GUI.Box(new Rect(TutorialBox), currentTutorialText, commandSkin.GetStyle("tutorialBoundingBoxBackground"));

            if (commandListOpened == false/* && editorHasBeenOpened == false && enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false*/)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage1, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }
            if (commandListOpened == true && editorHasBeenOpened == false /*&& enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false*/)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage2, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }

            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == false/* && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false*/)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage3, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }

            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == false /*&& enterAttackTarget == false && pressPlay == false*/)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage4, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }

            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == false/* && pressPlay == false*/)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage5, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }

            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true && chechpointEditorOpened == false)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage6, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }

            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true && chechpointEditorOpened == true && enterDefendOrder == false)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage7, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }
            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true && chechpointEditorOpened == true && enterDefendOrder == true && pressPlay == false)
            {
                GUI.Box(new Rect(TutorialBox), TutorialPage8, commandSkin.GetStyle("tutorialBoundingBoxBackground"));
            }
            if (commandListOpened == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true && chechpointEditorOpened == true && enterDefendOrder == true && pressPlay == true)
            {
                drawStartInfo = false;
            }
        }
    }
}
