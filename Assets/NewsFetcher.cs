using UnityEngine;
using System.Collections;

public class NewsFetcher : MonoBehaviour {
	string url = "www.worldtimeserver.com/time-zones/cst";
	public TextMesh status;
	WWW www;

	// Use this for initialization
	void Start () {
		www = new WWW(url);
	}
	
	// Update is called once per frame
	void Update () {
		if (www != null && www.isDone) {
			Debug.Log (www.text);
			//int start_ix = www.text.IndexOf ("<div class=\"vk_bk vk_ans\">");
			Debug.Log("Got " + www.size.ToString() + " bytes");
			string tag = "Server Time with seconds:";
			int start_ix = www.text.IndexOf (tag, 0);
			if (start_ix > 0) {
				start_ix += tag.Length;
				int end_ix = www.text.IndexOf ("-->", start_ix);
				if (end_ix > 0) {
					status.text = www.text.Substring (start_ix, end_ix - start_ix);
				} else
					Debug.Log ("End not found");
			} else
				Debug.Log ("Start not found, "+ start_ix.ToString());
			www = null;
		}
	}
}
