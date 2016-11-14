using UnityEngine;
using System.Collections;

public class Level1TutorialText : MonoBehaviour {

    public bool qHasBeenPressed = false;
    public bool editorHasBeenOpened = false;
    public bool enterProduceOrder = false;
    public bool enterAttackOrder = false;
    public bool enterAttackTarget = false;
    public bool pressPlay = false;
    bool drawStartInfo;

    float ToturialBoxStartPosX;
    float ToturialBoxStartPosY;
    float TutorialBoxHeight;
    float TutorialBoxWidth;
    Rect TutorialBox;

    public string TutorialPage1,
                  TutorialPage2,
                  TutorialPage3,
                  TutorialPage4,
                  TutorialPage5,
                  TutorialPage6;

    public GUISkin commandSkin;

    levelManager lvlManager;

    // Use this for initialization
    void Start()
    {
        ToturialBoxStartPosX = (Screen.width / 2) - (Screen.width / 6);
        ToturialBoxStartPosY = (Screen.height / 2) - (Screen.height / 3);
        TutorialBoxHeight = Screen.height / 6;
        TutorialBoxWidth = Screen.width / 3;
        drawStartInfo = true;
        TutorialBox = new Rect(ToturialBoxStartPosX, ToturialBoxStartPosY, TutorialBoxWidth, TutorialBoxHeight);

        lvlManager = GameObject.Find("LevelManager").GetComponent<levelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        GUI.skin = commandSkin;
        if (drawStartInfo && lvlManager.currentLevel == 1) { 
        if (qHasBeenPressed == false && editorHasBeenOpened == false && enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage1, commandSkin.GetStyle("tooltipBackground"));
        }

        if(qHasBeenPressed == true && editorHasBeenOpened == false && enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage2, commandSkin.GetStyle("tooltipBackground"));
        }

        if(qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == false && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage3, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == false && enterAttackTarget == false && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage4, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == false && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage5, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true && pressPlay == false)
        {
            GUI.Box(new Rect(TutorialBox), TutorialPage6, commandSkin.GetStyle("tooltipBackground"));
        }

        if (qHasBeenPressed == true && editorHasBeenOpened == true && enterProduceOrder == true && enterAttackOrder == true && enterAttackTarget == true &&pressPlay == true)
        {
                drawStartInfo = false;
        }
        }
    }
}
