using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionID {
	public string id;
	public MissionID(string id) {
		this.id = id;
	}
	public override string ToString() {
		return this.id;
	}
}

public class Mission : MonoBehaviour {
	public MissionID id;
	public MissionID upgrade;
	public MissionID downgrade;
	public string title;
	public int size;
	public string balltype;
	public int level;
	public string short_desc;
	public string long_desc;
	GameObject sphere;

	public Mission(MissionID mid, string title, int size, string balltype, 
		int level, string short_desc, string long_desc,
		MissionID upgrade = null, MissionID downgrade = null) {
		this.id = mid;
		this.title = title;
		this.size = size;
		this.balltype = balltype;
		this.level = level;
		this.short_desc = short_desc;
		this.long_desc = long_desc;
		this.upgrade = upgrade;
		this.downgrade = downgrade;
	}


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
