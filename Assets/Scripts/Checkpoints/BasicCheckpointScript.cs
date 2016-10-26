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

    //Cache SequenceEditor component.
    EditorList listComponent;

    //Range within which the checkpoint will give orders to units.
    public float controlRange = 10f;

    //Cache the SequenceManager and the name of the checkpoint.
	SequenceManager sm;
	string checkpointName;

  

    //Bools that determines what kind of order a checkpoint should issue units.
    bool giveMoveOrder = false;
    bool giveDefenceOrder = false;
    bool giveAttackOrder = false;

	void Start () {
        //Find the SequenceManager, and same the name of the checkpoint to a string.
		sm = GameObject.Find ("UIManager").GetComponent<SequenceManager> ();
		checkpointName = this.gameObject.name;

        //Add the sequenceEditor component to the list in SequenceManager.
        listComponent = gameObject.AddComponent<EditorList>();
        listComponent.listID = checkpointName;

        sm.editorlistGO.Add(listComponent);
    }
	
	void Update () {
        //Find all player units.
        GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

        //Issue the correct order
        if (giveMoveOrder)
        {
            Move(units);
        }else if (giveAttackOrder)
        {
            Attack(units);
        }else if (giveDefenceOrder)
        {
            Defend(units);
        }

        
	}

    void Move(GameObject[] units)
    {
        //For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
            }
        }
    }

    void Attack(GameObject[] units)
    {
        //For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                //TODO: Check melee or ranged
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
            }
        }
    }

    void Defend(GameObject[] units)
    {
        //For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                //TODO: Check melee or ranged
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
                aStar.receivedDefenceOrder = true;
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