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
	}

	private void initializeTable(){
		Instantiate(table);
	}

}
