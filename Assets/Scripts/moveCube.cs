using UnityEngine;
using System.Collections;
using System;

public class moveCube : MonoBehaviour {

	public NetworkPlayer theOwner;
	float lastClientHInput = 0f;
	float lastClientVInput = 0f;
	float serverCurrentHInput = 0f;
	float serverCurrentVInput = 0f;

	void Awake()
	{
		if (Network.isClient)
			enabled = false;
	}

	void Update () {

		if (theOwner != null && Network.player == theOwner) {
			float HInput = Input.GetAxis ("Horizontal");
			float VInput = Input.GetAxis ("Vertical");

			if(lastClientHInput!=HInput || lastClientVInput!=VInput)
			{
				lastClientHInput = HInput;
				lastClientVInput = VInput;
			}
			if (Network.isServer)
			{
				SendMovementInput(HInput, VInput);
			}
			else if (Network.isClient)
			{
				GetComponent<NetworkView>().RPC("SendMovementInput", RPCMode.Server, HInput, VInput);
			}
		}
		if(Network.isServer)
		{
			Vector3 moveDirection = new Vector3(serverCurrentHInput, 0, serverCurrentVInput);
			float speed = 5;
			transform.Translate(speed * moveDirection * Time.deltaTime);
		}
	}

	[RPC]
	void SetPlayer(NetworkPlayer player)
	{
		theOwner = player;
		if (player == Network.player)
			enabled = true;
	}

	/*[RPC]
	void SetPosition(Vector3 newPosition)
	{
		transform.position = newPosition;
	}*/

	[RPC]
	void SendMovementInput(float HInput, float VInput)
	{  
		serverCurrentHInput = HInput;
		serverCurrentVInput = VInput;
	}


	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
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
