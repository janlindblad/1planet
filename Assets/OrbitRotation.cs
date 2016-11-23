using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrbitRotation : MonoBehaviour {
	public GameObject orbit;
	public Light lightsrc;
	float speed = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		orbit.transform.RotateAround (Vector3.up, Vector3.up, speed);
		speed *= 0.96f;
		if(speed < 0.0001f) {
			lightsrc.intensity *= 0.96f;
			if(lightsrc.intensity < 0.0001f) {
				SceneManager.LoadScene("Status");
			}
		}
	}
}
