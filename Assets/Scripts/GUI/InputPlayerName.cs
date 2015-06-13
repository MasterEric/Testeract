using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputPlayerName : MonoBehaviour {
	private InputField field;
	
	// Use this for initialization
	void Start () {
		field = gameObject.GetComponent<InputField>();
		field.text = PlayerPrefs.GetString("PlayerName");
		field.onValueChange.AddListener(OnFieldChanged);
	}
	void OnFieldChanged (string val) {
		PlayerPrefs.SetString("PlayerName", val);
	}
}
