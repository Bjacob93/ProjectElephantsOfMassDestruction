using UnityEngine;
using System.Collections;

public class gameReset : MonoBehaviour {

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

    PauseScript pauseScript;

    // Use this for initialization
    void Start () {
        homeBaseGUI = GameObject.Find("GiraffeBase").GetComponent<HomebaseGUI>();

        checkpointGO = GameObject.Find("A");
        basicCheckpointScript = checkpointGO.GetComponent<BasicCheckpointScript>();

        scoreManagerGO = GameObject.Find("ScoreManager");
		scoreManagerScript = scoreManagerGO.GetComponent<ScoreManager>();

		buyEnemyUnitsGO = GameObject.Find("ElephantSpawn");
		buyEnemyUnitsScript = buyEnemyUnitsGO.GetComponent<BuyEnemyUnits>();

		unitManager = GameObject.Find("UnitManager");
		Uarray = unitManager.GetComponent<UnitArrays>();

        pauseScript = GameObject.Find("UIButtons").GetComponent<PauseScript>();
    }

	public void GameReset(){
        scoreManagerScript.ResetLives();

        buyEnemyUnitsScript.ResetSpawnTimer();
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

        homeBaseGUI.ResetSpawnTimer();

        homeBaseGUI.shrimp = 1;
        homeBaseGUI.unitCount = 0;
        basicCheckpointScript.shrimp = 1;

		Uarray.resetGame ();
        pauseScript.PausePlay();

		return;

	}
}
