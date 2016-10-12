using UnityEngine;
using System.Collections;

public class BasicCheckpointScript : MonoBehaviour {

    public Vector3 meleeDistination;
    public Vector3 rangedDistination;

    public float controlRange = 10f;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame 
	void Update () {
        GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

        foreach (GameObject e in units) {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange) {
                //TODO: Check melee or ranged
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
            }
        }
	}
}
