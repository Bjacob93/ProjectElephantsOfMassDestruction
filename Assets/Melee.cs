using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {

	public float speed = 30;
	public Transform target;
	public float playerMeleeDamage = 2;
	public float enemyMeleeDamage = 2;

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

			transform.Translate (dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation (dir);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * 5);
		}
	}

	public void DoMeleeHit(){
		Debug.Log ("do melee hit");
		//gameObject.GetComponentInChildren<player>().TakeDamage (enemyMeleeDamage);
		gameObject.GetComponent<player>().TakeDamage(enemyMeleeDamage);
		Destroy (gameObject);
	}
}
