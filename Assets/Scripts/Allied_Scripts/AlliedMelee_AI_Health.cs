using UnityEngine;
using System.Collections;

public class AlliedMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int alliedArmour = 0;
	public float currentHealth;
	public int moneyValue = 5;
	public int cost = 10;
    GameObject unitManager;
    UnitArrays Uarray;

    bool unitAdded = false;

	bool Died;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;

        unitManager = GameObject.Find("UnitManager");
        Uarray = unitManager.GetComponent<UnitArrays>();
        //Uarray.add(this.gameObject, "playerUnit");

    }
	
	// Update is called once per frame
	void Update () {

        if (!unitAdded)
        {
            Uarray.add(this.gameObject, "playerUnit");
            unitAdded = true;
        }
	
		if (currentHealth <= 0) {
			Die ();
		}

	}

	public void TakeDamage (float damageTaken)
	{
		if (Died) 
		{
			return;
		}
        //reduce the alliedHealth
        float tempDamage = (damageTaken - alliedArmour);

        currentHealth -= tempDamage > 0 ? tempDamage : 0;

  //      if (tempDamage < 0) tempDamage = 0; 
		//currentHealth -= (tempDamage);

		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die()
	{
		//Enemy is dead.
		Died = true;
        Uarray.remove(this.gameObject, "playerUnit");
        DestroyImmediate(this.gameObject);
	}
}