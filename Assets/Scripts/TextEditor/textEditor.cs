using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class textEditor : MonoBehaviour
{

    int charLimit = 250;
    private string textAreaString = "moveTo (A)";
    public GameObject checkpointA;
    public GameObject checkpointB;
    public GameObject checkpointC;
    private GameObject hud;
    private bool drawUI = false;
    List<KeyValuePair<int, string>> errorList = new List<KeyValuePair<int, string>>();
    int errorN = 0;
    
    void OnGUI()
    {
        if (!drawUI)//temp
        {
            textAreaString = GUI.TextArea(new Rect(300, 25, 450, 375), textAreaString, charLimit);
            if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 100, 250, 50), "Compile Code"))
            {
                CompileCode();

            }
            errorN = 0;
            foreach(KeyValuePair<int, string> error in errorList)
            {
                errorN++;
                GUI.Label(new Rect(10, 10 * errorN, 1000, 20), error.Value + " at line " + error.Key.ToString());
            }
        }            
    }
    void Start()
    {
        checkpointA.GetComponent<Transform>();
        checkpointB.GetComponent<Transform>();
        checkpointC.GetComponent<Transform>();
    }
    void CompileCode()
    {
        errorList.Clear();
        List<int> loopLines = new List<int>();
        bool looping = false;
        char[] delimiter = new[] { ';', '(', ')', ',', ' '};
        List<string> elementsInCode = new List<string>();
        List<string> listOfCommands = new List<string>();
        bool runCode = true;
        int increment = 0;

        string[] linesOfCode = textAreaString.Split('\n'); //split the string into lines
        for (int j = 0; j < linesOfCode.Length; j++)
        {
            Debug.Log(linesOfCode[j]);
            elementsInCode = linesOfCode[j].Split(delimiter).ToList(); //split each line into commands
            for (int i = 0; i < elementsInCode.Count; i++)
            {
                Debug.Log(elementsInCode[i]);
                if (elementsInCode[i] == "")//check if there is nothing in the string
                {
                    elementsInCode.RemoveAt(i);//remove the element 
                    i -= 1;//go backwards in the loop because an element was just removed
                }
                else if (!runCode)//check if we run this line of code
                {
                    if (elementsInCode[i] == "{")//check if there are other brackets within the brackets
                    {
                        increment += 1;
                    }
                    if (elementsInCode[i] == "}") //close brackets
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
                    switch (elementsInCode[i])//reads the command
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
                        case "if":
                            //check condition
                            //set accessBrackets to false if the condition is not met
                            runCode = false; //example
                            break;
                        case "splitAt":
                            int splitAt;
                            if (int.TryParse(elementsInCode[i+1], out splitAt))
                            {
                                listOfCommands.Add("Split");
                                if (splitAt == 2)
                                {                                   
                                    listOfCommands.Add("Every other");
                                }
                                else if(splitAt == 3)
                                {                                    
                                    listOfCommands.Add("Every Third");
                                }
                            }
                            else
                            {
                                errorList.Add(new KeyValuePair<int, string>(j, "Can't split at checkpoint " + elementsInCode[i + 1]));
                                //send error
                            }
                            break;
                        case "Attack":
                            listOfCommands.Add("Attack");
                            break;
                        case "defend":
                            listOfCommands.Add("Defend");
                            break;
                        case "produce":
                            listOfCommands.Add("Produce");
                            break;
                        case "moveTo":
                            bool containsCheckpoint = false;
                            listOfCommands.Add("Move");
                            string[] checkpoints = new[] { "Homebase", "A", "B", "C" };
                            foreach(string s in checkpoints)
                            {
                                if (elementsInCode[i + 1] == s)
                                {
                                    listOfCommands.Add(elementsInCode[i + 1]);
                                    break;
                                }
                            }
                            if(!containsCheckpoint)
                            {
                                errorList.Add(new KeyValuePair<int, string>(j, "No eligible checkpoint"));
                                //send error
                            }
                            break;
                        default:
                            Debug.Log("'" + elementsInCode[i] + "'");
                            errorList.Add(new KeyValuePair<int, string>(j, "No known command"));
                            break;
                    }
                }
            }
        }//end for loop              
    }
}