using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpdater : MonoBehaviour {
	public TextMesh status;

	// Use this for initialization
	void Start () {
		int num_total = GameControl.missiondb.get_ids().Length;
		int num_ongoing = GameControl.control.pad.ongoing_missions.Count;
		int num_completed = GameControl.control.pad.completed_missions.Count;
		status.text = "Level 1:   " + ((int)(100.0f*num_completed/num_total)).ToString() + "%\n\n"
			+ (num_completed * 102+25).ToString() + " XP\n"
			+ (num_completed).ToString() + " Avklarade uppdrag\n"
			+ "3 Vänner\n"
			+ "1 Rösttyrka\n";
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
