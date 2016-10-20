using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
	public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();
	public List<Variable> availableVariables = new List<Variable> ();
	private bool drawCommandList = false;
	int slotsX = 1;
	int slotsY = 12;
    public CommandDatabase database;
	float boxHeight = Screen.height / 24 - ((Screen.height / 24) / 10);
	float boxWidth = Screen.width / 8;
	float boxStartingPosX = (Screen.width - Screen.width / 7) - Screen.width / 80;
	float boxStartingPosY = Screen.height / 4;
	float boxOffSet = Screen.height / 24;
	float boundingBoxHeight;
	float boundingBoxWidth;
	float boundingBoxX;
	float boundingBoxY;

    void Start(){
		boundingBoxHeight = 12 * (boxHeight + ((Screen.height / 24) / 10)) + Screen.width/35;
		boundingBoxWidth = boxWidth + Screen.width / 40;
		boundingBoxX = boxStartingPosX - Screen.width/80;
		boundingBoxY = boxStartingPosY - Screen.width/70;

		for (int i = 0; i < (slotsX * slotsY); i++) {
			slots.Add (new Command ());
		}

        //Cache the database of commands so that we can always find any command we need.
		database = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

		for (int i = 0; i < database.commandDatabase.Count; i++) {
			availableCommands.Add (database.commandDatabase [i]);
			availableVariables.Add (database.variableDatabase [i]);
		}
    }

	void Update(){

		if(Input.GetButtonDown("Commandlist")){
			
			//TODO: fill lists, we need to find how many variables and commands are available when we open the list
			drawCommandList = !drawCommandList;
		}
	
	}

	void OnGUI(){
		
		if(drawCommandList){
			DrawCommandList ();
		}
		
	
	}

	void DrawCommandList(){
		
		float previousRectY =  boxStartingPosY;
		bool firstRectDrawn = false;
		GUI.Box (new Rect (boundingBoxX ,boundingBoxY, boundingBoxWidth ,boundingBoxHeight), "Command List");
		for (int x = 0; x < slotsX; x++) {
			for(int y = 0; y < slotsY; y++){
				if (firstRectDrawn == false) {	
					GUI.Box (new Rect (boxStartingPosX, previousRectY, boxWidth, boxHeight), "");
					firstRectDrawn = true;
				} else {
					previousRectY += boxOffSet;
					GUI.Box (new Rect (boxStartingPosX, previousRectY, boxWidth, boxHeight), "");
					
				}
			}
		}
	}

//	void DrawCommandList(){
//		int previousRectY = 0;
//		bool firstRectDrawn = false;
//		for (int x = 0; x < slotsX; x++) {
//			for(int y = 0; y < slotsY; y++){
//					GUI.Box (new Rect (10, 10*y ,100,100), y.ToString ());
//			}
//		}
//	}
}
