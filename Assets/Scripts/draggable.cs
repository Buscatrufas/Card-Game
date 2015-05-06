using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

	void Start(){
		this.enabled = false;
	}

	GameObject placeholder = null;


	public enum Slot
	{
		DEAD,
		BATTLE,
		REST,
		DECK,
		HAND
	};


	Slot typeOfItem = Slot.HAND; 

	public void OnBeginDrag (PointerEventData eventData){

		placeholder = new GameObject ();
		placeholder.transform.SetParent ( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement> ();
		le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex (this.transform.GetSiblingIndex ());

		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent (this.transform.parent.parent);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		//dropZone[] zones = GameObject.FindObjectsOfType<dropZone> ();
	}

	public void OnDrag (PointerEventData eventData){

		this.transform.position = eventData.position;

		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);
		
		int newSiblingIndex = placeholderParent.childCount;
		
		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {
				
				newSiblingIndex = i;
				
				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;
				
				break;
			}
		}
		
		placeholder.transform.SetSiblingIndex(newSiblingIndex);

	}

	public void OnEndDrag (PointerEventData eventData){

		this.transform.SetParent (parentToReturnTo);
		this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ());
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (typeOfItem == Slot.REST)
			this.enabled = false;
		Destroy (placeholder);

		//EventSystem.current.RaycastAll (eventData);
	}

	public Slot getType(){
		return typeOfItem;
	}

	public void changeSlot(Slot type){
		typeOfItem = type;
	}



}
