using UnityEngine;
using System.Collections;

public class AlliedMelee_AI_Movement : MonoBehaviour {

	public float speed = 10f;
	float MeleeRange = 10f;

	// Use this for initialization
	GameObject pathGO;
	Transform targetPathNode;
	Transform unitTransform;
	int enemyPathNodeIndex = 0;
	bool isInMeleeRange;
	public GameObject nearestPlayer;

	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find ("Path");
		isInMeleeRange = false;
	}

	void GetNextPathNode(){
		targetPathNode = pathGO.transform.GetChild (enemyPathNodeIndex);
		enemyPathNodeIndex++;
	}


	// Update is called once per frame
	void Update () {

		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag ("enemyUnits");
		GameObject nearestPlayer = null;
		float dist = Mathf.Infinity;

		foreach (GameObject e in playerUnits) {
			float d = Vector3.Distance (this.transform.position, e.transform.position);

			if (nearestPlayer == null || d < dist) {
				nearestPlayer = e;
				dist = d;
			}
		}
		if (dist < 100 && dist > 5) {
			transform.position = Vector3.MoveTowards (this.transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);

			if (nearestPlayer == null) {
				//no players?
				Debug.Log ("No enemies?");
			}

			Vector3 Lookdir = nearestPlayer.transform.position - transform.position;

			Quaternion lookRot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Lookdir), 360 * Time.deltaTime);

			transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

			if (dist < MeleeRange) {
				isInMeleeRange = true;
			}
		}else if (isInMeleeRange == false)
		{
			if (targetPathNode == null) {
				GetNextPathNode ();
				if (targetPathNode == null) {
					//at player base
					ReachedPlayerBase ();
				}
			}
			Vector3 dir = targetPathNode.position - this.transform.localPosition;

			float distThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distThisFrame) {
				// we reached the node
				targetPathNode = null;
			} else {
				//TODO: add the A* pathfinding instead
				//move towards node
				transform.Translate (dir.normalized * distThisFrame, Space.World);
				Quaternion targetRotation = Quaternion.LookRotation (dir);
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * 5);
			}

		}
	} 

	void ReachedPlayerBase(){
		//GameObject.FindObjectOfType<GameManager> ().loseLives();
		Destroy (gameObject);

	}
}