using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ServerConfing : MonoBehaviour {

	public Transform playerPrefab;
	public ArrayList playerScripts = new ArrayList();
	//private int numberOfPlayers = 0;

	public GameObject tablePrefab;
	public GameObject player1;
	public GameObject player2;

	public Text inputNameText;
	public GameObject inputNameInfo;
	public Text inputClassText;
	public GameObject playButton;
	public Text notice;

	void OnServerInitialized()
	{
		PrepareTable ();
	}
	
	void OnPlayerConnected	(NetworkPlayer player){
		SpawnPlayer (player);
	}

	void SpawnPlayer(NetworkPlayer player)
	{
		//string tempPlayerString = player.ToString();
		//int playerNumber = Convert.ToInt32(tempPlayerString);

		if (playerScripts.Count == 0) {
			//Instantiate(player1);
			player1.SetActive(true);

		} else if (playerScripts.Count == 1) {

			//Instantiate(player2);
		} else {
			notice.text = "Sala full";
		}
		/*string tempPlayerString = player.ToString();
		int playerNumber = Convert.ToInt32(tempPlayerString);	

		Transform newPlayerTransform = (Transform)Network.Instantiate(playerPrefab, transform.position, transform.rotation, playerNumber);
		playerScripts.Add(newPlayerTransform.GetComponent("moveCube"));

		NetworkView theNetworkView = newPlayerTransform.GetComponent<NetworkView>();
		theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
		int size = playerScripts.Count;
		Debug.Log ("Número de jugadores: " + size);*/
	}

	void PrepareTable(){
		inputNameText.enabled = false;
		Destroy (inputNameInfo);
		inputClassText.enabled = false;
		Destroy (playButton);
		Destroy (notice);

		//Transform newPlayerTransform = (Transform)Network.Instantiate(tablePrefab, transform.position, transform.rotation, 0);
		//NetworkView theNetworkView = newPlayerTransform.GetComponent<NetworkView>();
	}

	void OnPlayerDisconnected(){
		Debug.Log("Jugador desconectado");
	}
}
