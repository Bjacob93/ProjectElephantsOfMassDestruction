using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
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

    //Cache the GameReset script.
    gameReset resetScript;

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

        //Reference the resetScript.
        resetScript = GameObject.Find("UIButtons").GetComponent<gameReset>();
    }

    void OnGUI()
    {
        //Set the skin.
        GUI.skin = buttonSkin;

        //Only show the Reset button if the game is un-paused.
        if (!pauseScript.GetPauseStatus())
        {
            //If the button is pressed, pause the game and call the reset function.
            if (GUI.Button(button, "", buttonSkin.GetStyle("ResetButton")))
            {
                pauseScript.PausePlay();

                resetScript.GameReset();
            }
        }
    }
}
