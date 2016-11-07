using UnityEngine;
using System.Collections;

public class mainMenuVariables : MonoBehaviour {

    public bool useDragonDrop;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
