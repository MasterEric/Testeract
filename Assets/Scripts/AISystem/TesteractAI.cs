using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class TesteractAI : MonoBehaviour {

	//Dictionary<string, Bot> botList {
	//	get { return GameManager.GetGameManager().botPlayerList; }
	//}

	public GameObject botAIPrefab;

	public Vector3 blueFlagDefenseMarker;
	public Vector3 redFlagDefenseMarker;


	public static GameObject botPrefab {
		get { return GetAIInstance().botAIPrefab; }
	}

	NetworkTesterractPlayer player;	

	public static TesteractAI GetAIInstance(){
		return GameObject.FindGameObjectWithTag("TesteractAI").GetComponent<TesteractAI>();
	}

	public static bool DoesTessteractAIExtist(){
		return GameObject.FindGameObjectWithTag("TesteractAI") != null;
	}

	public enum BotState {
		Idle = 0,
		DefendingFlag = 1,
		TrackingEnemy = 2,
		AttemptingFlagCapture = 3,
		CapturingFlag = 4,
		DefendingTeammate = 5,
		DefendingFlagHolder = 6,
		AttackingFlagThief = 7
	}

	public enum BotSkillLevel{
		CheekyScrublord = 0,
		Noob = 1,
		Regular = 2,
		Advanced = 3,
		Master = 4,
		FazeClan420Noscoper = 5
	}
	
	void Start () {
		this.tag = "TesteractAI";
		CreateBot("HaX!", NetworkTesterractPlayer.Team.RED, BotSkillLevel.CheekyScrublord, BotState.DefendingFlag);
	}

	void Update () {
		
	}

	public void CreateBot(string name, NetworkTesterractPlayer.Team botTeam, BotSkillLevel skillLevel, BotState botState){
		Bot newBot = NetworkTesterractManager.GetManager().GameInstantiatePrefab(botPrefab).GetComponent<Bot>();
		newBot.Init(name, botTeam, skillLevel, botState);
	}

	public Vector3 getTeamFlagDefencePoint(NetworkTesterractPlayer.Team botTeam){

		if(botTeam == NetworkTesterractPlayer.Team.BLUE){

			return blueFlagDefenseMarker;

		} else {

			return redFlagDefenseMarker;

		}
	}
	public void CreateBot(string name, NetworkTesterractPlayer.Team botTeam, BotSkillLevel skillLevel){
		Bot newBot = GameManager.GetGameManager().GameInstantiatePrefab(botPrefab).GetComponent<Bot>();
		Bot.Init(name, botTeam, skillLevel);
	}
}
