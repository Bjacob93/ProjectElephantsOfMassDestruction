using UnityEngine;
using System.Collections;

public class AlledMelee_AI_Attack : MonoBehaviour {

	public float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;
	public int attackDamage = 20; // damage of each attack

	public GameObject nearestPlayer;
	public float MeleeRange = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		nearestPlayer = this.GetComponent<Astar> ().previousEnemy;

		float dist = Vector3.Distance (this.transform.position, nearestPlayer.transform.position);

		if (dist < MeleeRange && nearestPlayer != null) {
			meleeCoolDownLeft -= Time.deltaTime;
			if (meleeCoolDownLeft <= 0) {
				meleeCoolDownLeft = meleeCoolDown;

				nearestPlayer.GetComponent<EnemyMelee_AI_Health> ().currentHealth -= attackDamage;
				//nearestPlayer.GetComponent<EnemyMelee_AI_Health>().TakeDamage(attackDamage);
				Debug.Log (nearestPlayer.GetComponent<EnemyMelee_AI_Health> ().currentHealth);
			}
		}
	}
}