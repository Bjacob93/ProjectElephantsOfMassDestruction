using UnityEngine;
using System.Collections;

public class gameReset : MonoBehaviour {

    GameObject homeBaseGO;
    HomebaseGUI homeBaseGUI;

    GameObject checkpointGO;
    BasicCheckpointScript basicCheckpointScript;

    GameObject scoreManagerGO;
	ScoreManager scoreManagerScript;

    GameObject CapturePointGO;
    BasicCapturePoint basicCapturePoint;

	GameObject buyEnemyUnitsGO;
	BuyEnemyUnits buyEnemyUnitsScript;

	GameObject unitManager;
	UnitArrays Uarray;

	GameObject[] allies;
	GameObject[] enemies;

    // Use this for initialization
    void Start () {
        homeBaseGO = GameObject.Find("GiraffeBase");
        homeBaseGUI = homeBaseGO.GetComponent<HomebaseGUI>();

        checkpointGO = GameObject.Find("A");
        basicCheckpointScript = checkpointGO.GetComponent<BasicCheckpointScript>();

        scoreManagerGO = GameObject.Find("ScoreManager");
		scoreManagerScript = scoreManagerGO.GetComponent<ScoreManager>();

		buyEnemyUnitsGO = GameObject.Find("ElephantSpawn");
		buyEnemyUnitsScript = buyEnemyUnitsGO.GetComponent<BuyEnemyUnits>();

		unitManager = GameObject.Find("UnitManager");
		Uarray = unitManager.GetComponent<UnitArrays>();
    }

	public void GameReset(){
		scoreManagerScript.lives = scoreManagerScript.livesR;
		scoreManagerScript.money = 100;

		buyEnemyUnitsScript.restartTimer = 0;
        buyEnemyUnitsScript.amountOfEnemySpawned = 0;

        for(int i = 0; i < scoreManagerScript.basicCapturePointScripts.Count; i++)
        {
            scoreManagerScript.basicCapturePointScripts[i].capturePoints = scoreManagerScript.basicCapturePointScripts[i].avCaptureTimer;
            scoreManagerScript.basicCapturePointScripts[i].enemyHasCapturePoint = false;
            scoreManagerScript.basicCapturePointScripts[i].playerHasCapturePoint = false;
            scoreManagerScript.basicCapturePointScripts[i].neutralCapturePoint = true;
            scoreManagerScript.basicCapturePointScripts[i].elephantCapture.fillAmount = 0;
            scoreManagerScript.basicCapturePointScripts[i].giraffeCapture.fillAmount = 0;
        }

        homeBaseGUI.shrimp = 1;
        basicCheckpointScript.shrimp = 1;

		Uarray.resetGame ();

		return;

	}
}
