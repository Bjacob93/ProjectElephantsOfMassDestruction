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
			Instantiate(playerUnits, new Vector3(-4, 1, -4), Quaternion.Euler(0,180,0));
		}
	}
}
