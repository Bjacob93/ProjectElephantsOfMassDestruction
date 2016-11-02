using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    HomebaseGUI homebasePauseSetting;

    GameObject resetButton;
    GameObject playButton;
    public bool gameIsPaused = true;

    void Start()
    {
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

        gameIsPaused = !gameIsPaused;

        homebasePauseSetting.gameIsPaused = !homebasePauseSetting.gameIsPaused;
    }
}
