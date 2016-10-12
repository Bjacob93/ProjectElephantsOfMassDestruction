using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class UIPop : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject menu;
    public GameObject building;
    public GameObject editorPanel;
    private GameObject[] commands;
    public List<string> commandList;

    private Animator anim;
    public GameObject commandListPanel;
    private bool slideIn = false;
    Bounds buildingBounds;
    RectTransform rt;
    void Start()
    {
        rt = editorPanel.GetComponent<RectTransform>();
        anim = commandListPanel.GetComponent<Animator>();
        buildingBounds = building.GetComponent<Renderer>().bounds;
        anim.enabled = false;
    }
    private bool activeMenu = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            commandList.Clear();
            //draws a ray from the main camera to the mouse position
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit targetObject;

            if (Physics.Raycast(mouseRay, out targetObject))
            {
                GameObject hitObject = targetObject.transform.root.gameObject;

                SelectObject(hitObject);
            }
            else
            {
                ClearSelection();
            }
            if (selectedObject == building)
            {
                menu.SetActive(true);
                activeMenu = true;
            }           
        }
        if (activeMenu)
        {
            EditorPos();
        }
        //This is temp since graphic raycast is not working
        if (Input.GetKey(KeyCode.I))
        {
            anim.enabled = false;
            commands = GameObject.FindGameObjectsWithTag("Command");
            foreach(GameObject go in commands)
            {
                if(go.transform.parent.gameObject.name == "Code")
                {
                    commandList.Add(go.gameObject.name);
                }
            }
            menu.SetActive(false);           
        }
    }
    void EditorPos()
    {
        Vector3[] buildingCorners = new Vector3[8];

        buildingCorners[0] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x, //x value
            buildingBounds.center.y + buildingBounds.extents.y,  //y value
            buildingBounds.center.z + buildingBounds.extents.z));//z value

        buildingCorners[1] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));

        buildingCorners[2] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));

        buildingCorners[3] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));

        buildingCorners[4] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));

        buildingCorners[5] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));

        buildingCorners[6] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));

        buildingCorners[7] = Camera.main.WorldToScreenPoint(new Vector3
            (buildingBounds.center.x + buildingBounds.extents.x,
            buildingBounds.center.y + buildingBounds.extents.y,
            buildingBounds.center.z + buildingBounds.extents.z));
        //find min and max
        float min_x = buildingCorners[0].x;
        float min_y = buildingCorners[0].y;

        for (int i = 1; i < 8; i++)
        {
            if (buildingCorners[i].x < min_x)
            {
                min_x = buildingCorners[i].x;
            }
            if (buildingCorners[i].y < min_y)
            {
                min_y = buildingCorners[i].y;
            }
        }
        rt.position = new Vector2(min_x - 420, min_y);
    }
    public void MenuSlider()
    {
        slideIn = !slideIn;
        anim.enabled = true;
        if (slideIn)
        {
            anim.Play("CommandListSlideIn");
        }
        else if (!slideIn)
        {
            anim.Play("CommandListSlideOut");
        }   
    }
    void SelectObject(GameObject obj)
    {
        if (selectedObject != null)//is an object all ready selected?
        {
            if (obj == selectedObject) //is it the same object?
            {
                return; //return if its the same object
            }
            ClearSelection(); //clear if its not the same object
        }
        selectedObject = obj; //select new object
    }
    void ClearSelection()
    {
        selectedObject = null; //clear selection
    }
}
