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
		commandDatabase.Add (new Command ("Attack", "attack(X)", "A01", "<color=#FFFFF>" + "Move to a target location while attacking enemies on the way"+ "</color>", "Order units to move to the target location and attack all enemies on their way. Exchange the X with the name of the target chechkpoint.",  true, true, true, new Vector3(), 0, false, 1));

        //defend orders
        commandDatabase.Add(new Command("Defend", "defend", "D01", "<color=#FFFFF>" + "Defend the target location" + "</color>", "Order units to defend this position. Units under this command receive 5 less damage from all enemy attacks.", false, true, false, new Vector3(),0, false, 1));

        //move orders
		//commandDatabase.Add (new Command ("Move", "move", "M01", "<color=#FFFFF>" + "Move to target location and ignore enemies on the way" + "</color>", "Order units to move to the target location and ignore all enemies on their way. Exchange the X with the name of the target chechkpoint.", true, true, true, new Vector3(),0, false, 10));

        //production orders
		commandDatabase.Add (new Command ("Produce Unit", "produce", "P01", "<color=#FFFFF>" + "Produce a unit. Only available at the castle." + "</color>", "Produce a unit. Only available at the castle.", false, false, true, new Vector3(),0, false, 1));

        //For every order
		commandDatabase.Add(new Command("Split", "splitAt(X)", "FoE", "<color=#FFFFF>" +"Issue different commands to every X units. The next two lines in the editor must contain commands. \n \n Example: If 'Every Other' is chosen as parameter, every other unit will be issued the first command after this, while the rest are issued the second command after this." + "</color>", "Issue different commands to every X units. Two more commands must be specified on the lines below where this is entered. \n \n Example: if '2' is entered in the parentheses, the next command in the window will be issued to every other unit, while the rest are issued the command after that.", true, true, true, new Vector3(),0, false, 2));

        //variable commands for foe?
		commandDatabase.Add(new Command("Every other", "2", "FoE2", "<color=#FFFFF>" +"Issues a unique command to every other unit." +"</color>", "Issues a unique command to every other unit.", false, true, true, new Vector3(), 2, true, 2));

		commandDatabase.Add(new Command("Every third", "3", "FoE3", "<color=#FFFFF>" + "Issues a unique command to every third unit." + "</color>", "Issues a unique command to every third unit.", false, true, true, new Vector3(), 3, true, 3));

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        //scan for variables and automatically add them instead of doing it manually
        foreach (GameObject c in checkpoints)
        {
			commandDatabase.Add(new Command(c.name, c.name, "var" + c.name, "<color=#FFFFF>" +"This targets the location of checkpoint " + c.name +"</color>", "<color=#FFFFF>" + "This targets the location of checkpoint " + c.name +"</color>", false, true, true, c.transform.position,0, true, 1));
        }
    }
}
