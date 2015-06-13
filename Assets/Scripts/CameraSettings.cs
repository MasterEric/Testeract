using UnityEngine;
using System.Collections;

public class CameraSettings : MonoBehaviour {
	void FixedUpdate () {
		if(GameStateManager.isInitialized)
			this.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("FieldOfView");
	}
}
