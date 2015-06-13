using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;

public class ServerButtonInfo : MonoBehaviour {
	NetworkTesterractManager networkManager;
	MatchDesc data;
	
	public Text LeftText;
	public Text RightText;

	void Start() {
		this.networkManager = NetworkTesterractManager.GetManager();
		this.GetComponent<Button>().onClick.AddListener(OnClickButton);
		this.transform.localScale = new Vector3(1,1,1);
	}

	public void SetHostData(MatchDesc data) {
		Debug.Log ("Setting Match Data: "+data.name+":"+data.currentSize+"/"+data.maxSize);
		this.data = data;
	}

	void Update () {
		LeftText.text = GetTextLeft();
		RightText.text = GetTextRight();
	}

	string GetTextLeft() {
		return data.name;
	}

	string GetTextRight() {
		return data.currentSize +"/"+ data.maxSize;
	}
	
	void OnClickButton() {
		networkManager.networkMatch.MatchMakerJoinMatch(this.data);
	}

}
