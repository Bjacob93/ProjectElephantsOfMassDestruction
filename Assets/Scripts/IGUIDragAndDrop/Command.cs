using UnityEngine;
using System.Collections;

public class Command : MonoBehaviour {

    public string commandName;
    public int commandId;
    public string commandDesc;

    public Command()
    {

    }

    public Command (string name, int id, string desc)
    {
        commandName = name;
        commandId = id;
        commandDesc = desc;
    }
}
