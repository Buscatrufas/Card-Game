using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int turn;

	private bool setPlayer1;
	private bool setPlayer2;

	public GameObject Player1Zone;
	public GameObject Player2Zone;
	public GameObject card;

	public GameObject table;

	private GameObject hand1;

	private void Awake(){
		initializeTable ();
		StartGame ();
	}

	void Start(){


	}

	private void initializeTable(){
		//Instantiate(table);
		/*for(int i = 0; i < elementsOfPlayer1.Length; ++i){
			if(elementsOfPlayer1[i].name == "handPlayer1"){
				Debug.Log("TOTUS");
			}
		}*/

		GameObject[] elementsOfPlayer1 = GameObject.FindGameObjectsWithTag ("Player1");

		foreach (GameObject go in elementsOfPlayer1)
			if (go.name == "handPlayer1") {
				GameObject c = (GameObject) Instantiate (card);
				c.transform.SetParent(go.transform , false);
			}

	}

	void StartGame(){
		if (!setPlayer1 && !setPlayer2) {
			setPlayer1 = true;
			//posicionar en setPlayer1 en PlayerZone1
		} else if (!setPlayer1) {
			setPlayer1 = true;
			//posicionar en setPlayer1 en PlayerZone1
		} else if (!setPlayer2) {
			setPlayer2 = true;
			//posicionar en setPlayer2 en PlayerZone2
		}
	}


}
