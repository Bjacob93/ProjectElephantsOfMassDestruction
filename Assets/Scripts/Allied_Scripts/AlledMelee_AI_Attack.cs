using UnityEngine;
using System.Collections;

public class AlledMelee_AI_Attack : MonoBehaviour {

	public float meleeCoolDown = 0.5f; // attack cooldown
	float meleeCoolDownLeft = 0f;
	int attackDamage = 20; // damage of each attack

	public GameObject nearestPlayer; //Cache gameobject
    public float MeleeRange = 3f;   // Melee range used to check if we can attack the enemy

    float randV; // float for a random value
    float hitChance;    //float for the units hitChance

    Astar aStar;

    void Start ()
    {
        aStar = this.GetComponent<Astar>();
    }
	
	void Update () {

        hitChance = 0.9f; // hit chance increase this to increase the hit chance eg 0.95 would be 95% hit chance instead of 90%
        randV = Random.value;   // calculate a random value used to determine if we hit the target

        nearestPlayer = aStar.previousEnemy; //Get the gameobject this script works with form the aStarEnemy script, this is the nearest enemy which is also the gameObject which is attacked

        if (nearestPlayer != null){
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
                    meleeCoolDownLeft = meleeCoolDown;

                    //find check if the attack connects/hits, atm there are 90% for hit
                    if(randV < hitChance)
                    {
                        //Run the TakenDamage from AlliedMelee_AI_Health script to reduce the nearesPlayer health.
                        nearestPlayer.GetComponent<EnemyMelee_AI_Health>().TakeDamage(attackDamage);
                    } 
                }
            }
        }
	}
}