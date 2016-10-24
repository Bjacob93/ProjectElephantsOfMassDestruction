using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    BuyEnemyUnits enemySpawn;
    BuyUnits playerUnitProduction;

    GameObject resetButton;
    GameObject playButton;

    void Start()
    {
        enemySpawn = GameObject.FindGameObjectWithTag("EnemyBase").GetComponent<BuyEnemyUnits>();
        playerUnitProduction = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<BuyUnits>();
        resetButton = GameObject.FindGameObjectWithTag("ResetButton");
        playButton = GameObject.FindGameObjectWithTag("PlayButton");
    }

    public void Pause()
    {
        if (resetButton.activeSelf)
            resetButton.SetActive(false);
        else if (!resetButton.activeSelf)
            resetButton.SetActive(true);

        if (playButton.activeSelf)
            playButton.SetActive(false);
        else if (!playButton.activeSelf)
            playButton.SetActive(true);

        enemySpawn.gameIsPaused = !enemySpawn.gameIsPaused;
        playerUnitProduction.gameIsPaused = !playerUnitProduction.gameIsPaused;

    }

}
