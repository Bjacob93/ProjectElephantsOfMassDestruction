using UnityEngine;
using System.Collections;

public class Level1TutorialText : MonoBehaviour {

    public bool qHasBeenPressed = false;
    public bool editorHasBeenOpened = false;
    public bool enterProduceOrder = false;
    public bool enterAttackOrder = false;
    public bool enterAttackTarget = false;
    public bool pressPlay = false;
    bool drawStartInfo;

    float ToturialBoxStartPosX;
    float ToturialBoxStartPosY;
    float TutorialBoxHeight;
    float TutorialBoxWidth;
    Rect TutorialBox;

    string  TutorialPage1,
            TutorialPage2,
            TutorialPage3,
            TutorialPage4,
            TutorialPage5,
            TutorialPage6;

    string  dndTutorialPage1 = "Lets begin! \n " + 
                                "The Elephants attacks the homebase and we need to defend it, our goal is to eliminate all of the Elephants and Capture the flag \n " +
                                "In order to move around the map, you can use your mouse or WASD-buttons on the keyboard. \n  \n" +
                                "Press Tab-button on the keyboard or the show button on the screen to show the Command list.",

            dndTutorialPage2 = "This is the Command List, here are all of the commands which areavailable in the current level. To read more about the commandshover your curser over each command to read a description. \n \n " +
                                "Press the castle to open up the sequence editor",

            dndTutorialPage3 = "This is the editor here you place the commands from the command list. \n" +
                                "The editor is used to issue commands to the player giraffes, with the exception of produce which creates giraffes at the homebase. \n \n" +
                                "Drag a produce order to upper left in the editor to continue",

            dndTutorialPage4 = "The produce order creates giraffes at the player’s base, these giraffes receive the commands from the flag and homebase editors.\n \n" +
                                "Drag the “Attack” order onto the next line to continue",

            dndTutorialPage5 = "The attack command orders your giraffes at the editor's location to attack a set destination which is placed to the right of the attack order.\n \n " +
                                "Now give the Attack command the checkpoint A",

            dndTutorialPage6 = "The target checkpoint, in this case A is the place on the map where the “A” flag is placed. There can be multiple flags on a map, some of which have a capture point. The player needs to capture and hold these points in order to win. \n \n" +     
                                "Press play to begin";

    string  textTutorialPage1 = "Lets begin! \n " +
                                "The Elephants attacks the homebase and we need to defend it, our goal is to eliminate all of the Elephants and Capture the flag \n " +
                                "In order to move around the map, you can use your mouse or WASD-buttons on the keyboard. \n  \n" +
                                "Press Tab-button on the keyboard or the show button on the screen to show the Command list.",

            textTutorialPage2 = "This is the Command List, here are all of the commands which areavailable in the current level. To read more about the commandshover your curser over each command to read a description. \n \n " +
                                "Press the castle to open up the sequence editor",

            textTutorialPage3 = "",
            textTutorialPage4 = "",
            textTutorialPage5 = "",
            textTutorialPage6 = "";

    public GUISkin commandSkin;

    levelManager lvlManager;
    mainMenuVariables varKeeper;

    // Use this for initialization
    void Start()
    {
        ToturialBoxStartPosX = (Screen.width / 2) - (Screen.width / 6);
        ToturialBoxStartPosY = (Screen.height / 2) - (Screen.height / 3);
        TutorialBoxHeight = Screen.height / 6;
        TutorialBoxWidth = Screen.width / 3;
        drawStartInfo = true;
        TutorialBox = new Rect(ToturialBoxStartPosX, ToturialBoxStartPosY, TutorialBoxWidth, TutorialBoxHeight);

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
        }
        else
        {
            TutorialPage1 = textTutorialPage1;
            TutorialPage2 = textTutorialPage2;
            TutorialPage3 = textTutorialPage3;
            TutorialPage4 = textTutorialPage4;
            TutorialPage5 = textTutorialPage5;
            TutorialPage6 = textTutorialPage6;
        }
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        GUI.skin = commandSkin;
        if (drawStartInfo && lvlManager.currentLevel == 1) { 
        if (qHasBeenPressed == false/* && editorHasBeenOpened == false && enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false*/)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage1, commandSkin.GetStyle("tooltipBackground"));
        }

        if(qHasBeenPressed == true && editorHasBeenOpened == false /*&& enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false*/)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage2, commandSkin.GetStyle("tooltipBackground"));
        }

        if(qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == false/* && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false*/)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage3, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == false /*&& enterAttackTarget == false && pressPlay == false*/)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage4, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == false/* && pressPlay == false*/)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage5, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage6, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true &&pressPlay == true)
        {
                drawStartInfo = false;
        }
        }
    }
}
