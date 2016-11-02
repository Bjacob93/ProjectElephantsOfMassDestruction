using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Attack : MonoBehaviour {

    public Animator anim;

	public float meleeCoolDown = 0.5f; //Attack cooldown
	float meleeCoolDownLeft = 0f;
	int attackDamage = 20; // Damage of each attack

	public GameObject nearestPlayer; //Cache gameobject
	public float MeleeRange = 3f;  // Melee range used to check if we can attack the enemy

    AstarEnemy aStarEnemy;
    AlliedMelee_AI_Health AiHealth;

    float randV;        // float for a random value
    float hitChance;   //float for the units hitChance

    void Start()
    {
        anim = GetComponent<Animator>();

        //Find and store te AstarEnemy script
        aStarEnemy = this.GetComponent<AstarEnemy>();
    }
	
	void Update () {

        hitChance = 0.9f; // hit chance increase this to increase the hit chance eg 0.95 would be 95% hit chance instead of 90%
        randV = Random.value; // calculate a random value used to determine if we hit the target

        nearestPlayer = aStarEnemy.nearestEnemy; //Get the gameobject this script works with form the aStarEnemy script, this is the nearest enemy which is also the gameObject which is attacked

        if (nearestPlayer != null) {
            //Calculate distance to enemy
            float dist = Vector3.Distance(this.transform.position, nearestPlayer.transform.position);

            //Check if the enemy is in range and not null
            if (dist < MeleeRange && nearestPlayer != null)
            {
                //Update the hit cooldown
                meleeCoolDownLeft -= Time.deltaTime;
                //Check if we need to run the attack script since we reached the targeted cooldown
                if (meleeCoolDownLeft <= 0)
                {

                    if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
                    {
                        anim.Play("ATTACK", -1, 0f);
                    }  
                    meleeCoolDownLeft = meleeCoolDown;
                    //Create a random value between 0 and 1 if it is lower than 0.9 it hits, making the hit chance 90%
                    if (randV < hitChance)
                    {
                        //Run the TakenDamage from AlliedMelee_AI_Health script to reduce the nearesPlayer health.
                        nearestPlayer.GetComponent<AlliedMelee_AI_Health>().TakeDamage(attackDamage);
                    }
                    
                }
            }
        }
	}
}