using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Briefing : MonoBehaviour {
	Text mission_desc;

	// Use this for initialization
	void Start () {
		Mission mission = GameControl.missiondb.get_mission (GameControl.control.selected_mission);
		mission_desc = gameObject.GetComponentInChildren<Text> ();
		mission_desc.text = "Uppdrag> " + mission.title
		+ "\n\n"
		+ mission.short_desc
		+ "\n\n"
		+ mission.long_desc;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
