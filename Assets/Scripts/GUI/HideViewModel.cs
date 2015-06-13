using UnityEngine;
using System.Collections;

public class HideViewModel : MonoBehaviour {
	void FixedUpdate () {
		this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = ToBool(PlayerPrefs.GetInt("ViewModelsEnabled"));
	}
	bool ToBool(int i){
		if(i == 1)
			return true;
		else
			return false;
	}
}
