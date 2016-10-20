using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
	public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();
	private bool drawCommandList;
	public int slotsX = 1, slotsY = availableCommands.Count;

    public CommandDatabase commandDatabase;

    void Start(){
		for (int i = 0; i < (slotsX * slotsY); i++) {
			slots.Add (new Command ());
		}

        //Cache the database of commands so that we can always find any command we need.
        commandDatabase = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();
    }

	void Update(){
		if(Input.GetButtonDown("Commandlist")){
			drawCommandList = !drawCommandList;
		}
	
	}

	void OnGUI(){

		if(drawCommandList){
			DrawCommandList ();

		}
		
	
	}

	void DrawCommandList(){
		int previousRectY = 0;
		bool firstRectDrawn = false;
		for(int y = 0; y < slotsY; y++){
			for (int x = 0; x < slotsX; x++) {
				if (firstRectDrawn = false){
					GUI.Box (new Rect(Screen.width - (Screen.width/20), Screen.height/2 - (Screen.height/4), Screen.width/8, Screen.height/2), y.ToString());
				}
			}
		}
	}
}
