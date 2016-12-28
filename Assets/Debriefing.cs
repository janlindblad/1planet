using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debriefing : MonoBehaviour {
	public GameObject stars;
	public GameObject missionGem;
	public GameObject missionText;
	public GameObject missionExplosion;
	bool exploding;
	float gemsize;
	float r;
	float t;
	// Use this for initialization
	void Start () {
		t = 1;
		exploding = false;
		gemsize = 1;
	}
	
	// Update is called once per frame
	void Update () {
		r += .01f;
		missionGem.transform.rotation = Quaternion.Euler (new Vector3 (r, r, r));
		t *= 0.9998f;
		missionText.transform.localPosition = new Vector3 (0, 0.55f - t, 0);
		if (exploding) {
			gemsize *= 0.9f;
			missionGem.transform.localScale = new Vector3 (gemsize, gemsize, gemsize);
		} else {
			stars.transform.RotateAround (
				new Vector3 (0, 0, 0),
				Vector3.up,
				0.1f);
		}

	}

	public void Explode() {
		//missionGem.SetActive (false);
		//missionGem.transform.localScale = new Vector3(.01f,.01f,.01f);
		exploding = true;
		missionExplosion.SetActive(true);
		//stars.transform.rotation = Quaternion.identity;
	}
}
