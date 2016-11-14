using UnityEngine;
using System.Collections;

public class AlledMelee_AI_Attack : MonoBehaviour {

	public Animator anim;

	public float meleeCoolDown = 1.11f; // attack cooldown
	float meleeCoolDownLeft = 0f;
	float attackDamage; // damage of each attack

	public GameObject nearestPlayer; //Cache gameobject
    public float MeleeRange = 3f;   // Melee range used to check if we can attack the enemy

    float randV; // float for a random value
    float hitChance;    //float for the units hitChance
    float eachMissIncreaseChance;

    Astar aStar;

    void Start ()
    {
		anim = GetComponent<Animator>();

        aStar = this.GetComponent<Astar>();

        eachMissIncreaseChance = 0;
    }

    void Update() {
        //attackDamage = Random.Range(10f, 20f);
        //hitChance = 0.9f; // hit chance increase this to increase the hit chance eg 0.95 would be 95% hit chance instead of 90%
        //randV = Random.value;   // calculate a random value used to determine if we hit the target

        //nearestPlayer = aStar.previousEnemy; //Get the gameobject this script works with form the aStarEnemy script, this is the nearest enemy which is also the gameObject which is attacked

        //if (nearestPlayer != null)
        //{
        //    //Calculate distance to enemy
        //    float dist = Vector3.Distance(this.transform.position, nearestPlayer.transform.position);

        //    //Check if the enemy is in range and not null
        //    if (dist < MeleeRange && nearestPlayer != null)
        //    {
        //        //Update the hit cooldown
        //        meleeCoolDownLeft -= Time.deltaTime;
        //        //Check if we need to run the attack script since we reached the targeted cooldown
        //        if (meleeCoolDownLeft <= 0)
        //        {
        //            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
        //            {
        //                anim.Play("ATTACK", -1, 0f);
        //            }
        //            meleeCoolDownLeft = meleeCoolDown;

        //            //find check if the attack connects/hits, atm there are 90% for hit
        //            if (randV < hitChance + eachMissIncreaseChance)
        //            {
        //                //Run the TakenDamage from AlliedMelee_AI_Health script to reduce the nearesPlayer health.
        //                nearestPlayer.GetComponent<EnemyMelee_AI_Health>().TakeDamage(attackDamage);
        //                eachMissIncreaseChance = 0;
        //            }
        //            else
        //            {
        //                eachMissIncreaseChance += 5;
        //            }
        //        }
        //    }
        //}
    }

    void alliesAttack(GameObject nearestPlayer) {
        attackDamage = Random.Range(15f, 20f);
        hitChance = 0.9f; // hit chance increase this to increase the hit chance eg 0.95 would be 95% hit chance instead of 90%
        randV = Random.value;   // calculate a random value used to determine if we hit the target

        if (nearestPlayer != null){
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
                
                    //find check if the attack connects/hits, atm there are 90% for hit
                    if(randV < hitChance + eachMissIncreaseChance)
                    {
                        //Run the TakenDamage from AlliedMelee_AI_Health script to reduce the nearesPlayer health.
                        nearestPlayer.GetComponent<EnemyMelee_AI_Health>().TakeDamage(attackDamage);
                        eachMissIncreaseChance = 0;
                    }
                    else
                    {
                        eachMissIncreaseChance += 5;
                    }
                }
        }
	}
}