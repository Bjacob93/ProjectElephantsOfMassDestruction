using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

    //Cache button coordinates and dimensions.
    private Rect    button;
    private float   buttonX,
                    buttonY,
                    buttonWidth,
                    buttonHeight;

    //Cache skin.
    private GUISkin buttonSkin;

    //Cache the PauseScript.
    PauseScript pauseScript;

	void Start () {

        //Calculate dimensions.
        buttonX = Screen.width / 2 + Screen.width / 15;
        buttonY = Screen.height / 100;
        buttonWidth = Screen.width / 15;
        buttonHeight = Screen.width / 15;
        button = new Rect(buttonX, buttonY, buttonWidth, buttonHeight);

        //Reference skin.
        buttonSkin = Resources.Load("Graphix/interfaceButtons") as GUISkin;

        //Reference the pauseScript.
        pauseScript = GameObject.Find("UIButtons").GetComponent<PauseScript>();


    }

    void OnGUI()
    {
        //Set the skin.
        GUI.skin = buttonSkin;

        //Only show the Play button if the game is paused.
        if (pauseScript.GetPauseStatus())
        {
            //If the button is pressed, un-pause the game.
            if (GUI.Button(button, "", buttonSkin.GetStyle("PlayButton"))) pauseScript.PausePlay();
        }  
    }
}
