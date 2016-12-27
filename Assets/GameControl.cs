using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {
	[Serializable] public class PersistGameData {
		public Boolean welcomed = false;
		public List<MissionID> completed_missions;
		public List<OngoingMission> ongoing_missions;

		public PersistGameData() {
			completed_missions = new List<MissionID>();
			ongoing_missions = new List<OngoingMission>();
		}
	}

	public static GameControl control;
	public static MissionDB missiondb;
	public PersistGameData pad;

	public MissionID selected_mission;

	String persistFileName;
	BinaryFormatter bf = new BinaryFormatter ();

	void Awake () {
		Debug.Log ("GameControl awake()");
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
		// For testing
		/*
		commence_mission(new MissionID("1:2"));
		pad.ongoing_missions.Add(new OngoingMission(
			new MissionID("1:3"),
			DateTime.Now.AddDays(-1.2), 1));
		pad.ongoing_missions.Add(new OngoingMission(
			new MissionID("1:4"),
			DateTime.Now.AddDays(-2.2), 1));
		*/
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

	public void commence_mission(MissionID mid) {
		Mission m = missiondb.get_mission (mid);
		if (m == null) {
			Debug.LogError ("commence_mission mission not found!");
			return;
		}
		pad.ongoing_missions.Add (
			new OngoingMission (mid, m.days));
	}
	public List<MissionID> get_ongoing_missions() {
		return OngoingMission.get_missions (pad.ongoing_missions);
	}
	public OngoingMission get_ongoing_mission(MissionID mid) {
		return pad.ongoing_missions.Find (MissionID.ByMID (mid));
	}
}

public class MissionDB {
	public MissionDB() {
		Debug.Log ("MissionDB()");
	}

	public MissionID[] get_ids() {
		return new MissionID[] { 
			new MissionID("1:1"), 
			new MissionID("1:2"),
			new MissionID("1:3"),
			new MissionID("1:4"),
			new MissionID("1:5"),
			new MissionID("1:6"),
			new MissionID("1:7"),
			new MissionID("1:8"),
			new MissionID("1:9"),
			new MissionID("1:10"),
			//new MissionID("1:11"),
			//new MissionID("1:12"),
		};
	}
	//MissionID mid, string title, int size, string balltype, 
	//int level, string short_desc, string long_desc,
	//MissionID upgrade = null, MissionID downgrade = null
	public Mission get_mission(MissionID mid) {
		if (mid.id == "1:1") {
			return new Mission (
				new MissionID("1:1"), 
				"Din köttfria dag",
				2, 1, "Earth", 1,
				"Under 24 timmar ska du undvika allt kött.",
				"Ingen skinka till frukost, ingen kycklingfilé till lunch, ingen köttfärs till middag.\n\nFisk, ägg och blodpudding är ok under detta uppdrag. Dessa räknas inte som kött.",
				new MissionID("1:2"),
				null
			);
		} else if (mid.id == "1:2") {
			return new Mission (
				new MissionID("1:2"), 
				"Din köttfria vecka",
				3, 7, "Water", 1,
				"Under en vecka ska du undvika allt kött.",
				"Ingen skinka till frukost, ingen kycklingfilé till lunch, ingen köttfärs till middag.\n\nFisk, ägg och blodpudding är ok under detta uppdrag. Dessa räknas inte som kött.",
				null,
				null
			);
		} else if (mid.id == "1:3") {
			return new Mission (new MissionID("1:3"), "Din ultimata vecka 3", 3, 7, "Metal", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:4") {
			return new Mission (new MissionID("1:4"), "Din ultimata vecka 4", 5, 7, "Alpha", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:5") {
			return new Mission (new MissionID("1:5"), "Din ultimata vecka 5", 8, 7,"Checkerboard", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:6") {
			return new Mission (new MissionID("1:6"), "Din ultimata vecka 6", 4, 7, "Camouflage", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:7") {
			return new Mission (new MissionID("1:7"), "Din ultimata 2 dagar", 2, 2, "Floor", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:8") {
			return new Mission (new MissionID("1:8"), "Din ultimata 2 dagar", 5, 2, "Green", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:9") {
			return new Mission (new MissionID("1:9"), "Din ultimata månad", 1, 30, "Rainbow", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:10") {
			return new Mission (new MissionID("1:10"), "Din ultimata månad", 0, 30, "Bricks", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:11") {
			return new Mission (new MissionID("1:11"), "Din ultimata vecka 11", 1, 7, "Cloud", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else if (mid.id == "1:12") {
			return new Mission (new MissionID("1:12"), "Din ultimata vecka 12", 9, 7, "Red", 1, "Red red red.", "Long red silver fox.", null, null	);
		} else {
			Debug.Log ("MissionDB.get_mission(" + mid.id + ") not found");
			return null;
		}
	}
}
