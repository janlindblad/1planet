using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionBall : MonoBehaviour {
	GameObject sphere;
	Mission mission;

	public void init(MissionID mid, GameObject parent, Vector3 position) {
		mission = GameControl.missiondb.get_mission (mid);
		if(mission == null)
			Debug.Log ("MissionBall: mid " + mid.id + " not found");
		Debug.Log ("MissionBall: mid " + mid.id + " size:" + mission.size.ToString()+
			" prefab:"+"Prefabs/MissionBalls/"+mission.balltype);
		//sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//sphere.transform.position = position;
		//sphere.transform.rotation = Quaternion.identity;
		sphere = Instantiate(
			Resources.Load("Prefabs/MissionBalls/"+mission.balltype), 
			position, 
			Quaternion.identity,
			parent.transform) as GameObject;
		float bsiz = 0.5f + mission.size / 5.0f;
		sphere.transform.localScale = new Vector3 (bsiz, bsiz, bsiz);
		Debug.Log ("Instantiantion successful");
	}

	void Start () {
		Debug.Log ("In start()");
		if (SceneManager.GetActiveScene ().name == "Briefing") {
			Debug.Log ("In briefing scene");
		} else {
			Debug.Log ("In scene "+SceneManager.GetActiveScene().name);
		}

		//sphere = this.transform.FindChild("Sphere").gameObject;
		////float bsiz = 0.5f + mission.size / 10;
		//float bsiz = 1.5f;
		//sphere.transform.localScale = new Vector3 (bsiz, bsiz, bsiz);
		//MeshRenderer mr = sphere.GetComponent<MeshRenderer>();
		//Debug.Log (mr);
		//Material mat = Resources.Load("Materials/proto_map") as Material;
		//Debug.Log (mat);
		//mr.material = mat;
	}

	void Update () {		
	}
}
