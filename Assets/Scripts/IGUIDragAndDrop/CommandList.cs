using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
	public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();
	private bool drawCommandList = false;
    public CommandDatabase database;

    public EditorList sequenceEditor;


	//variables to draw the UI
	int slotsX = 1;
	int slotsY = 12;
	float boxHeight = Screen.height / 24 - ((Screen.height / 24) / 10);
	float boxWidth = Screen.width / 8;
	float boxStartingPosX = (Screen.width - Screen.width / 7) - Screen.width / 80;
	float boxStartingPosY = Screen.height / 4;
	float boxOffSetY = Screen.height / 24;
	float boundingBoxHeight;
	float boundingBoxWidth;
	float boundingBoxX;
	float boundingBoxY;
	Rect slotRect;

	//Tooltip
	private bool showToolTip;
	private string toolTip;

    //GUI apperance
    public GUISkin commandSkin;
    //public GUISkin tooltipSkin;

    void Start(){
		boundingBoxHeight = slotsY * (boxHeight + ((Screen.height / 24) / 10)) + Screen.width/35;
		boundingBoxWidth = boxWidth + Screen.width / 40;
		boundingBoxX = boxStartingPosX - Screen.width/80;
		boundingBoxY = boxStartingPosY - Screen.width/70 - 5;

		for (int i = 0; i < (slotsX * slotsY); i++) {
			slots.Add (new Command ());
            availableCommands.Add(new Command());
		}

        //Cache the database of commands so that we can always find any command we need.
		database = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

        //Cache the sequenceEditor
        sequenceEditor = GameObject.FindGameObjectWithTag("EditorList").GetComponent<EditorList>();

		//Add all available commands to the list.
        for (int i = 0; i < database.commandDatabase.Count; i++)
        {
            availableCommands[i] = database.commandDatabase[i];
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
		if(sequenceEditor.isDraggingCommand){
			GUI.Box (new Rect (Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), "<color=#000000>" + sequenceEditor.draggedCommand.commandName + "</color>", commandSkin.GetStyle ("commandSkin"));
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
		int slotNumber = 0;

        //Nested for-loop draws the commands
		for (int x = 0; x < slotsX; x++) {
			for(int y = 0; y < slotsY; y++){

                slotRect = new Rect(boxStartingPosX, previousRectY + y * boxOffSetY, boxWidth, boxHeight);

                //Specify the current command
                Command thisCommand = slots [slotNumber];

                //Draw the first command if it exists
                if (slots[slotNumber].commandName != "")
                {
                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandSkin"));

                    //Check if the mouse cursor is over the command and draw the tooltip if so
                    if (slotRect.Contains(e.mousePosition))
                    {
                        toolTip = CreateToolTip(thisCommand);
                        showToolTip = true;

                        //Call the function that handles drag and drop
                        DragonDrop(slotNumber, thisCommand);
                    }
                }

                //Check if drag release
                CheckForRelease(slotNumber);
                slotNumber++;                
			}
		}
	}

    //Method for DragonDrop
    void DragonDrop(int slotNumber, Command thisCommand)
    {

        Event e = Event.current;

        //Check if the mouse is dragging the current command
        if (e.button == 0 && e.type == EventType.mouseDrag && !sequenceEditor.isDraggingCommand)
        {
            sequenceEditor.isDraggingCommand = true;
            sequenceEditor.isDraggingCommand = true;
            sequenceEditor.draggedCommand = thisCommand;
            sequenceEditor.draggedCommand = thisCommand;
        }
    }

    void CheckForRelease(int slotNumber)
    {
        Event e = Event.current;

        //This statements handles placing commands in different slots
        if (slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && sequenceEditor.isDraggingCommand)
        {
            availableCommands[slotNumber] = sequenceEditor.draggedCommand;
            sequenceEditor.isDraggingCommand = false;
            sequenceEditor.draggedCommand = null;

        }// if left mouse click is lifted outside in the command list it will strop the draggin of a command and null the stored values in the drag int.
        else if (!slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && sequenceEditor.isDraggingCommand)
        {
            for (int i = 0; i < sequenceEditor.slotPositions.Count; i++)
            {
                slotRect = sequenceEditor.slotPositions[i];

                if (slotRect.Contains(e.mousePosition))
                {
                    sequenceEditor.enteredCommands[i] = sequenceEditor.draggedCommand;
                }
            }


            sequenceEditor.isDraggingCommand = false;
            sequenceEditor.draggedCommand = null;
        }
    }

    //Method for returning the text for the tooltip
	string CreateToolTip(Command command){
		toolTip = command.commandDesc;
		return toolTip;
	}
}
