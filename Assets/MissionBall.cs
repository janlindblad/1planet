using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionBall : MonoBehaviour {
	public GameObject gem;
	Mission mission;

	public void init(
		MissionID mid, 
		GameObject parent, 
		Vector3 position, 
		bool freeze = false,
		Vector3? localPosition = null, 
		Vector3? localScale = null) {

		mission = GameControl.missiondb.get_mission (mid);
		if (mission == null) {
			Debug.LogError ("MissionBall: mid " + mid.id + " not found");
			return;
		}
		//Debug.Log ("MissionBall: mid " + mid.id + " size:" + mission.size.ToString()+
		//	" prefab:"+"Prefabs/MissionBalls/"+mission.balltype);
		//sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		gem = Instantiate(
			Resources.Load("Prefabs/MissionBalls/"+mission.balltype), 
			position, 
			Quaternion.identity,
			parent.transform) as GameObject;
		gem.name = mission.id.id;
		if (localScale != null) {
			gem.transform.localScale = (Vector3)localScale;
		} else {
			float bsiz = 0.5f + mission.size / 4.0f;
			gem.transform.localScale = new Vector3 (bsiz, bsiz, bsiz);
		}
		if (localPosition != null) {
			gem.transform.localPosition = (Vector3)localPosition;
		}
		//MeshRenderer mr = sphere.GetComponent<MeshRenderer>();
		//Debug.Log (mr);
		//Material mat = Resources.Load("Materials/proto_map") as Material;
		//Debug.Log (mat);
		//mr.material = mat;
		//Debug.Log ("MissionBall: Instantiantion successful");
		if (freeze) {
			gem.GetComponent<Rigidbody>().constraints = 
				RigidbodyConstraints.FreezeRotationX | 
				RigidbodyConstraints.FreezeRotationY |
				RigidbodyConstraints.FreezePositionX | 
				RigidbodyConstraints.FreezePositionY |
				RigidbodyConstraints.FreezePositionZ;
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
