using UnityEngine;
using System.Collections;

public class BasicCapturePoint : MonoBehaviour {

    UnitArrays uArray;
    GameObject unitManager;

    public GameObject closestEnemy = null;
    bool enemyHasCapturePoint = false;
    public float distanceToEnemy;
    float enemyCaptureTimer = 0f;

    float CaptureTimeNeeded = 10f;
    float distanceNeededToCapture = 5;

    public GameObject closestPlayer = null;
    bool playerHasCapturePoint = false;
    public float distanceToPlayer;
    float playerCaptureTimer = 0;

    // Use this for initialization
    void Start () {
        unitManager = GameObject.Find("UnitManager");
        uArray = unitManager.GetComponent<UnitArrays>();
    }
	
    void FindClosestEnemy()
    {
        closestEnemy = uArray.scan(this.gameObject, "Ally");

        if (closestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);

          if(distanceToEnemy <= distanceNeededToCapture) { 
            if (playerHasCapturePoint || (!playerHasCapturePoint && !enemyHasCapturePoint))
            {
                enemyCaptureTimer += Time.deltaTime;
                    if (playerCaptureTimer > 0 && playerCaptureTimer != 0)
                    {
                        playerCaptureTimer -= Time.deltaTime;
                    }
                if(enemyCaptureTimer >= CaptureTimeNeeded)
                {
                    playerHasCapturePoint = false;
                    enemyHasCapturePoint = true;
                }
             }
            }
        }
    }

    void FindClosestPlayer()
    {
        closestPlayer = uArray.scan(this.gameObject, "Enemy");

        if (closestPlayer != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, closestPlayer.transform.position);
            if (distanceToEnemy <= distanceNeededToCapture)
            {
                if (enemyHasCapturePoint || (!enemyHasCapturePoint && !playerHasCapturePoint))
                {
                    playerCaptureTimer += Time.deltaTime;
                    if (playerCaptureTimer >= CaptureTimeNeeded)
                    {
                        playerHasCapturePoint = true;
                        enemyHasCapturePoint = false;
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        FindClosestEnemy();
        FindClosestPlayer();
	}
}
