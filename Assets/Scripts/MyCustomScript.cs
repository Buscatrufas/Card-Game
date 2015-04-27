using UnityEngine;
using System.Collections;

public class MyCustomScript : Photon.MonoBehaviour {


	float m_Health = 30;
	bool m_IsAwesome = true;

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	// Use this for initialization

	private void StartServer(){
		Network.InitializeServer (1, 25000, Network.HavePublicAddress ());
		MasterServer.RegisterHost (typeName, gameName);
	}

	void Start () {
		//PhotonNetwork.sendRate = 20;
		//PhotonNetwork.sendRateOnSerialize = 10;


	}

	void OnPhotonSerializeView(
		PhotonStream stream,
		PhotonMessageInfo info )
	{
		if (stream.isWriting == true) {
			stream.SendNext (m_Health);
			stream.SendNext (m_IsAwesome);
		} else {
			m_Health = (float) stream.ReceiveNext();
			m_IsAwesome = (bool) stream.ReceiveNext();
		}
		
	}

	void OnPhotonSerializeView(
			PhotonStream stream,
			PhotonStream message )
	{
		stream.Serialize (ref m_Health);
		stream.Serialize (ref m_IsAwesome);
	}
	

}
