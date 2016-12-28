﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debriefing : MonoBehaviour {
	public GameObject stars;
	public GameObject missionGem;
	public GameObject missionText;
	float r;
	float t;
	// Use this for initialization
	void Start () {
		t = 1;
	}
	
	// Update is called once per frame
	void Update () {
		stars.transform.RotateAround (
			new Vector3 (0, 0, 0),
			Vector3.up,
			0.1f);
		r += .01f;
		missionGem.transform.rotation = Quaternion.Euler (new Vector3 (r, r, r));
		t *= 0.9998f;
		missionText.transform.localPosition = new Vector3 (0, 0.55f - t, 0);
	}
}
