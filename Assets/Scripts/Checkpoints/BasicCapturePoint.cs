using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BasicCapturePoint : MonoBehaviour {

    UnitArrays uArray;
    GameObject unitManager;
    public Image elephantCapture;
    public Image giraffeCapture;

    public GameObject closestEnemy = null;
    public bool enemyHasCapturePoint = false;
    float enemyDistanceToCapturePoint;
    List<GameObject> closeEnemies = new List<GameObject>();

    float CaptureTime = 50f;
    float maxCaptureTimer;
    float avCaptureTimer;
    public  float CaptureTimer;
    float upperMidCaptureTime;
    float lowerMidCaptureTime;
    float distanceNeededToCapture = 5;
    public bool neutralCapturePoint = true;
    int modIndex = 1;

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
        closestEnemy = uArray.scan(this.gameObject, "Ally");

        if (closestEnemy != null)
        {
            enemyDistanceToCapturePoint = Vector3.Distance(transform.position, closestEnemy.transform.position);

            if (enemyDistanceToCapturePoint <= distanceNeededToCapture)
            {
                for (int i = 0; i < uArray.enemies.Length; i++)
                {
                    if(closeEnemies[i] == null)
                    {
                        closeEnemies.Add(closestEnemy);
                        break;
                    }
                    enemyDistanceToCapturePoint = Vector3.Distance(this.gameObject.transform.position, closeEnemies[i].transform.position);
                    if(playerDistanceToCapturePoint > distanceNeededToCapture)
                    {
                        closeEnemies.Remove(closestEnemy);
                    }
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
        }
    }

    void PlayerCapturePoint()
    {
        closestPlayer = uArray.scan(this.gameObject, "Enemy");

        if (closestPlayer != null)
        {
            playerDistanceToCapturePoint = Vector3.Distance(transform.position, closestPlayer.transform.position);

            if (playerDistanceToCapturePoint <= distanceNeededToCapture)
            {
                for (int i = 0; i < uArray.allies.Length; i++)
                {
                    if(closePlayers[i] == null)
                    {
                        closePlayers.Add(closestPlayer);
                        break;
                    }
                        playerDistanceToCapturePoint = Vector3.Distance(this.gameObject.transform.position, closePlayers[i].transform.position);
                        if(playerDistanceToCapturePoint > distanceNeededToCapture)
                        {
                            closePlayers.Remove(closestPlayer);
                        }
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
    }

	// Update is called once per frame
	void Update ()
    {
        if (modIndex % 2 == 0)
        {
            PlayerCapturePoint();
            modIndex++;
        }
        else
        {
            EnemyCapturePoint();
            modIndex++;
        }

        if((CaptureTimer == CaptureTime / 2))
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

        if(CaptureTimer > CaptureTime / 2)
        {
            giraffeCapture.fillAmount = (CaptureTimer / avCaptureTimer) - avCaptureTimer;
        }
        else if(CaptureTimer < CaptureTime / 2)
        {
            elephantCapture.fillAmount = CaptureTimer / avCaptureTimer;
        }
	}
}
