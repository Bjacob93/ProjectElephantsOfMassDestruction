﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
    public List<Command> availableCommands = new List<Command>();
    public List<Command> slots = new List<Command>();

    public CommandDatabase commandDatabase;

    void Start()
    {
        //Cache the database of commands so that we can always find any command we need.
        commandDatabase = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();
    }
}
