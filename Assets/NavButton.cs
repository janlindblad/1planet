using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavButton : MonoBehaviour {
	private bool pressed;
	public string nextScene = "";
	// Animation styles: fall, spin, flip
	public string animationStyle = "fall";
	public int animationFrames = 20;
	public Light lightA;
	public Light lightB;
	public Camera cameraA;
	public float lightAttenuation = 0.95f;
	public float initialSpeed = 0.02f;
	public float acceleration = 1.2f;
	float speed;
	float pos;

	void Start () {
		pressed = false;
		speed = initialSpeed;
	}
	
	void Update () {
		if(animationFrames <= 0 && nextScene != "")
			SceneManager.LoadScene (nextScene);
		if (pressed && animationFrames > 0) {
			--animationFrames;

			if (animationStyle == "fall") {
				gameObject.transform.position += new Vector3 (0, -speed, 0);
				speed *= acceleration;

			} else if (animationStyle == "spin") {
				pos += speed;
				gameObject.transform.rotation = 
					Quaternion.Euler (new Vector3 (0, 0, pos));
				speed *= acceleration;

			} else if (animationStyle == "flip") {
				pos += speed;
				gameObject.transform.rotation = 
					Quaternion.Euler (new Vector3 (0, pos, 0));
				speed *= acceleration;
			} else if (animationStyle == "shake") {
				cameraA.transform.rotation = 
					Quaternion.Euler (new Vector3 (0, 0, Random.Range(-speed,speed)));
				speed *= acceleration;
			}

			// Lights out if specified
			if(lightA != null)
				lightA.intensity *= lightAttenuation;
			if(lightB != null)
				lightB.intensity *= lightAttenuation;
		}
	}

	public void OnClick() {
		Debug.Log ("Clicked "+gameObject.name);
		pressed = true;
	}
}
