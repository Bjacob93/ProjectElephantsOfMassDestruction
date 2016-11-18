using UnityEngine;
using System.Collections;

public class ObjectivesWindow : MonoBehaviour {

    bool drawHelpWindow = false;
    bool drawHelpOpenText = true;

    //Variables that hold the dimensions of the bounding box of the window.
    float boundingBoxHeight;
    float boundingBoxWidth;
    float boundingBoxX;
    float boundingBoxY;
    public Rect boundingRect;

    float boxStartingPosX = (Screen.width - (Screen.width / 7)) - (Screen.width / 80);
    float boxStartingPosY = Screen.height / 4;
    float objectiveOffSet;
    int numOfObjectives;

    public GUISkin commandSkin;

    public string objective1 = "Kill all enemies";
    public string objective2 = "Hold all Capture points";
    public string objective3 = "Defend your base agains enemy attacks";
    public string[] objectives = new string[3]; //{ objetive1, objective2, objective3;

    // Use this for initialization
    void Start()
    {
        numOfObjectives = 3;
        objectives[0] = objective1;
        objectives[1] = objective2;
        objectives[2] = objective3;

        
        //Calculate the bounding box dimensions and define the resulting Rect.
        boundingBoxHeight = 100 + 5;
        boundingBoxWidth = 100;
        boundingBoxX = (boxStartingPosX  / 80) + 5;
        boundingBoxY = (boxStartingPosY  / 70) + 5;
        boundingRect = new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight);

       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DndHelpWindow"))
        {
            drawHelpWindow = !drawHelpWindow;
        }
        if (Input.GetButtonDown("DndHelpWindow"))
        {
            drawHelpOpenText = !drawHelpOpenText;
        }
    }

    void OnGUI()
    {
        GUI.skin = commandSkin;

        if (drawHelpWindow == true)
        {
            for (int i = 0; i < numOfObjectives; i++)
            {
                objectiveOffSet = objectives[i].Length;
                GUI.Box(new Rect(boundingBoxX, (i * 40) + (objectiveOffSet + 5), 80, (objectiveOffSet*2)+5), objectives[i], commandSkin.GetStyle("tooltipBackground"));
            }

        }
        if(drawHelpOpenText == true)
        {
            GUI.Box(new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight / 2), "Press H to \nopen objectives");
        }


    }
}
