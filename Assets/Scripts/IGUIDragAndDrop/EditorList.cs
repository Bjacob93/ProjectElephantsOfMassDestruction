using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
    public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();


}
