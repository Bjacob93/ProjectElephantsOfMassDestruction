using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class textEditor : MonoBehaviour
{
    //Set the character limit, and the text which is visible in the window at first.
    int charLimit = 250;
    private string textAreaString = "";

    //Boolean which determines whether to draw the window or not.
    public bool drawSequenceEditor = false;

    //List that keeps track of all the errors present in the code.
    List<KeyValuePair<int, string>> errorList = new List<KeyValuePair<int, string>>();

    //List that holds the viable commands.
    public List<Command> listOfCommands = new List<Command>();

    //Variables that determines whether or not the list belongs to a chechpoint, and which object the list belongs to.
    public bool belongsToCheckpoint;
    public string listID;

    //Cache dimensions of the textwindow.
    float textBoxStartX;
    float textBoxStartY;
    float textBoxWidth;
    float textBoxHeight;
    Rect  textBox;

    //Cache dimensions of the boundingbox.
    float boundingBoxStartX;
    float boundingBoxStartY;
    float boundingBoxWidth;
    float boundingBoxHeight;
    Rect  boundingBox;

    //Cache dimensions of Compile button.
    float comButtonStartX;
    float comButtonStartY;
    float comButtonWidth;
    float comButtonHeight;
    Rect  comButton;

    //GUI appearance
    public GUISkin commandSkin;

    //Cache the command database;
    CommandDatabase database;

    void Start()
    {
        //Define dimensions of the textWindow.
        textBoxStartX = Screen.width / 6;
        textBoxStartY = Screen.height / 4;
        textBoxWidth = Screen.width / 4;
        textBoxHeight = Screen.height / 24 * 6;
        textBox = new Rect(textBoxStartX, textBoxStartY, textBoxWidth, textBoxHeight);

        //Define dimensions of the bounding box.
        boundingBoxStartX = textBoxStartX - Screen.width / 80;
        boundingBoxStartY = textBoxStartY - Screen.width / 70 - 2;
        boundingBoxWidth = textBoxWidth + Screen.width / 40;
        boundingBoxHeight = textBoxHeight + Screen.width / 35;
        boundingBox = new Rect(boundingBoxStartX, boundingBoxStartY, boundingBoxWidth, boundingBoxHeight);

        //Define dimenstions of the Compile button.
        comButtonStartX = boundingBoxStartX;
        comButtonStartY = Screen.height / 35 * 19;
        comButtonWidth = Screen.width / 8;
        comButtonHeight = Screen.height / 24;
        comButton = new Rect(comButtonStartX, comButtonStartY, comButtonWidth, comButtonHeight);

        //Load the skin
        commandSkin = Resources.Load("Graphix/commandSkin") as GUISkin;

        //Reference the command database
        database = GameObject.Find("CommandDatabase").GetComponent<CommandDatabase>();
    }
    
    void Update()
    {
        //Check if the right mouse button is pressed, and close the window if it is.
        if (Input.GetButtonDown("SequenceEditor"))
        {
            drawSequenceEditor = false;
        }
    }

    void OnGUI()
    {
        GUI.skin = commandSkin;

        if (drawSequenceEditor)
        {
            //Draw the bounding box and the text window.
            GUI.Box(boundingBox, "Script Editor - " + this.gameObject.name);
            textAreaString = GUI.TextArea(new Rect(textBoxStartX, textBoxStartY, textBoxWidth, textBoxHeight), textAreaString, charLimit);

            //Check if the Compile button is pressed.
            if (GUI.Button(comButton, "Compile Code"))
            {
                CompileCode();

            }
            //Initialise an int that tracks how many errors there are in the code.
            int errorN = 0;

            //Go through the errors, and print them on the screen.
            foreach(KeyValuePair<int, string> error in errorList)
            {
                errorN++;
                GUI.Label(new Rect(10, 10 * errorN, 1000, 20), error.Value + " at line " + error.Key.ToString());
            }
        }
    }    

    void CompileCode()
    {
        //Clear the lists of errors and commands.
        errorList.Clear();
        listOfCommands.Clear();

        //Determine which characters break up the code.
        char[] delimiter = new[] { ')', '(', ' '};
        
        //Initialise a list of all the elements in the text field.
        List<string> elementsInCode = new List<string>();

        //Divide the string in the field into seperate strings for each line.
        string[] linesOfCode = textAreaString.Split('\n');

        //Go through all the lines in the field.
        for (int j = 0; j < linesOfCode.Length; j++)
        {
            //Split the line strings whenever a delimiter character is encountered.
            elementsInCode = linesOfCode[j].Split(delimiter).ToList();

            //Go through the elements.
            for (int i = 0; i < elementsInCode.Count; i++)
            {
                //If the element is empty, remove it and go one step back in the loop, so as not to skip elements.
                if (elementsInCode[i] == "")
                {
                    elementsInCode.RemoveAt(i);
                    i -= 1;
                }
            }        
            //Go through the elements again, now that all empties are gone.    
            for (int i = 0; i < elementsInCode.Count; i++)
            {
                //a switch-case which reads the command.
                switch (elementsInCode[i])
                {
                    //Split command.
                    case "splitAt":
                        if (i + 1 < elementsInCode.Count)
                        {
                            for(int d = 0; d < database.commandDatabase.Count; d++)
                            {
                                if (database.commandDatabase[d].commandId == "FoE")
                                {
                                    listOfCommands.Add(database.commandDatabase[d]);
                                    break;
                                }
                            }

                            //Add the command with the correct variable to the list of commands, OR output error.
                            if (elementsInCode[i + 1] == "2")
                            {
                                for (int d = 0; d < database.commandDatabase.Count; d++)
                                {
                                    if (database.commandDatabase[d].commandName == "Every other")
                                    {
                                        listOfCommands.Add(database.commandDatabase[d]);
                                        i += 1;
                                        break;
                                    }
                                }

                            }
                            else if (elementsInCode[i + 1] == "3")
                            {
                                for (int d = 0; d < database.commandDatabase.Count; d++)
                                {
                                    if (database.commandDatabase[d].commandName == "Every third")
                                    {
                                        listOfCommands.Add(database.commandDatabase[d]);
                                        i += 1;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                errorList.Add(new KeyValuePair<int, string>(j, "Can't split at " + elementsInCode[i + 1]));
                            }
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }                        
                        break;

                    //Attack command.
                    case "attack":
                        if (i + 1 < elementsInCode.Count)
                        {
                            for (int d = 0; d < database.commandDatabase.Count; d++)
                            {
                                if (database.commandDatabase[d].commandName == "Attack")
                                {
                                    listOfCommands.Add(database.commandDatabase[d]);
                                    break;
                                }
                            }

                            //Call the function to check if the checkpoint variable is viable.
                            ValidCheckpoint(elementsInCode[i + 1], j);
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }

                        break;
                    //Defend command.
                    case "defend":
                        if (i + 1 < elementsInCode.Count)
                        {
                            for (int d = 0; d < database.commandDatabase.Count; d++)
                            {
                                if (database.commandDatabase[d].commandName == "Defend")
                                {
                                    listOfCommands.Add(database.commandDatabase[d]);
                                    break;
                                }
                            }
                            ValidCheckpoint(elementsInCode[i + 1], j);
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }
                        break;

                    //Produce command.
                    case "produce":
                        if (i + 1 < elementsInCode.Count)

                            Debug.Log("Handling produce");

                        {
                            for (int d = 0; d < database.commandDatabase.Count; d++)
                            {
                                if (database.commandDatabase[d].commandName == "Produce")
                                {
                                    listOfCommands.Add(database.commandDatabase[d]);
                                    break;
                                }
                            }
                        }
                        break;

                    //Move command.
                    case "moveTo":
                        if (i + 1 < elementsInCode.Count)
                        {
                            for (int d = 0; d < database.commandDatabase.Count; d++)
                            {
                                if (database.commandDatabase[d].commandName == "Move")
                                {
                                    listOfCommands.Add(database.commandDatabase[d]);
                                    break;
                                }
                            }
                            ValidCheckpoint(elementsInCode[i + 1], j);
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }
                        break;
                    
                    //Next four are variables for checkpoints and homebase.
                    case "A":
                        for (int d = 0; d < database.commandDatabase.Count; d++)
                        {
                            if (database.commandDatabase[d].commandName == "A")
                            {
                                listOfCommands.Add(database.commandDatabase[d]);
                                break;
                            }
                        }
                        break;

                    case "B":
                        for (int d = 0; d < database.commandDatabase.Count; d++)
                        {
                            if (database.commandDatabase[d].commandName == "B")
                            {
                                listOfCommands.Add(database.commandDatabase[d]);
                                break;
                            }
                        }
                        break;

                    case "C":
                        for (int d = 0; d < database.commandDatabase.Count; d++)
                        {
                            if (database.commandDatabase[d].commandName == "C")
                            {
                                listOfCommands.Add(database.commandDatabase[d]);
                                break;
                            }
                        }
                        break;

                    case "Homebase":
                        for (int d = 0; d < database.commandDatabase.Count; d++)
                        {
                            if (database.commandDatabase[d].commandName == "Homebase")
                            {
                                listOfCommands.Add(database.commandDatabase[d]);
                                break;
                            }
                        }
                        break;

                    default:
                        errorList.Add(new KeyValuePair<int, string>(j, "No known command"));
                        break;
                }
            }
        }
    }    

    //Function that checks the availability of checkpoints.
    void ValidCheckpoint(string check, int line)
    {
        //A list of all the checkpoints in the level.
        string[] checkpoints = new[] { "Homebase", "A", "B", "C" };

        //Check if the entered checkpoint matches any of the checkpoints in the level, and add the command to the list if it does.
        foreach (string s in checkpoints)
        {
            if (check == s)
            {
                return;
            }
        }
        errorList.Add(new KeyValuePair<int, string>(line, "No eligible checkpoint"));
    }
}