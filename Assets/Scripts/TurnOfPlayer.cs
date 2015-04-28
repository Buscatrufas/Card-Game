using UnityEngine;
using System.Collections;

public class TurnOfPlayer : MonoBehaviour {

	public NetworkPlayer theOwner;
	
	void Awake()
	{
		if (Network.isClient)
			enabled = false;
	}

	[RPC]
	void SetPlayer(NetworkPlayer player)
	{
		theOwner = player;
		if (player == Network.player)
			enabled = true;
	}



	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		if (stream.isWriting) {
			Vector3 pos = transform.position;
			stream.Serialize (ref pos);
		}
		else
		{
			Vector3 receivedPosition = Vector3.zero;
			stream.Serialize(ref receivedPosition);
			transform.position = receivedPosition;
		}
	}
}
