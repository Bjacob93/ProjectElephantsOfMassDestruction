using UnityEngine;
using System.Collections;
using Pathfinding;

public class Astar : MonoBehaviour {
	public Vector3 targetPosition;

	Seeker seeker;

	void Start () {
		seeker = GetComponent<Seeker> ();	

		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

	void OnPathComplete(Path p){
		Debug.Log ("Path Completed" + p.error);
	}
		

	void Update () {
	
	}
}
