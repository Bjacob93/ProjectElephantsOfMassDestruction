using UnityEngine;
using System.Collections;

public class HomebaseGUI : MonoBehaviour {

    //Initialise variables to give the base it's own sequence editor.
    SequenceManager sequenceManager;
    string baseName;
    EditorList listComponent;

    //The base's location
    public Vector3 location;

    //Boolean for checking if the game is paused
    public bool gameIsPaused;

    // Use this for initialization
    void Start () {
        //Find the SequenceManager, and same the name of the checkpoint to a string.
        sequenceManager = GameObject.Find("UIManager").GetComponent<SequenceManager>();
        baseName = this.gameObject.name;

        //Add the sequenceEditor component to the list in SequenceManager.
        listComponent = gameObject.AddComponent<EditorList>();
        listComponent.listID = baseName;
        listComponent.belongsToCheckpoint = false;

        sequenceManager.editorlistGO.Add(listComponent);

        location = gameObject.transform.position;

        gameIsPaused = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (gameIsPaused)
        {
            return;
        }


	
	}

    void OnMouseDown()
    {
        //If the mouse is clicked on the object, go through the list of SequenceEditors.

        for (int i = 0; i < sequenceManager.editorlistGO.Count; i++)
        {
            //If a list has an id equal to the checkpoint name, draw that editor window in the UI.
            if (sequenceManager.editorlistGO[i].listID == baseName)
            {
                sequenceManager.editorlistGO[i].drawEditorWindow = true;
            }
            //Every list which id is different from the checkpoint name should not be drawn.
            else
            {
                sequenceManager.editorlistGO[i].drawEditorWindow = false;
            }
        }
    }

}
