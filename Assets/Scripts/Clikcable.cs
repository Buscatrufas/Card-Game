using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clikcable : MonoBehaviour {

	private GameObject selectorTarget = null;
	private bool picked = false;

	void OnMouseDown(){
		GameController gamer = GameObject.Find ("NetworkManager").GetComponent<GameController> ();
		//Controlar la primera carta, no puede ser un marco, no puede ser una carta en mano y no puede estar ya seleccionada
		if (gameObject.name != "AvatarPlayer" && gameObject.GetComponent<draggable> ().getType () == draggable.Slot.BATTLE && 
			picked == false) {
			Debug.Log("1");
			IsPicked = true;
			gamer.setSource (gameObject.name, tag);
		//Controlar que si esta seleccionada y no ha seleccionado un target se deseleccione
		} else if (picked == true && gamer.AmITheSource (gameObject.name)) {
			Debug.Log("2");
			picked = false;
			//Controlar si la carta puede ser el objetivo de una primera
		} else if ((gameObject.name != "AvatarPlayer") 
			&& gamer.CanTarget (gameObject.name, gameObject.tag) && (picked == false)) {
			Debug.Log("3");
			gamer.selectTargetToAttack (gameObject.name, gameObject.tag);
			//Lo mismo que el anterior pero ataque a la cabeza
		} else if (gamer.CanTarget (gameObject.name, gameObject.tag) && picked == false && gameObject.name == "AvatarPlayer") {
			Debug.Log("4");
			gamer.selectTargetToAttack (gameObject.name, gameObject.tag);
			//Destrozar el line renderer en un ataque ya hecho, de paso, deselecciona la carta
		}else if(picked == true && !gamer.AmITheSource(gameObject.name) && gameObject.name != "AvatarPlayer"){
			Debug.Log("5");
			gamer.CleanVariablesInAttackTurn(gameObject.name, gameObject.tag);
			Debug.Log("Line Renderer Borrado");
			picked = false;
		}else{
			Debug.Log("TOTUS");
		}
		/*int flag = 0;
		if (gameObject.tag == "Player1" && gameObject.name == "AvatarPlayer") {
			GameController gamer = GameObject.Find("NetworkManager").GetComponent<GameController>();
			gamer.selectTargetToAttack(gameObject.name, 0, gameObject.tag);
		} else if (gameObject.tag == "Player2" && gameObject.name == "AvatarPlayer") {
			GameController gamer = GameObject.Find("NetworkManager").GetComponent<GameController>();
			gamer.selectTargetToAttack(gameObject.name, 1, gameObject.tag);
		}
		else if ((gameObject.GetComponent<draggable> ().getType() == draggable.Slot.BATTLE) && 
		         (gameObject.GetComponent<draggable>().parentToReturnTo.name == "DropZone")){

			GameController gamer = GameObject.Find("NetworkManager").GetComponent<GameController>();
			gamer.selectTargetToAttack(gameObject.name, 2, gameObject.tag);
		}*/
	}

	public bool IsPicked{
		get{
			return this.picked;
		}set{
			picked = value;
		}
	}
}
