using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MissionSelector : MonoBehaviour {
	public Light spot;
	Camera cam;
	Vector3 targetPoint;
	Transform ball = null;
	public GameObject pipe;
	public GameObject panel;

	float rotation_speed = 1f;
	float movement_speed = 1f;

	void Start () {
		cam = GetComponent<Camera> ();
		fillMissionPipeline ();
	}
	
	void Update () {
		if (ball == null) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) {
				targetPoint = hit.point;
				if (hit.transform.tag == "MissionBall") {
					ball = hit.transform;
					Debug.Log ("Hit " + ball.name);
					GameControl.control.selected_mission = new MissionID(ball.name);
				}
			}
		} else {
			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotation_speed * Time.deltaTime);

			movement_speed = Mathf.Clamp (movement_speed * 1.1f, 1, 12);
			float step = movement_speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, step);
			if(Vector3.Distance(transform.position, targetPoint) < 0.5f) {
				SceneManager.LoadScene ("Briefing");
			}
		}
	}
	public void MissionClicked() {
		ball = gameObject.transform;
		Debug.Log ("Mission button clicked " + ball.name);
	}

	void fillMissionPipeline() {
		Debug.Log ("fillMissionPipeline");
		float pipe_y = 2.0f;
		float panel_y = 0.0f;
		foreach(var mid in GameControl.missiondb.get_ids()) {
			Debug.Log (GameControl.control.pad.ongoing_missions.Contains (mid));
			if (GameControl.control.pad.completed_missions.Contains (mid)) {
				Debug.Log ("Skipping " + mid.id);
				continue;
			} else if (GameControl.control.pad.ongoing_missions.Contains (mid)) {
				Debug.Log ("Instantiating button " + mid.id);
				MissionButton mb = gameObject.AddComponent<MissionButton> ();
				mb.init (mid, panel, new Vector3 (0, -panel_y, 0));
				panel_y += 0.65f;
			} else {
				Debug.Log ("Instantiating ball " + mid.id);
				MissionBall mb = gameObject.AddComponent<MissionBall> ();
				mb.init (mid, pipe, new Vector3 ((Random.value - 0.5f) * 2.0f, pipe_y, 0));
				pipe_y += 2.0f;
			}
		}
	}
}
