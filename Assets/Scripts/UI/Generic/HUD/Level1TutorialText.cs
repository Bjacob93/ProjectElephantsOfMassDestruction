using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level1TutorialText : MonoBehaviour {

    bool drawStartInfo;

    public int currentTutorialPage = 0;

    public bool requiresNextClickToProgress = false;

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


    string  tutorialPage1,
            tutorialPage2,
            tutorialPage3,
            tutorialPage4,
            tutorialPage5,
            tutorialPage6,
            tutorialPage7,
            tutorialPage8,
            tutorialPage9,
            tutorialPage10,
            tutorialPage11,
            tutorialPage12,
            tutorialPage13,
            tutorialPage14,
            tutorialPage15,
            tutorialPage16,
            tutorialPage17,
            tutorialPage18,
            tutorialPage19;

    string  dndTutorialPage1   = "Welcome!\n" + "The elephants are attacking the castle and we need to defend it!\n" +
                                 "Your goal is to defeat all the elephants and take control of the checkpoint in order to win.\n\n" +
                                 "Click \"Next\" to continue.",


            dndTutorialPage2   = "You can use the W,A,S, and D-buttons to move the camera. Take a look around and note the location of the castle, and the checkpoint which is " +
                                 "signified by a flag in a circle near the middle of the area.\n\n" +
                                 "Click \"Next\" to continue.",

            dndTutorialPage3   = "At the top of the screen you can see your current Lives. You lose a life if an elephant makes it to your castle. If your lives drops to 0, you lose.\n\n" +
                                 "Click \"Next\" to continue.",

            dndTutorialPage4   = "You control your units, in the shape of giraffes, by programming them with commands.\n\n Press \"Tab\" to open the command list.",

            dndTutorialPage5   = "This is the command list. Here you can see all the commands that are available in this level. You can mouse over the commands to read a tooltip, " +
                                 "to learn what each command does.\n\n" +
                                 "Click \"Next\" to continue.",

            dndTutorialPage6   = "You can open and close the command list at any time by pressing \"Tab\", or cliking the white arrow next to the list.\n\n" +
                                 "Now, click the castle.",

            dndTutorialPage7   = "This is the editor window. Here you can place commands from your command list, in order to program your giraffes.\n\n" +
                                 "Click \"Next\" to continue.",
                                 
            dndTutorialPage8   = "You do this by dragging a command from the list in the window, and dropping it into a empty slot in the editor window.\n\n" +
                                 "Now, drag a \"Produce Units\" command into the editor window.",

            dndTutorialPage9   = "The \"Produce Units\" command is your most basic command. Only available at the castle, it continuously produces giraffes - up to a cap.\n\n" +
                                 "Click the \"Play\" button - the right-pointing arrow in the top of the screen - and lets see what happens.",

            dndTutorialPage10  = "Congratulations, you produced your first giraffe! But what's this? He's not moving. We better do something about that.\n" +
                                 "Click the \"Reset\" button where the \"Play\" button was, so you can continue programming.",

            dndTutorialPage11  = "As you noticed, the produce command does not tell your giraffes how to behave. For that, we need more commands.\n\n" +
                                 "Drag an \"Attack\" command into the next line in the editor.",

            dndTutorialPage12  = "The \"Attack\" command will order your giraffes to move to a location which you specify. Notice that the editor list is asking for a variable.\n\n" +
                                 "Click \"Next\" to continue",

            dndTutorialPage13  = "Variables are also found in your command list. Notice the difference in shape between variables and commands.\n\n" +
                                 "Now, drag the \"A\" variable into to slot next to the \"Attack\" command.",

            dndTutorialPage14  = "Your castle will now produce giraffes, and order them to go to checkpoint A. They will attack any enemies they encounter on the way.\n\n" +
                                 "Now, close the editor window by right-clicking.",

            dndTutorialPage15  = "Remember that checkpoint you saw earlier? Find it again, and click it.",

            dndTutorialPage16  = "This is the editor window for this checkpoint. All checkpoints on the map, as well as your castle, have their own editor with their own commands.\n\n" +
                                 "Click \"Next\" to continue.",

            dndTutorialPage17  = "This means you can give your giraffes different orders depending on where they are on the map.\n\n" +
                                 "Now, you'd prefer if your units have a bit of an edge, right? Drag a \"Defend\" order into the checkpoint's editor.",

            dndTutorialPage18  = "The Defend order will order your giraffes to stay near the checkpoint, and defend it.\n" +
                                 "In addition, your giraffes will receive less damage from enemy attacks whenever they are defending.\n\n" +
                                 "Click \"Next\" to continue.",

            dndTutorialPage19  = "Your are now ready to defend your castle. Click \"Play\" again, when you're ready.\n\n" +
                                 "Be aware that you cannot change your commands once you've clicked play, so make sure everything is in order.";

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    string textTutorialPage1   = "Welcome!\n" + "The elephants are attacking the castle and we need to defend it!\n" +
                                 "Your goal is to defeat all the elephants and take control of the checkpoint in order to win.\n\n" +
                                 "Click \"Next\" to continue.",

	    textTutorialPage2  = "You can use the W,A,S, and D-buttons to move the camera. Take a look around and note the location of the castle, and the checkpoint, " +
                                 "signified by a flag in a circle near the middle of the area.\n\n" +
                                 "Click \"Next\" to continue.",

            textTutorialPage3  = "At the top of the screen you can see your current Lives. You lose a life if an elephant makes it to your castle. If your lives drops to 0, you lose.\n\n"+
                                 "Click \"Next\" to continue.",

            textTutorialPage4  = "You control your giraffes by programming them with commands.\n\n Press \"Tab\" to open the command list.",

            textTutorialPage5  = "This is the command list. Here you can see all the commands that are available in this level. You can mouse over the commands to read a tooltip, " +
                                 "to learn what each command does.\n\n" +
                                 "Click \"Next\" to continue",

			textTutorialPage6  = "You can open and close the command list at any time by pressing \"Tab\", or cliking the white arrow next to the list.\n\n" +
                                 "Now, click the castle.",

            textTutorialPage7  = "This is the editor window. Here you will program your castle and giraffes to do what you want them to.\n\n" +
                                 "Click \"Next\" to continue",

            textTutorialPage8  = "You do this by typing the commands from the command list into the editor window, exactly as they are displayed, and finish by clicking the \"Compile Code\" button.\n \n" +
                                 "Now, type  \"produceUnits\" in the \"Script Editor\", and click compile.",

            textTutorialPage9  = "The \"produceUnits\" command is your most basic command. Only available at the castle, it continuously produces giraffes - up to a cap.\n\n" +
                                 "Click the \"Play\" button - the right-pointing arrow in the top of the screen - and lets see what happens.",

			textTutorialPage10 = "Congratulations, you produced your first giraffe, but what's this? He's not moving. We better do something about that.\n" +
                                 "Click the \"Reset\" button where the \"Play\" button was, so you can continue programming.",

            textTutorialPage11 = "You can now produce giraffes, but they will not do anything unless you order them to, so let us do that.\n \n" +
                                 "Click \"Next\" to continue.",

            textTutorialPage12 = "Enter the \"attack(X)\" command from the command list. You will need to replace the 'X' with the variable 'A', which is also visible in the command list.\n" +
                                 "Remember to click \"Compile Code\".",

            textTutorialPage13 = "The \"attack(X)\" command will order your giraffes to move to a target location, specified by a variable within the parentheses, and attack any enemies they encounter.\n" +
                                 "Click \"Next\" to continue.",

            textTutorialPage14 = "As you probably noticed, variables are also visible in the command list, and they have a different shape than the commands.\n \n" +
                                 "Now, close the editor window by right clicking.",

            textTutorialPage15 = "Remember that checkpoint you saw earlier? Find it again, and click it.",

            textTutorialPage16 = "This is the editor window for this checkpoint. All checkpoints on the map, as well as your castle, have their own editor with their own commands.\n\n" +
                                 "Click \"Next\" to continue.",

            textTutorialPage17 = "This means you can give your giraffes different orders depending on where they are on the map.\n\n" +
                                 "Now, you'd prefer if your units have a bit of an edge, right? Enter the \"defend\" command, and click the \"Compile Code\" button.",

            textTutorialPage18 = "The Defend order will order your giraffes to stay near the checkpoint, and defend it.\n" +
                                 "In addition, your giraffes will receive less damage from enemy attacks whenever they are defending.\n \n" +
                                 "Click \"Next\" to continue.",

            textTutorialPage19 = "Your are now ready to defend your castle. Click \"Play\" again, when you're ready.\n\n" +
                                 "Be aware that you cannot change your commands once you've clicked play, so make sure everything is in order.";

    public GUISkin commandSkin;

    levelManager lvlManager;
    mainMenuVariables varKeeper;

    // Use this for initialization
    void Start()
    {
        TutorialBoxStartPosX = (Screen.width / 2) - (Screen.width / 6);
        TutorialBoxStartPosY = (Screen.height / 2) + (Screen.height / 6);
        TutorialBoxHeight = (Screen.height / 4) + (Screen.height / 16);
        TutorialBoxWidth = Screen.width / 3;
        drawStartInfo = true;
        TutorialBox = new Rect(TutorialBoxStartPosX, TutorialBoxStartPosY, TutorialBoxWidth, TutorialBoxHeight);

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
            tutorialPage9 = dndTutorialPage9;
            tutorialPage10 = dndTutorialPage10;
            tutorialPage11 = dndTutorialPage11;
            tutorialPage12 = dndTutorialPage12;
            tutorialPage13 = dndTutorialPage13;
            tutorialPage14 = dndTutorialPage14;
            tutorialPage15 = dndTutorialPage15;
            tutorialPage16 = dndTutorialPage16;
            tutorialPage17 = dndTutorialPage17;
            tutorialPage18 = dndTutorialPage18;
            tutorialPage19 = dndTutorialPage19;
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
            tutorialPage9 = textTutorialPage9;
            tutorialPage10 = textTutorialPage10;
            tutorialPage11 = textTutorialPage11;
            tutorialPage12 = textTutorialPage12;
            tutorialPage13 = textTutorialPage13;
            tutorialPage14 = textTutorialPage14;
            tutorialPage15 = textTutorialPage15;
            tutorialPage16 = textTutorialPage16;
            tutorialPage17 = textTutorialPage17;
            tutorialPage18 = textTutorialPage18;
            tutorialPage19 = textTutorialPage19;
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
                    currentTutorialText = tutorialPage1;
                    requiresNextClickToProgress = true;
                    break;
                case 1:
                    currentTutorialText = tutorialPage2;
                    requiresNextClickToProgress = true;
                    break;
                case 2:
                    currentTutorialText = tutorialPage3;
                    requiresNextClickToProgress = true;
                    break;
                case 3:
                    currentTutorialText = tutorialPage4;
                    break;
                case 4:
                    currentTutorialText = tutorialPage5;
                    requiresNextClickToProgress = true;
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
                    break;
                case 8:
                    currentTutorialText = tutorialPage9;
                    break;
                case 9:
                    currentTutorialText = tutorialPage10;
                    break;
                case 10:
                    currentTutorialText = tutorialPage11;
                    if (!varKeeper.useDragonDrop)
                    {
                        requiresNextClickToProgress = true;
                    }
                    break;
                case 11:
                    currentTutorialText = tutorialPage12;
                    if (varKeeper.useDragonDrop)
                    {
                        requiresNextClickToProgress = true;
                    }
                    break;
                case 12:
                    currentTutorialText = tutorialPage13;
                    if (!varKeeper.useDragonDrop)
                    {
                        requiresNextClickToProgress = true;
                    }
                    break;
                case 13:
                    currentTutorialText = tutorialPage14;
                    break;
                case 14:
                    currentTutorialText = tutorialPage15;
                    break;
                case 15:
                    currentTutorialText = tutorialPage16;
                    requiresNextClickToProgress = true;
                    break;
                case 16:
                    currentTutorialText = tutorialPage17;
                    break;
                case 17:
                    currentTutorialText = tutorialPage18;
                    requiresNextClickToProgress = true;
                    break;
                case 18:
                    currentTutorialText = tutorialPage19;
                    break;
                case 19:
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

        if (drawStartInfo && lvlManager.currentLevel == 1)
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
