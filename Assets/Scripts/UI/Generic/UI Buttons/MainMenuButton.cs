using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    //Cache variables for button coordinates and dimensions.
    private Rect    mainMenuButton;
    private float   buttonX,
                    buttonY,
                    buttonWidth,
                    buttonHeight;

    //Cache skin.
    GUISkin buttonSkin;


	void Start () {

        //Calculate dimensions.
        buttonWidth = Screen.width / 17;
        buttonHeight = Screen.width / 15;
        buttonX = Screen.width /2 - Screen.width / 15 - buttonWidth;
        buttonY = Screen.height / 100;

        mainMenuButton = new Rect(buttonX, buttonY, buttonWidth, buttonHeight);

        //Reference skin.
        buttonSkin = Resources.Load ("Graphix/interfaceButtons") as GUISkin;
	}

    void OnGUI()
    {
        //Set the skin.
        GUI.skin = buttonSkin;

        //Go to the main menu if button is pressed.
        if (GUI.Button(mainMenuButton, "", buttonSkin.GetStyle("MainMenuButton")))
        {
            SceneManager.LoadScene("mainMenu");
        }
    }
}
