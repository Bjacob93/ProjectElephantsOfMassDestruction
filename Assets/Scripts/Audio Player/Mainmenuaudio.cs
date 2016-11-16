using UnityEngine;
using System.Collections;

public class Mainmenuaudio : MonoBehaviour {
	
	public AudioSource MainMenuSound;
	public GameObject mainMenu;

	// Use this for initialization
	void Start () {
		if (mainMenu.activeInHierarchy) {
			MainMenuSound = gameObject.AddComponent<AudioSource> ();
			MainMenuSound.clip = Resources.Load ("Audio/MainMenu3") as AudioClip;
			MainMenuSound.loop = false;
			MainMenuSound.playOnAwake = false;
			MainMenuSound.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
