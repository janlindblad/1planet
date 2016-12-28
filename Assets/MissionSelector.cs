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

	float button_offset = 65.0f;

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
		//Debug.Log ("===== fillMissionPipeline: Balls =====");
		float pipe_y = 2.0f;
		foreach(var mid in GameControl.missiondb.get_ids()) {
			if (OngoingMission.get_missions(GameControl.control.pad.ongoing_missions).Contains (mid) ||
				GameControl.control.pad.completed_missions.Contains (mid)) {
				//Debug.Log ("Skipping ongoing/completed mission " + mid.id);
				continue;
			} else {
				//Debug.Log ("Instantiating ball " + mid.id);
				MissionBall mb = gameObject.AddComponent<MissionBall> ();
				mb.init (mid, pipe, new Vector3 ((Random.value - 0.5f) * 2.0f, pipe_y, 0));
				pipe_y += 2.0f;
			}
		}
		//Debug.Log ("===== fillMissionPipeline: Buttons =====");
		float panel_y = 0.0f;
		foreach (var mid in OngoingMission.get_missions(GameControl.control.pad.ongoing_missions)) {
			//Debug.Log ("Instantiating button " + mid.id + " at y=" + (panel_y).ToString ());
			panel_y += button_offset;
			RectTransform containerRectTransform = panel.GetComponent<RectTransform> ();
			containerRectTransform.offsetMin = new Vector2 (containerRectTransform.offsetMin.x, -button_offset);
			containerRectTransform.offsetMax = new Vector2 (containerRectTransform.offsetMax.x, -panel_y-button_offset/2);
			MissionButton mb = gameObject.AddComponent<MissionButton> ();
			mb.init (mid, panel, new Vector3 (0, panel_y - button_offset, 0));
		}
		Debug.Log ("===== fillMissionPipeline: done =====");
	}
}
