﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	//public draggable.Slot typeOfItem = draggable.Slot.INVENTORY;

	public void OnDrop (PointerEventData eventData){
		Debug.Log (eventData.pointerDrag + " OnDrop to " + gameObject.name);
		draggable d = eventData.pointerDrag.GetComponent<draggable> ();
		if (d != null ) { // && (typeOfItem == d.typeOfItem || typeOfItem==draggable.Slot.INVENTORY )
			d.parentToReturnTo = this.transform;
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
