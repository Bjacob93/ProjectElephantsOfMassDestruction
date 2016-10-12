using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class UIPop : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject menu;
    //this is so the script knows what gameobject is the building
    public GameObject building;

    private GameObject[] commands;
    public List<string> commandList;

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
            }
        }
        //This is temp since graphic raycast is not working
        if (Input.GetKey(KeyCode.I))
        {
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
