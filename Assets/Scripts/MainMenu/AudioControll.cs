using UnityEngine;
using System.Collections;

public class AudioControll : MonoBehaviour {

	AudioSource MainMenuSound;

	public GameObject InfoPanel;
//	public bool updateOn=true;
//	bool playAudio=false;

	//public GameObject MainMenu;

 void mainmenu(){
		if (InfoPanel.activeInHierarchy) {
			MainMenuSound = gameObject.AddComponent<AudioSource> ();
			MainMenuSound.clip = Resources.Load ("Audio/Info") as AudioClip;
			MainMenuSound.loop = true;
			MainMenuSound.playOnAwake = true;
			MainMenuSound.Play ();
		} else {
			MainMenuSound = gameObject.AddComponent<AudioSource> ();
			MainMenuSound.clip = Resources.Load ("Audio/MainMenu3") as AudioClip;
			MainMenuSound.loop = false;
			MainMenuSound.playOnAwake = false;
			MainMenuSound.Play ();
		}
	}
void inforunning(){
		if (InfoPanel.activeInHierarchy) {
			MainMenuSound.Stop ();
//			MainMenuSound = gameObject.AddComponent<AudioSource> ();
			MainMenuSound.clip = Resources.Load ("Audio/Info") as AudioClip;
			MainMenuSound.loop = true;
			MainMenuSound.playOnAwake = true;
			MainMenuSound.Play ();
		}
	}
	// Use this for initialization
	void Start () {
		MainMenuSound.Play ();
		if (InfoPanel.activeInHierarchy) {
			mainmenu ();
		} else
			MainMenuSound.Stop ();

	}	
//	// Update is called once per frame
void Update () {

}

//	IEnumerator updateOff(){
//		yield return new WaitForSeconds (0.1f);
//		updateOn = false;	}
}