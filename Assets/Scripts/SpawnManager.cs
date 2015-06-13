using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GameObject[] RedSpawns;
	public GameObject[] BlueSpawns;

	public GameObject SinglePlayerPrefab;
	
	void Start() {
		this.tag = "SpawnManager";
		if(GameStateManager.isSinglePlayer) {
			GameObject newPlayerObject = GameObject.Instantiate<GameObject>(SinglePlayerPrefab);
			NetworkTesterractPlayer newPlayer = newPlayerObject.GetComponent<NetworkTesterractPlayer>();
			newPlayer.SetTeam(NetworkTesterractPlayer.GetRandomTeam());
		}
	}

	public static bool DoesSpawnManagerExist() {
		return GameObject.FindGameObjectWithTag("SpawnManager") != null;
	}
	public static SpawnManager GetSpawnManager() {
		return GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
	}

	public void RespawnPlayer(NetworkTesterractPlayer player) {
		Vector3 pos;
		Quaternion rot;
		int num;
		switch(player.GetTeam()) {
			case NetworkTesterractPlayer.Team.BLUE:
				num = ((int)Mathf.Round(Random.value)*(BlueSpawns.Length-1));
				Debug.Log ("BlueSpawn"+num);
				pos = BlueSpawns[num].transform.position;
				rot = RedSpawns[num].transform.localRotation;
				break;
			case NetworkTesterractPlayer.Team.RED:
				num = ((int)Mathf.Round(Random.value)*(RedSpawns.Length-1));
				Debug.Log ("RedSpawn"+num);
				pos = RedSpawns[num].transform.position;
				rot = RedSpawns[num].transform.localRotation;
				break;
			default:
				pos = Vector3.zero;
				rot = Quaternion.identity;
				break;
		}
		Debug.Log ("Respawning player at: "+pos);
		player.gameObject.transform.position = pos;
		player.gameObject.transform.localRotation = rot;
	}
}
