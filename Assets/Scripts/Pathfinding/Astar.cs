using UnityEngine;
using System.Collections;
using Pathfinding;

//TODO: make unit face the traveled direction.

public class Astar : MonoBehaviour {

	//Cache target coordinates, seeker, controller and path.
	public Vector3 targetPosition;
	Seeker seeker;
	CharacterController controller;
	Path path;

	//Cache variables for behaviour
	public float speed = 30f;
	public Vector3 lookDirection;
	float meleeRange = 1f;
	float engagementRange = 10f;
	bool isInMeleeRange = false;

	//Cache variables that limits calls to pathfinding to once every second.
	float pathCooldown = 1;
	float pathCooldownRemaining = 0;

	//Cache variables for enemies
	public GameObject nearestEnemy;
	float distFar = Mathf.Infinity;

	//Float determines when a waypoint is close enough. Int references current target waypoint.
	public float maxWaypointDistance = 3f;
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

		//Put all enemies into an array, then find the one which is nearest.
		GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag ("enemyUnits");
		nearestEnemy = null;
		distFar = Mathf.Infinity;
		float distNear = 0.0f;

		foreach (GameObject enemy in enemyUnits) {
			distNear = Vector3.Distance (transform.position, enemy.transform.position);

			if (nearestEnemy == null || distNear < distFar) {
				nearestEnemy = enemy;
				distFar = distNear;
			}
		}

		pathCooldownRemaining -= Time.deltaTime;

		if (pathCooldownRemaining <= 0) {
			pathCooldownRemaining = pathCooldown;
			//Generate new path to nearest enemy, if within engagement range.
			if (nearestEnemy != null && distNear <= engagementRange) {
				seeker.StartPath (transform.position, nearestEnemy.transform.position, OnPathComplete);
		
				//Determine if enemy is within melee range
				if (distNear < meleeRange) {
					isInMeleeRange = true;
				} else {
					isInMeleeRange = false;
				}
			}
		}

		//If there is no path.
		if (path == null) {
			Debug.Log ("No path");
			return;
		}

		if (isInMeleeRange) {

			Debug.Log ("I'm in range");
			return;
		} else {
		
		}
		//If the unit has reached it's goal.
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("I'm here now");
			return;
		}
	
		//Set unit to look in the direction it is travelling
		lookDirection = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		lookDirection.y = 0;
		Quaternion lookRotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (lookDirection), 360 * Time.deltaTime);
		transform.rotation = Quaternion.Lerp ( transform.rotation, lookRotation, Time.deltaTime * 5);

		Debug.Log ("Moving");
		//Set's the direction of movement to a vector form current position to next waypoint, then calls the SimpleMove command in the CharacterController.
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized * speed * Time.deltaTime;
		controller.SimpleMove (dir);

		//If it has reached it's current waypoint.
		if (Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]) < maxWaypointDistance) {
			currentWaypoint++;
		}
	}
}

