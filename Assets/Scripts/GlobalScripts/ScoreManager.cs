using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

    //Cache the animator;
	Animator anim;  

    //Cache the audio sources
	public AudioSource levelMusic;
    public AudioSource victoryMusic;

	levelManager lvlManager;
    //Cache the index of the current scene.
    public int currentSceneIndex; 

	public float restartDelay = 1f;         // Time to wait before restarting the level
	float restartTimer;                     // Timer to count up to restarting the level

	private int livesRemaining;
    public int livesStart;

	public Text livesText;

    public GameObject[] capturePoint; //cache all capturePoints
    public List<BasicCapturePoint> basicCapturePointScripts = new List<BasicCapturePoint>(); // cache and prepare a lise for the scripts from capturePoint
    private bool playerHasAllCheckPoints = false; // bool used to check if the player got all capture points


    void Start()
    {
		
        //Reference the animator.
        anim = GetComponent<Animator>();

        //Define the index of the current scene.
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        victoryMusic = gameObject.AddComponent<AudioSource>();
		levelMusic = gameObject.AddComponent<AudioSource>();

        capturePoint = GameObject.FindGameObjectsWithTag("CapturePoint");

        livesRemaining = livesStart;
		if (currentSceneIndex == 1) {
			levelMusic.clip = Resources.Load("Audio/level1") as AudioClip;
		}
		if (currentSceneIndex == 2) {
			levelMusic.clip = Resources.Load("Audio/level2") as AudioClip;
		}		
		if (currentSceneIndex == 3) {
			levelMusic.clip = Resources.Load("Audio/level3") as AudioClip;
		}		
		if (currentSceneIndex == 4) {
			levelMusic.clip = Resources.Load("Audio/level4") as AudioClip;
		}		
		if (currentSceneIndex == 5) {
			levelMusic.clip = Resources.Load("Audio/level5") as AudioClip;
		}



        foreach (GameObject c in capturePoint)
        {
            basicCapturePointScripts.Add(c.GetComponent<BasicCapturePoint>());
        }
 
        levelMusic.playOnAwake = true;
        levelMusic.loop = true;
        levelMusic.Play();
        
    }

    void Update()
    {
        checkCapturePointsForOwnership();
    }

    public int GetLivesRemaining()
    {
        return livesRemaining;
    }

    public void LoseLife (int l = 1) {
		livesRemaining -= l;
	}

    public void ResetLives()
    {
        livesRemaining = livesStart;
    }
    
    public bool PlayerControlsAllPoints()
    {
        return playerHasAllCheckPoints;
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

    public void GameOver()
    {
        levelMusic.Stop ();
	    anim.SetTrigger ("GameOver");
    }

    public void GameOverReset()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Victory()
    {
        levelMusic.Stop ();
		anim.SetTrigger("VictoryAnimation");
        victoryMusic.clip = Resources.Load ("Audio/Victory") as AudioClip;
        victoryMusic.playOnAwake = true;
        victoryMusic.loop =false;
        victoryMusic.Play ();
	}
}
