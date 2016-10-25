using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceManager : MonoBehaviour {

	GameObject[] checkpoints;
	public List<EditorList> editorlistGO = new List<EditorList>();


	// Use this for initialization
	void Start () {
		checkpoints = GameObject.FindGameObjectsWithTag ("Checkpoint");
		foreach(GameObject e in checkpoints){
			string id = e.gameObject.name;
			editorlistGO.Add (new EditorList (id));
		}

		editorlistGO.Add (new EditorList ("GiraffeBase"));
	}

	void Update(){
		Debug.Log (editorlistGO.Count);
	}
}
