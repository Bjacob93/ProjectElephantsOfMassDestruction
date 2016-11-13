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

    int numOfObjectives;

    // Use this for initialization
    void Start()
    {
        numOfObjectives = 3;

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
        if(drawHelpWindow == true)
        {
            GUI.Box(new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, numOfObjectives * boundingBoxHeight), "Objectives");
            for (int i = 1; i < numOfObjectives + 1; i++)
            {
                GUI.Box(new Rect(50, i * 40, 50, 35), "Kill all enemies");
            }

        }
        if(drawHelpOpenText == true)
        {
            GUI.Box(new Rect(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight / 2), "Press H to \nopen objectives");
        }


    }

    void HelpText()
    {
    }
}
