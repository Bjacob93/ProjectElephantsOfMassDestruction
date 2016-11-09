using UnityEngine;
using System.Collections;

public class helmetHolder : MonoBehaviour {

	public Transform Head;
	public helmet startinghelmet;
	helmet equippedHelmet;

	void Start(){
		if (startinghelmet != null) {
			equipHelmet(startinghelmet);
		}
	}
	public void equipHelmet(helmet helmetToEquip){
		if (equippedHelmet != null) {
			Destroy (equippedHelmet.gameObject);
		}
		equippedHelmet = Instantiate(helmetToEquip, Head.position, Head.rotation) as helmet;
		equippedHelmet.transform.parent = Head;
	}
}
