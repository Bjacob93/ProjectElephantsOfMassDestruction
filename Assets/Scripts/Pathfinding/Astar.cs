﻿using UnityEngine;
using Pathfinding;

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
	public float meleeRange = 3f;
	public float engagementRange = 10f;
	bool isInMeleeRange = false;
	bool hasPathToEnemy = false;
	bool goToWaypoint = true;
	bool movingToWaypoint = true;

    //Boolean controlled by checkpoints
    public bool receivedNewDestination = false;

    //Cache variables that limits calls to pathfinding to once every second.
    bool pathCompleted = false;


	//Cache variables for enemies
	public GameObject nearestEnemy = null;
	public GameObject previousEnemy = null;
	public float distanceToEnemy;
    UnitArrays uArray;
    GameObject unitManager;

    //Float determines when a waypoint is close enough. Int references current target waypoint.
    public float maxWaypointDistance = 3f;
	private int currentWaypoint;

	void Start () {
		//Reference the seeker and controller component.
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();

		//Call the pathfinding method in 
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
        unitManager = GameObject.Find("UnitManager");
        uArray = unitManager.GetComponent<UnitArrays>();
    }

	/** Method to print out errors in the log if we get any. If we don't, it will set first waypoint 
	in the path to be the current waypoint for the unit. */
	void OnPathComplete(Path p){
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
            pathCompleted = true;
		}
	}

	//Method for finding all enemies.
	void FindNearestEnemy () {
        //Put all enemies into an array, then find the one which is nearest.
        nearestEnemy = uArray.scan(this.gameObject, "Enemy");

        if (nearestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        }

        PathToEnemy();

	}

	//Method for pathing to the nearest enemy.
	void PathToEnemy () {
		if (pathCompleted && !hasPathToEnemy) {
			//Generate new path to nearest enemy, if within engagement range.
			if (nearestEnemy != null && distanceToEnemy <= engagementRange) {
                if (pathCompleted) {
                    pathCompleted = false;
                    seeker.StartPath(transform.position, nearestEnemy.transform.position, OnPathComplete);
                    previousEnemy = nearestEnemy;
                    hasPathToEnemy = true;
                    goToWaypoint = false;
                    movingToWaypoint = false;
                }
				
			}
		} else if (nearestEnemy != null && distanceToEnemy <= engagementRange && previousEnemy != nearestEnemy) {
			hasPathToEnemy = false;
			goToWaypoint = false;
		} else if ((distanceToEnemy >= engagementRange && hasPathToEnemy) || nearestEnemy == null) {
			hasPathToEnemy = false;
			goToWaypoint = true;
            previousEnemy = null;
        }

        //Determine if enemy is within melee range
        if (nearestEnemy != null && distanceToEnemy < meleeRange)
        {
            isInMeleeRange = true;
        }
        else if (nearestEnemy == null || distanceToEnemy > meleeRange)
        {
            isInMeleeRange = false;
        }
    }

    //Make a new path to the target position, if not currently pathing to enemy, and not already pathing to the targe position.
    void PathToWaypoint()
    {
        if (goToWaypoint && !movingToWaypoint)
        {
            if (pathCompleted)
            {
                movingToWaypoint = true;
                pathCompleted = false;
                seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            }
        }
    }

    //Path to new destination, if a new destination has been received by checkpoint, building, etc.
    void PathToNewDestination()
    {
        if (pathCompleted)
        {
            pathCompleted = false;
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }
        receivedNewDestination = false;
    }

	//Method that rotates the unit towards its target.
	void RotateUnit(Vector3 direction){ 

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

		//If no enemy is near enough, make sure unit is pathing to waypoint.
		PathToWaypoint();

        //Path to another waypoint, if an update to destination has been received.
        if (receivedNewDestination) {
            PathToNewDestination();
        }

		//If there is no path.
		if (path == null) {
			return;
		}

		if (isInMeleeRange) {
			return;
		} else {
		
		}
		//If the unit has reached it's goal.
		if (currentWaypoint >= path.vectorPath.Count) {
			return;
		}
	
		//Set unit to look in the direction it is travelling
		direction = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		RotateUnit (direction);

		//Move the unit
		Move (direction, path);
	}
}