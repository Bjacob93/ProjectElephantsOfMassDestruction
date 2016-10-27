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

        //Go through all the slots in the Sequence Editor belonging to this checkpoint.
        for (int i = 0; i < listComponent.slots.Count; i++)
        {
            //If the slot is in the left of the editor and has a command in it.
            if (i % 2 == 0 && listComponent.slots[i].commandName != "")
            {
                //Save the id of the command in that slot to a string, and initialise a vector for the target coordinates of the command.
                string id = listComponent.slots[i].commandId;
                Vector3 target;

                //Switch that will catch the if of the command, and call the relevant function(s).
                switch (id)
                {
                    case "M01": //Move command.
                        //If a variable in the next slot exists.
                        if (listComponent.slots[i + 1].commandName != "")
                        {
                            //Get the target location of the command, and call the command.
                            target = listComponent.slots[i + 1].locationOfTarget;
                            Move(units, target);
                        }
                        break;

                    case "A01": //Attack command.
                        //If a variable in the next slot exists.
                        if (listComponent.slots[i + 1].commandName != "")
                        {
                            //Get the target location of the command, and call the command.
                            target = listComponent.slots[i + 1].locationOfTarget;
                            Attack(units, target);
                        }
                        break;

                    case "D01": //Defence command.
                        //If a variable in the next slot exists.
                        if (listComponent.slots[i + 1].commandName != "")
                        {
                            //Get the target location of the command, and call the command.
                            target = listComponent.slots[i + 1].locationOfTarget;
                            Defend(units, target);
                        }
                        break;
                    default:
                        break;
                }
            }
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