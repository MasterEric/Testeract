using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	
	Slider progressBar;
	public float progressBarMaxValue = 100;
	public string StartingScene;
	//Should print logs?
	public bool isVerbose = false;

	void Start() {
		this.tag = "SceneChanger";
		DontDestroyOnLoad(this.gameObject);
		//SetScene(StartingScene);
	}
	public static bool DoesSceneChangerExist() {
		return GameObject.FindGameObjectWithTag("SceneChanger") != null;
	}
	public static SceneChanger GetSceneChanger() {
		return GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneChanger>();
	}

	//RUN THIS ONE It uses the one in the loading manager.
	public void SetScene (string Scene) {
		if(isVerbose)
			Debug.Log ("Set Scene: "+Scene);
		StartCoroutine(LoadingScreen());
		StartCoroutine(SetSceneCoroutine(Scene));	
	}
	IEnumerator LoadingScreen() {
		AsyncOperation asyncLoading = Application.LoadLevelAsync("LoadingScreen");
		while(Application.isLoadingLevel)
			yield return asyncLoading;
		asyncLoading = null;
		if(isVerbose)
			Debug.Log ("Loading Screen Coroutine ended.");
	}
	IEnumerator SetSceneCoroutine(string Scene) {
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		if(isVerbose)
			Debug.Log ("Loading level "+Scene+"...");
		Debug.LogWarning("ASYNC LOAD STARTED - DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");

		AsyncOperation asyncLevel = Application.LoadLevelAsync(Scene);

		if(progressBar == null)
			while(GameObject.FindGameObjectWithTag("ProgressBar") == null)
				yield return asyncLevel;
			progressBar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Slider>();

		asyncLevel.allowSceneActivation = true;		
		while(!asyncLevel.isDone) {
			progressBar.value = asyncLevel.progress * progressBarMaxValue;
			if(isVerbose)
				Debug.Log ("Loading Scene"+asyncLevel.progress);
			yield return asyncLevel;
		}

		asyncLevel = null;
		if(isVerbose)
			Debug.Log ("New Level Coroutine ended");
	}
	void OnLevelWasLoaded(int level) {
		if(Application.loadedLevelName != "LoadingScreen") {
			if(isVerbose)
				Debug.Log ("Level was loaded.");
		} else {
			if(isVerbose)
				Debug.Log ("Load screen was loaded");
		}
	}
}
