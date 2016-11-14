using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    HomebaseGUI homebasePauseSetting;

    GameObject resetButton;
    GameObject playButton;
    public bool gameIsPaused = true;

    Level1TutorialText tutorialtext1;
    void Start()
    {
        homebasePauseSetting = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<HomebaseGUI>();
        resetButton = GameObject.FindGameObjectWithTag("ResetButton");
        playButton = GameObject.FindGameObjectWithTag("PlayButton");

        tutorialtext1 = GameObject.Find("UIManager").GetComponent<Level1TutorialText>();
    }

    public void Pause()
    {
        tutorialtext1.pressPlay = true;

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
