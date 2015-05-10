using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(AudioSource))]
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
	public GameObject lockZaphire;
	public draggable script;

	public GameObject table;
	public Text life1;
	public Text life2;

	private GameObject hand1;

	private static GameObject[] listOfElement1;
	private static GameObject[] listOfElement2;
	Player jug1 = new Player ("Buscatrufas", "Guardian");
	Player jug2 = new Player ("Butifarra", "Asesino");

	private void Awake(){

		listOfElement1 = GameObject.FindGameObjectsWithTag ("Player1");
		listOfElement2 = GameObject.FindGameObjectsWithTag ("Player2");
		selectInit ();
		manageGame ();
		initializeTable ();

		//StartGame ();
	}

	void Start(){

		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
	}

	private void initializeTable(){
		//var monsterCollection = MonsterContainer.Load(Path.Combine(Application.dataPath, "monsters.xml"));
		/*Debug.Log (monsterCollection.Monsters[0].getName()+ " " + monsterCollection.Monsters[0].getAtk() + " " + 
		            monsterCollection.Monsters[0].getHealth());*/
		foreach (GameObject go in listOfElement1) {
			if (go.name == "NamePlayer") {
				Text t = go.GetComponent<Text> ();
				t.text = jug1.getNamePlayer();
				life1.text = jug1.Health.ToString();
			
			}
			if (go.name == "handPlayer") {
				for (int i = 0; i<3; ++i) {
					createDinamicCard(go, "Player1", jug1);
				}
			}
		}

		foreach (GameObject go in listOfElement2) {
			if (go.name == "NamePlayer") {
				Text t = go.GetComponent<Text> ();
				t.text = jug2.getNamePlayer();
				life2.text = jug2.Health.ToString();
			}

			if (go.name == "handPlayer") {
				createDinamicCard(go, "Player2", jug2);
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
			Debug.Log("Cristales jugador1: " + jug1.Zaphires);
			initRound (listOfElement1, "Player1");
		} else if (turnPlayer2 && jug2.Zaphires < 10) {
			jug2.Zaphires += 1;
			Debug.Log("Cristales jugador2: " + jug2.Zaphires);
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
			manageGame ();
			ManageChangeOfTurn(listOfElement2, listOfElement1, jug2);
		} else {
			turnPlayer2 = false;
			turnPlayer1 = true;
			manageGame ();
			ManageChangeOfTurn(listOfElement1, listOfElement2, jug1);
		}

	}

	public void ManageChangeOfTurn(GameObject[] list1, GameObject[] list2, Player jug){
		foreach (GameObject go in list1) {
			if(go.name == "handPlayer"){
				if(go.transform.childCount>0){
					foreach(Transform cardInGame in go.transform){
						if(isUsableThisTransformCard(cardInGame, jug)){
								cardInGame.GetComponent<draggable>().enabled = true;
								Image img = cardInGame.GetComponent<Image> ();
								img.color = Color.green;
						}
					}
				}
			}
		}
		foreach(GameObject go in list2){
			if(go.name == "handPlayer"){
				if(go.transform.childCount>0){
					foreach(Transform cardInHand in go.transform){
						cardInHand.GetComponent<draggable>().enabled = false;
						Image img = cardInHand.GetComponent<Image> ();
						img.color = Color.red;
					}
				}
			}
			if(go.name == "DropZone"){
				if(go.transform.childCount>0){
					foreach(Transform cardInDropZone in go.transform){
						cardInDropZone.GetComponent<draggable>().enabled = false;
						Image img = cardInDropZone.GetComponent<Image> ();
						img.color = Color.red;
					}
				}
			}
			if(go.name == "ManaPlayer"){
				int reloadZaphires = go.transform.childCount;
				for(int i = reloadZaphires-1; i>=0; --i){
					Destroy(go.transform.GetChild(i).gameObject);
				}
				for(int i = 0; i<reloadZaphires; ++i){
					GameObject mana = (GameObject) Instantiate(zhapire);
					mana.transform.SetParent (go.transform, false);
					mana.tag = go.tag;
				}
				Debug.Log(go.tag);
				//jug.Zaphires = go.transform.childCount;
				//Debug.Log("Numero de cristales al final del turno: " + jug.Zaphires);
			}
		}
	}

	public void createDinamicCard(GameObject go, string player, Player jug){

		var monsterCollection = MonsterContainer.Load (Path.Combine (Application.dataPath, "monsters.xml"));
		GameObject c = (GameObject)Instantiate (card);

		int it = Random.Range (0, monsterCollection.Monsters.Length);

		foreach (Transform data in c.transform) {
			if (data.name == "name") {
				Text t = data.GetComponent<Text> ();
				t.text = monsterCollection.Monsters [it].getName ();
				c.name = monsterCollection.Monsters [it].getName ();
			}
			if (data.name == "Attack") {
				Text t = data.GetComponentInChildren<Text> ();
				t.text = monsterCollection.Monsters [it].getAtk ().ToString ();
			}
			if (data.name == "Defense") {
				Text t = data.GetComponentInChildren<Text> ();
				t.text = monsterCollection.Monsters [it].getHealth ().ToString ();
			}
			if (data.name == "cost") {
				Text t = data.GetComponentInChildren<Text> ();
				t.text = monsterCollection.Monsters [it].getCost ().ToString ();
			}
			
		}


		c.transform.SetParent (go.transform, false);
		c.tag = player;
		if ((player == "Player1" && !turnPlayer1) || (!isUsableThisCard (c, jug))) {
			c.GetComponent<draggable> ().enabled = false;
			Image img = c.GetComponent<Image> ();
			img.color = Color.red;
		} else if ((player == "Player2" && !turnPlayer2) || (!isUsableThisCard (c, jug))) {
			c.GetComponent<draggable> ().enabled = false;
			Image img = c.GetComponent<Image> ();
			img.color = Color.red;
		} else {
			Image img = c.GetComponent<Image> ();
			img.color = Color.green;
		}

	}

	public bool isUsableThisCard(GameObject go, Player jug){
		foreach (Transform cost in go.transform) {
			if (cost.name == "cost") {
				Text sCost = cost.GetComponentInChildren<Text>();
				int price = int.Parse(sCost.text);
				if(jug.Zaphires>=price) return true;
			}
		}
		return false;
	}

	public bool isUsableThisTransformCard(Transform go, Player jug){
		foreach (Transform cost in go.transform) {
			if (cost.name == "cost") {
				Text sCost = cost.GetComponentInChildren<Text>();
				int price = int.Parse(sCost.text);
				if(jug.Zaphires>=price){ 
					return true;
				}
			}
		}
		return false;
	}

	public void DisabledZaphires(GameObject card){
		foreach (Transform cost in card.transform) {
			if (cost.name == "cost") {
				Text sCost = cost.GetComponentInChildren<Text>();
				int price = int.Parse(sCost.text);
				GameObject[] zhapireContent = GameObject.FindGameObjectsWithTag(card.tag);
				foreach(GameObject zaphire in zhapireContent){
					if(zaphire.name == "ManaPlayer"){
						for(int i = zaphire.transform.childCount - 1; i >= zaphire.transform.childCount - price ; --i)
							Destroy(zaphire.transform.GetChild(i).gameObject);
						for(int i = 0; i<price; ++i){
							GameObject mana = (GameObject)Instantiate (lockZaphire);
							mana.transform.SetParent (zaphire.transform, false);
							mana.tag = card.tag;
						}
					}

					//if(zaphire.name == "handPlayer")
				}
				if(cost.tag == "Player1") {
					Debug.Log(jug1.Zaphires);
					jug1.Zaphires -= price;
					DisabledHandPostDrop(card.tag, jug1);
				}else{
					Debug.Log(jug2.Zaphires);
					jug2.Zaphires -= price;
					DisabledHandPostDrop(card.tag, jug2);
				}
			}
		}
	}

	public void DisabledHandPostDrop(string tag, Player jug){

		GameObject[] reviewHand = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject hand in reviewHand) {
			if(hand.name == "handPlayer"){
				for(int i = 0; i<hand.transform.childCount; ++i){
					if(!isUsableThisTransformCard(hand.transform.GetChild(i), jug)){
						hand.transform.GetChild(i).GetComponent<draggable>().enabled = false;
						Image img = hand.transform.GetChild(i).GetComponent<Image> ();
						img.color = Color.red;
					}
				}
			}
		}

	}
}
