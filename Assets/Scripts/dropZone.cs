using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public GameObject gc;

	//public draggable.Slot typeOfItem = draggable.Slot.INVENTORY;

	public void OnDrop (PointerEventData eventData){
		Debug.Log (eventData.pointerDrag + " OnDrop to " + gameObject.name);
		draggable d = eventData.pointerDrag.GetComponent<draggable> ();
		if (d != null && d.tag == gameObject.tag ) { // && (typeOfItem == d.typeOfItem || typeOfItem==draggable.Slot.INVENTORY )
			d.parentToReturnTo = this.transform;
			if(gameObject.name == "DropZone"){
				d.changeSlot(draggable.Slot.REST);
				GameController gamer = gc.GetComponent<GameController>();
				gamer.DisabledZaphires(eventData.pointerDrag);
			}
		}

	}

	public void OnPointerEnter (PointerEventData eventData){

		if(eventData.pointerDrag == null)
			return;

		draggable d = eventData.pointerDrag.GetComponent<draggable> ();
		if (d != null ) { // && (typeOfItem == d.typeOfItem || typeOfItem==draggable.Slot.INVENTORY )
			d.placeholderParent = this.transform;
		}
	}

	public void OnPointerExit (PointerEventData eventData){
		if(eventData.pointerDrag == null)
			return;
		
		draggable d = eventData.pointerDrag.GetComponent<draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}

}
