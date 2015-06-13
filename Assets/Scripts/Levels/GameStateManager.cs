using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {
	static bool isDone = false;
	public static bool isSinglePlayer;
	public static bool isInitialized = false;

	void Start () {
		DontDestroyOnLoad(this.gameObject);
		if(!PlayerPrefs.HasKey("ViewModelsEnabled"))
			PlayerPrefs.SetInt("ViewModelsEnabled", 1);
		if(!PlayerPrefs.HasKey("Volume"))
			PlayerPrefs.SetFloat("Volume", 100);
		if(!PlayerPrefs.HasKey("FieldOfView"))
			PlayerPrefs.SetFloat("FieldOfView", 75);
		if(!PlayerPrefs.HasKey("PlayerName"))
			PlayerPrefs.SetString("PlayerName", "Player");
		isInitialized = true;
	}
	void Update() {
		if(!isDone) {
			SceneChanger.GetSceneChanger().SetScene(SceneChanger.GetSceneChanger().StartingScene);
			isDone = true;
		}
	}
	void OnApplicationQuit() {
		Debug.LogWarning("Game closing...");
		PlayerPrefs.Save();
		NetworkTesterractManager.GetManager().ClientStop();
		NetworkTesterractManager.GetManager().networkMatch.MatchMakerStop();
	}

}
