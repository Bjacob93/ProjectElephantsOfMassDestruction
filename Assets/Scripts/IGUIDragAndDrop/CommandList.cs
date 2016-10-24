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

	//Drag and Drop
	private bool draggingCommand;
	private Command draggedCommand;
	private int previousCommandIndex;

	//Tooltip
	private bool showToolTip;
	private string toolTip;

    //GUI apperance
    public GUISkin commandSkin;
    //public GUISkin tooltipSkin;

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


		//Add all available commands to the list.
		for (int i = 0; i < database.commandDatabase.Count; i++) {
			availableCommands.Add (database.commandDatabase [i]);
		}

        //Make the "slots" list contain the same elements as the "available" list
		for (int j = 0; j < availableCommands.Count; j++) {
			slots [j] = availableCommands [j];
		}
    }

	void Update(){

        //Open or close the Command List
		if(Input.GetButtonDown("Commandlist")){	
			drawCommandList = !drawCommandList;
		}
	}

	void OnGUI(){

        //Set the skin for the boxes
        GUI.skin = commandSkin;

        //Set the current tooltip string to be blank
		toolTip = "";

        //Draw the command list
		if(drawCommandList){
			DrawCommandList ();

		}

        //Show the tooltip at the mouse position
		if (showToolTip) {
			GUI.Box (new Rect (Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), toolTip, commandSkin.GetStyle("tooltipBackground"));

            //If the tooltip string is blank, stop drawing the tooltip
			if (toolTip == "") {
				showToolTip = false;
			}
		}

        //DragonDrop
		if(draggingCommand){
			GUI.Box (new Rect (Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), "<color=#000000>" + draggedCommand.commandName + "</color>", commandSkin.GetStyle ("commandSkin"));
		}

		
	
	}

    //Method that takes care of drawing the command list
	void DrawCommandList(){

        //Event handles mouse input
		Event e = Event.current;

        //Draw the bounding box.
        GUI.Box (new Rect (boundingBoxX ,boundingBoxY, boundingBoxWidth ,boundingBoxHeight), "Command List");

        //Variables for drawing the commands
        float previousRectY =  boxStartingPosY;
		bool firstRectDrawn = false;
		int slotNumber = 0;
		slotRect = new Rect(boxStartingPosX, previousRectY, boxWidth, boxHeight);

        //Nested for-loop draws the commands
		for (int x = 0; x < slotsX; x++) {
			for(int y = 0; y < slotsY; y++){

                //Specify the current command
				Command thisCommand = slots [slotNumber];

                //Draw the first command if it exists
				if (firstRectDrawn == false && slots [slotNumber].commandName != null) {
					GUI.Box (slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle ("commandSkin"));
                    
                    //Specify that the first command has been drawn
                    firstRectDrawn = true;

                    //Check if the mouse cursor is over the command and draw the tooltip if so
					if (slotRect.Contains (e.mousePosition)) {
						toolTip = CreateToolTip (thisCommand);
						showToolTip = true;

                        //Check if the mouse is dragging the current command
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingCommand) {
							draggingCommand = true;
							previousCommandIndex = slotNumber;
							draggedCommand = thisCommand;
							//availableCommands[slotNumber] = new Command(); //used to delete the dragged items so that it doesnt multiply/copy itself
						}
                        //Check if mouse stops dragging a command
						if (e.type == EventType.mouseUp && draggingCommand) {
							availableCommands [previousCommandIndex] = availableCommands [slotNumber];
							availableCommands [slotNumber] = draggedCommand;
							draggingCommand = false;
							draggedCommand = null;
						}
					}
				}
                //Draw the rest of the commands and - as above - check for mouse over and drag.
                else if (slots [slotNumber].commandName != null) {
					previousRectY += boxOffSet;
					slotRect = new Rect (boxStartingPosX, previousRectY, boxWidth, boxHeight);
					GUI.Box (slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle ("commandSkin"));	
					if (slotRect.Contains (e.mousePosition)) {
						toolTip = CreateToolTip (slots [slotNumber]);
						showToolTip = true;
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingCommand) {
							draggingCommand = true;
							previousCommandIndex = slotNumber;
							draggedCommand = thisCommand;
							//availableCommands[slotNumber] = new Command(); //used to delete the dragged items so that it doesnt multiply/copy itself
						}
						if (e.type == EventType.mouseUp && draggingCommand) {
							availableCommands [previousCommandIndex] = availableCommands [slotNumber];
							availableCommands [slotNumber] = draggedCommand;
							draggingCommand = false;
							draggedCommand = null;
						}
					}
				} else {
					if(slotRect.Contains (e.mousePosition)){
						if(e.type == EventType.mouseUp && draggingCommand){
							availableCommands [slotNumber] = draggedCommand;
							draggingCommand = false;
							draggedCommand = null;
						}
					}
				}
                //Increment the slot in the list we're currently interested in.
				slotNumber++;
			}
		}
	}

    //Method for returning the text for the tooltip
	string CreateToolTip(Command command){
		toolTip = command.commandDesc;
		return toolTip;
	}
}
