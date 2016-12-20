using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionButton : MonoBehaviour {
	GameObject button;
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
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
