using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {
	
	List<NetworkTesterractPlayer> realPlayersBlue;
	List<NetworkTesterractPlayer> realPlayersRed;
	List<NetworkTesterractPlayer> botsBlue;
	List<NetworkTesterractPlayer> botsRed;

	public List<NetworkTesterractPlayer> playerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(realPlayersBlue); l.AddRange(realPlayersRed); l.AddRange(botsBlue); l.AddRange(botsRed);
				return l; }
	}
	public List<NetworkTesterractPlayer> bluePlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(realPlayersBlue); l.AddRange(botsBlue);
				return l; }
	}
	public List<NetworkTesterractPlayer> redPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(realPlayersRed); l.AddRange(botsRed);
				return l; }
	}
	public List<NetworkTesterractPlayer> realPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(realPlayersBlue); l.AddRange(realPlayersRed);
				return l; }
	}
	public List<NetworkTesterractPlayer> botPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(botsBlue); l.AddRange(botsRed);
				return l; }
	}
	public List<NetworkTesterractPlayer> blueRealPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(realPlayersBlue);
				return l; }
	}
	public List<NetworkTesterractPlayer> redRealPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(realPlayersRed);
				return l; }
	}
	public List<NetworkTesterractPlayer> blueBotPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(botsBlue);
				return l; }
	}
	public List<NetworkTesterractPlayer> redBotPlayerList {
		get {	List<NetworkTesterractPlayer> l = new List<NetworkTesterractPlayer>();
				l.AddRange(botsRed);
				return l; }
	}

	int blueScore;
	int redScore;

	public GameObject blueFlagPrefab;
	public GameObject redFlagPrefab;

	GameObject blueFlag;
	GameObject redFlag;
	
	public static bool DoesGameManagerExist() {
		return GameObject.FindGameObjectWithTag("GameManager") != null;
	}
	public static GameManager GetGameManager() {
		return GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
		// Use this for initialization
	void Start () {
		this.tag = "GameManager";
	}

	public GameObject GameInstantiatePrefab(GameObject prefab) {
		ClientScene.RegisterPrefab(prefab);
		GameObject obj = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
		NetworkServer.Spawn(obj);
		return obj;
	}

	public GameObject GetFlag(NetworkTesterractPlayer.Team team) {
		switch(team) {
			case NetworkTesterractPlayer.Team.BLUE:
				return blueFlag;
				break;
			case NetworkTesterractPlayer.Team.RED:
				return redFlag;
				break;
		}
	}
	
	public void AddRealPlayer(NetworkTesterractPlayer player) {
		switch(player.GetTeam()) {
			case NetworkTesterractPlayer.Team.BLUE:
				realPlayersBlue.Add (player);
				break;
			case NetworkTesterractPlayer.Team.RED:
				realPlayersRed.Add (player);
				break;
		}
	}
	public void AddBot(NetworkTesterractPlayer player) {
		switch(player.GetTeam()) {
			case NetworkTesterractPlayer.Team.BLUE:
				botsBlue.Add (player);
				break;
			case NetworkTesterractPlayer.Team.RED:
				botsRed.Add (player);
				break;
		}
	}

	public int GetScore(NetworkTesterractPlayer.Team team) {
		switch(team) {
			case NetworkTesterractPlayer.Team.BLUE:
				return blueScore;
			case NetworkTesterractPlayer.Team.RED:
				return redScore;
			default:
				return -1;
		}
	}
	
}
