using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    mainMenuVariables varKeeper;

    void Start()
    {
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
    }

	public void LoadByIndex(int sceneIndex) {
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
