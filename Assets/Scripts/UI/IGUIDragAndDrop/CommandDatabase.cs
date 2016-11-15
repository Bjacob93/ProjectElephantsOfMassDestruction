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
		commandDatabase.Add (new Command ("Attack", "A01", "<color=#FFFFF>" + "Move to a target location while attacking enemies on the way"+ "</color>", "",  true, true, true, new Vector3(), 0, false, 1));

		//defend orders
		commandDatabase.Add (new Command ("Defend", "D01", "<color=#FFFFF>" +"Defend the target location"+ "</color>", "", true, true, true, new Vector3(),0, false, 1));

        //move orders
		commandDatabase.Add (new Command ("Move", "M01", "<color=#FFFFF>" + "Move to target location and ignore enemies on the way" + "</color>", "", true, true, true, new Vector3(),0, false, 10));

        //production orders
		commandDatabase.Add (new Command ("Produce Unit", "P01", "<color=#FFFFF>" +"Produce a unit at base" + "</color>", "", false, false, true, new Vector3(),0, false, 1));

        //For every order
		commandDatabase.Add(new Command("Split ", "FoE", "<color=#FFFFF>" +"The next line will run for every x amout of times for a unit, eg if the next line is attack checkpoint A and the one after it is B the units will switch between the two as the target they are going to " + "</color>", "", true, true, true, new Vector3(),0, false, 2));

        //variable commands for foe?
		commandDatabase.Add(new Command("Every other", "FoE2", "<color=#FFFFF>" +"This var splits the units' target so that the first unit goes to A while the second unit goes to B " +"</color>", "", false, true, true, new Vector3(), 2, true, 2));

		commandDatabase.Add(new Command("Every third", "FoE3", "<color=#FFFFF>" +"This var splits the units' target so that the first and second unit goes to A while the third unit goes to B " + "</color>", "", false, true, true, new Vector3(), 3, true, 3));

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        //scan for variables and automatically add them instead of doing it manually
        foreach (GameObject c in checkpoints)
        {
			commandDatabase.Add(new Command(c.name, "var" + c.name, "<color=#FFFFF>" +"This targets the location of checkpoint " + c.name +"</color>", "", false, true, true, c.transform.position,0, true, 1));
        }
    }
}
