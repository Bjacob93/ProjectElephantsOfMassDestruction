using UnityEngine;
using System.Collections;

public class LivesWindow : MonoBehaviour {

    //Cache coordinates and dimensions of UI box.
    private Rect    livesBox,
                    labelRect;
    private float   livesBoxX,
                    livesBoxY,
                    livesBoxWidthAndHeight,
                    labelX,
                    labelY,
                    labelWidth,
                    labelHeigth;

    //Cache the score manager.
    ScoreManager scoreManager;

    //Cache the GUI skin.
    GUISkin boxSkin;

	void Start () {
        //Reference the score manager.
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        //Reference the skin and set the font size and style.
        boxSkin = Resources.Load("Graphix/hudBoxes") as GUISkin;
        boxSkin.GetStyle("livesBox").fontSize = Screen.width / 30;
        boxSkin.GetStyle("livesBox").fontStyle = FontStyle.Bold;
        boxSkin.GetStyle("livesBox").padding.top = Screen.width / 100;
        boxSkin.GetStyle("livesBoxText").fontSize = Screen.width / 90;
        boxSkin.GetStyle("livesBoxText").fontStyle = FontStyle.Bold;
        boxSkin.GetStyle("livesBoxText").alignment = TextAnchor.UpperCenter;

        //Calculate the box coordinat and dimensions.
        livesBoxWidthAndHeight = Screen.width / 15;
        livesBoxX = Screen.width / 2 - livesBoxWidthAndHeight / 2;
        livesBoxY = Screen.height / 100;
        livesBox = new Rect(livesBoxX, livesBoxY, livesBoxWidthAndHeight, livesBoxWidthAndHeight);

        //set the Label location;
        labelX = livesBoxX;
        labelY = livesBoxY + Screen.height / 50;
        labelWidth = livesBoxWidthAndHeight;
        labelHeigth = Screen.height / 20;
        labelRect = new Rect(labelX, labelY, labelWidth, labelHeigth);
    }

    void OnGUI()
    {
        GUI.skin = boxSkin;

        GUI.Box(livesBox, "" + scoreManager.GetLivesRemaining(), boxSkin.GetStyle("livesBox"));
        GUI.Box(labelRect, "Lives:", boxSkin.GetStyle("livesBoxText"));
    }
}
