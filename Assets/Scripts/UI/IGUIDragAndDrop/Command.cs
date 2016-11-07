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

    public bool isVariable;

    public Vector3 locationOfTarget;

    public int variableForEveryX;

    //Empty constructor for a command
    public Command()
    {
        commandName = "";
    }

    //Constructor for specific commands
	public Command (string name, string id, string desc, bool reqVar, bool atCheck, bool atBase, Vector3 location, int varFoE, bool isVar)
    {
        commandName = name;
        commandId = id;
        commandDesc = desc;
        requiresVariable = reqVar;
        availableAtBase = atBase;
        availableAtCheckpoint = atCheck;
        locationOfTarget = location;
        variableForEveryX = varFoE;
        isVariable = isVar;
    }
}
