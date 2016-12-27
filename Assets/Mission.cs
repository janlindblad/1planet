using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class MissionID : IEquatable<MissionID> {
	public string id;

	static public Predicate<OngoingMission> ByMID(MissionID mid) {
		return delegate(OngoingMission omid) {
			return (mid.id == omid.id.id);
		};
	}

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

[Serializable] public class OngoingMission {
	public enum Timing { Ongoing, Ended, Overdue };
	public MissionID id;
	public DateTime start;
	public int days;

	public static List<MissionID> get_missions(List<OngoingMission> oms) {
		List<MissionID> mids = new List<MissionID>();
		foreach (var mid in oms) {
			mids.Add (mid.id);
		}
		return mids;
	}
	public OngoingMission(MissionID mid, int days) {
		init(mid, DateTime.Now, days);
	}
	public OngoingMission(MissionID mid, DateTime start, int days) {
		init(mid, start, days);
	}
	void init(MissionID mid, DateTime start, int days) {
		this.id = mid;
		this.start = start;
		this.days = days;
		//this.end = start.AddDays(days);
		//DateTime.Now.ToString("hh:mm:ss"); 
		//TimeSpan span = endTime.Subtract ( startTime );
		//Console.WriteLine( "Time Difference (seconds): " + span.Seconds );			
	}
	public Timing get_timing() {
		DateTime now = DateTime.Now;
		if (now < start.AddDays (days)) {
			return Timing.Ongoing;
		}
		if (now < start.AddDays (2 * days)) {
			return Timing.Ended;
		}
		return Timing.Overdue;
	}
}

public class Mission { //: MonoBehaviour {
	public MissionID id;
	public MissionID upgrade;
	public MissionID downgrade;
	public string title;
	public int size;
	public int days;
	public string balltype;
	public int level;
	public string short_desc;
	public string long_desc;

	public Mission(MissionID mid, string title, int size, int days, string balltype, 
		int level, string short_desc, string long_desc,
		MissionID upgrade = null, MissionID downgrade = null) {
		this.id = mid;
		this.title = title;
		this.size = size;
		this.days = days;
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
