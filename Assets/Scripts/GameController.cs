using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int turn;

	private bool setPlayer1;
	private bool setPlayer2;

	public GameObject Player1Zone;
	public GameObject Player2Zone;

	public GameObject table;

	private void Awake(){
		initializeTable ();
		StartGame ();
	}

	private void initializeTable(){
		Instantiate(table);
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
