using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour {

	static int selected = 0;

    mainMenuVariables varKeeper;
	Button Mainmenu;
	public Sprite MainMenuSprite;


    void Start()
    {
		varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
		Mainmenu = GetComponent<Button>();
		Mainmenu.image.sprite = MainMenuSprite;
    }

	public void LoadByIndex(int sceneIndex) {
		SceneManager.LoadScene(sceneIndex);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void dnd()
    {
        varKeeper.useDragonDrop = true;
    }
    public void text()
    {
        varKeeper.useDragonDrop = false;
    }
}
