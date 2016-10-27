using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandDatabase : MonoBehaviour {
	public List<Command> commandDatabase = new List<Command>();
    GameObject[] checkpoints;
    GameObject homebase;

	void Start(){
		//Use this srcipt to create new commands as below and store them in the database.
  
	    //attack orders
		commandDatabase.Add (new Command ("Attack", "A01", "Move to a checkpoint while attacking enemies on the way", true, true, true, new Vector3()));

		//defend orders
		commandDatabase.Add (new Command ("Defend", "D01", "Defend the current position", true, true, true, new Vector3()));

		//production orders
		commandDatabase.Add (new Command ("Produce", "P01", "Produce a unit at base", false, false, true, new Vector3()));

        //TODO: scan for variables and automatically add them instead of doing it manually

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        homebase = GameObject.FindGameObjectWithTag("PlayerBase");

        foreach (GameObject c in checkpoints)
        {
            commandDatabase.Add(new Command("checkpoint " + c.name, "var" + c.name, "location of checkpoint " + c.name, false, true, true, c.transform.position));
        }

		commandDatabase.Add (new Command ("Homebase", "varPlayerBase", "location of Homebase", false, true, true, homebase.transform.position));
	}
}
