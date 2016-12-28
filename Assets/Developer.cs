using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Developer : MonoBehaviour {
	public Text status;

	// Use this for initialization
	void Start () {
		UpdateStatus ();
	}

	void UpdateStatus() {
		status.text = "Status:\n";
		status.text += "\nTotal missions: " + GameControl.missiondb.get_ids ().Length.ToString ();
		status.text += "\nOngoing missions: " + GameControl.control.pad.ongoing_missions.Count.ToString ();
		status.text += "\nComplpeted missions: " + GameControl.control.pad.completed_missions.Count.ToString ();
	}
		
	// Update is called once per frame
	void Update () {
	}

	public void ResetAllGameData() {
		GameControl.ResetAllGameData ();
		UpdateStatus ();
	}
}
