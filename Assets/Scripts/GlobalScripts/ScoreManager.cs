using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {
	Animator anim;  

	public float restartDelay = 5f;         // Time to wait before restarting the level
	float restartTimer;                     // Timer to count up to restarting the level

	public int lives = 20;
	public int money = 100;

	public Text moneyText;
	public Text livesText;

    public GameObject[] capturePoint; //cache all capturePoints
    public List<BasicCapturePoint> basicCapturePointScripts = new List<BasicCapturePoint>(); // cache and prepare a lise for the scripts from capturePoint
    public bool playerHasAllCheckPoints = false; // bool used to check if the player got all capture points

    // Use this for initialization
    public void LoseLife (int l = 1) {
		//lives -= l;
		if (lives <= 0) {
			GameOver ();
			timeupdate ();
		}
	}

    public int getMoney()
    {
        return money;
    }

	IEnumerator waitandprint(float waitTime){
		yield return new WaitForSeconds (waitTime);

	}

	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}
	void timeupdate(){
		restartTimer += Time.deltaTime;
		if (lives == 0) {
			if (restartTimer >= restartDelay) {
				// .. then reload the currently loaded level.
				SceneManager.LoadSceneAsync ("Scenes/mainMenu", LoadSceneMode.Single);
			}
		}
	}

    //function to check if a player owns the different capturePoints
    void checkCapturePointsForOwnership()
    {
        for (int i = 0; i < basicCapturePointScripts.Count; i++)
        {
            if(basicCapturePointScripts[i].playerHasCapturePoint != true)
            {
                playerHasAllCheckPoints = false;
                break; // if the player does not have all capturePoints stop the loop
            }
            if(i == basicCapturePointScripts.Count - 1)
            {
                playerHasAllCheckPoints = true;
                //if the player got all capturePoints set the bool to true.

            }
        }
    }

	// Update is called once per frame
	void Update () {
        checkCapturePointsForOwnership();

        moneyText.text = "Money: $" + money.ToString ();
		livesText.text = "Lives " + lives.ToString ();
		timeupdate ();
	}
	void Start(){
        capturePoint = GameObject.FindGameObjectsWithTag("CapturePoint");
        foreach(GameObject c in capturePoint)
        {
            basicCapturePointScripts.Add(c.GetComponent<BasicCapturePoint>());
        }
    }

	public void LoadByIndex(int sceneIndex) {
		SceneManager.LoadScene(sceneIndex);
	}
			
	public void GameOver(){
		anim.SetTrigger ("GameOver");
	}

    public void Victory()
    {
        anim.SetTrigger("VictoryAnimation");
    }
}
