using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;

public class GameCommand : MonoBehaviour {
	public NetworkTesterractManager networkManager;
	public void HostGame() {
		networkManager.networkMatch.MatchMakerCreateMatch("Testeract Server", 8, true, "");
	}
	public void LoadSceneSinglePlayer(string SceneName) {
		GameStateManager.isSinglePlayer = true;		
		if(SceneChanger.DoesSceneChangerExist()) {
			Debug.LogWarning("Setting scene to "+SceneName+"...");
			SceneChanger.GetSceneChanger().SetScene(SceneName);
		} else {
			Debug.LogError("Error: SceneChanger does not exist, could not load scene.");
		}
	}
	public void LoadSceneMultiPlayer(string SceneName) {
		GameStateManager.isSinglePlayer = false;
		if(SceneChanger.DoesSceneChangerExist()) {
			Debug.LogWarning("Setting scene to "+SceneName+"...");
			SceneChanger.GetSceneChanger().SetScene(SceneName);
		} else {
			Debug.LogError("Error: SceneChanger does not exist, could not load scene.");
		}
	}
	public void ExitGame(){
		Application.Quit();
	}
}
