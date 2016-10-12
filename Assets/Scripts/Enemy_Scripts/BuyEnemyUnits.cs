using UnityEngine;
using System.Collections;

public class BuyEnemyUnits : MonoBehaviour
{

    public float restartDelay = 10f;         // Time to wait before restarting the level
    float restartTimer;                     // Timer to count up to restarting the level

    public GameObject enemyUnits;

    void start()
    {
        
    }

    void Update()
    {
        restartTimer += Time.deltaTime;
        
        if (restartTimer >= restartDelay)
        {
            Instantiate(enemyUnits, new Vector3(-45, 1, -45), Quaternion.Euler(0, 0, 0));
            restartTimer = 0;
        }
    }

    void OnMouseDown()
    {

    }
}
