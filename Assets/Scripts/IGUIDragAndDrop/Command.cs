using UnityEngine;
using System.Collections;

[System.Serializable]
public class Command {

    //Variables for all commands
    public string commandName;
	public string commandId;
    public string commandDesc;

    //Empty constructor for a command
    public Command()
    {
        commandName = "";
    }

    //Constructor for specific commands
	public Command (string name, string id, string desc)
    {
        commandName = name;
        commandId = id;
        commandDesc = desc;
    }
}
