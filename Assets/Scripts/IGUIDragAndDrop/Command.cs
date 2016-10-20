using UnityEngine;
using System.Collections;

public class Command : MonoBehaviour {

    //Variables for all commands
    public string commandName;
    public int commandId;
    public string commandDesc;

    //Empty constructor for a command
    public Command()
    {

    }

    //Constructor for specific commands
    public Command (string name, int id, string desc)
    {
        commandName = name;
        commandId = id;
        commandDesc = desc;
    }
}
