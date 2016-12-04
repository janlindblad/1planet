using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MissionSelector : MonoBehaviour {
	public Light spot;
	Camera cam;
	Vector3 targetPoint;
	Transform ball = null;

	float rotation_speed = 1f;
	float movement_speed = 1f;

	void Start () {
		cam = GetComponent<Camera> ();
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
				SceneManager.LoadScene ("Status");
			}
		}
	}
}
