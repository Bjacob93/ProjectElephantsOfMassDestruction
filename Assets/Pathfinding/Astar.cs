using UnityEngine;
using System.Collections;
using Pathfinding;

public class Astar : MonoBehaviour {

	//Cache target coordinates, seeker, controller and path.
	public Vector3 targetPosition;

	Seeker seeker;
	CharacterController controller;
	Path path;

	//The speed with which the unit will move
	public float speed = 30;

	//Float determines when a waypoint is close enough. Int references current target waypoint.
	public float maxWaypointDistance = 3;
	private int currentWaypoint;

	void Start () {
		//Reference the seeker and controller component.
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();

		//Call the pathfinding method in 
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

	//Method to print out errors in the log if we get any. If we don't, it will set first waypoint in the path to be the current waypoint for the unit.
	void OnPathComplete(Path p){
		Debug.Log ("Path Completed" + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}
		

	void Update () {
		//If there is no path.
		if (path == null) {
			return;
		}

		//If the unit has reached it's goal.
		if (currentWaypoint >= path.vectorPath.Count) {
			return;
		}

		//Set's the direction of movement to a vector form current position to next waypoint, then calls the SimpleMove command in the CharacterController.
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized * speed * Time.deltaTime;
		controller.SimpleMove (dir);

		//If it has reached it's current waypoint.
		if (Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]) < maxWaypointDistance) {
			currentWaypoint++;
		}
	}
}
