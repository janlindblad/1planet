using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	Text text;
	int ctr = 0;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponentsInChildren<Text>()[0];
	}
	
	// Update is called once per frame
	void Update () {
		++ctr;
		text.text = "Button " + ctr.ToString ();
	}

	void OnClick() {
		Debug.Log ("Button clicked A");
		ctr = 0;
	}


	public void ButtonClicked() {
		Debug.Log ("Button clicked B");
		ctr = -1000;
	}
}
