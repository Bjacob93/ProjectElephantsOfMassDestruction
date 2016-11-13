using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	static int selected = 0;

    mainMenuVariables varKeeper;

    void Start()
    {
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
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
