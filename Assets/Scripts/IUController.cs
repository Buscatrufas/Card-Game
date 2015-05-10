using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IUController : MonoBehaviour {

	/*--- Datos de conexión ---*/
	public string connectionIP = "127.0.0.1";
	public int connectionPort = 25001;

	/*--- Elementos del dropdown list ---*/
	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl;// = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();

	/*--- Elementos adicionales de la interfaz ---*/
	public InputField name;
	public Text warning;

	/*--- Declaracion de jugador ---*/
	private Player jug;

	/*--- Control de inicio de juego ---*/
	bool IUEnabled = true;


	public void pushPlay(){
		if (name.text.Length == 0) {
			warning.text = "Necesitas introducir un nombre";
		} else {
			warning.text = "";

			switch(getValueComboBox()+1){

			case 1:
				 jug = new Player(name.text, comboBoxList[0].text);
				break;

			case 2:
				jug = new Player(name.text, comboBoxList[1].text);
				break;

			case 3:
				jug = new Player(name.text, comboBoxList[2].text);
				break;

			case 4:
				jug = new Player(name.text, comboBoxList[3].text);
				break;
			
			}

			Debug.Log("Se ha creado un jugador con el nombre : " + jug.getNamePlayer() + " y de clase:  "  + jug.ClassPlayer );	
			Network.Connect(connectionIP, connectionPort);

		}
	}

	void Start() {
		comboBoxList = new GUIContent[4];
		comboBoxList[0] = new GUIContent("Guardian");
		comboBoxList[1] = new GUIContent("Asesino");
		comboBoxList[2] = new GUIContent("Hechicero");
		comboBoxList [3] = new GUIContent ("Clerigo");

		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
			listStyle.padding.right =
				listStyle.padding.top =
				listStyle.padding.bottom = 4;
		
		comboBoxControl = new ComboBox(new Rect(300, 190, 200, 30), comboBoxList[0], comboBoxList, "button", "box", listStyle);
	}

	void OnGUI () {
		if(IUEnabled) comboBoxControl.Show ();
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button(new Rect(10, 50, 120, 20), "Initialize Server"))
			{
				IUEnabled = !IUEnabled;
				Network.InitializeServer(32, connectionPort, false);
			}
		}
		
	}

	public int getValueComboBox(){
		return comboBoxControl.SelectedItemIndex;
	}

}
