using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavButton : MonoBehaviour {
	private bool pressed;
	public Light lightA;
	public Light lightB;
	float speed;

	void Start () {
		pressed = false;
		speed = 0.02f;
	}
	
	void Update () {
		if (pressed) {
			if (gameObject.transform.position.y < -1) {
				SceneManager.LoadScene ("Mission");
			}
			gameObject.transform.position += new Vector3(0,-speed,0);
			speed *= 1.2f;
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
