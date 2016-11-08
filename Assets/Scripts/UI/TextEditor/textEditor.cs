using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class textEditor : MonoBehaviour
{

    int charLimit = 250;
    private string textAreaString = "moveTo (A)\nsplitAt (2)";
    private bool drawSequenceEditor = false;
    List<KeyValuePair<int, string>> errorList = new List<KeyValuePair<int, string>>();
    List<string> listOfCommands = new List<string>();
    public bool belongsToCheckpoint;
    public string listID;
    
    void OnGUI()
    {
        if (drawSequenceEditor)
        {
            textAreaString = GUI.TextArea(new Rect(300, 25, 450, 375), textAreaString, charLimit);
            if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 100, 250, 50), "Compile Code"))
            {
                CompileCode();

            }
            int errorN = 0;
            foreach(KeyValuePair<int, string> error in errorList)
            {
                errorN++;
                GUI.Label(new Rect(10, 10 * errorN, 1000, 20), error.Value + " at line " + error.Key.ToString());
            }
        }
    }    
    void CompileCode()
    {
        errorList.Clear();
        char[] delimiter = new[] { ')', '(', ' '};
        List<string> elementsInCode = new List<string>();
        string[] linesOfCode = textAreaString.Split('\n'); //split the string into lines
        for (int j = 0; j < linesOfCode.Length; j++)
        {
            elementsInCode = linesOfCode[j].Split(delimiter).ToList(); //split each line into commands
            for (int i = 0; i < elementsInCode.Count; i++)
            {
                if (elementsInCode[i] == "")//check if there is nothing in the string
                {
                    elementsInCode.RemoveAt(i);//remove the element
                    i -= 1;//go backwards in the loop because an element was just removed
                }
            }            
            for (int i = 0; i < elementsInCode.Count; i++)
            {                
                switch (elementsInCode[i])//reads the command
                {
                    case "splitAt":
                        if (i + 1 < elementsInCode.Count)
                        {
                            if (elementsInCode[i + 1] == "2")
                            {
                                listOfCommands.Add("Split");
                                listOfCommands.Add("Every other");
                                i += 1;
                            }
                            else if (elementsInCode[i + 1] == "3")
                            {
                                listOfCommands.Add("Split");
                                listOfCommands.Add("Every Third");
                                i += 1;
                            }
                            else
                            {
                                errorList.Add(new KeyValuePair<int, string>(j, "Can't split at " + elementsInCode[i + 1]));
                                //send error
                            }
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }                        
                        break;
                    case "attack":
                        if (i + 1 < elementsInCode.Count)
                        {
                            ValidCheckpoint(elementsInCode[i + 1], "Attack", j);
                            i += 1;
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }
                        break;
                    case "defend":
                        if (i + 1 < elementsInCode.Count)
                        {
                            ValidCheckpoint(elementsInCode[i + 1], "Defend", j);
                            i += 1;
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }
                        break;
                    case "produce":
                        listOfCommands.Add("Produce");
                        break;
                    case "moveTo":
                        if (i + 1 < elementsInCode.Count)
                        {
                            ValidCheckpoint(elementsInCode[i + 1], "Move", j);
                            i += 1;
                        }
                        else
                        {
                            errorList.Add(new KeyValuePair<int, string>(j, "No argument in " + elementsInCode[i]));
                        }
                        break;
                    default:
                        errorList.Add(new KeyValuePair<int, string>(j, "No known command"));
                        break;
                }
            }
        }        
    }    
    void ValidCheckpoint(string check, string command, int line)
    {
        string[] checkpoints = new[] { "Homebase", "A", "B", "C" };
        foreach (string s in checkpoints)
        {
            if (check == s)
            {
                listOfCommands.Add(command);   
                listOfCommands.Add(s);
                return;
            }
        }
        errorList.Add(new KeyValuePair<int, string>(line, "No eligible checkpoint"));
        return;
    }
}