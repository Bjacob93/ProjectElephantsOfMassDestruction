using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class textCommandList : MonoBehaviour
{

    /**
     * This class handles the CommandList window.
     */

    //Initialise lists. The availableCommands list holds all the commands the list should contain. 
    //The slots list holds the actual slots.
    public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();
    
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

    //Tooltip.
    private bool showToolTip;
    private string toolTip;

    //GUI appearence.
    public GUISkin commandSkin;

    Level1TutorialText tutorial;

    //Animator
    private float speed;
    private bool animState = true;
    //button for animation
    private float buttonW;
    private float buttonH;
    private float buttonX;
    private float buttonXMax;
    private float buttonY;

    void Start()
    {
        //Reference the database of commands so that we can always find any command we need.
        database = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();

        //Reference the Sequence Manager script.
        sequenceManager = GameObject.Find("UIManager").GetComponent<SequenceManager>();

        //Reference the levelManager.
        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();

        //Calculate the bounding box dimensions and define the resulting Rect.
        boundingBoxHeight = numberOfSlots * (boxHeight + ((Screen.height / 24) / 10)) + (Screen.width / 35);
        boundingBoxWidth = boxWidth + (Screen.width / 40);
        boundingBoxX = boxStartingPosX - (Screen.width / 80);
        boundingBoxY = (boxStartingPosY - (Screen.width / 70)) - 5;
        boundingRect = new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight);

        if (lvlManager.currentLevel == 1)
        {
            tutorial = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();
        }

        //Fill the lists with empty commands.
        for (int i = 0; i < numberOfSlots; i++)
        {
            slots.Add(new Command());
            availableCommands.Add(new Command());
        }

        //Add all available commands to the list.
        for (int i = 0; i < database.commandDatabase.Count; i++)
        {
            if (database.commandDatabase[i].lvlAvailability <= lvlManager.getCurrentLevel())
            {
                availableCommands[i] = database.commandDatabase[i];
            }
        }

        //Make the "slots" list contain the same elements as the "available" list.
        for (int j = 0; j < availableCommands.Count; j++)
        {
            slots[j] = availableCommands[j];
        }
        //Animation button
        speed = Time.deltaTime * 600;
        buttonH = 32;
        buttonW = 32;
        buttonX = boundingBoxX - buttonW;
        buttonXMax = buttonX;
        buttonY = boundingBoxY - (buttonH * 0.5f) + (boundingBoxHeight * 0.5f);
    }

    void Update()
    {
        if (!animState && buttonX >= buttonXMax + 20)
        {
            if (lvlManager.currentLevel == 1 && !tutorial.commandListOpened)
            {
                tutorial.commandListOpened = true;
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
        if (Input.GetButtonDown("Commandlist"))
        {
            animState = !animState;
        }
    }

    void OnGUI()
    {
        //Set the skin for the boxes.
        GUI.skin = commandSkin;

        //Set the current tooltip string to be blank.
        toolTip = "";
        //draw toggle button
        if (GUI.Button(new Rect(buttonX, buttonY, buttonW, buttonH), "", commandSkin.GetStyle("slideButton")))
        {
            animState = !animState;
        }
        DrawCommandList();        
        //Show the tooltip at the mouse position.
        if (showToolTip)
        {
            float toolTipHeight = toolTip.Length / 1.4f;

            if (Event.current.mousePosition.x > Screen.width - (boxWidth - 2))
            {
                GUI.Box(new Rect(Event.current.mousePosition.x - (boxWidth * 1.02f), Event.current.mousePosition.y, 200, toolTipHeight), toolTip, commandSkin.GetStyle("tooltipBackground"));
            }
            else
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 13, Event.current.mousePosition.y, 200, toolTipHeight), toolTip, commandSkin.GetStyle("tooltipBackground"));
            }

            //If the tooltip string is blank, stop drawing the tooltip.
            if (toolTip == "")
            {
                showToolTip = false;
            }
        }
    }

    //Method that takes care of drawing the command list.
    void DrawCommandList()
    {
        //Event handles mouse input.
        Event e = Event.current;

        //Draw the bounding box.
        GUI.Box(new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight), "Command List");

        //Variables for drawing the commands.
        float previousRectY = boxStartingPosY;
        int slotNumber = 0;

        //For-loop draws the commands.
        for (int y = 0; y < numberOfSlots; y++)
        {

            //Define the Rect for the current slot.
            slotRect = new Rect(boxStartingPosX, previousRectY + y * boxOffSetY, boxWidth, boxHeight);

            //Specify the current command
            Command thisCommand = slots[slotNumber];

            //Draw the first command if it exists
            if (slots[slotNumber].commandName != "")
            {
                if (sequenceEditor != null)
                {
                    if (sequenceEditor.belongsToCheckpoint)
                    {
                        if (!thisCommand.availableAtCheckpoint)
                        {
                            if (thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableUnavailable"));

                            }
                            else
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandUnavailable"));

                            }
                        }
                        else
                        {
                            bool orderHasBeenEntered = false;
                            bool forEveryHasBeenUsed = false;

                            for (int i = 0; i < sequenceEditor.slots.Count; i++)
                            {
                                if (i % 2 == 0)
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
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                                else
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandUnavailable"));

                                }
                            }
                            else if (!forEveryHasBeenUsed && (thisCommand.commandId == "FoE2" || thisCommand.commandId == "FoE3"))
                            {
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableUnavailable"));

                                }
                                else
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandUnavailable"));

                                }
                            }
                            else
                            {
                                if (thisCommand.isVariable)
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableAvailable"));

                                }
                                else
                                {
                                    GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandAvailable"));

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
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableUnavailable"));

                            }
                            else
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandUnavailable"));

                            }
                        }
                        else
                        {
                            if (thisCommand.isVariable)
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableAvailable"));

                            }
                            else
                            {
                                GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandAvailable"));

                            }
                        }
                    }
                }
                else
                {
                    if (thisCommand.isVariable)
                    {
                        GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("variableAvailable"));

                    }
                    else
                    {
                        GUI.Box(slotRect, "<color=#000000>" + thisCommand.commandNameText + "</color>", commandSkin.GetStyle("commandAvailable"));

                    }
                }

                //Check if the mouse cursor is over the command.
                if (slotRect.Contains(e.mousePosition))
                {
                    //Draw the tooltip.
                    toolTip = CreateToolTip(thisCommand);
                    showToolTip = true;
                }
            }
            //Increment the slot we're currently handling.
            slotNumber++;
        }
    }

    //Method for returning the text for the tooltip
    string CreateToolTip(Command command)
    {
        toolTip = command.commandDescText;
        return toolTip;
    }
}
