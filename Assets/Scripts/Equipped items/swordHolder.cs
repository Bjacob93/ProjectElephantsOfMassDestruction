using UnityEngine;
using System.Collections;

public class swordHolder : MonoBehaviour {

	public Transform RightHand;
	public sword startingsword;
	sword equippedSword;

	void Start(){
		if (startingsword != null) {
			equipSword(startingsword);
		}
	}
	public void equipSword(sword swordToEquip){
		if (equippedSword != null) {
			Destroy (equippedSword.gameObject);
		}
		equippedSword = Instantiate(swordToEquip, RightHand.position, RightHand.rotation) as sword;
			equippedSword.transform.parent = RightHand;
	}
}
