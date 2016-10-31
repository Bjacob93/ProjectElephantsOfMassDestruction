using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicCheckpointScript : MonoBehaviour {

    /**
     * This class will control the checkpoints. They will read the editorlist pertaining to each checkpoint and give the
     * relevant orders to units in range.
     */

    //Cache destination vectors
    public Vector3 meleeDistination;
    public Vector3 rangedDistination;
    public Vector3 location;

    //Cache SequenceEditor component.
    EditorList listComponent;
	SequenceManager sm;
	string checkpointName;

    //Range within which the checkpoint will give orders to units.
    public float controlRange = 10f;

    //Variables for the ForEvery() function
    bool forEveryRan = false;
    int shrimp = 1;
    int fish;

    //Cache target waypoint
    Vector3 target;

    void Start () {
        //Find the SequenceManager, and same the name of the checkpoint to a string.
		sm = GameObject.Find ("UIManager").GetComponent<SequenceManager> ();
		checkpointName = this.gameObject.name;

        //Save the location of this checkpoint to a variable.
        location = gameObject.transform.position;

        //Create an EditorList component attached to this game object and add it to the list in SequenceManager.
        listComponent = gameObject.AddComponent<EditorList>();
        listComponent.listID = checkpointName;
        listComponent.belongsToCheckpoint = true;
        sm.editorlistGO.Add(listComponent);

        
    }
	
	void Update () {

        //Find all player units.
        GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

        //check if the slot is to the left in the editor
        for (int i = 0; i < listComponent.slots.Count; i++)
        {
            //check to see if the slot is empty
            if (i % 2 == 0 && listComponent.slots[i].commandName != "")
            {
                //crate a sring to keep track of the slots commandId
                string id = listComponent.slots[i].commandId;
                fish = listComponent.slots[i + 1].variableForEveryX;


                if (forEveryRan == true)
                {
                    shrimp++;
                    forEveryRan = false;
                }
                command(i, id, units, fish);
            }
        }
    }

    void command(int i, string id, GameObject[] units, int fisk)
    {
        //determine the corrent ation based in the commandId
        switch (id)
        {
            case "FoE":
                if (forEveryRan == false)
                {
                    id = listComponent.slots[i].commandId;
                    if (shrimp % fish != 0)
                    {
                        command(i + 2, id, units, fish);
                    }
                    else
                    {
                        command(i + 3, id, units, fish);
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
                    aStar.receivedNewDestination = true;
                    aStar.commanderID = checkpointName;
                    aStar.receivedDefenceOrder = true;
                    forEveryRan = true;
                }
            }
        }
    }

	void OnMouseDown(){
        //If the mouse is clicked on the object, go through the list of SequenceEditors.

        for(int i = 0; i < sm.editorlistGO.Count; i++)
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
	}
}