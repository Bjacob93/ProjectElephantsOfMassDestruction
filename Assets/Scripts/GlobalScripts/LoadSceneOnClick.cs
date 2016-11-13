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

	public void StageSelected(int Stage){
		selected = Stage;
	}
		
	public void LoadByIndex(int sceneIndex) {
		sceneIndex = selected;
			SceneManager.LoadScene(sceneIndex);
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
