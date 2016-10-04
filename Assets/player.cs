using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	GameObject pathGO;
	Transform targetPathNode;
	Transform unitTransform;
	int playerPathNodeIndex = 0;

	public float health = 10f;
	public float speed = 10f;

	public float MeleeRange = 5f;
	public GameObject meleePrefab;

	float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;

	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find ("Path");
		unitTransform = transform.Find ("player");

	}

	void GetNextPathNode(){
		targetPathNode = pathGO.transform.GetChild (playerPathNodeIndex);
		playerPathNodeIndex++;
	}
		

	// Update is called once per frame
	void Update () {

		// TODO: optimize
		//find player(enemies enemy)
		EnemyMelee_AI_Movement[] enemies = GameObject.FindObjectsOfType<EnemyMelee_AI_Movement> ();

		EnemyMelee_AI_Movement nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach (EnemyMelee_AI_Movement e in enemies) {
			float d = Vector3.Distance (this.transform.position, e.transform.position);
			//Debug.Log(d);

			if (nearestEnemy == null || d < dist) {
				nearestEnemy = e;
				dist = d;
			}
		}

		if (dist > 100) {

			if (targetPathNode == null) {
				GetNextPathNode ();
				if (targetPathNode == null) {
					//at player base
					ReachedEnemyBase ();
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
		else if (dist < 5)
		{

			if (nearestEnemy == null) {
				//no players?
				Debug.Log ("No enemies?");
			}

			transform.position = Vector3.MoveTowards (this.transform.position, nearestEnemy.transform.position, speed * Time.deltaTime);
//
//			Vector3 dir = nearestEnemy.transform.position - this.transform.position;
//
//			Quaternion lookRot = Quaternion.LookRotation (dir);
//
//			transform.LookAt (nearestEnemy.transform);
//
//			//unitTransform.rotation = Quaternion.Euler (0, lookRot.eulerAngles.y, 0);

			Vector3 Lookdir = nearestEnemy.transform.position - transform.position;

			Quaternion lookRot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Lookdir), 360 * Time.deltaTime);

			transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
//			if (dist < MeleeRange) {
//				meleeCoolDownLeft -= Time.deltaTime;
//				if (meleeCoolDownLeft <= 0) {
//					meleeCoolDownLeft = meleeCoolDown;
//					meleeHit (nearestEnemy);
//				}
//			}
		}
	}

	void meleeHit(Enemy_AI e){
		//fire from weapon
		GameObject meleeGo = (GameObject)Instantiate (meleePrefab, this.transform.position, this.transform.rotation);

		Melee s = meleeGo.GetComponent<Melee>();
		s.target = e.transform;
		Debug.Log ("Enemy hit");
	}

	void ReachedEnemyBase(){
		Destroy (gameObject);

	}

	public void TakeDamage(float damage){
		health -= damage;
		Debug.Log ( "player: DAMAGE TAKEN" );
		if (health <= 0) {
			Die ();
		}
	}

	public void Die() {
		//TODO: do this better, can fail.
//		GameObject.FindObjectOfType<GameManager> ().money += moneyValue;
		Destroy (gameObject);
		Debug.Log ("player died");
	}
		
}