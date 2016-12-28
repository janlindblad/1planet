using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debriefing : MonoBehaviour {
	public GameObject stars;
	public GameObject missionGem;
	public GameObject missionText;
	public GameObject missionExplosion;
	public GameObject missionEncircling;
	public GameObject content;
	bool ending;
	float gemsize;
	float r;
	float t;
	// Use this for initialization
	void Start () {
		t = 1;
		ending = false;
		gemsize = 1;
		content.transform.localPosition = new Vector3 (
			content.transform.localPosition.x, 
			500f,
			content.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		r += .01f;
		missionGem.transform.rotation = Quaternion.Euler (new Vector3 (r, r, r));
		t *= 0.9998f;
		missionText.transform.localPosition = new Vector3 (0, 0.55f - t, 0);
		if (ending) {
			gemsize *= 0.9f;
			missionGem.transform.localScale = new Vector3 (gemsize, gemsize, gemsize);
		} else {
			stars.transform.RotateAround (
				new Vector3 (0, 0, 0),
				Vector3.up,
				0.1f);
		}
		if (content.transform.localPosition.y < 1450) {
			content.transform.localPosition = new Vector3 (
				content.transform.localPosition.x, 
				content.transform.localPosition.y + 3f,
				content.transform.localPosition.z);
		}
	}

	public void Explode() {
		ending = true;
		missionExplosion.SetActive(true);
	}
	public void Encircling() {
		ending = true;
		missionEncircling.SetActive(true);
	}
}
