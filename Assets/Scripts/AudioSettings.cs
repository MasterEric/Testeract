using UnityEngine;
using System.Collections;

public class AudioSettings : MonoBehaviour {
	void FixedUpdate () {
		if(GameStateManager.isInitialized)
			AudioListener.volume = PlayerPrefs.GetFloat("Volume")/100;
	}
}
