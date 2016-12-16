using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {
	[Serializable] public class PersistGameData {
		public Boolean welcomed = false;
		public MissionID[] completed_missions = {};
		public MissionID[] ongoing_missions = {};
	}

	public static GameControl control;
	public static MissionDB missiondb;
	public PersistGameData pad;
	String persistFileName;
	BinaryFormatter bf = new BinaryFormatter ();

	void Awake () {
		persistFileName = Application.persistentDataPath + "/gamestate.dat";
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
		missiondb = new MissionDB ();
	}
	
	void Update () {	
	}

	void OnEnable() {
		pad = Load ();
	}
	void OnDisable() {
		Save (pad);
	}

	public void Save(PersistGameData gd) {
		FileStream fs = File.Create (persistFileName);
		bf.Serialize (fs, gd);
		fs.Close ();
	}
	public PersistGameData Load() {
		if(File.Exists(persistFileName)) {
			FileStream fs = File.Open (persistFileName, FileMode.Open);
			PersistGameData gd = (PersistGameData)bf.Deserialize (fs);
			fs.Close ();
			Debug.Log("Loaded state data");
			return gd;
		} else {
			Debug.Log("Created new state data");
			return new PersistGameData();
		}
	}
}

public class MissionDB {
	MissionID[] get_ids() {
		return new MissionID[] { 
			new MissionID("1.1"), 
			new MissionID("1.2"),
			new MissionID("1.3"),
			new MissionID("1.4"),
			new MissionID("1.5"),
			new MissionID("1.6"),
			new MissionID("1.7"),
			new MissionID("1.8"),
			new MissionID("1.9"),
			new MissionID("1.10"),
			new MissionID("1.11"),
			new MissionID("1.12"),
		};
	}
	//MissionID mid, string title, int size, string balltype, 
	//int level, string short_desc, string long_desc,
	//MissionID upgrade = null, MissionID downgrade = null
	Mission get(MissionID mid) {
		if (mid.id == "1:1") {
			return new Mission (
				new MissionID("1:1"), 
				"Din köttfria dag",
				2, "earth", 1,
				"Under 24 timmar ska du undvika allt kött.",
				"Ingen skinka till frukost, ingen kycklingfilé till lunch, ingen köttfärs till middag.\n\nFisk, ägg och blodpudding är ok under detta uppdrag. Dessa räknas inte som kött.",
				new MissionID("1:2"),
				null
			);
		} else if (mid.id == "1:2") {
			return new Mission (
				new MissionID("1:2"), 
				"Din köttfria vecka",
				3, "earth", 1,
				"Under en vecka ska du undvika allt kött.",
				"Ingen skinka till frukost, ingen kycklingfilé till lunch, ingen köttfärs till middag.\n\nFisk, ägg och blodpudding är ok under detta uppdrag. Dessa räknas inte som kött.",
				null,
				null
			);
		} else if (mid.id == "1:3") {
			return new Mission (new MissionID("1:3"), "Din ultimata vecka 3", 3, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:4") {
			return new Mission (new MissionID("1:4"), "Din ultimata vecka 4", 5, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:5") {
			return new Mission (new MissionID("1:5"), "Din ultimata vecka 5", 8, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:6") {
			return new Mission (new MissionID("1:6"), "Din ultimata vecka 6", 4, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:7") {
			return new Mission (new MissionID("1:7"), "Din ultimata vecka 7", 2, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:8") {
			return new Mission (new MissionID("1:8"), "Din ultimata vecka 8", 5, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:9") {
			return new Mission (new MissionID("1:9"), "Din ultimata vecka 9", 1, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:10") {
			return new Mission (new MissionID("1:10"), "Din ultimata vecka 10", 0, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:11") {
			return new Mission (new MissionID("1:11"), "Din ultimata vecka 11", 1, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:12") {
			return new Mission (new MissionID("1:12"), "Din ultimata vecka 12", 9, "red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else {
			return null;
		}
	}
}
