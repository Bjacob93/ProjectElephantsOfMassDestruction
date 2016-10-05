using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	public int lives = 20;
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

	public void GameOver(){
		Debug.Log ("Game Over");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	
	}

	// Update is called once per frame
	void Update () {
		moneyText.text = "Money: $" + money.ToString ();
		livesText.text = "Lives" + lives.ToString ();
	}
}
