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
        buttonX = Screen.width / 3;
        buttonY = Screen.height / 100;
        buttonWidth = Screen.width / 21;
        buttonHeight = Screen.height / 9;
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
