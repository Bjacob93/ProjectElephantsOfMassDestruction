using UnityEngine;
using System.Collections;

[System.Serializable]
public class Command {

    //Variables for all commands
    public string commandName;
	public string commandId;
    public string commandDesc;

    public bool requiresVariable;
    public bool availableAtCheckpoint;
    public bool availableAtBase;

    //Empty constructor for a command
    public Command()
    {
        commandName = "";
    }

    //Constructor for specific commands
	public Command (string name, string id, string desc, bool reqVar, bool atCheck, bool atBase)
    {
        commandName = name;
        commandId = id;
        commandDesc = desc;
        requiresVariable = reqVar;
        availableAtBase = atBase;
        availableAtCheckpoint = atCheck;
    }
}
