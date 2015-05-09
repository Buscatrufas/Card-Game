using UnityEngine;
using System.Collections;

public class totus : MonoBehaviour {

	public GameObject source;
	public GameObject target;
	private LineRenderer lr;

	// Use this for initialization
	void Start () {

		 
		lr = gameObject.AddComponent<LineRenderer> ();
		lr.SetWidth (6, 6);
		lr.SetVertexCount (2);


		lr.SetPosition (0, source.transform.position);
		lr.SetPosition (1, target.transform.position);
	}


	
	// Update is called once per frame
	void Update () {



	}
}
