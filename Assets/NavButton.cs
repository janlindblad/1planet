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

		/*
		for (var touch : Touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				// Construct a ray from the current touch coordinates
				var ray = Camera.main.ScreenPointToRay (touch.position);
				if (Physics.Raycast (ray)) {
					// Create a particle if hit
					Instantiate (particle, transform.position, transform.rotation);
				}
			}
		}
		*/

	}

	public void OnClick() {
		Debug.Log ("Clicked "+gameObject.name);
		pressed = true;
	}
}
