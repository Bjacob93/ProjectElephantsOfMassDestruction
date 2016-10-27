using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    BuyEnemyUnits enemySpawn;
    HomebaseGUI homebasePauseSetting;

    GameObject resetButton;
    GameObject playButton;

    void Start()
    {
        enemySpawn = GameObject.FindGameObjectWithTag("EnemyBase").GetComponent<BuyEnemyUnits>();
        homebasePauseSetting = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<HomebaseGUI>();
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
        homebasePauseSetting.gameIsPaused = !homebasePauseSetting.gameIsPaused;


    }

}
