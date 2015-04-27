using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine; 
using System.Collections;

public class MyClient: MonoBehaviour  {

	private LoadBalancingClient loadBalancingClient;
	public string TurnbasedAppId;
	//public string userName;
	//public string userToken;


	void start(){
		loadBalancingClient = new LoadBalancingClient ("app.exitgamescloud.com:5055", TurnbasedAppId, "1.0");
		bool connectInProccess = loadBalancingClient.ConnectToRegionMaster ("eu");
		Debug.Log ("Estado: " + connectInProccess);
	}

	void update(){
		if (loadBalancingClient != null) loadBalancingClient.Service ();
	}

	void OnApplicationQuit(){
		if (loadBalancingClient != null) loadBalancingClient.Disconnect ();
	}

	void OnGUI(){
		if (loadBalancingClient != null) GUILayout.Label(loadBalancingClient.State + " " + loadBalancingClient.Server);
	}

}
