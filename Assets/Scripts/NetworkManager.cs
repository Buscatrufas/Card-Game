using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour {

	private int serverPort = 9955;
	private int maxConnections = 512;
	private string serverName = "TestServer";
	bool isRefreshing = false;
	float refreshRequestLenght = 3.0f;
	HostData[] hostData;

	public GameObject table;

	private void Start(){
		
	}

	private void StartServer(){
		Network.InitializeServer (maxConnections, serverPort, false);
		MasterServer.RegisterHost (serverName, "PEne", "Esto es una prueba");

	}

	public IEnumerator RefresHostList(){
		Debug.Log ("Refreshing...");
		MasterServer.RequestHostList (serverName);
		//float timeStarted = Time.time;
		float timeEnd = Time.time + refreshRequestLenght;

		while (Time.time < timeEnd) {
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}


		if(hostData == null || hostData.Length == 0)
			Debug.Log("No hay servidores activos");
		else
			Debug.Log (hostData.Length + " Servidores encontrados"); 
	}

	private void SpawnPlayer(){
		//Crear jugador con la información y posicionarlo en la mesa + sumar cartas
	}

	void OnConnectedToServer(){
	}

	void OnDisconnectedFromServer(NetworkDisconnection info){
	}

	void OnFailedToConnect(NetworkConnectionError error){
	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent){
		if(masterServerEvent == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Register OK");
		}
	}

	void OnServerInitialized(){
		Debug.Log ("SERVIDOR LANZADO");
		SpawnPlayer ();
	}

	void OnPlayerConnected(NetworkPlayer player){
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("Jugador desconectado de: " + player.ipAddress + ":" + player.port);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info){
	}

	void OnNetworkInstantiate(NetworkMessageInfo info){
	}

	void OnApplicationQuit(){

		if (Network.isServer) {
			Network.Disconnect(200);
			MasterServer.UnregisterHost();
		}
	}


	public void OnGUI(){



		if (Network.isServer) 
			GUILayout.Label ("Servidor");
		else if (Network.isClient)
			GUILayout.Label ("Cliente");

		if (Network.isClient) {
			if(GUI.Button(new Rect(Screen.width/2 - 75f, 25f, 150f, 30f), "Spawn"))
			   SpawnPlayer();

		}

		if(!Network.isServer && !Network.isClient){
				if(GUI.Button(new Rect(25f, 25f, 150f, 30f), "Start New Server")){
					StartServer();
				}
				
				if(GUI.Button(new Rect(25f, 65f, 150f, 30f), "Refresh server List")){
					StartCoroutine("RefresHostList");
				}
				
				if (hostData != null) {
					for(int i = 0; i < hostData.Length; ++i){
						if(GUI.Button(new Rect(Screen.width/2, 65f + (30f* i), 300f, 30f), hostData[i].gameName)){
							Network.Connect(hostData[i]);
							Instantiate(table);
						}
					}
					
				}
			}
	}

}
