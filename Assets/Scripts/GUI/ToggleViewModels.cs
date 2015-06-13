using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleViewModels : MonoBehaviour {
	private Toggle toggle;
	
	// Use this for initialization
	void Start () {
		toggle = gameObject.GetComponent<Toggle>();
		toggle.isOn = ToBool(PlayerPrefs.GetInt("ViewModelsEnabled"));
		Debug.Log(PlayerPrefs.GetInt("ViewModelsEnabled"));
		toggle.onValueChanged.AddListener(OnToggleChanged);
	}
	void OnToggleChanged (bool val) {
		PlayerPrefs.SetInt("ViewModelsEnabled", ToInt(toggle.isOn));
	}
	bool ToBool(int i){
		if(i == 1)
			return true;
		else
			return false;
	}
	int ToInt(bool i){
		Debug.Log(i);
		if(i)
			return 1;
		else
			return 0;
	}
}
