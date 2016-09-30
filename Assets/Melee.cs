using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {

	public float speed = 30;
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = target.position - this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame) {
			// we reached the node
			DoMeleeHit();
		} else {
			//TODO: add the A* pathfinding instead

			transform.Translate (dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation (dir);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * 5);
		}
	}

	void DoMeleeHit(){
		Destroy (gameObject);
	}
}
