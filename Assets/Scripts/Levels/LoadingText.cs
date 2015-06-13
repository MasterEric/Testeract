using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingText : MonoBehaviour {

	public GameObject loading;
	public float delay = 1f;
	public Sprite[] sprites;

	float counter = 0;

	// Update is called once per frame
	void FixedUpdate () {
		counter += Time.deltaTime;
		if(counter >= delay) {
			counter = 0;			
			for( int i = 0; i < sprites.Length; i++ ) {
				if( loading.GetComponent<Image>().sprite.name == sprites[i].name ) {
					if(i == sprites.Length - 1) {
						loading.GetComponent<Image>().sprite = sprites[0];
					} else {
						loading.GetComponent<Image>().sprite = sprites[i+1];
					}
					return;
				}
			}
		}
	}
}
