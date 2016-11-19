using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EditorList : MonoBehaviour{

    /**
     * This class handles the Sequence Editor. It controls drawing it in the UI, and has an ID so that multiple instances
     * of it can be initialised, one for each checkpoint and the homebase.
     */

    //The ID of the list
    public string listID;
    public bool belongsToCheckpoint;

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
    public List<Command> enteredCommands;
    public List<Command> slots;

    //A list of the slot positions so that they can be easily accessed by the CommandList script.
    public List<Rect> slotPositions;

    //Cache the database of commands.
    public CommandDatabase commandDatabase;

    //Bool that controls when to draw the editor window.
    public bool drawEditorWindow;

    //Variables hold the number of rows and coloumns in the sequence editor, and the resulting total number of slots.
    int slotsRow;
    int slotsCol;
    int totalSlots;

    //Cache the command list window.
    Rect commandBoundRect;

    //Cache the dimensions of the slots.
    Rect slotRect;
    float boxHeight;
    float boxWidth;
    float boxStartingPosX;
    float boxStartingPosY;
    float boxOffsetX;
    float boxOffsetY;

    //Cache the dimensions of the bounding box.
    Rect  boundingRect;
    float boundingBoxHeight;
    float boundingBoxWidth;
    float boundingBoxX;
    float boundingBoxY;

    //Drag and Drop
    public bool isDraggingCommand;
    public Command draggedCommand;
    private int previousCommandIndex;

    //Tooltip
    private bool showToolTip;
    private string toolTip;

    //GUI appearance
    public GUISkin commandSkin;

    PauseScript pauseScript;
    levelManager lvlManager;
    Level1TutorialText tutorial1text;
	Level2TutorialText tutorial2text;
    
    void Start()
    {
        //Define dimensions of the slots.
        boxStartingPosX = Screen.width / 6;
        boxStartingPosY = Screen.height / 4;
        boxWidth = Screen.width / 8 - (Screen.height / 24) / 10;
        boxHeight = Screen.height / 24 - (Screen.height / 24) / 10;
        boxOffsetY = Screen.height / 24;
        boxOffsetX = Screen.width / 8;

        //Define dimensions of the bounding box.
        boundingBoxHeight = 6 * (boxHeight + ((Screen.height / 24) / 10)) + Screen.width / 35;
        boundingBoxWidth = 2 * boxWidth + Screen.width / 40;
        boundingBoxX = boxStartingPosX - Screen.width / 80;
        boundingBoxY = boxStartingPosY - Screen.width / 70 - 5;

        commandSkin = Resources.Load("Graphix/commandSkin") as GUISkin;

        commandSkin.GetStyle("EditorBoundingBox").fontStyle = FontStyle.Bold;
        commandSkin.GetStyle("EditorBoundingBox").fontSize = Screen.width / 80;
        commandSkin.GetStyle("EditorBoundingBox").alignment = TextAnchor.UpperCenter;

        //Define the variables
        enteredCommands = new List<Command>();
        slots = new List<Command>();
        slotPositions = new List<Rect>();

        drawEditorWindow = false;
        slotsRow = 6;
        slotsCol = 2;

        pauseScript = GameObject.Find("UIButtons").GetComponent<PauseScript>();

        //Get the dimensions of the command window from the CommandList script.
        commandBoundRect = GameObject.FindGameObjectWithTag("CommandList").GetComponent<CommandList>().boundingRect;

        //Calculate the total number of slots.  
        totalSlots = slotsCol * slotsRow;

        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
        if (lvlManager.currentLevel == 1)
            tutorial1text = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();

        //Define the bounding box.
        boundingRect = new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight);

        //Fill the slots list and enteredCommands list with empty commands.
        for (int i = 0; i < totalSlots; i++)
        {
            slots.Add (new Command());
            enteredCommands.Add (new Command());
        }

        //Cache the database of commands so that we can always find any command we need.
        commandDatabase = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

        //Add all the positions and dimensions of the slots to a list, to be referenced by the CommandList script.
        for (int y = 0; y < slotsRow; y++)
        {
            for (int x = 0; x < slotsCol; x++)
            {
                slotRect = new Rect(boxStartingPosX + x * boxOffsetX, boxStartingPosY + y * boxOffsetY, boxWidth, boxHeight);
                slotPositions.Add(slotRect);
            }
        }
    }

    void Update()
    {
        //Check if the hotkey "e" for opening the Sequence Editor is pressed, and change the state of the window if so.
        if (Input.GetButtonDown("SequenceEditor")) 
        {
            drawEditorWindow = false;
            if(lvlManager.currentLevel == 1 && tutorial1text.currentTutorialPage == 13 && !belongsToCheckpoint)
            {
                tutorial1text.currentTutorialPage++;
            }
        }
    }

    public void OnGUI()
    {
        //Set the GUI skin
        GUI.skin = commandSkin;

        //Reset tooltip string
        toolTip = "";

        //Draw the editor
        if (drawEditorWindow)
        {
            DrawEditor();
            if(lvlManager.currentLevel == 1)
            {
                if (tutorial1text.currentTutorialPage == 5 && belongsToCheckpoint == false)
                {
                    tutorial1text.currentTutorialPage++;
                }
                else if (tutorial1text.currentTutorialPage == 14 && belongsToCheckpoint == true)
                {
                    tutorial1text.currentTutorialPage++;
                }
            }
        }

        //Show tooltip at the mouse position
        if (showToolTip)
        {
            float toolTipHeight = toolTip.Length / 1.4f;

            if (Event.current.mousePosition.x > Screen.width - (boxWidth))
            {
                GUI.Box(new Rect(Event.current.mousePosition.x - boxWidth, Event.current.mousePosition.y, 200, toolTipHeight), toolTip, commandSkin.GetStyle("tooltipBackground"));
            }
            else
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, toolTipHeight), toolTip, commandSkin.GetStyle("tooltipBackground"));
            }

            //If the tooltip string is blank, stop drawing the tooltip
            if (toolTip == "")
            {
                showToolTip = false;
            }
        }
    }

    //Method that draws the editor window
    void DrawEditor()
    {
        Event e = Event.current;

        //Draw the bounding box.
        if(this.gameObject.name != "GiraffeBase")
        {
            GUI.Box(boundingRect, "Checkpoint " + this.gameObject.name + "  Editor", commandSkin.GetStyle("EditorBoundingBox"));
        }
        else
        {
            GUI.Box(boundingRect,  this.gameObject.name + "  Editor", commandSkin.GetStyle("EditorBoundingBox"));
        }

        //Variables for drawing the commands
        int slotNumber = 0;
        
        //For every slot in the Sequence Editor
        for (int y = 0; y < slotsRow; y++)
        {
            for (int x = 0; x < slotsCol; x++)
            {
                slots[slotNumber] = enteredCommands[slotNumber];

                //Specify the command we're currenly handling
                Command thisCommand = slots[slotNumber];

                //Update the slot position
                slotRect = new Rect(boxStartingPosX + x * boxOffsetX, boxStartingPosY + y * boxOffsetY, boxWidth, boxHeight);

                if (lvlManager.currentLevel == 1) {
                    if(!belongsToCheckpoint)
                    {
                        if (tutorial1text.currentTutorialPage == 7 && thisCommand.commandId == "P01")
                        {
                            tutorial1text.currentTutorialPage++;
                        }
                        else if(tutorial1text.currentTutorialPage == 10 && thisCommand.commandId == "A01")
                        {
                            tutorial1text.currentTutorialPage++;
                        }
                        else if(tutorial1text.currentTutorialPage == 12 && thisCommand.commandId == "varA")
                        {
                            tutorial1text.currentTutorialPage++;
                        }
                    }
                    else if (tutorial1text.currentTutorialPage == 16 && thisCommand.commandId == "D01")
                    {
                        tutorial1text.currentTutorialPage++;
                    }
                }
				if (lvlManager.currentLevel == 2) {
					if (!belongsToCheckpoint) {
						if (tutorial2text.currentTutorialPage == 2 && thisCommand.commandId == "FoE")
							tutorial2text.currentTutorialPage++;
					}
				
				}
                //Draw any empty slots
                if (thisCommand.commandName == "")
                {
                    if(x == 0)
                    {
                        GUI.Box(slotRect, "", commandSkin.GetStyle("commandEmpty"));
                    }
                    else if (slots[slotNumber - 1].requiresVariable)
                    {
                        GUI.Box(slotRect, "<color=#000000>" + "Insert Variable" + "</color>", commandSkin.GetStyle("variableUnavailable"));
                    }
                    else if(!slots[slotNumber - 1].requiresVariable && slots[slotNumber - 1].commandName != "")
                    {
                        GUI.Box(slotRect, "<color=#000000>" + "No Variable Needed" + "</color>", commandSkin.GetStyle("variableAvailable"));
                    }
                    else
                    {
                        GUI.Box(slotRect, "", commandSkin.GetStyle("variableEmpty"));
                    }
                    if (pauseScript.GetPauseStatus())
                    {
                        CheckReleased(slotNumber);
                    }

                    //Draw any filled slots
                }
                else if (thisCommand.commandName != "")
                {
                    if(x == 1 && slots[slotNumber - 1].commandName == "")
                    {
                        if (thisCommand.isVariable)
                        {
                            GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("variableError"));

                        }
                        else
                        {
                            GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandError"));

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

                    //Check if the mouse is over the slot
                    if (slotRect.Contains(e.mousePosition))
                    {

                        //Get tooltip text and tell it to draw
                        showToolTip = true;
                        toolTip = CreateToolTip(thisCommand);

                        //Call the drag and drop method
                        if (pauseScript.GetPauseStatus())
                        {
                            DragonDrop(slotNumber);
                        }
                    }
                }
                //Increment the current slot.
                slotNumber++;
            }
        }
    }

    //Method for DragonDrop
    void DragonDrop(int slotNumber) { 

        //Initialise the event condition.
        Event e = Event.current;

        //Check if the mouse is dragging the current command
        if (slotRect.Contains(e.mousePosition) && e.button == 0 && e.type == EventType.mouseDrag && !isDraggingCommand)
        {
            if(belongsToCheckpoint && enteredCommands[slotNumber].commandId == "P01")
            {
                return;
            }
            else
            {
                isDraggingCommand = true;
                previousCommandIndex = slotNumber;
                draggedCommand = enteredCommands[slotNumber];
                enteredCommands[slotNumber] = new Command();
            }
        }

        //Swap positions if the mouse is released over a slot.
        else if (slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && isDraggingCommand)
        {
            enteredCommands[previousCommandIndex] = enteredCommands[slotNumber];
            enteredCommands[slotNumber] = draggedCommand;
            isDraggingCommand = false;
            draggedCommand = null;
        }
    }

    //Check if the mouse button is released (...)
    void CheckReleased(int slotNumber)
    {
        Event e = Event.current;

        //(...) on an empty slot.
        if (slotRect.Contains(e.mousePosition) && e.type == EventType.MouseUp && isDraggingCommand)
        {
            enteredCommands[slotNumber] = draggedCommand;
            draggedCommand = null;
            isDraggingCommand = false;
        }
        //(...) outside the slots.
        else if (!slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && isDraggingCommand)
        {
            isDraggingCommand = false;
            draggedCommand = null;
        }
    }

    //Method for returning the text for the tooltip
    string CreateToolTip(Command command)
    {
        toolTip = command.commandDescDnD;
        return toolTip;
    }
}
