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

    public int currentTutorialPage = 0;

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

    string dndTutorialPage1 =   "Welcome!\n" + "The elephants are attacking the castle and we need to defend it!\n" +
                                "Your goal is to defeat all the elephants and take control of the checkpoint in order to win.\n\n" +
                                "Click \"Next\" to continue.",

            dndTutorialPage2 =  "You can use WASD to move the camera around the map. Take a look around and note the location of the castle and the checkpoint, " + 
                                "signified by a flag in a circle near the middle of the map." + 
                                "Click \"Next\" to continue.",

            dndTutorialPage3 =  "At the top of the screen you can see your current Money and Lives.You lose a live if an elephant makes it to your base. If your lives drops to 0, you lose." +
                                "We'll talk about money in a moment." +
                                "Click \"Next\" to continue.",

            dndTutorialPage4 =  "You control your units by programming them with commands.Press \"tap\" to open the command list.",

            dndTutorialPage5 =  "This is the command list.Here you can see all the commands that are available in this level.You can mouse over the commands to read a tooltip, " + 
                                "that explains what each command does." + 
                                "You can open and close the command list at any time by pressing tap, or cliking the white arrow lext to the list." + 
                                "Now, click the castle.",

            dndTutorialPage6 =  "This is the editor window.Here you can place commands from your command list, in order to program your units." + 
                                "You do this by click and dragging a command from the list in the window, and dropping it into a slot in the editor window." + 
                                "Now, drag a \"Produce Unit\" command into the editor window.",

            dndTutorialPage7 =  "The \"Produce Unit\" command is your most basic command.It produces giraffes at your base, at the cost of money." +
                                "You can see how much money you currently have in the top of the screen.It takes 10 monies to produce a unit, " + 
                                "and you get 8 monies from each elephant your units eliminate." + 
                                "Click \"Next\" to continue.",

            dndTutorialPage8 =  "Note that the \"Produce Unit\" command merely produces your units.It does not tell them how to behave.Let us do something about that." + 
                                "Drag an \"Attack\" command into the next line in the editor.",

            dndTutorialPage9 =  "The \"Attack\" command will order your units to move to a location which you specify.Notice that the editor list is asking for a variable." +
                                "Variables are also found in your command list.Notice the difference in shape between variables and commands."+ 
                                "Now, drag the \"A\" variable into to slot next to the \"Attack\" command.",

            dndTutorialPage10 = "Your castle will now produce units, and order them to go to checkpoint A, and attack any enemies they encounter on the way." +
                                "Now, close the editor window by right clicking.",

            dndTutorialPage11 = "Remember that checkpoint you saw earlier? Find it again, and click it.",

            dndTutorialPage12 = "This is the editor window for this checkpoint. All checkpoints on the map, as well as your castle, have their own editor with their own commands." + 
                                "This means you can give units different orders depending on where they are on the map." + 
                                "Now, drag a \"Defend\" order into the checkpoint's editor.",

            dndTutorialPage13 = "The Defend order will order your units to stay near the checkpoint, and defend it from the savage elephants. " + 
                                "In addition, you units will receive less damage from enemy attacks whenever they are defending." +
                                "Click \"Next\" to continue.",

            dndTutorialPage14 = "Your should now be ready to defend against the elephant onslaught. Click the \"Play\" button in the top of the screen whenever you're ready." + 
                                "Be aware, however, that you cannot change your commands once you've clicked play, so make sure everything is in order." + 
                                "Should your units fail, you can always click \"Restart\" and try again. You will not lose your commands!",
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
                    requiresNextClickToProgress = true;
                    break;
                case 1:
                    currentTutorialText = TutorialPage2;
                    requiresNextClickToProgress = true;
                    break;
                case 2:
                    currentTutorialText = TutorialPage3;
                    requiresNextClickToProgress = true;

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
                    requiresNextClickToProgress = true;
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
                    requiresNextClickToProgress = true;
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
                    currentTutorialPage++;
                    requiresNextClickToProgress = false;
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
