using UnityEngine;
using System.Collections;

public class levelManager : MonoBehaviour {

    public int currentLevel;

	// Use this for initialization
	void Start () {
	}
	
	public int getCurrentLevel()
    {
        return currentLevel;
    }
}
