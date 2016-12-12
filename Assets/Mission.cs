using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

	public string title;
	public int size;
	public string balltype;
	public int level;
	public string short_desc;
	public string long_desc;
	GameObject sphere;

	void Start () {
		sphere = this.transform.FindChild("Sphere").gameObject;
		float bsiz = 0.5f + size / 10;
		sphere.transform.localScale = new Vector3 (bsiz, bsiz, bsiz);
		MeshRenderer mr = sphere.GetComponent<MeshRenderer>();
		Debug.Log (mr);
		Material mat = Resources.Load("Materials/proto_map") as Material;
		Debug.Log (mat);
		mr.material = mat;
	}
	
	void Update () {		
	}
}
