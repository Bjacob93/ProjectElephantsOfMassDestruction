using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {

	public float health = 10f;
	public float speed = 5f;
	public float damage = 5;
	public int moneyValue = 5;

	float MeleeRange = 10f;
	public GameObject meleePrefab;
	float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;


	GameObject pathGO;
	Transform targetPathNode;
	Transform unitTransform;
	int enemyPathNodeIndex = 12;

	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find ("Path");
	}

	void GetNextPathNode(){
		targetPathNode = pathGO.transform.GetChild (enemyPathNodeIndex);
		enemyPathNodeIndex--;
	}

	// Update is called once per frame
	void Update () {
	
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
		if (dist < 100) {
			transform.position = Vector3.MoveTowards (this.transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);

			if (nearestPlayer == null) {
				//no players?
				Debug.Log ("No enemies?");
			}

			Vector3 Lookdir = nearestPlayer.transform.position - transform.position;

			Quaternion lookRot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Lookdir), 360 * Time.deltaTime);

			transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

			if (dist < MeleeRange) {
				meleeCoolDownLeft -= Time.deltaTime;
				if (meleeCoolDownLeft <= 0 && Lookdir.magnitude < MeleeRange) {
					meleeCoolDownLeft = meleeCoolDown;
					meleeHit (nearestPlayer);
				}
			}
		} 
		else 
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
		GameObject.FindObjectOfType<GameManager> ().loseLives();
		Destroy (gameObject);

	}

	public void TakeDamage(float damage){
		this.health -= damage;
		Debug.Log ( "Enemy: DAMAGE TAKEN" );
		if (health <= 0) {
			Die ();
		}
	}

	public void Die() {
		//TODO: do this better, can fail.
		GameObject.FindObjectOfType<GameManager> ().money += moneyValue;
		Destroy (gameObject);
		Debug.Log ("Enemy died");
	}

	void meleeHit(player e){
		//fire from weapon
		GameObject meleeGo = (GameObject)Instantiate (meleePrefab, this.transform.position, this.transform.rotation);

		Melee s = meleeGo.GetComponent<Melee>();
		s.target = e.transform;
		Debug.Log ("Player hit");
	}
}
