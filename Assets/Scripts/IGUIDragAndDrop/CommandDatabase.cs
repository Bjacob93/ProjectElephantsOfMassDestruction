using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandDatabase : MonoBehaviour {
	public List<Command> command = new List<> (Command);


	void Start(){
		command.Add (new Command ());
	
	}
}
