using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Health : MonoBehaviour {

	public int startingHealth = 100;
	public int enemyArmour = 0;
	public float currentHealth;
	public int moneyValue = 5;
    GameObject unitManager;
    UnitArrays Uarray;
	public AudioSource Gameover;
    public GameObject enemyUnits;
    public GameObject enemySpawn;

    GameObject playerBase;
    bool unitAdded = false;

    ScoreManager scoreManager;

    bool Died;

	// Use this for initialization 
	void Start () {
		currentHealth = startingHealth;

        unitManager = GameObject.Find("UnitManager");

        Uarray = unitManager.GetComponent<UnitArrays>();

        playerBase = GameObject.FindGameObjectWithTag("PlayerBase");
        //Uarray.add(this.gameObject, "enemyUnit");

        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        enemySpawn = GameObject.FindGameObjectWithTag("EnemyBase");

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

		if (!Gameover.isPlaying) {
			scoreManager.timeupdate ();
		}

	}

	public void TakeDamage (float damageTaken)
	{
		if (Died == true) 
		{
			return;
		}
		//reduce the alliedHealth
		currentHealth -= (damageTaken - enemyArmour);

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public void Die()
	{
		//enemy is dead
		Died = true;
        scoreManager.money += moneyValue;

        Uarray.remove(this.gameObject, "enemyUnit");
		DestroyImmediate (this.gameObject);
	}

    void AtPlayerBase()
    {
        this.gameObject.transform.position = enemySpawn.transform.position;
        scoreManager.LoseLife(1);
		if (scoreManager.lives==0) {
			Gameover = gameObject.AddComponent<AudioSource> ();
			Gameover.clip = Resources.Load ("Audio/GameOver") as AudioClip;
			Gameover.playOnAwake = true;
			scoreManager.GameOver ();
			Gameover.Play ();
		}
    }
}