using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour {

	public GameObject Ready;
	public GameObject Attack;
	public GameObject NextTurn;
	public Canvas table;

	private GameObject gamer;
	void Awake(){
		gamer = GameObject.Find ("NetworkManager");
	}

	public void PushReady(){
		Ready.SetActive (false);
		Attack.SetActive (true);
		table.renderMode = RenderMode.ScreenSpaceCamera;
		gamer.GetComponent<GameController> ().PrepareTableToAttack ();
	}

	public void EndAttack(){
		gamer.GetComponent<GameController> ().DoDamage ();
		gamer.GetComponent<GameController> ().CleanVariables ();
		gamer.GetComponent<GameController> ().CleanDictionary ();
		Attack.SetActive (false);
		NextTurn.SetActive (true);
		table.renderMode = RenderMode.ScreenSpaceOverlay;
	}

	public void EndTurn(){
		NextTurn.SetActive (false);
		Ready.SetActive (true);
		gamer.GetComponent<GameController>().pushNext();
	}

	
	public void disableAllButtons(){
		Ready.SetActive (false);
		Attack.SetActive (false);
		NextTurn.SetActive (false);
	}
}
