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
		commandDatabase.Add (new Command ("Attack", "A01", "Move to a target location while attacking enemies on the way", true, true, true, new Vector3(), 0));

		//defend orders
		commandDatabase.Add (new Command ("Defend", "D01", "Defend the target location", true, true, true, new Vector3(),0));

        //move orders
        commandDatabase.Add (new Command ("Move", "M01", "Move to target location and ignore enemies on the way", true, true, true, new Vector3(),0));

        //production orders
        commandDatabase.Add (new Command ("Produce", "P01", "Produce a unit at base", false, false, true, new Vector3(),0));

        //For every order
        commandDatabase.Add(new Command("For every - ", "FoE", "this is a while loop. fx for every 2 will make it do the first line every 2nd time and thrid line the other time", true, true, true, new Vector3(),0));       

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        homebase = GameObject.FindGameObjectWithTag("PlayerBase");
        //scan for variables and automatically add them instead of doing it manually
        foreach (GameObject c in checkpoints)
        {
            commandDatabase.Add(new Command("checkpoint " + c.name, "var" + c.name, "location of checkpoint " + c.name, false, true, true, c.transform.position,0));
        }

		commandDatabase.Add (new Command ("Homebase", "varPlayerBase", "location of Homebase", false, true, true, homebase.transform.position,0));

   //variable commands for foe?
        commandDatabase.Add(new Command("ones", "foeone", "this is a variable containing the number 1", false, true, true, new Vector3(),1));

        commandDatabase.Add(new Command("twice", "foetwice", "this is a variable containing the number 2", false, true, true, new Vector3(),2));

        commandDatabase.Add(new Command("thrice", "foethrice", "this is a variable containing the number 3", false, true, true, new Vector3(),3));
    }
}
