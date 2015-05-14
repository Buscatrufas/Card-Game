using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class totus : MonoBehaviour {

	public GameObject target;
	public GameObject source;

	public Canvas table;

	void Awake(){
		table.renderMode = RenderMode.ScreenSpaceCamera;
	}
	// Use this for initialization
	void Start () {
	
		/*LineRenderer lr = source.AddComponent<LineRenderer> ();
		lr.useWorldSpace = true;
		lr.sortingOrder = 1;
		lr.SetWidth (1, 1);
		lr.SetVertexCount (2);*/

		//lr.SetPosition (0, source.transform.position);
		//lr.SetPosition (1, target.transform.position);
	}


	
	// Update is called once per frame
	void Update () {

	}

	public void OnMouseDown(){
		Debug.Log (gameObject.name);
	}
}
