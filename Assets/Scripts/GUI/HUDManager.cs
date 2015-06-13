using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {
	public GameObject HUDPrefab;
	GameObject currentHUD;
	Text redScoreText;
	Text blueScoreText;

	void Start () {
		currentHUD = (GameObject)Instantiate(HUDPrefab);
		blueScoreText = GameObject.FindGameObjectWithTag("GUIBlueScore").GetComponent<Text>();
		redScoreText = GameObject.FindGameObjectWithTag("GUIRedScore").GetComponent<Text>();
	}
	
	public void UpdateScores(int redScore, int blueScore){
		blueScoreText.text = blueScore.ToString();
		redScoreText.text = redScore.ToString();
	}
}
