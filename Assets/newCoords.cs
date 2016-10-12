using UnityEngine;
using System.Collections;

public class newCoords : MonoBehaviour {

	public Vector3 checkpointA = new Vector3(29,0,-35);
	public Vector3 checkpointB = new Vector3(-36 , 0 , 45);

	public float controlRange = 10f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame 
	void Update () {
		GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

		if (Input.GetKey(KeyCode.J)){
		foreach (GameObject e in units) {
				//TODO: Check melee or ranged
				e.GetComponent<Astar>().targetPosition = checkpointA;
				e.GetComponent<Astar>().receivedNewDestination = true;
			}
		}
		if (Input.GetKey(KeyCode.L)){
			foreach (GameObject e in units) {
					//TODO: Check melee or ranged
					e.GetComponent<Astar>().targetPosition = checkpointB;
					e.GetComponent<Astar>().receivedNewDestination = true;
				}
			}
		}
}
