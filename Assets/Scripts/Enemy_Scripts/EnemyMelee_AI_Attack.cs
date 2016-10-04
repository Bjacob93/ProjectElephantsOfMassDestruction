using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Attack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public float attackDamage = 20;

	GameObject alliedUnit;
	AlliedMelee_AI_Health AlliedHealth;

	float timer;

	// Use this for initialization
	void Start () {
		alliedUnit = GetComponent<EnemyMelee_AI_Movement> ().nearestPlayer;
		AlliedHealth = GetComponent<AlliedMelee_AI_Health> ().currentHealth;
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && AlliedHealth > 0) {
			Attack ();
		}
	
	}

	void Attack()
	{
		timer = 0f;

		if (AlliedHealth.currentHealth > 0)
		{
			AlliedHealth.TakeDamage (attackDamage);
		}
	}
}
