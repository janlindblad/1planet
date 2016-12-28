using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Briefing : MonoBehaviour {
	Mission mission;
	Text mission_desc;

	// Use this for initialization
	void Start () {
		mission_desc = gameObject.GetComponentInChildren<Text> ();
		mission = GameControl.missiondb.get_mission (GameControl.control.selected_mission);
		if(mission == null) {
			Debug.LogError ("mission_completed: No mission");
			mission_desc.text = "Uppdrag> No title";
			return;
		}
		mission_desc.text = "Uppdrag> " + mission.title
		+ "\n\n"
		+ mission.short_desc
		+ "\n\n"
		+ mission.long_desc;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void mission_accepted() {
		if (mission == null) {
			Debug.LogError ("mission_accepted: No mission");
			return;
		}
		GameControl.control.commence_mission(mission.id);
	}
	public void mission_rejected() {
	}
	public void mission_completed() {
		if (mission == null) {
			Debug.LogError ("mission_completed: No mission");
			return;
		}
		GameControl.control.completed_mission (mission.id);
	}
	public void mission_failed() {
		if (mission == null) {
			Debug.LogError ("mission_failed: No mission");
			return;
		}
		GameControl.control.failed_mission (mission.id);
	}
}
