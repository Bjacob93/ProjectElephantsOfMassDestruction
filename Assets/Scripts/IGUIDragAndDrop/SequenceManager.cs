using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceManager : MonoBehaviour {

    /**
     * This class handles all the Sequence Editors in the game. The list editorListGO contains a list for every checkpoint, 
     * each of which has an ID that is equal to the name of the checkpoint, as well as an entry for the homebase.
     */


    //Cache variables
    GameObject UImanager;
    GameObject[] checkpoints;
	public List<EditorList> editorlistGO = new List<EditorList>();
}
