using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject pathGO;
	Transform targetPathNode;
	Transform unitTransform;
	int enemyPathNodeIndex = 12;

	public float health = 10f;
	public float speed = 5f;

	public int moneyValue = 5;
	private GameObject unit;
	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find ("Path");
		unitTransform = transform.Find ("Enemy");
	}

	void GetNextPathNode(){
		targetPathNode = pathGO.transform.GetChild (enemyPathNodeIndex);
		enemyPathNodeIndex--;
	}
			
	// Update is called once per frame
	void Update () {
		//		// TODO: optimize
		//find player(enemies enemy)
		player[] players = GameObject.FindObjectsOfType<player>();

		player nearestPlayer = null;
		float dist = Mathf.Infinity;

		foreach (player e in players) {
			float d = Vector3.Distance (this.transform.position, e.transform.position);

			if (nearestPlayer == null || d < dist) {
				nearestPlayer = e;
				dist = d;
			}
		}
		if (dist > 100) {

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
		} else {

			transform.position = Vector3.MoveTowards (this.transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);

			if (nearestPlayer == null) {
				//no players?
				Debug.Log ("No enemies?");
			}

			Vector3 Lookdir = nearestPlayer.transform.position - this.transform.position;

			Quaternion lookRot = Quaternion.LookRotation (Lookdir);
			Debug.Log (lookRot);
			unitTransform.rotation = Quaternion.Euler (0, lookRot.eulerAngles.y, 0);
		}
}
		

	void ReachedPlayerBase(){
		GameObject.FindObjectOfType<GameManager> ().loseLives();
		Destroy (gameObject);
	
	}

	public void TakeDamage(float damage){
		health -= damage;
		if (health <= 0) {
			Die ();
		}
	}

	public void Die() {
		//TODO: do this better, can fail.
		GameObject.FindObjectOfType<GameManager> ().money += moneyValue;
		Destroy (gameObject);
	}
}
