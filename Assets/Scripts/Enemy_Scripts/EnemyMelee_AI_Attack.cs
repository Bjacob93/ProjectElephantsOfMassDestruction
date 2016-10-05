using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Attack : MonoBehaviour {

//	public float timeBetweenAttacks = 0.5f; // time in sec. before each attack

	public float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;
	public int attackDamage = 20; // damage of each attack

//	GameObject alliedUnit; // reference the targeted game object
//	AlliedMelee_AI_Health AlliedHealth; // reference the health of target
//
//	float timer; //time for counting to next attack

	//do this much better
	public GameObject nearestPlayer;
	public float MeleeRange = 10f;
	// Use this for initialization
	void Start () {
//		alliedUnit = GameObject.Find()
	}
	
	// Update is called once per frame
	void Update () {

//		timer += Time.deltaTime;
//		if (timer >= timeBetweenAttacks && AlliedHealth > 0) {
//			Attack ();
//		}
		nearestPlayer = this.GetComponent<EnemyMelee_AI_Movement>().nearestPlayer;
//		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag ("playerUnits");
//		nearestPlayer = null;
//		float dist = Mathf.Infinity;

		float dist = Vector3.Distance (this.transform.position, nearestPlayer.transform.position);
//		foreach (GameObject e in playerUnits) {
//			float d = Vector3.Distance (this.transform.position, e.transform.position);
//
//			if (nearestPlayer == null || d < dist) {
//				nearestPlayer = e;
//				dist = d;
//			}
//		}

		if (dist < MeleeRange && nearestPlayer != null) {
			meleeCoolDownLeft -= Time.deltaTime;
				if (meleeCoolDownLeft <= 0) {
					meleeCoolDownLeft = meleeCoolDown;

				nearestPlayer.GetComponent<AlliedMelee_AI_Health> ().currentHealth -= attackDamage;
				//nearestPlayer.GetComponent<AlliedMelee_AI_Health>().TakeDamage(attackDamage);
				//Debug.Log (nearestPlayer.GetComponent<AlliedMelee_AI_Health> ().currentHealth);
		}
	}
	}
}

//	void Attack()
//	{
//		timer = 0f;
//
//		if (AlliedHealth.currentHealth > 0)
//		{
//			AlliedHealth.TakeDamage (attackDamage);
//		}
//	}