using UnityEngine;
using System.Collections;

//TODO: Make "Not Enough Money" message appear in-game.

public class BuyUnits : MonoBehaviour {

	public GameObject playerUnits;

    GameObject unitManager;
    UnitPrices unitPrices;

    GameObject scoreHolder;
    ScoreManager scoreManager;

	void start(){
        unitManager = GameObject.FindGameObjectWithTag("UnitManager");
        unitPrices = unitManager.GetComponent<UnitPrices>();

        scoreHolder = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoreHolder.GetComponent<ScoreManager>();
    }
	void update(){

    }

    void BuyMeleeUnit()
    {
        int money = scoreManager.getMoney();

        Debug.Log(money);
        Debug.Log(unitPrices.alliedMeleeCost);
        if (scoreManager.money < unitPrices.alliedMeleeCost)
        {
            Debug.Log("Not enought money");

            return;
        }

        scoreManager.money -= unitPrices.alliedMeleeCost;

        Instantiate(playerUnits, new Vector3(45, 1, 45), Quaternion.Euler(0, 0, 0));
    }

	void OnMouseDown ()
	{
		if (gameObject.tag == "PlayerBase") {

            BuyMeleeUnit();
		}
	}
}
