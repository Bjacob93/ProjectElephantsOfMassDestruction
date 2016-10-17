using UnityEngine;
using System.Collections;

public class gameReset : MonoBehaviour {

	GameObject scoreManagerGO;
	ScoreManager scoreManagerScript;

	GameObject buyEnemyUnitsGO;
	BuyEnemyUnits buyEnemyUnitsScript;

	GameObject unitManager;
	UnitArrays Uarray;

	GameObject[] allies;
	GameObject[] enemies;

	// Use this for initialization
	void Start () {
		scoreManagerGO = GameObject.Find("ScoreManager");
		scoreManagerScript = scoreManagerGO.GetComponent<ScoreManager>();

		buyEnemyUnitsGO = GameObject.Find("ElephantSpawn");
		buyEnemyUnitsScript = buyEnemyUnitsGO.GetComponent<BuyEnemyUnits>();

		unitManager = GameObject.Find("UnitManager");
		Uarray = unitManager.GetComponent<UnitArrays>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GameReset(){
		scoreManagerScript.lives = 20;
		scoreManagerScript.money = 100;

		buyEnemyUnitsScript.restartTimer = 0;

		Uarray.resetGame ();

		return;

	}
}
