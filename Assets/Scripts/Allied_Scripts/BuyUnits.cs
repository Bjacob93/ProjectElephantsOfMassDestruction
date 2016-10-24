using UnityEngine;
using System.Collections;

//TODO: Make "Not Enough Money" message appear in-game.

public class BuyUnits : MonoBehaviour {

	public GameObject playerUnits;

    GameObject unitManager;
    UnitPrices unitPrices;

    GameObject scoreHolder;
    ScoreManager scoreManager;

    public bool gameIsPaused = true;

	void Start(){
        unitManager = GameObject.Find("UnitManager");
        unitPrices = unitManager.GetComponent<UnitPrices>();

        scoreHolder = GameObject.Find("ScoreManager");
        scoreManager = scoreHolder.GetComponent<ScoreManager>();
    }

	void OnMouseDown ()
	{
        if (gameIsPaused)
        {
            return;
        }

		if (gameObject.tag == "PlayerBase")
        {
            if (scoreManager.money < unitPrices.alliedMeleeCost)
            {
                Debug.Log("Not enought money");
                return;
            }
      
            scoreManager.money -= unitPrices.alliedMeleeCost;

            Instantiate(playerUnits, new Vector3(45, 1, 45), Quaternion.Euler(0, 0, 0));
        }
	}
}
