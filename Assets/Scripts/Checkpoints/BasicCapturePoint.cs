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
    float avCaptureTimer; // cache avarage of the Capture time
    public float CaptureTimer; // this is the variable which increase or decrese depending on who is capturing the point
    public float distanceNeededToCapture = 10; // maximum distance units can be away to capture the point
    public bool neutralCapturePoint = true; // bool used as neutral state of the capture point
    public int modIndex = 1;

    // chace variables used for checking distance to enemies and if they can capture
    public GameObject closestEnemy = null; // cahce closest enemy
    public bool enemyHasCapturePoint = false; // bool used as enemy state of the capture point
    float enemyDistanceToCapturePoint; // enemies distance to the capture point
    List<GameObject> closeEnemies = new List<GameObject>(); // list of close enemies

    //cache variables used for checking distance to player and if they can capture. The things here are the same as above but for palyer units
    public GameObject closestPlayer = null; 
    public bool playerHasCapturePoint = false;
    float playerDistanceToCapturePoint;
    List<GameObject> closePlayers = new List<GameObject>();

    // Use this for initialization
    void Start () {
        maxCaptureTimer = CaptureTime; // set the maxCapture timer to the CaptureTimer
        avCaptureTimer = maxCaptureTimer / 2; // calculate the avarage CaptureTimer
        CaptureTimer = CaptureTime / 2;

        unitManager = GameObject.Find("UnitManager"); // find the unitManager
        uArray = unitManager.GetComponent<UnitArrays>(); // find the UnitArrays script on the unitManager
    }
    //The EnemyCapturePoint and PlayerCapturePoint are the same so comments will only be written in here, the only difference is the variables and strings
    void EnemyCapturePoint()
    {
        closestEnemy = uArray.scan(this.gameObject, "Enemy"); // run function form UnitArrays to find all Enemies on the map. "Enemy" is used to indicate what we are looking for

        if (closestEnemy != null) // we find an enemy do the following
        {
            enemyDistanceToCapturePoint = Vector3.Distance(transform.position, closestEnemy.transform.position); // calculat the distance form the capture point it the enemy
            if (enemyDistanceToCapturePoint <= distanceNeededToCapture) // check if the enemy is close enough
            {
                if (!closeEnemies.Contains(closestEnemy)) // if our list of close enemies does not contain the current enemy add it
                {
                    closeEnemies.Add(closestEnemy);
                }

                if (playerHasCapturePoint || (!playerHasCapturePoint && !enemyHasCapturePoint)) //if the enemy does not currently control the capturePpoint capture it
                {
                    CaptureTimer -= Time.deltaTime * closeEnemies.Count; // timer used to capturePoint, the more units there are the faster it goes 
                    if (CaptureTimer <= 0) // when the timer reaches 0 the enemies have the point and if the timer reaches maxCaptureTime the player has the capturePoint
                    {
                        CaptureTimer = 0; // make sure we do not go lower than possible
                        //change the different states of the bools to sya that the enemy currently has the capturepoint
                        playerHasCapturePoint = false;
                        enemyHasCapturePoint = true;
                        neutralCapturePoint = false;
                    }
                }
            }  
        }
        for (int i = 0; i < closeEnemies.Count; i++) //check if the enemy is still in range if not remove them from the list
        {
            if (enemyDistanceToCapturePoint > distanceNeededToCapture)
            {
                closeEnemies.RemoveAt(i);
            }
            else if (closeEnemies[i] == null) // if the enemy was killed while they were range we remove them
            {
                closeEnemies.RemoveAt(i);
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
                if (!closePlayers.Contains(closestPlayer))
                {
                    closePlayers.Add(closestPlayer);
                }
               
                if (enemyHasCapturePoint || (!enemyHasCapturePoint && !playerHasCapturePoint))
                {
                    CaptureTimer += Time.deltaTime * closePlayers.Count;
                    if (CaptureTimer >= maxCaptureTimer)
                    {
                        CaptureTimer = maxCaptureTimer;
                        playerHasCapturePoint = true;
                        enemyHasCapturePoint = false;
                        neutralCapturePoint = false;
                    }
                }
            }
        }
        for (int i = 0; i < closePlayers.Count; i++)
        {
            if (playerDistanceToCapturePoint > distanceNeededToCapture)
            {
                closePlayers.RemoveAt(i);
            }
            else if (closePlayers[i] == null)
            {
                closePlayers.RemoveAt(i);
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        //run both function for capturing the point.
        PlayerCapturePoint();
        EnemyCapturePoint();

        if(CaptureTimer == avCaptureTimer) // check if the capturePoint is in neutral state.
        {
            playerHasCapturePoint = false;
            enemyHasCapturePoint = false;
            neutralCapturePoint = true;
        }

        if(neutralCapturePoint == true && closeEnemies.Count <= 0 && closePlayers.Count <= 0) // if the capturepoint is in neutral state and an enemy r player did not completly capture the point slowly reset the timer
        {
            if (CaptureTimer < avCaptureTimer) // check if we are below the avarage time and reset
            {
                CaptureTimer += Time.deltaTime;
            }else if(CaptureTimer > avCaptureTimer) // check if we are above the avarage time and reset
            {
                CaptureTimer -= Time.deltaTime;
            }
        }
        if (CaptureTimer > avCaptureTimer) // fill the capture indicate for the giraffes if we are above the avarage timer and there are player units in range
        {
            giraffeCapture.fillAmount = (CaptureTimer-avCaptureTimer) / avCaptureTimer;
        }
        else if(CaptureTimer < avCaptureTimer) // fill the capture indicate for the elepants if we are below the avarage timer and there are enemy units in range
        {
            elephantCapture.fillAmount = (avCaptureTimer - CaptureTimer) / avCaptureTimer;
        }
	}
}
