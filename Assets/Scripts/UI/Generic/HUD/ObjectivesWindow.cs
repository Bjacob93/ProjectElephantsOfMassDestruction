using UnityEngine;
using System.Collections;

public class ObjectivesWindow : MonoBehaviour {

    //Bool that tracks if the objectives window is expanded or not.
    bool objectivesWindowExpanded = false;

    //Cache the objectives box.
    private Rect    ObjectivesBox;
    private float   boxX,
                    boxY;

    //Cache dimensions of the objectives box when closed.
    private float   closedWidth,
                    closedHeight;

    //Cache dimensions of the objectives box when open.
    private float   openWidth,
                    openHeight;

    public GUISkin  commandSkin;

    public string   objective1 = "Kill all enemies.",
                    objective2 = "Hold all Capture points.",
                    objective3 = "Defend your base agains enemy attacks.";

    void Start()
    {
        //Calculate the starting position of the objectives box.
        boxX = Screen.width / 100;
        boxY = Screen.height / 100;

        //Calculate the dimensions of the closed box.
        closedWidth = Screen.width / 6;
        closedHeight = Screen.height / 8;

        //Calculate the dimentions of the expanded box.
        openWidth = Screen.width / 6;
        openHeight = Screen.height / 3;

        ObjectivesBox = new Rect(boxX, boxY, closedWidth, closedHeight);

        commandSkin = Resources.Load("Graphix/commandSkin") as GUISkin;

        commandSkin.GetStyle("ObjectivesBox").fontSize = Screen.width / 80;
        commandSkin.GetStyle("ObjectivesBox").fontStyle = FontStyle.Bold;

        commandSkin.GetStyle("ObjectivesBox").padding.left = Screen.width / 45;
        commandSkin.GetStyle("ObjectivesBox").padding.right = Screen.width / 45;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DndHelpWindow"))
        {
            objectivesWindowExpanded = !objectivesWindowExpanded;
        }
    }

    void OnGUI()
    {
        GUI.skin = commandSkin;

        if (objectivesWindowExpanded)
        {
            commandSkin.GetStyle("ObjectivesBox").alignment = TextAnchor.UpperCenter;
            commandSkin.GetStyle("ObjectivesBox").padding.top = Screen.height / 17;

            ObjectivesBox = new Rect(boxX, boxY, openWidth, openHeight);
            GUI.Box(ObjectivesBox, objective1 + "\n\n" + objective2 + "\n\n" + objective3, commandSkin.GetStyle("ObjectivesBox"));
            
        }
        else
        {
            commandSkin.GetStyle("ObjectivesBox").alignment = TextAnchor.MiddleCenter;
            commandSkin.GetStyle("ObjectivesBox").padding.top = 0;

            ObjectivesBox = new Rect(boxX, boxY, closedWidth, closedHeight);
            GUI.Box(ObjectivesBox, "Press H to open objectives", commandSkin.GetStyle("ObjectivesBox"));
        }
    }
}
