using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BasicCapturePoint : MonoBehaviour {

    //global caches used to both functions
    UnitArrays uArray;
    GameObject unitManager;
    public Image elephantCapture;
    public Image giraffeCapture;
    public Image NeutralCapture;
    GameObject captureTimeGO;

    float CaptureTime = 50f; // total amount of time, this can be anything, since the program uses 1/2 of this number to calculate when the points state is neutral
    float maxCaptureTimer; // used to cache the captureTime at the start of the scropt to make sure that it does not change in regards to the player
    public float avCaptureTimer; // cache avarage of the Capture time
    public float capturePoints; // this is the variable which increase or decrese depending on who is capturing the point
    private float distanceNeededToCapture = 5; // maximum distance units can be away to capture the point
    public bool neutralCapturePoint = true; // bool used as neutral state of the capture point
    public int modIndex = 1;

    // chace variables used for checking distance to enemies and if they can capture
    public GameObject closestEnemy = null; // cahce closest enemy
    public bool enemyHasCapturePoint = false; // bool used as enemy state of the capture point
    float enemyDistanceToCapturePoint; // enemies distance to the capture point
    List<GameObject> nearbyEnemies = new List<GameObject>(); // list of close enemies

    //cache variables used for checking distance to player and if they can capture. The things here are the same as above but for palyer units
    public GameObject closestPlayer = null; 
    public bool playerHasCapturePoint = false;
    float playerDistanceToCapturePoint;
    List<GameObject> nearbyPlayers = new List<GameObject>();

    levelManager lvlManager;

    // Use this for initialization
    void Start () {
        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();

		if (lvlManager.currentLevel == 4) 
		{
			distanceNeededToCapture = 10;
			CaptureTime = 40f;
		}
        // set the maxCapture timer to the capturePoints and avarage capturePoints
        maxCaptureTimer = CaptureTime; 
        avCaptureTimer = maxCaptureTimer / 2;
        capturePoints = CaptureTime / 2;

        // find the unitManager an UnitArrays
        unitManager = GameObject.Find("UnitManager"); 
        uArray = unitManager.GetComponent<UnitArrays>();
    }
    //The EnemyCapturePoint and PlayerCapturePoint are the same so comments will only be written in here, the only difference is the variables and strings
    void EnemyCapturePoint()
    {
        // run function form UnitArrays to find all Enemies on the map. "Enemy" is used to indicate what we are looking for
        closestEnemy = uArray.scan(this.gameObject, "Enemy"); 

        if (closestEnemy != null)
        {
            // calculate the distance from the capture point to the enemy
            enemyDistanceToCapturePoint = Vector3.Distance(transform.position, closestEnemy.transform.position); 
            // check if the enemy is close enough
            if (enemyDistanceToCapturePoint <= distanceNeededToCapture) 
            {
                // if our list of close enemies does not contain the current enemy add it
                if (!nearbyEnemies.Contains(closestEnemy)) 
                {
                    nearbyEnemies.Add(closestEnemy);
                }
                // timer used to capturePoint, the more units there are the faster it goes 
                capturePoints -= (Time.deltaTime * nearbyEnemies.Count) * 1.2f;
                // when the timer reaches 0 the enemies have the point and if the timer reaches maxCaptureTime the player has the capturePoint
                if (capturePoints <= 0) 
                {
                    // make sure we do not go lower than possible
                    capturePoints = 0; 
                    //change the different states of the bools to say that the enemy currently has the capturepoint
                    playerHasCapturePoint = false;
                    enemyHasCapturePoint = true;
                    neutralCapturePoint = false;

                }
            }  
        }
        //check if the enemy is still in range if not remove them from the list
        for (int i = 0; i < nearbyEnemies.Count; i++) 
        {
            if (enemyDistanceToCapturePoint > distanceNeededToCapture)
            {
                nearbyEnemies.RemoveAt(i);
            }
            // if the enemy was killed while they were range we remove them
            else if (nearbyEnemies[i] == null) 
            {
                nearbyEnemies.RemoveAt(i);
            }
        }
    }

    void PlayerCapturePoint()
    {
        closestPlayer = uArray.scan(this.gameObject, "Ally");

        if (closestPlayer != null)
        {
            playerDistanceToCapturePoint = Vector3.Distance(transform.position, closestPlayer.transform.position);    
            if (playerDistanceToCapturePoint <= distanceNeededToCapture)
            {
                if (!nearbyPlayers.Contains(closestPlayer))
                {
                    nearbyPlayers.Add(closestPlayer);
                }
                capturePoints += (Time.deltaTime * nearbyPlayers.Count) * 1.2f;
                if (capturePoints >= maxCaptureTimer)
                {
                    capturePoints = maxCaptureTimer;
                    playerHasCapturePoint = true;
                    enemyHasCapturePoint = false;
                    neutralCapturePoint = false;
                }                
            }
        }
        for (int i = 0; i < nearbyPlayers.Count; i++)
        {
            if (playerDistanceToCapturePoint > distanceNeededToCapture)
            {
                nearbyPlayers.RemoveAt(i);
            }
            else if (nearbyPlayers[i] == null)
            {
                nearbyPlayers.RemoveAt(i);
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        //run both function for capturing the point.
        PlayerCapturePoint();
        EnemyCapturePoint();

        if(capturePoints == avCaptureTimer) // check if the capturePoint is in neutral state.
        {
            playerHasCapturePoint = false;
            enemyHasCapturePoint = false;
            neutralCapturePoint = true;
        }

        // if the capturepoint is in neutral state and an enemy r player did not completly capture the point slowly reset the timer
        if (neutralCapturePoint == true && nearbyEnemies.Count <= 0 && nearbyPlayers.Count <= 0) 
        {
            // check if we are below the avarage time and reset
            if (capturePoints < avCaptureTimer) 
            {
                capturePoints += (Time.deltaTime / 2);
            }
            // check if we are above the avarage time and reset
            else if (capturePoints > avCaptureTimer) 
            {
                capturePoints -= (Time.deltaTime / 2);
            }
        }
        // fill the capture indicate for the giraffes if we are above the avarage timer and there are player units in range
        if (capturePoints > avCaptureTimer)
        {
            giraffeCapture.fillAmount = (capturePoints - avCaptureTimer) / avCaptureTimer;
        }
        // fill the capture indicate for the elepants if we are below the avarage timer and there are enemy units in range
        else if (capturePoints < avCaptureTimer) 
        {
            elephantCapture.fillAmount = (avCaptureTimer - capturePoints) / avCaptureTimer;
        }
	}
}
