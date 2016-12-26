using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionBall : MonoBehaviour {
	GameObject sphere;
	Mission mission;

	public void init(MissionID mid, GameObject parent, Vector3 position, bool freeze = false) {
		mission = GameControl.missiondb.get_mission (mid);
		if(mission == null)
			Debug.Log ("MissionBall: mid " + mid.id + " not found");
		Debug.Log ("MissionBall: mid " + mid.id + " size:" + mission.size.ToString()+
			" prefab:"+"Prefabs/MissionBalls/"+mission.balltype);
		//sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere = Instantiate(
			Resources.Load("Prefabs/MissionBalls/"+mission.balltype), 
			position, 
			Quaternion.identity,
			parent.transform) as GameObject;
		sphere.name = mission.id.id;
		float bsiz = 0.5f + mission.size / 4.0f;
		sphere.transform.localScale = new Vector3 (bsiz, bsiz, bsiz);
		//MeshRenderer mr = sphere.GetComponent<MeshRenderer>();
		//Debug.Log (mr);
		//Material mat = Resources.Load("Materials/proto_map") as Material;
		//Debug.Log (mat);
		//mr.material = mat;
		Debug.Log ("Instantiantion successful");
		if (freeze) {
			sphere.GetComponent<Rigidbody>().constraints = 
				RigidbodyConstraints.FreezeRotationX | 
				RigidbodyConstraints.FreezeRotationY |
				RigidbodyConstraints.FreezePositionX | 
				RigidbodyConstraints.FreezePositionY |
				RigidbodyConstraints.FreezePositionZ;
			sphere.transform.localScale = new Vector3 (40f, 40f, 0.4f);
			sphere.transform.localPosition = new Vector3 (-140f, 0f, -0.1f);
			Collider collider = gameObject.transform.GetComponentInChildren<Collider> ();
			collider.enabled = false;
		}
	}

	void Start () {
		//Debug.Log ("In start()");
		if (SceneManager.GetActiveScene ().name == "Briefing") {
			//Debug.Log ("In briefing scene");
		} else {
			//Debug.Log ("In scene "+SceneManager.GetActiveScene().name);
		}

	}

	void Update () {		
	}
}
