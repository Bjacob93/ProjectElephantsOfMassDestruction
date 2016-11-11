using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CapturePointUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public Image captureimage;
    float capPoints = 0;
    float maxPoints = 100;
	// Update is called once per frame
	void Update () {
        captureimage.fillAmount = (capPoints / 50) - 50;
        captureimage.fillAmount = capPoints / 50;
	}
}
