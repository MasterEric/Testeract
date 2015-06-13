using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;

public class NetworkTesterractMatch : MonoBehaviour {
	//Use an instance of network match instead of making this derive from NetworkMatch.
	NetworkTesterractManager networkManager;
	List<MatchDesc> matchList;
	bool matchCreated;

	void Start() {
		networkManager = NetworkTesterractManager.GetManager();
		matchList = new List<MatchDesc>();
		matchCreated = false;
	}
	public void MatchMakerStart() {
		NetworkManager.singleton.StartMatchMaker();
	}
	public void MatchMakerStop() {
		if(MatchMakerExists()) {
			NetworkManager.singleton.StopMatchMaker();
		}
	}
	public bool MatchMakerExists() {
		return NetworkManager.singleton.matchMaker != null;
	}
	public List<MatchDesc> MatchMakerGetMatchList() {
		if(!MatchMakerExists())
			MatchMakerStart();
		return matchList;
	}

	public void MatchMakerCreateMatch(string name, uint size, bool shouldAdvertise, string password) {
		if(!MatchMakerExists())
			MatchMakerStart();
		CreateMatchRequest create = new CreateMatchRequest();
		create.name = name;
		create.size = size;
		create.advertise = shouldAdvertise;
		create.password = password;
		NetworkManager.singleton.matchMaker.CreateMatch(create, OnMatchCreate);
	}
	public void MatchMakerJoinMatch(MatchInfo matchInfo) {
		if(!MatchMakerExists())
			MatchMakerStart();
		NetworkManager.singleton.matchMaker.JoinMatch(matchInfo.networkId, "", OnMatchJoined);
	}
	public void MatchMakerJoinMatch(MatchDesc matchInfo) {
		if(!MatchMakerExists())
			MatchMakerStart();
		NetworkManager.singleton.matchMaker.JoinMatch(matchInfo.networkId, "", OnMatchJoined);
	}


	public void OnMatchCreate(CreateMatchResponse matchResponse) {
		if (matchResponse.success) {
			Debug.Log("Create match succeeded");
			matchCreated = true;
			Utility.SetAccessTokenForNetwork(matchResponse.networkId, new NetworkAccessToken(matchResponse.accessTokenString));
			NetworkServer.Listen(new MatchInfo(matchResponse), 25777);
			
		}
		else {
			Debug.LogError ("Create match failed");
		}
	}
	
	public void OnMatchList(ListMatchResponse matchListResponse) {
		if (matchListResponse.success && matchListResponse.matches != null) {
			matchList = matchListResponse.matches;
		}
	}
	
	public void OnMatchJoined(JoinMatchResponse matchJoin) {
		if (matchJoin.success) {
			Debug.Log("Join match succeeded");
			if (matchCreated) {
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchJoin.networkId, new NetworkAccessToken(matchJoin.accessTokenString));
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(new MatchInfo(matchJoin));
		} else {
			Debug.LogError("Join match failed");
		}
	}
	
	public void OnConnected(NetworkMessage msg) {
		Debug.Log("Connected!"+msg.ToString());
	}
}
