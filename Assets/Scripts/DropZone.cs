using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    //Manages the drop zone panels
    
    //public DragNDrop.Slot typeOfItem = DragNDrop.Slot.INVENTORY;
    
    //Checks when a draggable object moves into the area
    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        DragNDrop d = eventData.pointerDrag.GetComponent<DragNDrop>();
        if (d != null) {
            d.placeholderParent = this.transform;
        }
    }

    //Checks when a draggable object exits the area
    public void OnPointerExit(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        DragNDrop d = eventData.pointerDrag.GetComponent<DragNDrop>();
        if (d != null && d.placeholderParent == this.transform) {
            d.placeholderParent = d.returnToHand;
        }
    }

    //Checks where an object is to be dropped
    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("OnDrop to " + gameObject.name);

        DragNDrop d = eventData.pointerDrag.GetComponent<DragNDrop>();
        if (d != null) {
            d.returnToHand = this.transform;
        }
    }
}
