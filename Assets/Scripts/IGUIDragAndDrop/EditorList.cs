using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
    public List<Command> enteredCommands = new List<Command>();
    public List<Command> slots = new List<Command>();
    public CommandDatabase commandDatabase;
    private bool drawEditorWindow = false;

    //Variables hold the numver of rows and coloumns in the sequence editor
    int slotsRow = 6;
    int slotsCol = 2;

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
    private bool draggingCommand;
    private Command draggedCommand;
    private int previousCommandIndex;

    //Event handles mouse input
    Event e = Event.current;

    //Tooltip
    private bool showToolTip;
    private string toolTip;

    //GUI apperance
    public GUISkin commandSkin;


    void Start()
    {
        //Calculate the dimensions of the bounding box
        boundingBoxHeight = 6 * (boxHeight + ((Screen.height / 24) / 10)) + Screen.width / 35;
        boundingBoxWidth = 2 * boxWidth + Screen.width / 40;
        boundingBoxX = boxStartingPosX - Screen.width / 80;
        boundingBoxY = boxStartingPosY - Screen.width / 70 - 5;

        //Fill the slots list and enteredCommands list with empty commands
        for (int i = 0; i < 12; i++)
        {
            slots.Add (new Command());
            enteredCommands.Add (new Command());
        }

        //Cache the database of commands so that we can always find any command we need.
        commandDatabase = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();


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
            GUI.Box(new Rect(e.mousePosition.x + 13, e.mousePosition.y, 200, 40), toolTip, commandSkin.GetStyle("tooltipBackground"));

            //If the tooltip string is blank, stop drawing the tooltip
            if (toolTip == "")
            {
                showToolTip = false;
            }
        }

        //DragonDrop
        if (draggingCommand)
        {
            GUI.Box(new Rect(e.mousePosition.x + 13, e.mousePosition.y, 200, 40), "<color=#000000>" + draggedCommand.commandName + "</color>", commandSkin.GetStyle("commandSkin"));
        }
    }

    //Method that draws the editor window
    void DrawEditor()
    {
        //Draw the bounding box.
        GUI.Box(new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight), "Sequence Editor");

        //Variables for drawing the commands
        float previousRectY = boxStartingPosY;
        bool firstRectDrawn = false;
        int slotNumber = 0;
        

        for (int y = 0; y < slotsCol; y++)
        {
            for (int x = 0; x < slotsRow; x++)
            {
                //Specify the command we're currenly handling
                Command thisCommand = slots[slotNumber];

                //Update the slot position
                slotRect = new Rect(boxStartingPosX + y * boxOffsetY, boxStartingPosY + x * boxOffSetX, boxWidth, boxHeight);

                //Draw any empty slots
                if (thisCommand.commandName == "")
                {
                    GUI.Box(slotRect, "");
                }else if (thisCommand.commandName != "")
                {
                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandName + "</color>", commandSkin.GetStyle("commandSkin"));
                }

                slotNumber++;
            }
        }
    }
}
