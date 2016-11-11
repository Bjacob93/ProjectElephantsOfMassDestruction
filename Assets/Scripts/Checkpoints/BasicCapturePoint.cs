using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BasicCapturePoint : MonoBehaviour {

    UnitArrays uArray;
    GameObject unitManager;
    public Image elephantCapture;
    public Image giraffeCapture;
    public Image NeutralCapture;
    GameObject captureTimeGO;

    public GameObject closestEnemy = null;
    public bool enemyHasCapturePoint = false;
    float enemyDistanceToCapturePoint;
    List<GameObject> closeEnemies = new List<GameObject>();

    float CaptureTime = 50f;
    float maxCaptureTimer;
    float avCaptureTimer;
    public float CaptureTimer;
    float upperMidCaptureTime;
    float lowerMidCaptureTime;
    public float distanceNeededToCapture = 10;
    public bool neutralCapturePoint = true;
    public int modIndex = 1;

    public GameObject closestPlayer = null;
    public bool playerHasCapturePoint = false;
    float playerDistanceToCapturePoint;
    List<GameObject> closePlayers = new List<GameObject>();

    // Use this for initialization
    void Start () {
        maxCaptureTimer = CaptureTime;
        avCaptureTimer = maxCaptureTimer / 2;
        CaptureTimer = CaptureTime / 2;

        unitManager = GameObject.Find("UnitManager");
        uArray = unitManager.GetComponent<UnitArrays>();
    }

    void EnemyCapturePoint()
    {
        closestEnemy = uArray.scan(this.gameObject, "Enemy");

        if (closestEnemy != null)
        {
            enemyDistanceToCapturePoint = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (enemyDistanceToCapturePoint <= distanceNeededToCapture)
            {
                if (!closeEnemies.Contains(closestEnemy))
                {
                    closeEnemies.Add(closestEnemy);
                    Debug.Log(closeEnemies.Count);
                }

                if (playerHasCapturePoint || (!playerHasCapturePoint && !enemyHasCapturePoint))
                {
                    CaptureTimer -= Time.deltaTime * closeEnemies.Count;
                    if (CaptureTimer <= 0)
                    {
                        CaptureTimer = 0;
                        playerHasCapturePoint = false;
                        enemyHasCapturePoint = true;
                        neutralCapturePoint = false;
                    }
                }
            }
            for (int i = 0; i < closeEnemies.Count; i++)
            {
                if (enemyDistanceToCapturePoint > distanceNeededToCapture)
                {
                    closeEnemies.RemoveAt(i);
                }
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
                    Debug.Log(CaptureTimer);
                    if (CaptureTimer >= maxCaptureTimer)
                    {
                        CaptureTimer = maxCaptureTimer;
                        playerHasCapturePoint = true;
                        enemyHasCapturePoint = false;
                        neutralCapturePoint = false;
                    }
                }
            }
            for (int i = 0; i < closePlayers.Count; i++)
            {
                if (playerDistanceToCapturePoint > distanceNeededToCapture)
                {
                    closePlayers.RemoveAt(i);
                }
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        PlayerCapturePoint();
        EnemyCapturePoint();

        if(CaptureTimer == avCaptureTimer)
        {
            playerHasCapturePoint = false;
            enemyHasCapturePoint = false;
            neutralCapturePoint = true;
        }

        if(neutralCapturePoint == true && closeEnemies.Count <= 0 && closePlayers.Count <= 0)
        {
            if(CaptureTimer < avCaptureTimer)
            {
                CaptureTimer += Time.deltaTime;
            }else if(CaptureTimer > avCaptureTimer)
            {
                CaptureTimer -= Time.deltaTime;
            }
        }

        if(CaptureTimer > avCaptureTimer)
        {
            giraffeCapture.fillAmount = (CaptureTimer-avCaptureTimer) / avCaptureTimer;
        }
        else if(CaptureTimer < avCaptureTimer)
        {
            elephantCapture.fillAmount = (avCaptureTimer - CaptureTimer) / avCaptureTimer;
        }
	}
}
