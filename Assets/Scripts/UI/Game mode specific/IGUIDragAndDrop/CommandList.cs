using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    PauseScript pauseScript;

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
    Level1TutorialText tutorial1;
    Level2TutorialText tutorial2;

    //Tooltip.
    private bool showToolTip;
	private string toolTip;

    //Animator
    private string buttonState = "hide";
    float speed;
    private bool animState = true;
    //button for animation
    float buttonW;
    float buttonH;
    float buttonX;
    float buttonXMax;
    float buttonY;
    //GUIStyle iconStyle;
    //GUI appearence.
    public GUISkin commandSkin;

    void Start() { 
        //Reference the database of commands so that we can always find any command we need.
        database = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

        //Reference the Sequence Manager script.
        sequenceManager = GameObject.Find("UIManager").GetComponent<SequenceManager>();

        //Reference the levelManager.
        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
        if(lvlManager.currentLevel == 1)
        {
            tutorial1 = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();
        }
		if (lvlManager.currentLevel == 2) {
            tutorial2 = GameObject.Find("UIManager").GetComponent<Level2TutorialText>();
		}

        pauseScript = GameObject.Find("UIButtons").GetComponent<PauseScript>();

        //Calculate the bounding box dimensions and define the resulting Rect.
        boundingBoxHeight = numberOfSlots * (boxHeight + ((Screen.height / 24) / 10)) + (Screen.height/10);
		boundingBoxWidth = boxWidth + (Screen.width / 22);
		boundingBoxX = boxStartingPosX - (Screen.width/45);
		boundingBoxY = (boxStartingPosY - (Screen.width/18));
        

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

        //Animation button
        speed = Time.deltaTime * 600;
        buttonH = 32;
        buttonW = 32;
        buttonX = boundingBoxX - buttonW;
        buttonXMax = buttonX;
        buttonY = boundingBoxY - (buttonH * 0.5f) + (boundingBoxHeight * 0.5f);

        //iconStyle.normal.background = buttonTexture;
    }

	void Update(){
        if (!animState && buttonX >= buttonXMax + 20)
        {
			if (lvlManager.currentLevel == 1 && tutorial1.currentTutorialPage == 3)
            {
                tutorial1.currentTutorialPage++;
            }
			if (lvlManager.currentLevel == 2 && tutorial2.currentTutorialPage == 0) {
                tutorial2.currentTutorialPage++;
			}
            if (buttonX < buttonXMax)
            {
                buttonX = buttonXMax;
            }
            buttonX -= speed;
            boundingBoxX -= speed;
            boxStartingPosX -= speed;
        }
        else if (animState && buttonX <= Screen.width - buttonW - 20)
        {
            if (buttonX > Screen.width - buttonW)
            {
                buttonX = Screen.width - buttonW;
            }
            buttonX += speed;
            boxStartingPosX += speed;
            boundingBoxX += speed;
        }

        //Open or close the Command List.
        if (Input.GetButtonDown("Commandlist")){
            animState = !animState;
		}

        boundingRect = new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight);
    }
    void OnGUI(){

        //Set the skin for the boxes.
        GUI.skin = commandSkin;

        //Set the current tooltip string to be blank.
		toolTip = "";
        //draw toggle button
        if (GUI.Button(new Rect(buttonX, buttonY, buttonW, buttonH), "", commandSkin.GetStyle("slideButton")))
        {
            animState = !animState;
        }

        //Go through all instances of EditorList.
        for (int i = 0; i < sequenceManager.editorlistGO.Count; i++)
        {
            //If one of them is currently open in the UI, make that the reference point for sequenceEditor.
            if (sequenceManager.editorlistGO[i].drawEditorWindow)
            {
                sequenceEditor = sequenceManager.editorlistGO[i];
                break;
            }
            if (!sequenceManager.editorlistGO[i].drawEditorWindow && i == sequenceManager.editorlistGO.Count - 1)
            {
                sequenceEditor = null;
            }
        }
        DrawCommandList();

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
        commandSkin.GetStyle("boundingBox").fontSize = Screen.width / 70;
        commandSkin.GetStyle("boundingBox").fontStyle = FontStyle.Bold;
        commandSkin.GetStyle("boundingBox").padding.top = Screen.height / 17;

        GUI.Box (boundingRect, "Command List", commandSkin.GetStyle("boundingBox"));

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
                    //if this is a checkpoint
                    if (sequenceEditor.belongsToCheckpoint)
                    {
                        //...and the command is not available here, draw it as unavailable.
                        if (!thisCommand.availableAtCheckpoint)
                        {
                            GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandUnavailable"));
                        }
                        //..and the command is available here.
                        else
                        {
                            if (!thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandAvailable"));

                            }
                            bool variableNeeded = false;
                            bool forEveryHasBeenUsed = false;

                            //Find out if a command (or specifically a split command) has been entered, which requires a variable.
                            for (int i = 0; i < sequenceEditor.slots.Count; i++)
                            {
                                if (i % 2 == 0 )
                                {
                                    if (sequenceEditor.slots[i].requiresVariable)
                                    {
                                        variableNeeded = true;
                                    }
                                    if (sequenceEditor.slots[i].commandId == "FoE")
                                    {
                                        forEveryHasBeenUsed = true;
                                    }   
                                }
                            }

                            //If no commands that require variables has been entered
                            if (!variableNeeded)
                            {
                                //..draw all variables as unavailable
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                            }

                            //If a command requires a variable, but it is not a split command,
                            else if (!forEveryHasBeenUsed && (thisCommand.commandId == "FoE2" || thisCommand.commandId == "FoE3"))
                            {
                                //Draw the split variables as unavailable
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                            }

                            //If a command requires a variable.
                            else
                            {
                                //Draw all variables as available
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableAvailable"));

                                }
                            }
                        }
                    }

                    //Drawing commands for list at homebase.
                    else
                    {
                        //If the command is not available at homebase, draw it as unavailable
                        if (!thisCommand.availableAtBase)
                        {
                            GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandUnavailable"));
                        }
                        
                        //If it is availale:
                        else
                        {
                            //if it is not a variable, draw it as available.
                            if (!thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandAvailable"));
                            }

                            //Variables for next part.
                            bool variableNeeded = false;
                            bool forEveryHasBeenUsed = false;

                            //Find out if a command (or specifically a split command) has been entered, which requires a variable.
                            for (int i = 0; i < sequenceEditor.slots.Count; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    if (sequenceEditor.slots[i].requiresVariable)
                                    {
                                        variableNeeded = true;
                                    }
                                    if (sequenceEditor.slots[i].commandId == "FoE")
                                    {
                                        forEveryHasBeenUsed = true;
                                    }
                                }
                            }

                            //If no commands that require variables has been entered
                            if (!variableNeeded)
                            {
                                //..draw all variables as unavailable
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                            }

                            //If a command requires a variable, but it is not a split command,
                            else if (!forEveryHasBeenUsed && (thisCommand.commandId == "FoE2" || thisCommand.commandId == "FoE3"))
                            {
                                //Draw the split variables as unavailable
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                            }

                            //If a command requires a variable.
                            else
                            {
                                //Draw all variables as available
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableAvailable"));

                                }
                            }
                        }
                    }
                }

                //If no editor window is open, draw everything as available.
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
                    if (pauseScript.GetPauseStatus())
                    {
                        DragonDrop(slotNumber, thisCommand);
                    }
                }
            }
            //Check if dragging is stopped.
            if (pauseScript.GetPauseStatus())
            {
                CheckForRelease(slotNumber);
            }
            //Increment the slot we're currently handling.
            slotNumber++;                
		}
	}

    //Method for DragonDrop
    void DragonDrop(int slotNumber, Command thisCommand)
    {

        Event e = Event.current;

        //Check if the mouse is dragging the current command.
        if (slotRect.Contains(e.mousePosition) && e.button == 0 && e.type == EventType.mouseDrag && !sequenceEditor.isDraggingCommand)
        {
            if(sequenceEditor.belongsToCheckpoint && !thisCommand.availableAtCheckpoint)
            {

            }
            else if(!sequenceEditor.belongsToCheckpoint && !thisCommand.availableAtBase)
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
            if (sequenceEditor != null)
            {
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
                                if (!sequenceEditor.draggedCommand.isVariable)
                                {
                                    sequenceEditor.enteredCommands[i] = sequenceEditor.draggedCommand;
                                }
                                else if (sequenceEditor.enteredCommands[i + 1].commandName == "")
                                {
                                    sequenceEditor.enteredCommands[i + 1] = sequenceEditor.draggedCommand;
                                }
                            }
                            else
                            {
                                if (sequenceEditor.draggedCommand.isVariable)
                                {
                                    if (!sequenceEditor.enteredCommands[i - 1].requiresVariable)
                                    {

                                    }
                                    else
                                    {
                                        sequenceEditor.enteredCommands[i] = sequenceEditor.draggedCommand;

                                    }
                                }
                                else if (sequenceEditor.enteredCommands[i - 1].commandName == "")
                                {
                                    sequenceEditor.enteredCommands[i - 1] = sequenceEditor.draggedCommand;
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
    }

    //Method for returning the text for the tooltip
	string CreateToolTip(Command command){
		toolTip = command.commandDescDnD;
		return toolTip;
	}
}
