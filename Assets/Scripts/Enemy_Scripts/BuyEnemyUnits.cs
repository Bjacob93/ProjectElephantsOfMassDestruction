using UnityEngine;
using System.Collections;

public class BuyEnemyUnits : MonoBehaviour
{

    public float restartDelay = 5f;         // Time to wait before restarting the level
    public float restartTimer;              // Timer to count up to restarting the level

    public int amountOfEnemySpawned = 0;    // Keep track of the amount of enemies spawn.
    public int maxEnemySpawn = 20;          // Used to set the maximum amount of enemies a level can spawn. 
                                            // This is used to define when the palyer wins eg when there are 0 enemies left the player wins

    public GameObject enemyUnits;

    public bool gameIsPaused = true;

    void start()
    {
        
    }

    void Update()
    {
        if (gameIsPaused)
        {
            return;
        }

        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            if (amountOfEnemySpawned < maxEnemySpawn)
            {
                Instantiate(enemyUnits, new Vector3(-45, 1, -45), Quaternion.Euler(0, 0, 0));
                restartTimer = 0;
                amountOfEnemySpawned++;
            }
        }
    }

    void OnMouseDown()
    {

    }
}
