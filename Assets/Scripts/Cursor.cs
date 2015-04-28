using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	
	//Hotspot Offset
	public Vector2 CursorOffset = Vector2.zero;

	//Override system cursor?
	public bool ShowSystemCursor = false;

	//Speed of cursor movment for keyboard override (pixels per second) 
	public float CursorSpeed = 100.0f;

	//Should enable keyboard override?
	public bool EnableKeyboardOverride = false;

	//Transform component
	private Transform ThisTransform = null;

	void Start(){

		//Cache transform
		ThisTransform = transform;

		//Hide or show system cursor
		UnityEngine.Cursor.visible = ShowSystemCursor;
	}

	void Update(){

		if(EnableKeyboardOverride){

			float Move = CursorSpeed * Time.deltaTime;
			
			ThisTransform.Translate(Move * Input.GetAxis("Horizontal") * Move, Input.GetAxis("Vertical") * Move, 0);

			return;
		}

		ThisTransform.position = new Vector3(Input.mousePosition.x + CursorOffset.x, 
		                                     Input.mousePosition.y + CursorOffset.y, ThisTransform.position.z);
	}

}
