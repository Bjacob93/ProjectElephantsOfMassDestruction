using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
	public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();
	private bool drawCommandList = false;
    public CommandDatabase database;


	//variables to draw the UI
	int slotsX = 1;
	int slotsY = 12;
	float boxHeight = Screen.height / 24 - ((Screen.height / 24) / 10);
	float boxWidth = Screen.width / 8;
	float boxStartingPosX = (Screen.width - Screen.width / 7) - Screen.width / 80;
	float boxStartingPosY = Screen.height / 4;
	float boxOffSet = Screen.height / 24;
	float boundingBoxHeight;
	float boundingBoxWidth;
	float boundingBoxX;
	float boundingBoxY;
	Rect slotRect;
	//Tooltip
	private bool showToolTip;
	private string toolTip;

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


		//
		for (int i = 0; i < database.commandDatabase.Count; i++) {
			availableCommands.Add (database.commandDatabase [i]);
		}

		for (int j = 0; j < availableCommands.Count; j++) {
			slots [j] = availableCommands [j];
		}

    }

	void Update(){

		if(Input.GetButtonDown("Commandlist")){	
			//TODO: fill lists, we need to find how many variables and commands are available when we open the list
			drawCommandList = !drawCommandList;
		}
	
	}

	void OnGUI(){

		toolTip = "";
		if(drawCommandList){
			DrawCommandList ();

		}
		if (showToolTip) {
			GUI.Box (new Rect (Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 100, 40), toolTip);

			if (toolTip == "") {
				showToolTip = false;
			}
		}

		
	
	}

	void DrawCommandList(){
		
		float previousRectY =  boxStartingPosY;
		bool firstRectDrawn = false;
		GUI.Box (new Rect (boundingBoxX ,boundingBoxY, boundingBoxWidth ,boundingBoxHeight), "Command List");
		int slotNumber = 0;
		slotRect = new Rect(boxStartingPosX, previousRectY, boxWidth, boxHeight);
		for (int x = 0; x < slotsX; x++) {
			for(int y = 0; y < slotsY; y++){

				Command thisCommand = slots [slotNumber];
				if (firstRectDrawn == false && slots[slotNumber].commandName != null) {		
					GUI.Box (slotRect, "<color=#000000>" +  thisCommand.commandName  + "</color>");
					firstRectDrawn = true;
					if (slotRect.Contains (Event.current.mousePosition)) {
						toolTip = CreateToolTip (thisCommand);
						showToolTip = true;
					}
				} else if(slots[slotNumber].commandName != null) {
					previousRectY += boxOffSet;
					slotRect = new Rect(boxStartingPosX, previousRectY, boxWidth, boxHeight);
					GUI.Box (slotRect, "<color=#000000>" +  thisCommand.commandName  + "</color>");	
					if (slotRect.Contains (Event.current.mousePosition)) {
						toolTip = CreateToolTip (slots [slotNumber]);
						showToolTip = true;
					}
				}
				slotNumber++;
			}
		}
	}

	string CreateToolTip(Command command){
		toolTip = command.commandDesc;
		return toolTip;
	}
}
