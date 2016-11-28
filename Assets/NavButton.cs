using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavButton : MonoBehaviour {
	private bool pressed;
	public Light lightA;
	public Light lightB;
	void Start () {
		pressed = false;
	}
	
	void Update () {
		if (pressed) {
			if (gameObject.transform.position.y < -1) {
				SceneManager.LoadScene ("Start");
			}
			gameObject.transform.position += new Vector3(0,-0.04f,0);
			lightA.intensity *= 0.95f;
			lightB.intensity *= 0.95f;
		}
	}

	public void OnClick() {
		Debug.Log ("Clicked "+gameObject.name);
		pressed = true;
	}
}
