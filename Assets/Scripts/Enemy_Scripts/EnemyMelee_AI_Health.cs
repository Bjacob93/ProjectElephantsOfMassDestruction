using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int alliedArmour = 10;
	public int currentHealth;
	public int moneyValue = 5;
    GameObject unitManager;
    UnitArrays Uarray;

    bool unitAdded = false;

    bool Died;

	// Use this for initialization 
	void Start () {
		currentHealth = startingHealth;

        unitManager = GameObject.Find("UnitManager");

        Uarray = unitManager.GetComponent<UnitArrays>();

        //Uarray.add(this.gameObject, "enemyUnit");

    }
	
	void Update () {

        if (!unitAdded)
        {
            Uarray.add(this.gameObject, "enemyUnit");
            unitAdded = true;
        }

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
		GameObject.FindObjectOfType<ScoreManager> ().money += moneyValue;

        Uarray.remove(this.gameObject, "enemyUnit");
		Destroy (this.gameObject);
	}
}