using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int alliedArmour = 10;
	public int currentHealth;
	public int moneyValue = 5;

	bool Died;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
	}
	
	void Update () {

		if (currentHealth <= 0) {
			Die ();
		}

	}

	public void TakeDamage (int damageTaken)
	{
		if (Died == true) 
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
		Debug.Log ("Died");
		GameObject.FindObjectOfType<ScoreManager> ().money += moneyValue;
		Destroy (gameObject);
	}
}