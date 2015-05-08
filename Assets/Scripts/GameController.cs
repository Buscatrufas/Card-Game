using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class GameController : MonoBehaviour {

	private int turn;

	//private bool setPlayer1 = false;
	//private bool setPlayer2 = false;

	private bool turnPlayer1 = false;
	private bool turnPlayer2 = false;

	public GameObject Player1Zone;
	public GameObject Player2Zone;
	public GameObject card;
	public GameObject zhapire;
	public draggable script;

	public GameObject table;
	public Text life1;
	public Text life2;

	private GameObject hand1;

	private static GameObject[] listOfElement1;
	private static GameObject[] listOfElement2;
	Player jug1;
	Player jug2;



	private void Awake(){

		jug1 = new Player ("Buscatrufas", "Guardian");
		jug2 = new Player ("Butifarra", "Asesino");
		listOfElement1 = GameObject.FindGameObjectsWithTag ("Player1");
		listOfElement2 = GameObject.FindGameObjectsWithTag ("Player2");
		selectInit ();
		initializeTable ();
		manageGame ();
		//StartGame ();
	}


		


	private void initializeTable(){
		//var monsterCollection = MonsterContainer.Load(Path.Combine(Application.dataPath, "monsters.xml"));
		/*Debug.Log (monsterCollection.Monsters[0].getName()+ " " + monsterCollection.Monsters[0].getAtk() + " " + 
		            monsterCollection.Monsters[0].getHealth());*/
		foreach (GameObject go in listOfElement1) {
			if (go.name == "NamePlayer") {
				Text t = go.GetComponent<Text> ();
				t.text = jug1.NamePlayer.ToUpper();
				life1.text = jug1.Health.ToString();
			
			}
			if (go.name == "handPlayer") {
				for (int i = 0; i<3; ++i) {
					createDinamicCard(go, "Player1");
				}
			}
		}

		foreach (GameObject go in listOfElement2) {
			if (go.name == "NamePlayer") {
				Text t = go.GetComponent<Text> ();
				t.text = jug2.NamePlayer.ToUpper();
				life2.text = jug2.Health.ToString();
				
			}

			if (go.name == "handPlayer") {
				createDinamicCard(go, "Player2");
			}	

		}

	}

	/*void StartGame(){
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

	}*/

	void selectInit(){
		if (Random.Range (0, 2) == 0) {
			turnPlayer1 = true;
		} else {
			turnPlayer2 = true;
		}
	}

	public void manageGame(){
			
		if (turnPlayer1 && jug1.Zaphires < 10) {
			jug1.Zaphires += 1;
			initRound (listOfElement1, "Player1");
		} else if (turnPlayer2 && jug2.Zaphires < 10) {
			jug2.Zaphires += 1;
			initRound (listOfElement2, "Player2");
		}


	}

	public void initRound(GameObject[] listOfElement, string player){
		foreach (GameObject go in listOfElement)
			if (go.name == "ManaPlayer") {
				GameObject mana = (GameObject)Instantiate (zhapire);
				mana.transform.SetParent (go.transform, false);
				mana.tag = player;
				//return true;
			}
		//return false;
	}

	public void pushNext(){

		if (turnPlayer1) {
			turnPlayer1 = false;
			turnPlayer2 = true;
			ManageChangeOfTurn(listOfElement2, listOfElement1);
		} else {
			turnPlayer2 = false;
			turnPlayer1 = true;
			ManageChangeOfTurn(listOfElement1, listOfElement2);
		}
		manageGame ();
	}

	public void ManageChangeOfTurn(GameObject[] list1, GameObject[] list2){
		foreach (GameObject go in list1) {
			if(go.name == "handPlayer"){
				if(go.transform.childCount>0){
					foreach(Transform cardInGame in go.transform)
							cardInGame.GetComponent<draggable>().enabled = true;
				}
			}
		}
	}

	public void createDinamicCard(GameObject go, string player){

		var monsterCollection = MonsterContainer.Load(Path.Combine(Application.dataPath, "monsters.xml"));
		GameObject c = (GameObject)Instantiate (card);
		int it = Random.Range (0, monsterCollection.Monsters.Length);
		foreach(Transform data in c.transform){
			if(data.name == "name"){
				Text t = data.GetComponent<Text>();
				t.text = monsterCollection.Monsters[it].getName();
			}
			if(data.name == "Attack"){
				Text t = data.GetComponentInChildren<Text>();
				t.text = monsterCollection.Monsters[it].getAtk().ToString();
			}
			if(data.name == "Defense"){
				Text t = data.GetComponentInChildren<Text>();
				t.text = monsterCollection.Monsters[it].getHealth().ToString();
			}
			
		}
		c.transform.SetParent (go.transform, false);
		c.tag = player;
		if(player == "Player1" && !turnPlayer1)
			c.GetComponent<draggable> ().enabled = false;
		if(player == "Player2" && !turnPlayer2)
			c.GetComponent<draggable> ().enabled = false;

	}
}
