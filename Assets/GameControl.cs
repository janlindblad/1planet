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

	static public void ResetAllGameData() {
		control.pad = new PersistGameData ();
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
		Debug.Log ("CommenceMission: completed=" + pad.completed_missions.Count+" ongoing="+pad.ongoing_missions.Count);
		pad.ongoing_missions.Add (
			new OngoingMission (mid, m.days));
		Debug.Log ("CommenceMission: completed=" + pad.completed_missions.Count+" ongoing="+pad.ongoing_missions.Count);
	}
	public void completed_mission(MissionID mid) {
		Mission m = missiondb.get_mission (mid);
		if (m == null) {
			Debug.LogError ("completed_mission mission not found!");
			return;
		}
		Debug.Log ("CompletedMission: completed=" + pad.completed_missions.Count+" ongoing="+pad.ongoing_missions.Count);
		pad.completed_missions.Add (mid);
		pad.ongoing_missions.RemoveAll (MissionID.ByMID (mid));
		Debug.Log ("CompletedMission: completed=" + pad.completed_missions.Count+" ongoing="+pad.ongoing_missions.Count);
	}
	public void failed_mission(MissionID mid) {
		Mission m = missiondb.get_mission (mid);
		if (m == null) {
			Debug.LogError ("failed_mission mission not found!");
			return;
		}
		Debug.Log ("FailedMission: completed=" + pad.completed_missions.Count+" ongoing="+pad.ongoing_missions.Count);
		pad.ongoing_missions.RemoveAll (MissionID.ByMID (mid));
		Debug.Log ("FailedMission: completed=" + pad.completed_missions.Count+" ongoing="+pad.ongoing_missions.Count);
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
			new MissionID("1:11"),
			new MissionID("1:12"),
			new MissionID("1:LEVELUP"),
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
				2, 1, "Rainbow", 1,
				"Under 24 timmar ska du undvika allt kött.",
				"Ingen skinka till frukost, ingen kycklingfilé till lunch, ingen köttfärs till middag. Fisk, ägg och blodpudding är ok under detta uppdrag. Dessa räknas inte som kött.\n\nOmkring 15% av all växthuseffekt i världen kommer av köttproduktion. [https://en.wikipedia.org/wiki/Environmental_impact_of_meat_production]",
				new MissionID("1:2"), null);
		} else if (mid.id == "1:2") {
			return new Mission (
				new MissionID("1:2"), 
				"Din köttfria vecka",
				4, 7, "Rainbow", 1,
				"Under en vecka ska du undvika allt kött.",
				"Ingen skinka till frukost, ingen kycklingfilé till lunch, ingen köttfärs till middag. Fisk, ägg och blodpudding är ok under detta uppdrag. Dessa räknas inte som kött.\n\nOmkring 15% av all växthuseffekt i världen kommer av köttproduktion. [https://en.wikipedia.org/wiki/Environmental_impact_of_meat_production]",
				null, null);
		} else if (mid.id == "1:3") {
			return new Mission (
				new MissionID("1:3"), 
				"Din duschsnåla vecka", 
				1, 7, "Water", 1, 
				"Under en vecka ska du duscha sammanlagt högst 30 minuter.", 
				"Bad i badhus är ok under detta uppdrag.\n\nDet är dyrt att värma vatten, därför är det viktigt att inte duscha länge eller för ofta. Dessutom går nästan hela värmen förlorad genom avloppet inom några sekunder. Ungefär 10% av elräkningen går till värmning av duschvatten. [http://sciencenordic.com/seven-tips-cut-your-co2-emissions]", 
				null, null);
		} else if (mid.id == "1:4") {
			return new Mission (
				new MissionID("1:4"), 
				"Din koksnåla vecka", 
				1, 7, "Water", 1, 
				"Under en vecka ska du inte koka upp något vatten i onödan.", 
				"Mycket av det vatten vi kokar upp i köket kokas upp i onödan. En liter vatten till en kopp te. Potatisen simmar i kastrullen. Mikra tekoppen istället, och lägg i potatisen först häll sedan i vatten så det når hälften av de översta potatisarna.", 
				null, null);
		} else if (mid.id == "1:5") {
			return new Mission (
				new MissionID("1:5"), 
				"Din kastfria vecka", 
				5, 7, "Red", 1, 
				"Under en vecka ska ingen mat kastas.", 
				"I Sverige slängs omkring 30% av all ätbar mat. Det ger stora miljökostnader till ingen nytta. I det här uppdraget ska vi vara 10 ggr bättre, släng max 3% av matvärdet under en vecka. Ät upp alla rester innan något nytt lagas. Planera matlagningen så att öppnade råvaror går åt till nästa rätt. Släng inget utan att smaka. Yoghurt som är 3 veckor efter bäst före datum är ofta god att äta.", 
				null, null);
		} else if (mid.id == "1:6") {
			return new Mission (
				new MissionID("1:6"), 
				"Prova på smartare transport", 
				4, 7, "Camouflage", 1, 
				"Ta dig till en vardagsaktivitet på ett smartare sätt minst en gång under veckan.", 
				"Åk till någon plats du brukar åka till, tex. jobb, skola, fritidsaktivitet eller affären på ett klimatmässigt bättre vis än vanligt. Främst cykel eller till fots, men om du brukar åka bil, med är allmänna kommunikationsmedel ett alternativ.\n\nTransportsektorn står för omkring en fjärdedel av alla utsläpp av växhusgaser. [https://www.epa.gov/ghgemissions/sources-greenhouse-gas-emissions].", 
				null, null);
		} else if (mid.id == "1:7") {
			return new Mission (
				new MissionID("1:7"), 
				"Låt-vattnet-svalna vecka", 
				1, 7, "Water", 1, 
				"Låt varmvattnet svalna innan du häller ut det.", 
				"Varmt vatten innehåller mycket energi. Släpp inte ner det i avloppet varmt, utan låt det svalna (dvs. värma rummet) till rumstemperatur först. Plocka upp potatisen ur kastrullen. Eller häll av vattnet i den smutsiga bunken. Eller sätt i proppen i vasken en stund. Låt badvattnet efter barnen svalna innan du släpper ut det.\n\nEn liter kokande vatten ger 0.2 kWh gratis värme. [http://www.gronatips.se/index.php?id=11]", 
				null, null);
		} else if (mid.id == "1:8") {
			return new Mission (
				new MissionID("1:8"), 
				"Släck och stäng av vecka", 
				1, 2, "Clouds", 1, 
				"Släck efter dig och stäng av apparater du inte använder.", 
				"Det räcker kanske inte så långt för att rädda världen, men det här är i alla fall lätt att göra.", 
				null, null);
		} else if (mid.id == "1:9") {
			return new Mission (
				new MissionID("1:9"), 
				"Träffa en miljöorganisation", 
				5, 90, "Earth", 1, 
				"Gå på ett möte (tex. volontärmöte) med en klimat/miljöorganisation", 
				"Det finns många klimat/miljöorganisationer på olika håll i landet. De brukar hålla öppna volontärmöten eller verkstäder där man kan hjälpa till. Gå på ett möte och kolla läget. Greenpeace, 350.org, PUSH, Jordens vänner, Klimatauktion, Naturskyddsföreningen är bara några av de mer välkända. Gå till klimatverige.se för en mycket längre lista.", 
				null, null	);
		} else if (mid.id == "1:10") {
			return new Mission (
				new MissionID("1:10"), 
				"Skriv en dikt, låt eller tanke", 
				4, 14, "Alpha", 1, 
				"Skriv en dikt, sätt ord till en låt eller skriv ner en klimat-miljötanke på några rader.", 
				"Skicka in det färdiga resultatet till kultur@xxx.se . Om du skickar in något tillåter vi oss att publicera, bearbeta eller använda verken för bästa klimatnytta", 
				null, null	);
		} else if (mid.id == "1:11") {
			return new Mission (
				new MissionID("1:11"), 
				"Räkna ut hur mycket energi du använder", 
				4, 2, "Clouds", 1, 
				"Gå till xxx.se och svara på frågorna för att se hur mycket energi du gör av med.", 
				"Se hur du ligger till.", 
				null, null	);
		} else if (mid.id == "1:12") {
			return new Mission (
				new MissionID("1:12"), 
				"Läs en bok om klimatet", 
				5, 30, "Red", 1, 
				"Läs en av böckerna x, y, z.", 
				".", 
				null, null);
		} else if (mid.id == "1:LEVELUP") {
			return new Mission (
				new MissionID("1:LEVELUP"), 
				"Level 2", 
				12, 1, "GlassDandelion", 1, 
				"Välkommen upp till Level 2", 
				"Level 1 handlade om enkla saker du själv kan göra för klimatet, utan att göra någon livsstilsändring eller påverka någon annans liv. Level 2 kommer att ta detta till familj och hushåll. Då blir det lite svårare.\n\nLycka till!", 
				null, null);
		} else {
			Debug.Log ("MissionDB.get_mission(" + mid.id + ") not found");
			return null;
		}
		// skänk nånting begagnat
		// kom med en idé
		// ta klimatsmart testet för ditt hus
		// recensera appen

		// FIXME: tidigast igen tid på uppdrag
	}
}
// 1 pk
// 2 egna vanor
// 3 hushållets vanor
// 4 fördjupad förståelse
// 5 sprid budskapet
// 6 bli aktivist
// 7 reflektera
// 8 investera i framtiden
// 9 så ett frö
