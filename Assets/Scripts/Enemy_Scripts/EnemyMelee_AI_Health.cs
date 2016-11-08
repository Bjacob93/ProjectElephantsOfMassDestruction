﻿using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int alliedArmour = 10;
	public float currentHealth;
	public int moneyValue = 5;
    GameObject unitManager;
    UnitArrays Uarray;

    GameObject playerBase;
    bool unitAdded = false;

    bool Died;

	// Use this for initialization 
	void Start () {
		currentHealth = startingHealth;

        unitManager = GameObject.Find("UnitManager");

        Uarray = unitManager.GetComponent<UnitArrays>();

        playerBase = GameObject.FindGameObjectWithTag("PlayerBase");
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

        float distance = Vector3.Distance(this.transform.position, playerBase.transform.position);
        if (distance < 10)
        {
            AtPlayerBase();
        }

	}

	public void TakeDamage (float damageTaken)
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

	public void Die()
	{
		//enemy is dead
		Died = true;
		GameObject.FindObjectOfType<ScoreManager> ().money += moneyValue;

        Uarray.remove(this.gameObject, "enemyUnit");
		Destroy (this.gameObject);
	}

    void AtPlayerBase()
    {
        Die();
        GameObject.FindObjectOfType<ScoreManager>().LoseLife(1);
    }
}