using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BuyEnemyUnits : MonoBehaviour
{
    public float restartDelay = 5f;         // Time to wait before restarting the level, the number indicates seconds
    public float restartTimer;              // Timer to count up to restarting the level

    public int amountOfEnemySpawned = 0;    // Keep track of the amount of enemies spawn.
    public int maxEnemySpawn = 20;          // Used to set the maximum amount of enemies a level can spawn. 
                                            // This is used to define when the palyer wins eg when there are 0 enemies left the player wins
	public bool victoryCheck = false;
    public GameObject enemyUnits;

    UnitArrays listOfEnemyUnits;

    PauseScript pause = null;

    ScoreManager sm;

    float waitTime = 5f;
    float wattTimeTimer;
    float victoryTimer;

    void Start()
    {
        listOfEnemyUnits = GameObject.Find("UnitManager").GetComponent<UnitArrays>();

        pause = GameObject.Find("UIButtons").GetComponent<PauseScript>();

        if(pause == null)
        {
            Debug.Log("Didn't find pause");
        }
        sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

    }

	void victory(){
		sm.Victory();
	}
    void Update()
    {

        if (pause.GetPauseStatus())
        {
            return;
        }

        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            if (amountOfEnemySpawned < maxEnemySpawn)
            {
                Instantiate(enemyUnits, this.transform.position, Quaternion.Euler(0, 0, 0));
                restartTimer = 0;
                amountOfEnemySpawned++;
            }
        }

        if (amountOfEnemySpawned == maxEnemySpawn)
        {
            victoryTimer += Time.deltaTime;
            if(victoryTimer >= waitTime)
            {
                for (int i = 0; i < listOfEnemyUnits.enemies.Length; i++)
                {
                    if (listOfEnemyUnits.enemies[i] != null)
                    {
                        break;
                    }
                    else if (i == listOfEnemyUnits.enemies.Length - 1 && sm.PlayerControlsAllPoints())
                    {
						if(!victoryCheck){
							victory ();
							victoryCheck=true;
						}
						timeupdate ();

						}
                }
            }
        }
    }

    void timeupdate()
	{
		if (!sm.victoryMusic.isPlaying) {
			wattTimeTimer += Time.deltaTime;
			if (wattTimeTimer >= waitTime) {
				SceneManager.LoadSceneAsync ("Scenes/mainMenu", LoadSceneMode.Single);
			}
		}
	}
    void OnMouseDown()
    {

    }
}
