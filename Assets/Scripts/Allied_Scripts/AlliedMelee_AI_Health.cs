using UnityEngine;
using System.Collections;

public class AlliedMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int alliedArmour = 10;
	public int currentHealth;
	public int moneyValue = 5;
	public int cost = 10;
    GameObject unitManager;
    UnitArrays Uarray;

	bool Died;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;

        unitManager = GameObject.Find("UnitManager");
        Uarray = unitManager.GetComponent<UnitArrays>();
        Uarray.add(this.gameObject, "playerUnit");

    }
	
	// Update is called once per frame
	void Update () {
	
		if (currentHealth <= 0) {
			Die ();
		}

	}

	public void TakeDamage (int damageTaken)
	{
		if (Died) 
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
        Uarray.remove(this.gameObject, "playerUnit");
        Destroy (this.gameObject);
	}
}