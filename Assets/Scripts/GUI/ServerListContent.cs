using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;
public class ServerListContent : MonoBehaviour {

	public NetworkTesterractManager networkManager;
	public GameObject buttonPrefab;
	
	float TimeSinceReset;

	void Start() {
		networkManager = NetworkTesterractManager.GetManager();
	}

	void FixedUpdate () {
		
		if(networkManager.networkMatch.MatchMakerExists() && (networkManager.networkMatch.MatchMakerGetMatchList() != null)) {
			List<MatchDesc> matches = networkManager.networkMatch.MatchMakerGetMatchList();		
			foreach(Transform child in this.transform)
				GameObject.Destroy(child.gameObject);

			this.transform.localScale = new Vector3(1, matches.Count, 1);
		
			for (int i = 0; i < matches.Count; i++) {
				MatchDesc server = matches[i];
				Debug.Log("Server #"+i+":"+server.name+":"+server.currentSize+"/"+server.maxSize);
				GameObject button = (GameObject) Instantiate(buttonPrefab,this.transform.position, Quaternion.identity);
				button.transform.parent = this.transform;
				button.transform.localPosition = new Vector3(-15,-(50+100*i),0);
				button.GetComponent<ServerButtonInfo>().SetHostData(server);
			}
		} else {
			if(networkManager.networkMatch.MatchMakerGetMatchList() == null) {
				Debug.Log("No matches available.");
				this.transform.localScale = new Vector3(1, 0, 1);
			}
		}
	}
}
