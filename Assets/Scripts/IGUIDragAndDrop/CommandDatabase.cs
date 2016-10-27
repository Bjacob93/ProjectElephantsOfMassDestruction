using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandDatabase : MonoBehaviour {
	public List<Command> commandDatabase = new List<Command>();

	void Start(){
		//Use this srcipt to create new commands as below and store them in the database.
  
		    //attack orders
		    commandDatabase.Add (new Command ("Attack", "A01", "Move to a checkpoint while attacking enemies on the way"));

		    //defend orders
		    commandDatabase.Add (new Command ("Defend", "D01", "Defend the current position"));

		    //production orders
		    commandDatabase.Add (new Command ("Produce", "P01", "Produce a unit at base"));

		//TODO: scan for variables and automatically add them instead of doing it manually

		commandDatabase.Add (new Command ("checkpoint A", "varA", "location of checkpoint A"));

		commandDatabase.Add (new Command ("checkpoint B", "varB", "location of checkpoint B"));

		commandDatabase.Add (new Command ("Homebase", "varGiraffeBase", "location of checkpoint Homebase"));
	}
}
