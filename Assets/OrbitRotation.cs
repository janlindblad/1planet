﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrbitRotation : MonoBehaviour {
	public GameObject orbit;
	public Light lightsrc;
	float speed = 5;
	public bool welcome = true;

	void Start () {
	}
	
	void Update () {
		orbit.transform.RotateAround (Vector3.up, Vector3.up, speed);
		speed *= 0.96f;
		if(speed < 0.0001f) {
			if (GameControl.control.pad.welcomed) {
				SceneManager.LoadScene ("Status");
			} else {
				GameControl.control.pad.welcomed = true;
				SceneManager.LoadScene ("Welcome");
			}
		}
	}
}