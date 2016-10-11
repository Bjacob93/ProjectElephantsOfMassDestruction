using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
	Animator anim;   

	public int lives = 2;
	public int money = 100;

	public Text moneyText;
	public Text livesText;

	// Use this for initialization
	public void LoseLife (int l = 1) {
		lives -= l;
		if (lives <= 0) {
			GameOver ();
		}
	}

	IEnumerator waitandprint(float waitTime){
		yield return new WaitForSeconds (waitTime);

	}

	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {
		moneyText.text = "Money: $" + money.ToString ();
		livesText.text = "Lives" + lives.ToString ();
	}
	void Start(){
		LoseLife ();
	}
	public void LoadByIndex(int sceneIndex) {
		SceneManager.LoadScene(sceneIndex);
	}
			
	public void GameOver(){
		anim.SetTrigger ("GameOver");
		//waitandprint (2);
		//LoadByIndex();
		//		Debug.Log ("Game Over");
		//SceneManager.LoadScene (SceneManager.GetActiveScene (mainMenu).name);
	}
}
