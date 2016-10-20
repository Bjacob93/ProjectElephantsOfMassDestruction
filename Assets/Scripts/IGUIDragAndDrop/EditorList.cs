using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorList : MonoBehaviour {

    //Initialise lists for the Editor window. These will contain the Command objects that has been dragged to the Editor-window.
    public List<Command> enteredCommands = new List<Command>();
    public List<Command> slots = new List<Command>();

    public CommandDatabase commandDatabase;

    private bool drawEditor = false;

    void Start()
    {
        //Cache the database of commands so that we can always find any command we need.
        commandDatabase = GameObject.FindGameObjectWithTag("CommandDatabase").GetComponent<CommandDatabase>();
    }

    void Update()
    {
        if (Input.GetButtonDown("SequenceEditor"))
        {
            drawEditor = !drawEditor;
        }
    }

    void OnGUI()
    {
        if (drawEditor)
        {
            DrawEditor();
        }
    }

    void DrawEditor()
    {

    }
}
