using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicCheckpointScript : MonoBehaviour {

    /**
     * This class will control the checkpoints. They will read the editorlist pertaining to each checkpoint and give the
     * relevant orders to units in range.
     */
    PauseScript pause;

    //Cache destination vectors
    public Vector3 meleeDistination;
    public Vector3 rangedDistination;
    public Vector3 location;

    //Cache SequenceEditor component.
    EditorList listComponent;
    textEditor textListComponent;
	SequenceManager sm;
    Level1TutorialText tutorialtext;
	public string checkpointName;

    levelManager lvlManager;

    //Range within which the checkpoint will give orders to units.
    public float controlRange = 10f;

    //Variables for the ForEvery() function
    bool forEveryRan = false;
    public int shrimp = 1;
    int fish;

    //Cache target waypoint
    Vector3 target;

    public float distance;

    //Cache the object which holds the variable for DnD or Text edit mode.
    mainMenuVariables varKeeper;

    //Cache list of all the checkpoints and bases in the level.
    List<GameObject> locations;

    void Start () {
        //Reference to the correct mainMenuVariables script.
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();

        //Find the SequenceManager, and same the name of the checkpoint to a string.
		sm = GameObject.Find ("UIManager").GetComponent<SequenceManager> ();


        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
        if(lvlManager.currentLevel == 1)
        tutorialtext = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();

        checkpointName = this.gameObject.name;

        pause = GameObject.Find("Pause").GetComponent<PauseScript>();

        //Save the location of this checkpoint to a variable.
        location = gameObject.transform.position;

        if (varKeeper.useDragonDrop){
            //Create an EditorList component attached to this game object and add it to the list in SequenceManager.
            listComponent = gameObject.AddComponent<EditorList>();
            listComponent.listID = checkpointName;
            listComponent.belongsToCheckpoint = true;
            sm.editorlistGO.Add(listComponent);
        }
        else
        {
            textListComponent = gameObject.AddComponent<textEditor>();
            textListComponent.listID = checkpointName;
            textListComponent.belongsToCheckpoint = false;
            sm.editorListText.Add(textListComponent);
        }
    }
	
	void Update () {

        //Find all player units.
        GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

        if (pause.gameIsPaused) {
            return;
        }

        if (!varKeeper.useDragonDrop)
        {
            gatherTextCommands(units);
        }
        else
        {
            gatherCommands(units);
        }
    }

    void gatherCommands(GameObject[] units)
    {
        //check if the slot is to the left in the editor
        for (int i = 0; i < listComponent.slots.Count; i++)
        {
            //check to see if the slot is empty
            if (i % 2 == 0 && listComponent.slots[i].commandName != "")
            {
                //crate a sting to keep track of the slots commandId
                string id = listComponent.slots[i].commandId;
                fish = listComponent.slots[i + 1].variableForEveryX;


                if (forEveryRan)
                {
                    shrimp++;
                    forEveryRan = false;
                }
                commandDragonDrop(i, id, units, fish);
            }
        }
    }

    void commandDragonDrop(int i, string id, GameObject[] units, int fisk)
    {
        //determine the corrent ation based in the commandId
        switch (id)
        {
            case "FoE":
                if (!forEveryRan)
                {
                    id = listComponent.slots[i].commandId;
                    if (shrimp % fish != 0)
                    {
                        commandDragonDrop(i + 2, id, units, fish);
                    }
                    else
                    {
                        commandDragonDrop(i + 3, id, units, fish);
                    }
                }
                break;


            case "A01":
                //if attack command is initiated set target to the slot var to the left of the attack command
                target = listComponent.slots[i + 1].locationOfTarget;
                // run the attack command
                Attack(units, target);
                break;

            case "M01":
                target = listComponent.slots[i + 1].locationOfTarget;
                //run the move command
                Move(units, target);
                break;

            case "D01":
                target = listComponent.slots[i + 1].locationOfTarget;
                //run the defend command
                Defend(units, target);
                break;

            default:

                break;
        }

    }
    
    void gatherTextCommands(GameObject[] units)
    {
        //check if the slot is to the left in the editor
        for (int i = 0; i < textListComponent.listOfCommands.Count; i++)
        {
            //crate a sting to keep track of the slots commandId
            string id = textListComponent.listOfCommands[i].commandId;

            if(textListComponent.listOfCommands.Count > i + 1)
            {
                fish = textListComponent.listOfCommands[i + 1].variableForEveryX;
            }

            if (forEveryRan)
            {
                shrimp++;
                forEveryRan = false;
            }
            commandTextEditor(i, id, units, fish);
        }
    }

    void commandTextEditor(int i, string id, GameObject[] units, int fisk)
    {
        //determine the corrent ation based in the commandId
        switch (id)
        {
            case "FoE":
                if (!forEveryRan)
                {
                    id = textListComponent.listOfCommands[i].commandId;
                    if (shrimp % fish != 0)
                    {
                        commandTextEditor(i + 2, id, units, fish);
                    }
                    else
                    {
                        commandTextEditor(i + 3, id, units, fish);
                    }
                }
                break;


            case "A01":
                //if attack command is initiated set target to the slot var to the left of the attack command
                target = textListComponent.listOfCommands[i + 1].locationOfTarget;
                // run the attack command
                Attack(units, target);
                break;

            case "M01":
                target = textListComponent.listOfCommands[i + 1].locationOfTarget;
                //run the move command
                Move(units, target);
                break;

            case "D01":
                target = textListComponent.listOfCommands[i + 1].locationOfTarget;
                //run the defend command
                Defend(units, target);
                break;

            default:

                break;
        }
    }

    void Move(GameObject[] units, Vector3 targetLocation)
    {
        //For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                Astar aStar = e.GetComponent<Astar>();
                if (checkpointName != aStar.commanderID)
                {
                    aStar.targetPosition = targetLocation;
                    aStar.receivedNewDestination = true;
                    aStar.commanderID = checkpointName;
                    forEveryRan = true;
                }
            }
        }
    }

    void Attack(GameObject[] units, Vector3 targetLocation)
    {
        //For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
        foreach (GameObject e in units)
        {
            distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                Astar aStar = e.GetComponent<Astar>();
                if (checkpointName != aStar.commanderID)
                {
                    aStar.targetPosition = targetLocation;
                    aStar.receivedNewDestination = true;
                    aStar.commanderID = checkpointName;
                    forEveryRan = true;
                }
                aStar.receivedNewDestination = true;
            }
        }
    }

    void Defend(GameObject[] units, Vector3 targetLocation)
    {
        //For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                Astar aStar = e.GetComponent<Astar>();
                if (checkpointName != aStar.commanderID)
                {
                    aStar.targetPosition = targetLocation;
                    aStar.commanderID = checkpointName;
                    aStar.receivedDefenceOrder = true;
                    forEveryRan = true;
                }
            }
        }
    }

	void OnMouseDown(){

        if(lvlManager.currentLevel == 1 && tutorialtext.chechpointEditorOpened == false)
        {
            tutorialtext.chechpointEditorOpened = true;
        }

        //If the mouse is clicked on the object, go through the list of SequenceEditors.
        if (varKeeper.useDragonDrop)
        {
            for (int i = 0; i < sm.editorlistGO.Count; i++)
            {
                //If a list has an id equal to the checkpoint name, draw that editor window in the UI.
                if (sm.editorlistGO[i].listID == checkpointName)
                {
                    sm.editorlistGO[i].drawEditorWindow = true;
                }
                //Every list which id is different from the checkpoint name should not be drawn.
                else
                {
                    sm.editorlistGO[i].drawEditorWindow = false;
                }
            }
        }else
        {
            for (int i = 0; i < sm.editorListText.Count; i++)
            {
                //If a list has an id equal to the checkpoint name, draw that editor window in the UI.
                if (sm.editorListText[i].listID == checkpointName)
                {
                    sm.editorListText[i].drawSequenceEditor = true;
                }
                //Every list which id is different from the checkpoint name should not be drawn.
                else
                {
                    sm.editorListText[i].drawSequenceEditor = false;
                }
            }
        }
	}
}