using UnityEngine;
using System.Collections;

public class mainMenuVariables : MonoBehaviour {

    public bool useDragonDrop;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	public void setTrue(){
		useDragonDrop = true; 
	}

	public void setFalse(){
		useDragonDrop = false;
		}
}
