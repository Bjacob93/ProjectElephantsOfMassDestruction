using UnityEngine;
using System.Collections;

public class backbutton : MonoBehaviour {

	public bool onclick = false;
	public GameObject mainmenus;
	Mainmenuaudio mm;

	public void onclickk(){
		onclick = true;
	}

	// Use this for initialization
	void Start () {
			if (onclick == true) {
				mm = GameObject.Find ("Canvas").GetComponent<Mainmenuaudio> ();
				mm.MainMenuSound.Play ();
			}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
