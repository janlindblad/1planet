using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MissionButton : MonoBehaviour {
	GameObject button;
	MissionBall ball;
	Text mission_title;
	Slider mission_timer;
	Text mission_timer_text;
	OngoingMission omi;
	Mission mission;

	public void init(MissionID mid, GameObject parent, Vector3 position) {
		omi = GameControl.control.get_ongoing_mission (mid);
		if (omi == null) {
			Debug.LogError ("MissionButton: mid " + mid.id + " not ongoing");
			return;
		}
		mission = GameControl.missiondb.get_mission (mid);
		UnityEngine.Object mission_prefab;
		switch (omi.get_timing ()) {
		case OngoingMission.Timing.Ongoing:
			mission_prefab = Resources.Load ("Prefabs/MissionButtons/OngoingMission");
			break;
		case OngoingMission.Timing.Ended:
			mission_prefab = Resources.Load ("Prefabs/MissionButtons/EndedMission");
			break;
		default:
		case OngoingMission.Timing.Overdue:
			mission_prefab = Resources.Load ("Prefabs/MissionButtons/OverdueMission");
			break;
		}
		button = Instantiate(
			mission_prefab,
			new Vector3 (0, 0, 0), 
			Quaternion.identity,
			parent.transform) as GameObject;
		button.name = mission.id.id;
		button.transform.localScale = new Vector3 (1, 1, 1);
		button.transform.localPosition = position;
		Debug.Log ("MissionButton: Instantiantion successful");
		Button uibutton = button.GetComponent<Button>();
		uibutton.onClick.AddListener(() => {
			button_clicked();
		});


		// Setup sphere
		ball = button.AddComponent<MissionBall> ();
		ball.init(mid, button, new Vector3(0, 0, 0), true);
		//ball.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);

		// Setup text
		mission_title = button.GetComponentInChildren<Text> ();
		mission_title.text = mission.title;

		// Setup timer
		mission_timer = button.GetComponentInChildren<Slider> ();
		if (omi.get_timing () != OngoingMission.Timing.Overdue) {
			TimeSpan remain = omi.get_remaining_time ();
			mission_timer.maxValue = omi.days * 24 * 60;
			mission_timer.minValue = 0;
			mission_timer.value = (int)remain.TotalMinutes;

			mission_timer_text = mission_timer.GetComponentInChildren<Text> ();
			mission_timer_text.text = mission_timer_text.text = omi.get_remaining_time_description();
		} else {
			mission_timer.enabled = false;
		}
	}

	void button_clicked() {
		Debug.Log ("MissionButton " + omi.id.id + " clicked");
		GameControl.control.selected_mission = omi.id;
		SceneManager.LoadScene ("Debriefing");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
