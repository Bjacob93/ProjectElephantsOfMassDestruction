﻿using UnityEngine;
using System.Collections;
using Pathfinding;

//TODO: attack?

public class Astar : MonoBehaviour {

	//Cache target coordinates, seeker, controller and path.
	public Vector3 targetPosition;
	Seeker seeker;
	CharacterController controller;
	Path path;

	//Cache variables for behaviour
	public float speed = 30f;
	float rotationSpeed = 10f;
	public Vector3 direction;
	float meleeRange = 1f;
	float engagementRange = 10f;
	bool isInMeleeRange = false;
	bool hasPathToEnemy = false;

	//Cache variables that limits calls to pathfinding to once every second.
	float pathCooldown = 1;
	float pathCooldownRemaining = 0;

	//Cache variables for enemies
	public GameObject nearestEnemy;
	float distanceToEnemy;
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

	//Method for finding all enemies.
	void FindNearestEnemy () {
		//Put all enemies into an array, then find the one which is nearest.
		GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag ("enemyUnits");
		nearestEnemy = null;
		distFar = Mathf.Infinity;

		if (enemyUnits != null) {
			foreach (GameObject enemy in enemyUnits) {
				distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);

				if (nearestEnemy == null || distanceToEnemy < distFar) {
					nearestEnemy = enemy;
					distFar = distanceToEnemy;
				}
			}
			PathToEnemy();
		}
	}

	void PathToEnemy () {
		if (pathCooldownRemaining <= 0 && !hasPathToEnemy) {
			pathCooldownRemaining = pathCooldown;
			//Generate new path to nearest enemy, if within engagement range.
			if (nearestEnemy != null && distanceToEnemy <= engagementRange) {
				seeker.StartPath (transform.position, nearestEnemy.transform.position, OnPathComplete);
				hasPathToEnemy = true;

				//Determine if enemy is within melee range
				if (distanceToEnemy < meleeRange) {
					isInMeleeRange = true;
				} else {
					isInMeleeRange = false;
				}
			}
		} else if (distanceToEnemy >= engagementRange && hasPathToEnemy) {
			hasPathToEnemy = false;
		}
	}

	//Method that rotates the unit towards its target.
	void RotateUnit(Vector3 direction){
		pathCooldownRemaining -= Time.deltaTime;

		if (direction == Vector3.zero) {
			return;
		} else {
			Quaternion rotation = transform.rotation;
			Quaternion toTarget = Quaternion.LookRotation(direction);

			rotation = Quaternion.Slerp (rotation, toTarget, rotationSpeed * Time.deltaTime);
			Vector3 euler = rotation.eulerAngles;
			euler.z = 0;
			euler.x = 0;
			rotation = Quaternion.Euler (euler);

			transform.rotation = rotation;
		}
	}

	//Method for moving the unit.
	void Move(Vector3 direction, Path path){
		//Set's the direction of movement to a vector form current position to next waypoint, then calls the SimpleMove command in the CharacterController.
		Vector3 dir = direction * speed * Time.deltaTime;
		controller.SimpleMove (dir);

		//If it has reached it's current waypoint.
		if (Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]) < maxWaypointDistance) {
			currentWaypoint++;
		}
	}


	void Update () {

		//Find nearest enemy, and path to it if it exist and is close enough.
		FindNearestEnemy();

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
		direction = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		RotateUnit (direction);

		//Move the unit
		Move (direction, path);
	}
}

