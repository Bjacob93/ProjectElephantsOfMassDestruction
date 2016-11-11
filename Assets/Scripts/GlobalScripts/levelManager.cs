using UnityEngine;
using System.Collections;

public class levelManager : MonoBehaviour {

    int currentLevel;

	// Use this for initialization
	void Start () {
        currentLevel = 1;
	}
	
	int getCurrentLevel()
    {
        return currentLevel;
    }
}
