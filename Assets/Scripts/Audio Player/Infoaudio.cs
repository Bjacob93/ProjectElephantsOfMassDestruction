using UnityEngine;
using System.Collections;

public class Infoaudio : MonoBehaviour {
	public AudioSource Infosound;
	public GameObject InfoPanel;
	Mainmenuaudio mm;


	// Use this for initialization
	void Start () {
		mm = GameObject.Find ("Canvas").GetComponent<Mainmenuaudio>();
			mm.MainMenuSound.Stop ();
			Infosound = gameObject.AddComponent<AudioSource> ();
			Infosound.clip = Resources.Load ("Audio/Info") as AudioClip;
			Infosound.loop = true;
			Infosound.playOnAwake = true;
			Infosound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
