using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {

	private NavMeshAgent aiAgent;
	private NetworkTesterractPlayer player;

	public string botName;
	public NetworkTesterractPlayer.Team botTeam;
	public TesteractAI.BotSkillLevel botSkillLevel;

	private TesteractAI.BotState currentBotState;

	bool isInitialized = false;

	void Start () {
		isInitialized = false;
		//player = GetComponent<NetworkTesterractPlayer>();
		//GameManager.GetGameManager().AddBot(player)
	}

	void Initialize() {
		isInitialized = true;
		player.SetPlayerName(name);
	}

	void Update () {
		if(isInitialized) {
			switch(currentBotState) {
				case TesteractAI.BotState.Idle:
					Debug.Log ("Bot is idle.");
					break;
				case TesteractAI.BotState.DefendingFlag:
					
				default:
					break;
			}
		}
	}

	public void Init(string botNameToSet, NetworkTesterractPlayer.Team botTeamToSet, TesteractAI.BotSkillLevel skillLevel){

		botName = botNameToSet;
		botTeam = botTeamToSet;
		botSkillLevel = skillLevel;

	}

	void DefendingFlag(){

	}
}