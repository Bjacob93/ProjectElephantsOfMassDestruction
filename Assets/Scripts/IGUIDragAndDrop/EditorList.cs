using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorList : MonoBehaviour {

    public string listID;

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
    public List<Command> enteredCommands = new List<Command>();
    public List<Command> slots = new List<Command>();

    public List<Rect> slotPositions = new List<Rect>();

    public CommandDatabase commandDatabase;
    private bool drawEditorWindow = false;

    //Variables hold the numver of rows and coloumns in the sequence editor
    int slotsRow = 6;
    int slotsCol = 2;

    int totalSlots;

    //Varibles that hold the dimensions of the drawn commands
    float boxHeight = Screen.height / 24 - (Screen.height / 24) / 10;
    float boxWidth = Screen.width / 8 - (Screen.height / 24) / 10;
    float boxStartingPosX = (Screen.width / 6);
    float boxStartingPosY = Screen.height / 4;
    float boxOffSetX = Screen.height / 24;
    float boxOffsetY = Screen.width / 8;
    float boundingBoxHeight;
    float boundingBoxWidth;
    float boundingBoxX;
    float boundingBoxY;
    Rect slotRect;

    //Drag and Drop
    public bool isDraggingCommand;
    public Command draggedCommand;
    private int previousCommandIndex;

    //Tooltip
    private bool showToolTip;
    private string toolTip;

    //GUI apperance
    public GUISkin commandSkin;

    public EditorList(string id)
    {
        listID = id;
    }

    void Start()
    {
        totalSlots = slotsCol * slotsRow;

        //Calculate the dimensions of the bounding box
        boundingBoxHeight = 6 * (boxHeight + ((Screen.height / 24) / 10)) + Screen.width / 35;
        boundingBoxWidth = 2 * boxWidth + Screen.width / 40;
        boundingBoxX = boxStartingPosX - Screen.width / 80;
        boundingBoxY = boxStartingPosY - Screen.width / 70 - 5;

        //Fill the slots list and enteredCommands list with empty commands
        for (int i = 0; i < totalSlots; i++)
        {
            slots.Add (new Command());
            enteredCommands.Add (new Command());
        }

        //Cache the database of commands so that we can always find any command we need.
        commandDatabase = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

        //Add all the positions and dimensions of the slots to a list for reference
        for (int y = 0; y < slotsCol; y++)
        {
            for (int x = 0; x < slotsRow; x++)
            {
                slotRect = new Rect(boxStartingPosX + y * boxOffsetY, boxStartingPosY + x * boxOffSetX, boxWidth, boxHeight);
                slotPositions.Add(slotRect);
            }
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("SequenceEditor"))
        {
            drawEditorWindow = !drawEditorWindow;
        }
    }

    void OnGUI()
    {
        //Set the GUI skin
        GUI.skin = commandSkin;

        //Reset tooltip string
        toolTip = "";

        //Draw the editor
        if (drawEditorWindow)
        {
            DrawEditor();
        }

        //Show tooltip at the mouse position
        if (showToolTip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), toolTip, commandSkin.GetStyle("tooltipBackground"));

            //If the tooltip string is blank, stop drawing the tooltip
            if (toolTip == "")
            {
                showToolTip = false;
            }
        }

        //DragonDrop
        if (isDraggingCommand)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, 40), "<color=#000000>" + draggedCommand.commandName + "</color>", commandSkin.GetStyle("commandSkin"));
        }
    }

    //Method that draws the editor window
    void DrawEditor()
    {
        Event e = Event.current;

        //Draw the bounding box.
        GUI.Box(new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight), "Sequence Editor");

        //Variables for drawing the commands
        int slotNumber = 0;
        

        for (int y = 0; y < slotsCol; y++)
        {
            for (int x = 0; x < slotsRow; x++)
            {
                slots[slotNumber] = enteredCommands[slotNumber];

                //Specify the command we're currenly handling
                Command thisCommand = slots[slotNumber];

                //Update the slot position
                slotRect = new Rect(boxStartingPosX + y * boxOffsetY, boxStartingPosY + x * boxOffSetX, boxWidth, boxHeight);

                //Draw any empty slots
                if (thisCommand.commandName == "")
                {
                    GUI.Box(slotRect, "");
                    CheckReleased(slotNumber);

                //Draw any filled slots
                }else if (thisCommand.commandName != "")
                {
                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandSkin"));

                    //Check if the mouse is over the slot
                    if (slotRect.Contains(e.mousePosition))
                    {

                        //Get tooltip text and tell it to draw
                        showToolTip = true;
                        toolTip = CreateToolTip(thisCommand);

                        //Call the drag and drop method
                        DragonDrop(slotNumber);
                    }
                }
                slotNumber++;
            }
        }
    }

    //Method for DragonDrop
    void DragonDrop(int slotNumber) { 

        Event e = Event.current;

        //Check if the mouse is dragging the current command
        if (e.button == 0 && e.type == EventType.mouseDrag && !isDraggingCommand)
        {
            isDraggingCommand = true;
            previousCommandIndex = slotNumber;
            draggedCommand = enteredCommands[slotNumber];
            enteredCommands[slotNumber] = new Command();
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

    void CheckReleased(int slotNumber)
    {
        Event e = Event.current;
        if (slotRect.Contains(e.mousePosition) && e.type == EventType.MouseUp && isDraggingCommand)
        {
            enteredCommands[slotNumber] = draggedCommand;
            draggedCommand = null;
            isDraggingCommand = false;
        }

        else if (!slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && isDraggingCommand)
        {
            isDraggingCommand = false;
            draggedCommand = null;
        }
    }

    //Method for returning the text for the tooltip
    string CreateToolTip(Command command)
    {
        toolTip = command.commandDesc;
        return toolTip;
    }
}
