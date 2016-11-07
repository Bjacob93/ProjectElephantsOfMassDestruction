using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class textEditor : MonoBehaviour
{

    int charLimit = 250;
    private string textAreaString = "if (enemyNearby == true)";
    public GameObject checkpointA;
    public GameObject checkpointB;
    public GameObject checkpointC;
    private GameObject hud;

    void OnGUI()
    {
        textAreaString = GUI.TextArea(new Rect(300, 25, 450, 375), textAreaString, charLimit);
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 100, 250, 50), "Compile Code"))
        {
            CompileCode();
        }
    }
    void Start()
    {
        checkpointA.GetComponent<Transform>();
        checkpointB.GetComponent<Transform>();
        checkpointC.GetComponent<Transform>();
    }
    public void CompileCode()
    {
        List<int> loopLines = new List<int>();
        bool looping = false;
        char[] delimiter = new[] { ';', '(', ')', ',', ' ' };
        List<string> commandsInCode = new List<string>();
        bool runCode = true;
        int increment = 0;

        string[] linesOfCode = textAreaString.Split('\n'); //split the string into lines
        for (int j = 0; j < linesOfCode.Length; j++)
        {
            Debug.Log(linesOfCode[j]);
            commandsInCode = linesOfCode[j].Split(delimiter).ToList(); //split each line into commands
            for (int i = 0; i < commandsInCode.Count; i++)
            {
                Debug.Log(commandsInCode[i]);
                if (commandsInCode[i] == "")//check if there is nothing in the string
                {
                    commandsInCode.RemoveAt(i);//remove the index
                    i -= 1;//go backwards in the loop because an index was just removed
                }
                else if (!runCode)//check if we run this line of code
                {
                    if (commandsInCode[i] == "{")//check if there are other brackets within the brackets
                    {
                        increment += 1;
                    }
                    if (commandsInCode[i] == "}") //close brackets
                    {
                        increment -= 1;
                        if (increment == 0) //when the closing bracket is found; start running code again.
                        {
                            runCode = true;
                        }
                    }
                }
                else
                {
                    switch (commandsInCode[i])//reads the command
                    {
                        case "{":
                            increment += 1;
                            break;
                        case "}":
                            increment -= 1;
                            if (looping)//if looping is true jump back to the loop start line
                            {
                                //set the code iterator to the last loop start
                                j = loopLines[loopLines.Count - 1];
                                //remove the last element
                                loopLines.RemoveAt(loopLines.Count - 1);
                            }
                            break;
                        case "while":
                            loopLines.Add(j); //add the line which the loop starts
                            //do some other shit
                            break;
                        case "moveTo":
                            //read next string to see move location
                            Vector3 moveTo;
                            string location = commandsInCode[i + 1];
                            if (location == "A")
                            {
                                moveTo = checkpointA.transform.position;
                            }
                            else if (location == "B")
                            {
                                moveTo = checkpointC.transform.position;
                            }
                            else if (location == "C")
                            {
                                moveTo = checkpointB.transform.position;
                            }
                            else { Debug.Log("No Valid location"); }
                            //send location to script
                            //sendmovement(moveTo);
                            break;
                        case "if":
                            //check condition
                            //set accessBrackets to false if the condition is not met
                            runCode = false; //example
                            break;
                        case "defend":
                            //assume defensive position
                            break;
                        case "attack":
                            //attack something
                            break;
                    }
                }
            }
        }
    }
}