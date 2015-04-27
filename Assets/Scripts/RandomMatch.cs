using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomMatch : Photon.MonoBehaviour{

	private PhotonView myPhotonView;
	
	private string roomName = "Room01";
	private string roomStatus = "";
	
	private int MaxPlayer = 2;
	private string maxPlayerString = "2";
	
	private Room[] game;
	
	private Vector2 scrollPosition;
	
	// Use this for initialization
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnJoinedLobby()
	{
		Debug.Log("JoinRandom");
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}
	
	void OnJoinedRoom()
	{
		Debug.Log ("Ha entrado en la sala");
		/*GameObject monster = PhotonNetwork.Instantiate("monsterprefab", Vector3.zero, Quaternion.identity, 0);
		monster.GetComponent<myThirdPersonController>().isControllable = true;
		myPhotonView = monster.GetComponent<PhotonView>();*/
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		
		if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
		{
			bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;
			
			if (shoutMarco && GUILayout.Button("Marco!"))
			{
				myPhotonView.RPC("Marco", PhotonTargets.All);
			}
			if (!shoutMarco && GUILayout.Button("Polo!"))
			{
				myPhotonView.RPC("Polo", PhotonTargets.All);
			}
		}
		
		if (PhotonNetwork.insideLobby == true) {
			GUI.Box(new Rect(Screen.width/2.5f, Screen.height/3f, 400, 500), "");
			GUILayout.BeginArea(new Rect(Screen.width/2.5f, Screen.height/3f, 400, 500));
			GUI.color = Color.red;
			GUILayout.Box("Lobby");
			GUI.color = Color.white;
			
			GUILayout.Label("Room Name: ");
			roomName = GUILayout.TextField(roomName);
			GUILayout.Label("Max amount of players 1-20:");
			maxPlayerString = GUILayout.TextField(maxPlayerString,2);
			if(maxPlayerString != ""){
				
				MaxPlayer = int.Parse(maxPlayerString);
				
				if(MaxPlayer > 20) MaxPlayer = 20;
				if(MaxPlayer == 0) MaxPlayer = 1;
			}else{
				MaxPlayer = 1;
			}
			
			if(GUILayout.Button("Create room ") ){
				
				if(roomName != "" && MaxPlayer >0){
					PhotonNetwork.CreateRoom(roomName, true, true, MaxPlayer);
				}
			}
			
			GUILayout.Space(20);
			GUI.color = Color.red;
			GUILayout.Box("Game rooms");
			GUI.color = Color.white;
			GUILayout.Space(20);
			
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(400), GUILayout.Height(300));
			
			foreach (RoomInfo game in PhotonNetwork.GetRoomList()){
				
				GUI.color = Color.green;
				GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers);
				GUI.color = Color.white;
				if(GUILayout.Button("Join Room")){
					PhotonNetwork.JoinRoom(game.name);
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}

}
