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
		commandDatabase.Add (new Command ("Attack", "A01", "<color=#FFFFF>" + "Move to a target location while attacking enemies on the way"+ "</color>", true, true, true, new Vector3(), 0, false));

		//defend orders
		commandDatabase.Add (new Command ("Defend", "D01", "<color=#FFFFF>" +"Defend the target location"+ "</color>", true, true, true, new Vector3(),0, false));

        //move orders
		commandDatabase.Add (new Command ("Move", "M01", "<color=#FFFFF>" + "Move to target location and ignore enemies on the way" + "</color>", true, true, true, new Vector3(),0, false));

        //production orders
		commandDatabase.Add (new Command ("Produce", "P01", "<color=#FFFFF>" +"Produce a unit at base" + "</color>", false, false, true, new Vector3(),0, false));

        //For every order
		commandDatabase.Add(new Command("Split ", "FoE", "<color=#FFFFF>" +"The next line will run for every x amout of times for a unit, eg if the next line is attack checkpoint A and the one after it is B the units will switch between the two as the target they are going to " + "</color>", true, true, true, new Vector3(),0, false));

        //variable commands for foe?
		commandDatabase.Add(new Command("Every other", "FoE2", "<color=#FFFFF>" +"This var splits the units' target so that the first unit goes to A while the second unit goes to B " +"</color>", false, true, true, new Vector3(), 2, true));

		commandDatabase.Add(new Command("Every third", "FoE3", "<color=#FFFFF>" +"This var splits the units' target so that the first and second unit goes to A while the third unit goes to B " + "</color>", false, true, true, new Vector3(), 3, true));

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        homebase = GameObject.FindGameObjectWithTag("PlayerBase");
        //scan for variables and automatically add them instead of doing it manually
        foreach (GameObject c in checkpoints)
        {
			commandDatabase.Add(new Command(c.name, "var" + c.name, "<color=#FFFFF>" +"This targets the location of checkpoint " + c.name +"</color>", false, true, true, c.transform.position,0, true));
        }

		commandDatabase.Add (new Command ("Homebase", "varPlayerBase", "<color=#FFFFF>" + "This targets the location of the players base. "+"</color>", false, true, true, homebase.transform.position,0, true));   
    }
}
