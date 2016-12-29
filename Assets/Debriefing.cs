using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debriefing : MonoBehaviour {
	public GameObject stars;
	public GameObject missionText;
	public GameObject missionExplosion;
	public GameObject missionEncircling;
	public GameObject content;
	public GameObject cameraA;
	MissionBall missionGem;
	bool ending;
	float gemsize;
	float r;
	float t;

	Mission mission;
	Text mission_desc;

	void Start () {
		mission_desc = missionText.GetComponentInChildren<Text> ();
		mission = GameControl.missiondb.get_mission (GameControl.control.selected_mission);
		if (mission == null) {
			Debug.LogError ("debriefing: No mission");
			//return;
			// for testing
			mission = GameControl.missiondb.get_mission (new MissionID ("1:1"));
			Debug.Log ("debriefing: Using mission "+mission.id.id+" for now");
		}

		mission_desc.text = "Uppdrag> " + mission.title
			+ "\n\n"
			+ mission.short_desc
			+ "\n\n"
			+ mission.long_desc;

		t = 1;
		ending = false;
		gemsize = 1;
		content.transform.localPosition = new Vector3 (
			content.transform.localPosition.x, 
			500f,
			content.transform.localPosition.z);
		missionGem = cameraA.AddComponent<MissionBall> ();
		missionGem.init (mission.id, cameraA, new Vector3 (0, 0, 0), true,
			new Vector3 (0, 0.8f, 2.5f), new Vector3 (gemsize, gemsize, gemsize));
	}
	
	// Update is called once per frame
	void Update () {
		r += .01f;
		missionGem.gem.transform.rotation = Quaternion.Euler (new Vector3 (r, r, r));
		t *= 0.9998f;
		//missionText.transform.localPosition = new Vector3 (0, 0.55f - t, 0);
		if (ending) {
			gemsize *= 0.9f;
			missionGem.gem.transform.localScale = new Vector3 (gemsize, gemsize, gemsize);
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

	public void mission_completed() {
		Encircling ();
		if (mission == null) {
			Debug.LogError ("mission_completed: No mission");
			return;
		}
		GameControl.control.completed_mission (mission.id);
	}
	public void mission_failed() {
		Explode ();
		if (mission == null) {
			Debug.LogError ("mission_failed: No mission");
			return;
		}
		GameControl.control.failed_mission (mission.id);
	}
}
