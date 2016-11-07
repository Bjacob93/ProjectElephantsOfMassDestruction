using UnityEngine;
using System.Collections;

public class uiManager : MonoBehaviour {

    mainMenuVariables varKeeper;

    GameObject uiStyle;


	// Use this for initialization
	void Start () {
        varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();

        if (varKeeper.useDragonDrop)
        {
            uiStyle = GameObject.Find("TextEditing");
            uiStyle.SetActive(false);
        }else
        {
            uiStyle = GameObject.Find("DragonDrop");
            uiStyle.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
