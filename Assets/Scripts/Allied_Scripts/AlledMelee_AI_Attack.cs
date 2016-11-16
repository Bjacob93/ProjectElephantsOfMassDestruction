using UnityEngine;
using System.Collections;

public class AlledMelee_AI_Attack : MonoBehaviour
{

    public Animator anim;
	public AudioSource Sword;

    public float meleeCoolDown = 2.11f; // attack cooldown
    float meleeCoolDownLeft = 0f;
    float attackDamage; // damage of each attack

    public GameObject nearestPlayer; //Cache gameobject
    public float MeleeRange = 3f;   // Melee range used to check if we can attack the enemy

    float randV; // float for a random value
    float hitChance;    //float for the units hitChance
    float eachMissIncreaseChance;

    Astar aStar;

    void Start()
    {
        anim = GetComponent<Animator>();
		Sword = GetComponent<AudioSource>();

        aStar = this.GetComponent<Astar>();

        eachMissIncreaseChance = 0;
    }

    public void alliesAttack(GameObject nearestPlayer)
    {
        attackDamage = Random.Range(15f, 20f);
        hitChance = 0.9f; // hit chance increase this to increase the hit chance eg 0.95 would be 95% hit chance instead of 90%
        randV = Random.value;   // calculate a random value used to determine if we hit the target

        if (nearestPlayer != null)
        {
            //Update the hit cooldown
            meleeCoolDownLeft -= Time.deltaTime;
            //Check if we need to run the attack script since we reached the targeted cooldown
            if (meleeCoolDownLeft <= 1f)
            {
                if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
                {
                    anim.Play("ATTACK", -1, 0f);
                }
                if (meleeCoolDownLeft <= 0)
                {

                    //find check if the attack connects/hits, atm there are 90% for hit
                    if (randV < hitChance + eachMissIncreaseChance)
                    {
                        //Run the TakenDamage from AlliedMelee_AI_Health script to reduce the nearesPlayer health.
                        nearestPlayer.GetComponent<EnemyMelee_AI_Health>().TakeDamage(attackDamage);
                        eachMissIncreaseChance = 0;
                        meleeCoolDownLeft = meleeCoolDown;
                        if(Sword.isPlaying)
                        Sword.Play();
                    }
                    else
                    {
                        eachMissIncreaseChance += 5;
                        meleeCoolDownLeft = meleeCoolDown;
                        if(Sword.isPlaying)
                        Sword.Play();
                    }
                }
            }
        }
    }
}