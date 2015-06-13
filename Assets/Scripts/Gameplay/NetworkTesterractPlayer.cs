using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkTesterractPlayer : NetworkBehaviour {
	
	public Material redMaterial;
	public Material blueMaterial;
	public Material neutralMaterial;
	Renderer playerRenderer;

	Bot bot;
	
	[SyncVar]
	Team currentTeam;
	[SyncVar]
	float currentHealth;
	[SyncVar]
	string currentName;	

	const float maxHealth = 100;
	const float DeathHeight = -10;
	
	public enum Team {
		RED = 0,
		BLUE = 1
	}
	
	public static Team GetRandomTeam() {
		return (Team)((int)Mathf.Round(Random.value));
	}
	
	void Start () {
		playerRenderer = GetComponentInChildren<MeshRenderer>();
		bot = GetComponent<Bot>();
		Respawn();
		if(isLocalPlayer) {
			SetTeam(GetRandomTeam());
			SetPlayerName(PlayerPrefs.GetString("PlayerName"));
		}
	}
	
	public bool IsBot() {
		return (this.bot != null);
	}

	public void SetTeam(Team t) {
		Debug.Log ("Team:"+t);
		this.currentTeam = t;
	}
	
	public Team GetTeam() {
		return this.currentTeam;
	}

	public void SetPlayerName(string s) {
		this.currentName = s;
	}
	
	
	public string GetPlayerName() {
		return this.currentName;	
	}

	void Respawn() {
		SpawnManager.GetSpawnManager().RespawnPlayer(this);
		this.currentHealth = maxHealth;
	}
	
	void SetPlayerColor() {
		switch(GetTeam()) {
		case Team.BLUE:
			playerRenderer.material = blueMaterial;
			break;
		case Team.RED:
			playerRenderer.material = redMaterial;
			break;
		default:
			playerRenderer.material = neutralMaterial;
			break;
		}
	}
	
	void FixedUpdate() {
		if(this.transform.position.y <= DeathHeight)
			Respawn();
		else if(this.currentHealth <= 0)
			Respawn();
	}
}
