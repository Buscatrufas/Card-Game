using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.EventSystems;

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

	public Canvas table;
	public Text life1;
	public Text life2;

	private GameObject selectorSource = null;
	private GameObject selectorTarget = null;

	private static GameObject[] listOfElement1;
	private static GameObject[] listOfElement2;

	public GameObject JugAvatar1;
	public GameObject JugAvatar2;
	public GameObject result;
	public Sprite Win;
	public Sprite Lose;
	int stateArrow = 0;

	Dictionary<GameObject, GameObject> sourcesAndTargets;

	Player jug1 = new Player ("Buscatrufas", "Guardian");
	Player jug2 = new Player ("Butifarra", "Asesino");

	private void Awake(){
		result.SetActive (false);
		listOfElement1 = GameObject.FindGameObjectsWithTag ("Player1");
		listOfElement2 = GameObject.FindGameObjectsWithTag ("Player2");
		sourcesAndTargets = new Dictionary<GameObject, GameObject> ();
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
			//Debug.Log("Cristales jugador1: " + jug1.Zaphires);
			initRound (listOfElement1, "Player1");
		} else if (turnPlayer2 && jug2.Zaphires < 10) {
			jug2.Zaphires += 1;
			//Debug.Log("Cristales jugador2: " + jug2.Zaphires);
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
			ChangeStateOfCard (listOfElement2);
			turnPlayer1 = false;
			turnPlayer2 = true;
			jug1.CostInTurn = 0;
			manageGame ();
			ManageChangeOfTurn(listOfElement2, listOfElement1, jug2);
		} else {
			ChangeStateOfCard (listOfElement1);
			turnPlayer2 = false;
			turnPlayer1 = true;
			jug2.CostInTurn = 0;
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

			if(go.name == "ManaPlayer"){
				jug.Zaphires = go.transform.childCount;
				//Debug.Log("Jugador: " + jug.getNamePlayer() + " " + jug.Zaphires);
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
				//jug.Zaphires = go.transform.childCount;
				//Debug.Log("Numero de cristales al final del turno: " + jug.Zaphires);
			}
		}
		//Debug.Log (jug.getNamePlayer () + " cristales: " + jug.Zaphires);
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
				int costTurn;
				if (card.tag == "Player1"){ 
					jug1.CostInTurn += price;
					costTurn = jug1.CostInTurn;
				}
				else{ 
					jug2.CostInTurn +=price;
					costTurn = jug2.CostInTurn;
				}
				GameObject[] zhapireContent = GameObject.FindGameObjectsWithTag(card.tag);
				foreach(GameObject zaphire in zhapireContent){
					if(zaphire.name == "ManaPlayer"){
						for(int i = 0; i < costTurn; ++i)
							Destroy(zaphire.transform.GetChild(i).gameObject);
						for(int i = 0; i<costTurn; ++i){
							GameObject mana = (GameObject)Instantiate (lockZaphire);
							mana.transform.SetParent (zaphire.transform, false);
							mana.tag = card.tag;
						}
					}
				}
				if(card.tag == "Player1") {
					jug1.Zaphires -= price;
					DisabledHandPostDrop(card.tag, jug1);
				}else if(card.tag == "Player2"){
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

	public void PrepareTableToAttack(){
		if (turnPlayer1)
			DisableHandPreAttack (listOfElement1);
			else
				DisableHandPreAttack(listOfElement2);
	}



	public void ChangeStateOfCard(GameObject[] ListOfElement){
		foreach (GameObject hand in ListOfElement) {
			if(hand.name == "DropZone"){
				foreach(Transform card in hand.transform)
					if(card.GetComponent<draggable>().getType() == draggable.Slot.REST){
						card.GetComponent<draggable>().changeSlot(draggable.Slot.BATTLE);
				}
			}
		}
	}
	public void DisableHandPreAttack(GameObject[] ListOfElement){
		foreach (GameObject hand in ListOfElement) {
			if(hand.name == "handPlayer"){
				for(int i = 0; i<hand.transform.childCount; ++i){
						hand.transform.GetChild(i).GetComponent<draggable>().enabled = false;
						Image img = hand.transform.GetChild(i).GetComponent<Image> ();
						img.color = Color.red;
					
				}
			}
			if(hand.name == "DropZone"){
				for(int i = 0; i<hand.transform.childCount; ++i){
					if(hand.transform.GetChild(i).GetComponent<draggable>().getType() == draggable.Slot.BATTLE){
						Image img = hand.transform.GetChild(i).GetComponent<Image> ();
						img.color = Color.blue;
					}
				}
			}
		}
	}

	public void selectTargetToAttack(string gameObjectName, string tag){


			if(gameObjectName == "AvatarPlayer" && tag == "Player1")
				selectorTarget = JugAvatar1;
		else if(gameObjectName == "AvatarPlayer" && tag == "Player2")
				selectorTarget = JugAvatar2;
			else {
				//Debug.Log("Es una carta");
				string path = tag+"/DropZone/"+gameObjectName;
				selectorTarget = GameObject.Find(path);
			}

			sourcesAndTargets.Add (selectorSource, selectorTarget);
			LineRenderer lr = selectorSource.AddComponent<LineRenderer>();
			lr.useWorldSpace = true;
			lr.sortingOrder = 1;
			lr.SetWidth (1, 1);
			lr.SetVertexCount (2);
			
			lr.SetPosition (0, selectorSource.transform.position);
			lr.SetPosition (1, selectorTarget.transform.position);

			selectorSource = null;
			selectorTarget = null;


	}


	public void setSource(string gameObjectName, string tag){
		string path = tag+"/DropZone/"+gameObjectName;
		selectorSource = GameObject.Find(path);
	}

	public bool AmITheSource(string name){
		if (selectorSource == null)
			return false;
		if(selectorSource.name == name){
			CleanVariables();
			return true;
		}
		return false;
	}

	public bool CanTarget(string name, string tag){
		if (selectorSource != null && selectorSource.tag != tag)
			return true;
		return false;
	}

	public void CleanVariables(){
		selectorSource = null;
		selectorTarget = null;
		//Limpiar si existe un lineRenderer aun por medio
	}

	/*--- Se hace daño y se destruye el lineRenderer ---*/
	public void DoDamage(){
		if (turnPlayer1)
			tag = "Player1";
		else
			tag = "Player2";

		string path = tag+"/DropZone";
		GameObject zoneToAttack = GameObject.Find (path);


		for (int i = 0; i < zoneToAttack.transform.childCount; ++i) {
			if(zoneToAttack.transform.GetChild(i).GetComponent<LineRenderer>() != null){
				GameObject cardToAttack = zoneToAttack.transform.GetChild(i).gameObject;
				cardToAttack.GetComponent<Clikcable>().IsPicked = false;
				if(sourcesAndTargets.ContainsKey(cardToAttack)){
					foreach(Transform atk in cardToAttack.transform){
						if(atk.name == "Attack"){
							Text t = atk.GetComponentInChildren<Text> ();
							if(turnPlayer1 && sourcesAndTargets[cardToAttack].GetComponent<draggable>() == null){
								jug2.Health -= int.Parse(t.text);
								life2.text = jug2.Health.ToString();
							}else if(turnPlayer2 && sourcesAndTargets[cardToAttack].GetComponent<draggable>() == null){
								jug1.Health -= int.Parse(t.text);
								life1.text = jug1.Health.ToString();
							}
							else if(sourcesAndTargets[cardToAttack].GetComponent<draggable>() != null){
								UpdateHPCard(cardToAttack, getAtkFromTarget(sourcesAndTargets[cardToAttack]));
								UpdateHPCard(sourcesAndTargets[cardToAttack], getAtkFromTarget(cardToAttack));
								if(getHPCard(sourcesAndTargets[cardToAttack].name, sourcesAndTargets[cardToAttack].tag) <= 0){
									/*string ruta = tag+"/DropZone/"+name;
									card = GameObject.Find(ruta);
									Destroy (card);*/
									Destroy(sourcesAndTargets[cardToAttack]);
								}//DESTROZAR
								if(getHPCard(cardToAttack.name, cardToAttack.tag) <= 0) Destroy(cardToAttack);
							}

						}
					}
				}
				Destroy(zoneToAttack.transform.GetChild(i).GetComponent<LineRenderer>());
			}
		}

		if (turnPlayer1) 
			CheckEndGame(jug2);
		else
			CheckEndGame(jug1);
	}

	/*--- Borrar cuando este creado el lineRenderer ---*/
	public void CleanVariablesInAttackTurn(string name, string tag){

		string path = tag+"/DropZone/"+name;
		card = GameObject.Find(path);
		Debug.Log ("Amos a arreglar esto no?");
		if (sourcesAndTargets.Remove (card)) {
			Destroy(card.GetComponent<LineRenderer>());
		}

		selectorSource = null;
		selectorTarget = null;
		
	}

	public void CleanDictionary(){
		sourcesAndTargets.Clear ();
	}

	public void CheckEndGame(Player jug){
		ButtonsController disableAll = GameObject.Find ("NetworkManager").GetComponent<ButtonsController> ();
		if (jug.Health <= 0 && jug.NamePlayer != jug1.NamePlayer) {
			result.SetActive(true);
			Image sol = result.GetComponent<Image>();
			sol.sprite = Win;
			disableAll.disableAllButtons();
		}else if(jug.Health <= 0 && jug.NamePlayer == jug1.NamePlayer){
			result.SetActive(true);
			Image sol = result.GetComponent<Image>();
			sol.sprite = Lose;
			disableAll.disableAllButtons();
		}
		//MOSTRAR PANTALLAS Y PARAR JUEGO
	}

	public int getAtkFromTarget(GameObject cardTarget){
		foreach (Transform atk in cardTarget.transform) {
			if (atk.name == "Attack") {
				Text t = atk.GetComponentInChildren<Text> ();
				return int.Parse (t.text);
			}
		}
		return 0;
	}

	public void UpdateHPCard(GameObject go, int dmg){
		foreach (Transform hp in go.transform) {
			if (hp.name == "Defense") {
				Text t = hp.GetComponentInChildren<Text> ();
				t.text = (int.Parse(t.text) - dmg).ToString();
				int.Parse(t.text);
			}
		}
	}

	public int getHPCard(string name, string tag){
		string path = tag+"/DropZone/"+name;
		GameObject go = GameObject.Find (path);
		foreach (Transform hp in go.transform) {
			if (hp.name == "Defense") {
				Text t = hp.GetComponentInChildren<Text>();
				return int.Parse(t.text);
			}
		}
		return 0;
	}


}
