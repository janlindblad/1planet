using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {
	[Serializable] public class PersistGameData {
		public Boolean welcomed = false;
	}

	public static GameControl control;
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
