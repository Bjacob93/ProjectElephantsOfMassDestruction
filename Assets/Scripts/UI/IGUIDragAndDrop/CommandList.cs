using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandList : MonoBehaviour {

    /**
     * This class handles the CommandList window.
     */

    //Initialise lists. The availableCommands list holds all the commands the list should contain. 
    //The slots list holds the actual slots.
	public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();

    //Bool handles whether or not to draw the window in the UI.
	private bool drawCommandList = false;

    //Cache the database, SequenceManger and Editolist scripts, as well as the LevelManager.
    public CommandDatabase database;
    public SequenceManager sequenceManager;
    public EditorList sequenceEditor;
    levelManager lvlManager;

	//The number of slots in the window
	int numberOfSlots = 12;

    //Variables that hold the dimensions of the bounding box of the window.
    float boundingBoxHeight;
    float boundingBoxWidth;
    float boundingBoxX;
    float boundingBoxY;
    public Rect boundingRect;

    //Variables the hold the dimensions of the slots
    float boxHeight = (Screen.height / 24) - ((Screen.height / 24) / 10);
	float boxWidth = (Screen.width / 8);
	float boxStartingPosX = (Screen.width - (Screen.width / 7)) - (Screen.width / 80);
	float boxStartingPosY = Screen.height / 4;
	float boxOffSetY = Screen.height / 24;
	Rect slotRect;
    Level1TutorialText tutorialtext;

	//Tooltip.
	private bool showToolTip;
	private string toolTip;

    //GUI appearence.
    public GUISkin commandSkin;

    void Start(){

        //Reference the database of commands so that we can always find any command we need.
        database = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

        //Reference the Sequence Manager script.
        sequenceManager = GameObject.Find("UIManager").GetComponent<SequenceManager>();
        tutorialtext = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();

        //Reference the levelManager.
        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();

        //Calculate the bounding box dimensions and define the resulting Rect.
        boundingBoxHeight = numberOfSlots * (boxHeight + ((Screen.height / 24) / 10)) + (Screen.width/35);
		boundingBoxWidth = boxWidth + (Screen.width / 40);
		boundingBoxX = boxStartingPosX - (Screen.width/80);
		boundingBoxY = (boxStartingPosY - (Screen.width/70)) - 5;
        boundingRect = new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight);

        //Fill the lists with empty commands.
		for (int i = 0; i < numberOfSlots; i++) {
			slots.Add (new Command ());
            availableCommands.Add(new Command());
		}

		//Add all available commands to the list.
        for (int i = 0; i < database.commandDatabase.Count; i++)
        {
            if(database.commandDatabase[i].lvlAvailability <= lvlManager.getCurrentLevel())
            {
                availableCommands[i] = database.commandDatabase[i];
            }
        }

        //Make the "slots" list contain the same elements as the "available" list.
		for (int j = 0; j < availableCommands.Count; j++) {
			slots [j] = availableCommands [j];
		}
    }

	void Update(){

        //Open or close the Command List.
		if(Input.GetButtonDown("Commandlist")){	
			drawCommandList = !drawCommandList;
            if(tutorialtext.qHasBeenPressed == false)
            {
                tutorialtext.qHasBeenPressed = true;
            } 
		}
	}

	void OnGUI(){

        //Set the skin for the boxes.
        GUI.skin = commandSkin;

        //Set the current tooltip string to be blank.
		toolTip = "";

        //Draw the command list.
		if(drawCommandList){
            //Go through all instances of EditorList.
            for(int i = 0; i < sequenceManager.editorlistGO.Count; i++)
            {
                //If one of them is currently open in the UI, make that the reference point for sequenceEditor.
                if (sequenceManager.editorlistGO[i].drawEditorWindow)
                {
                    sequenceEditor = sequenceManager.editorlistGO[i];
                    break;
                }
                if(!sequenceManager.editorlistGO[i].drawEditorWindow && i == sequenceManager.editorlistGO.Count - 1)
                {
                    sequenceEditor = null;
                }
            }
            DrawCommandList ();
		}

        //Show the tooltip at the mouse position.
		if (showToolTip) {
            float toolTipHeight = toolTip.Length / 1.4f;

            if(Event.current.mousePosition.x > Screen.width - (boxWidth-2))
            {
                GUI.Box(new Rect(Event.current.mousePosition.x - (boxWidth * 1.02f), Event.current.mousePosition.y, 200, toolTipHeight), toolTip, commandSkin.GetStyle("tooltipBackground"));
            }
            else
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, toolTipHeight), toolTip, commandSkin.GetStyle("tooltipBackground"));
            }
            

            //If the tooltip string is blank, stop drawing the tooltip.
			if (toolTip == "") {
				showToolTip = false;
			}
		}

        //Draw a representation of the command at the mouse position if a command is being dragged.
        if (sequenceEditor != null) {
            if (sequenceEditor.isDraggingCommand) {
                if (sequenceEditor.draggedCommand.isVariable)
                {
                    GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), "<color=#000000>" + sequenceEditor.draggedCommand.commandName + "</color>", commandSkin.GetStyle("variableAvailable"));

                }else
                {
                    GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), "<color=#000000>" + sequenceEditor.draggedCommand.commandName + "</color>", commandSkin.GetStyle("commandAvailable"));

                }
            }
        }
	}

    //Method that takes care of drawing the command list.
	void DrawCommandList(){

        //Event handles mouse input.
		Event e = Event.current;

        //Draw the bounding box.
        GUI.Box (new Rect (boundingBoxX ,boundingBoxY, boundingBoxWidth ,boundingBoxHeight), "Command List");

        //Variables for drawing the commands.
        float previousRectY =  boxStartingPosY;
		int slotNumber = 0;

        //For-loop draws the commands.
		for(int y = 0; y < numberOfSlots; y++){

            //Define the Rect for the current slot.
            slotRect = new Rect(boxStartingPosX, previousRectY + y * boxOffSetY, boxWidth, boxHeight);

            //Specify the current command
            Command thisCommand = slots [slotNumber];

            //Draw the first command if it exists
            if (slots[slotNumber].commandName != "")
            {
                if(sequenceEditor != null)
                {
                    if (sequenceEditor.belongsToCheckpoint)
                    {
                        if (!thisCommand.availableAtCheckpoint)
                        {
                            if (thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                            }
                            else
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandUnavailable"));

                            }
                        }
                        else
                        {
                            bool orderHasBeenEntered = false;
                            bool forEveryHasBeenUsed = false;

                            for (int i = 0; i < sequenceEditor.slots.Count; i++)
                            {
                                if (i % 2 == 0 )
                                {
                                    if (sequenceEditor.slots[i].commandId == "A01" || sequenceEditor.slots[i].commandId == "D01" || sequenceEditor.slots[i].commandId == "M01")
                                    {
                                        orderHasBeenEntered = true;
                                    }
                                    else if (sequenceEditor.slots[i].commandId == "FoE")
                                    {
                                        forEveryHasBeenUsed = true;
                                    }   
                                }
                            }
                            if (!orderHasBeenEntered && (thisCommand.commandId == "varA" || thisCommand.commandId == "varB" || thisCommand.commandId == "varC" || thisCommand.commandId == "varD" || thisCommand.commandId == "varPlayerBase"))
                            {
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                                else
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandUnavailable"));

                                }
                            }
                            else if(!forEveryHasBeenUsed && (thisCommand.commandId == "FoE2" || thisCommand.commandId == "FoE3"))
                            {
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                                else
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandUnavailable"));

                                }
                            }
                            else
                            {
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableAvailable"));

                                }
                                else
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandAvailable"));

                                }
                            }
                        }
                    }
                    else
                    {
                        if (!thisCommand.availableAtBase)
                        {
                            if (thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                            }
                            else
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandUnavailable"));

                            }
                        }
                        else
                        {
                            if (thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableAvailable"));

                            }
                            else
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandAvailable"));

                            }
                        }
                    }
                }
                else
                {
                    if (thisCommand.isVariable)
                    {
                        GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableAvailable"));

                    }
                    else
                    {
                        GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandAvailable"));

                    }
                }

                //Check if the mouse cursor is over the command.
                if (slotRect.Contains(e.mousePosition))
                {
                    //Draw the tooltip.
                    toolTip = CreateToolTip(thisCommand);
                    showToolTip = true;

                    //Call the function that handles drag and drop.
                    DragonDrop(slotNumber, thisCommand);
                }
            }
            //Check if dragging is stopped.
            CheckForRelease(slotNumber);

            //Increment the slot we're currently handling.
            slotNumber++;                
		}
	}

    //Method for DragonDrop
    void DragonDrop(int slotNumber, Command thisCommand)
    {

        Event e = Event.current;

        //Check if the mouse is dragging the current command.
        if (e.button == 0 && e.type == EventType.mouseDrag && !sequenceEditor.isDraggingCommand)
        {
            if(sequenceEditor.belongsToCheckpoint && thisCommand.commandId == "P01")
            {

            }
            else
            {
                sequenceEditor.isDraggingCommand = true;
                sequenceEditor.draggedCommand = thisCommand;
            }
        }
    }

    //Check if the mouse drag is released.
    void CheckForRelease(int slotNumber)
    {
        Event e = Event.current;
        if (sequenceEditor != null)
        {
            //If mouse drag is released outside of the command list (...)
            if (!slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && sequenceEditor.isDraggingCommand)
            {
                //(...) go through all slots in the Sequence Editor.
                for (int i = 0; i < sequenceEditor.slotPositions.Count; i++)
                {
                    slotRect = sequenceEditor.slotPositions[i];

                    //If any of them contains the mouse, but the dragged command in that slot.
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (i % 2 == 0)
                        {
                            sequenceEditor.enteredCommands[i] = sequenceEditor.draggedCommand;
                        }
                        else
                        {
                            if (sequenceEditor.enteredCommands[i - 1].commandName == "")
                            {
                                sequenceEditor.enteredCommands[i - 1] = sequenceEditor.draggedCommand;
                            }
                            else
                            {
                                sequenceEditor.enteredCommands[i] = sequenceEditor.draggedCommand;

                            }
                        }
                    }
                }
                //Null the dragged command, and tell the script it is no longer dragging anything.
                sequenceEditor.isDraggingCommand = false;
                sequenceEditor.draggedCommand = null;
            }
        }
    }

    //Method for returning the text for the tooltip
	string CreateToolTip(Command command){
		toolTip = command.commandDesc;
		return toolTip;
	}
}
