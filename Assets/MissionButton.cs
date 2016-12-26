using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour {
	GameObject button;
	MissionBall ball;
	Text mission_title;
	Slider mission_timer;
	Text mission_timer_text;
	Mission mission;

	public void init(MissionID mid, GameObject parent, Vector3 position) {
		mission = GameControl.missiondb.get_mission (mid);
		if(mission == null)
			Debug.Log ("MissionButton: mid " + mid.id + " not found");
		Debug.Log ("MissionButton: mid " + mid.id + " size:" + mission.size.ToString()+
			" prefab:"+"Prefabs/MissionButton/"+mission.balltype);
		//sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Object ended_mission = Resources.Load("Prefabs/MissionButtons/EndedMission");
		Debug.Log ("ended_mission");
		button = Instantiate(
			ended_mission,
			new Vector3 (0, 0, 0), 
			Quaternion.identity,
			parent.transform) as GameObject;
		button.name = mission.id.id;
		button.transform.localScale = new Vector3 (1, 1, 1);
		button.transform.localPosition = position;
		Debug.Log ("Instantiantion successful");

		// Setup sphere
		ball = button.AddComponent<MissionBall> ();
		ball.init(mid, button, new Vector3(0, 0, 0), true);
		//ball.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);

		// Setup text
		mission_title = button.GetComponentInChildren<Text> ();
		mission_title.text = mission.title;

		// Setup timer
		mission_timer = button.GetComponentInChildren<Slider> ();
		mission_timer.maxValue = 17;
		mission_timer.minValue = 0;
		mission_timer.value = 15;

		mission_timer_text = mission_timer.GetComponentInChildren<Text> ();
		mission_timer_text.text = mission_timer_text.text = "20 min";

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
