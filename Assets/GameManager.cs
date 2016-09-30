using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int money = 100;
	public int lives = 20;

	public Text moneyText;
	public Text livesText;

	public void loseLives (int l = 1){
		lives -= l;
		if (lives <= 0) {
			GameOver ();
		}	
	}

	public void GameOver(){
		//TODO: Send pLayer to GameOver screen then menu.
		Debug.Log ("GameOver");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	
	}

	void update(){
	//FIXME: does not have to update every frame
		moneyText.text = money.ToString();
		livesText.text = lives.ToString();


	}
}
