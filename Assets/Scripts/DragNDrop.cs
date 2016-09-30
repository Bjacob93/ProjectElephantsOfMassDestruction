using UnityEngine;
using UnityEngine.UI;
//using System.Collections;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform returnToHand = null;
    public Transform placeholderParent = null;
    //public enum Slot { WEAPON, HEAD, CHEST, LEGS, FEET, INVENTORY };
    //public Slot typeOfItem = Slot.WEAPON;

	GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("onBeginDrag");
        
		placeholder = new GameObject ();
		placeholder.transform.SetParent (this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement> ();
		le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        returnToHand = this.transform.parent;
        placeholderParent = returnToHand;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("onDrag");
        
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent) {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++) {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x) {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex) {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log("onEndDrag");

        this.transform.SetParent(returnToHand);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy (placeholder);
    }
}
