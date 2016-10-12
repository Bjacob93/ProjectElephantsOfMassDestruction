using UnityEngine;
using System.Collections;

public class AlledMelee_AI_Attack : MonoBehaviour {

	public float meleeCoolDown = 0.5f;
	float meleeCoolDownLeft = 0f;
	int attackDamage = 20; // damage of each attack

	public GameObject nearestPlayer;
	public float MeleeRange = 3f;

    Astar aStar;

    void Start ()
    {
        aStar = this.GetComponent<Astar>();
    }
	
	void Update () {
	
		nearestPlayer = aStar.previousEnemy;

        if (nearestPlayer != null){
            float dist = Vector3.Distance(this.transform.position, nearestPlayer.transform.position);

            if (dist < MeleeRange && nearestPlayer != null)
            {
                meleeCoolDownLeft -= Time.deltaTime;
                if (meleeCoolDownLeft <= 0)
                {
                    meleeCoolDownLeft = meleeCoolDown;

                    nearestPlayer.GetComponent<EnemyMelee_AI_Health>().TakeDamage(attackDamage);
                }
            }
        }
	}
}