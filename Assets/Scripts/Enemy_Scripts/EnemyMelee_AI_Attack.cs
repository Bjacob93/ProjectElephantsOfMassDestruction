using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Attack : MonoBehaviour {

	public float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;
	int attackDamage = 20; // damage of each attack

	public GameObject nearestPlayer;
	public float MeleeRange = 3f;

    AstarEnemy aStarEnemy;
    AlliedMelee_AI_Health AiHealth;

    void Start()
    {
        aStarEnemy = this.GetComponent<AstarEnemy>();
    }
	
	void Update () {

		nearestPlayer = aStarEnemy.nearestEnemy;

        if (nearestPlayer != null) {
            float dist = Vector3.Distance(this.transform.position, nearestPlayer.transform.position);

            if (dist < MeleeRange && nearestPlayer != null)
            {
                meleeCoolDownLeft -= Time.deltaTime;
                if (meleeCoolDownLeft <= 0)
                {
                    meleeCoolDownLeft = meleeCoolDown;

                    nearestPlayer.GetComponent<AlliedMelee_AI_Health>().TakeDamage(attackDamage);
                }
            }
        }
	}
}