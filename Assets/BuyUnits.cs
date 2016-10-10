using UnityEngine;
using System.Collections;

public class BuyUnits : MonoBehaviour {

	public GameObject playerUnits;
	void start(){
		
	}
	void update(){
	}

	void OnMouseDown ()
	{
		if (gameObject.tag == "PlayerBase") {
			ScoreManager sm = GameObject.FindObjectOfType<ScoreManager> ();
			if (sm.money < GameObject.FindObjectOfType<AlliedMelee_AI_Health> ().cost) {
				Debug.Log("Not enought money");
				return;
			}

			sm.money -= GameObject.FindObjectOfType<AlliedMelee_AI_Health> ().cost;

			Instantiate(playerUnits, new Vector3(45, 1, 45), Quaternion.Euler(0,0,0));
			//Debug.Log("fuck");
		}
	}
}
