using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;

public class NetworkTesterractManager : MonoBehaviour {
	public NetworkTesterractMatch networkMatch;
	public GameObject playerPrefab;

	void Start() {
 		DontDestroyOnLoad(this.gameObject);
	}

	public static NetworkTesterractManager GetManager() {
		return GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkTesterractManager>();
	}	

	public void ClientConnect(string networkAddress, int networkPort) {
		NetworkManager.singleton.networkAddress = networkAddress;
		NetworkManager.singleton.networkPort = networkPort;
		NetworkManager.singleton.maxDelay = 0.01f;
		NetworkManager.singleton.StartClient();
	}
	public MatchInfo ClientGetHost() {
		return NetworkManager.singleton.matchInfo;
	}

	public void ClientStop() {
		NetworkManager.singleton.StopClient();
	}



	public virtual void OnStartServer(NetworkConnection conn) {
		//Called when a client connects.
		Debug.Log ("OnStartServer");
	}
	public virtual void OnStopServer(NetworkConnection conn) {
		//Called when a client connects.
		Debug.Log ("OnStopServer");
	}
	public virtual void OnServerConnect(NetworkConnection conn) {
		//Called when a client connects.
		Debug.Log ("OnServerConnect");
	}
	public virtual void OnServerDisconnect(NetworkConnection conn) {
		//Called when a client disconnects.
		Debug.Log ("OnServerDisconnect");
		NetworkServer.DestroyPlayersForConnection(conn);	
	}
	public virtual void OnServerReady(NetworkConnection conn) {
		//Called when a client is ready
		Debug.Log ("OnServerReady");
		NetworkServer.SetClientReady(conn);
	}
	public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		Debug.Log ("OnServerAddPlayer");
		var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
	//public virtual void OnServerRemovePlayer(NetworkConnection conn, short playerControllerId) {
	//	//Called when a player is removed for a client
	//	NetworkPlayer player;
	//	if (conn.GetPlayer(playerControllerId, out player))
	//	{
	//		if (player.NetworkIdentity != null && player.NetworkIdentity.gameObject != null)
	//			NetworkServer.Destroy(player.NetworkIdentity.gameObject);
	//	}
	//	
	//}
	public virtual void OnServerError(NetworkConnection conn, int errorCode) {
		//Called when a network error occurs.
		Debug.LogError("Network Server Error #"+errorCode);
	}
	

	public virtual void OnStartClient(NetworkConnection conn) {
		//Called when a client connects.
		Debug.Log ("OnStartClient");
	}
	public virtual void OnStopClient(NetworkConnection conn) {
		//Called when a client connects.
		Debug.Log ("OnStopClient");
	}
	//public virtual void OnClientConnect(NetworkConnection conn) {
	//	//Called when a client connects to a server.
	//	//BROKEN: See file:///C:/Program%20Files/Unity/Editor/Data/Documentation/en/Manual/UNetManager.html	
	//	NetworkClientManager.Ready(conn);
	//	NetworkClientManager.AddPlayer(0);
	//}
	public virtual void OnClientDisconnect(NetworkConnection conn) {
		Debug.Log ("OnClientDisconnect");	
		//Called when your client disconnects from a server.
		//StopClient();
	}
	public virtual void OnClientError(NetworkConnection conn, int errorCode) {
		//Called when a network error occurs.
		Debug.LogError("Network Client Error #"+errorCode);
	}
	public virtual void OnClientNotReady(NetworkConnection conn) {
		Debug.Log ("OnClientNotReady");	
		//Called when told to be in a not-ready state by the server.
	}


	public virtual void OnStartHost(CreateMatchResponse matchInfo) {
		//Called when host is started.
		Debug.Log ("OnStartHost");	
	}
	public virtual void OnStopHost(CreateMatchResponse matchInfo) {
		//Called when host is stopped.
		Debug.Log ("OnStopHost");	
	}
}
