using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionID : IEquatable<MissionID> {
	public string id;
	public MissionID(string id) {
		this.id = id;
	}
	public override string ToString() {
		return this.id;
	}
	public override int GetHashCode()
	{
		return id.GetHashCode();
	}
	public override bool Equals(object obj) {
		if (obj == null) return false;
		MissionID objAsMissionID = obj as MissionID;
		if (objAsMissionID == null) return false;
		else return Equals(objAsMissionID);
	}

	public bool Equals(MissionID mid) {
		if (mid == null) return false;
		return this.id == mid.id;
	}
		
}

public class OngoingMission {
	public MissionID id;
	public DateTime start;
	public DateTime end;

	public OngoingMission(Mission mission) {
		init(mission.id);
	}
	public OngoingMission(MissionID mid) {
		init(mid);
	}
	void init(MissionID mid) {
		this.id = mid;
		this.start = DateTime.Now;
		this.end = start.AddHours (24);
		//DateTime.Now.ToString("hh:mm:ss"); 
		//TimeSpan span = endTime.Subtract ( startTime );
		//Console.WriteLine( "Time Difference (seconds): " + span.Seconds );			
	}
}

public class Mission { //: MonoBehaviour {
	public MissionID id;
	public MissionID upgrade;
	public MissionID downgrade;
	public string title;
	public int size;
	public string balltype;
	public int level;
	public string short_desc;
	public string long_desc;

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

	void Awake() {
		//DontDestroyOnLoad (this);
	}

	void Start () {
		Debug.Log ("In Mission.start()");
	}
	
	void Update () {		
	}
}
