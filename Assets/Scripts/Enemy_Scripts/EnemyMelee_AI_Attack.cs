using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Attack : MonoBehaviour {

	public float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;
	int attackDamage = 20; // damage of each attack

	public GameObject nearestPlayer;
	public float MeleeRange = 3f;
	
	void Update () {

		nearestPlayer = this.GetComponent<EnemyMelee_AI_Movement>().nearestPlayer;

        if (nearestPlayer != null) {
            float dist = Vector3.Distance(this.transform.position, nearestPlayer.transform.position);

            if (dist < MeleeRange && nearestPlayer != null)
            {
                meleeCoolDownLeft -= Time.deltaTime;
                if (meleeCoolDownLeft <= 0)
                {
                    meleeCoolDownLeft = meleeCoolDown;

					//nearestPlayer.GetComponent<AlliedMelee_AI_Health> ().currentHealth -= attackDamage;
                    nearestPlayer.GetComponent<AlliedMelee_AI_Health>().TakeDamage(attackDamage);
                    Debug.Log("Dealt" + nearestPlayer.GetComponent<AlliedMelee_AI_Health>().currentHealth + "damage to ally");
                }
            }
        }
	}
}