using UnityEngine;
using System.Collections;

public class AlliedMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int alliedArmour = 0;
	public int currentHealth;
	public int moneyValue = 5;

	bool Died;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage (int damageTaken)
	{
		if (Died = true) 
		{
			return;
		}

		//reduce the alliedHealth
		currentHealth -= damageTaken;

		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die()
	{
		//enemy is dead
		Died = true;
		Destroy (gameObject);
	}
}
