//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
 
// This script should be attached to a Canvas object in your scene.

public class DemoScene : MonoBehaviour
{  
	public TextAsset cityFile;
	
	private int controlFontSize = 12;
	
	private Sun mSun;
	private Earth mEarth;
	private List<CityMarker> mCities;
	private float mMaxCityRank;
   
   // Splits a line from a .csv file
	// From: http://answers.unity3d.com/questions/144200/are-there-any-csv-reader-for-unity3d-without-needi.html
	// Used to parse populated places
	private string[] SplitCsvLine(string line)
	{
		string pattern = @"
		# Match one value in valid CSV string.
		(?!\s*$) 														# Don't match empty last value.
		\s* 																# Strip whitespace before value.
		(?: 																# Group for value alternatives.
		'(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)' 					# Either $1: Single quoted string,
		| ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)"" 			# or $2: Double quoted string,
		| (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*) 			# or $3: Non-comma, non-quote stuff.
		) 																	# End group of value alternatives.
		\s* 																# Strip whitespace after value.
		(?:,|$) 															# Field ends on comma or EOS.
		";
		 
		string[] values = (from Match m in Regex.Matches(line, pattern,
		RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
		select m.Groups[1].Value).ToArray();
		 
		return values;
	}
		
	void Start()
	{
		mMaxCityRank = 1;
		mSun = new Sun();
		LoadCities();
		LoadEarth("Earth-1");  
		EnableCities();
			
		// Get the Canvas the script is attached to
		UIElement canvas = new UIElement(gameObject);
		canvas.AddVerticalLayoutGroup(expandWidth: false, expandHeight: true);

		// Create a main panel which will include all UI elements
      UIElement mainPanel = new UIPanel(canvas, "Main Panel");
		mainPanel.AddVerticalLayoutGroup(expandWidth: true, padding: new RectOffset(0,0,5,0));  // left, right, top, bottom
		mainPanel.AddLayoutElement(preferredWidth: 210);
                   
		// Create a header panel at the top with the title text
      UIElement titlePanel = new UIEmptyPanel(mainPanel, "Title Panel");
		titlePanel.AddVerticalLayoutGroup(expandWidth: true, padding: new RectOffset(5,5,0,0));  // left, right, top, bottom
		titlePanel.AddLayoutElement(minHeight: 40);

		new UIText(titlePanel, "UltimaEarth 2.0", fontSize: 18);		
		new UIText(titlePanel, "for uGUI (4.6+)", fontSize: 14);		

		// create a panel for all content below the header panel
      UIElement contentPanel = new UIPanel(mainPanel, "Content Panel");
		contentPanel.AddVerticalLayoutGroup(padding: new RectOffset(5,5,5,0)); // left, right, top, bottom
		//contentPanel.AddLayoutElement(preferredHeight: 1000);

		// A group panel is used as a toggle group		
      UIGroupPanel radioGroup = new UIGroupPanel(contentPanel, "Earth Representation");
      // The radio buttons will be arranged vertically
		radioGroup.AddVerticalLayoutGroup(spacing: 0, padding: new RectOffset(5,0,0,5));   // left, right, top, bottom
		radioGroup.AddLayoutElement(preferredWidth: 210);

		UIText header1 = new UIText(radioGroup, "Earth Representation", fontSize: 14);		
		header1.AddLayoutElement(preferredWidth: 200, preferredHeight: 30);
		header1.SetAlignment(TextAnchor.MiddleCenter);

      UIToggleButton radio1 = new UIToggleButton(radioGroup, "Natural, Ocean Floor", fontSize: controlFontSize);
		radio1.AddLayoutElement(preferredWidth: 200);
		
		// When a toggle button is attached to the group panel it is turns into a radio group
		// Sets the intial state of this toggle button to 'on'
		radio1.SetState(isOn: true); 
		radioGroup.AddToggleButton(radio1);
		radio1.OnValueChanged((v) => {
			if (v) {
				LoadEarth("Earth-1");
			}
		});

      UIToggleButton radio2 = new UIToggleButton(radioGroup, "Natural, Ocean Floor, Ice", fontSize: controlFontSize);
		radio2.AddLayoutElement(preferredWidth: 200); 
		// add the second toggle button to the radio group
		radioGroup.AddToggleButton(radio2);
		radio2.OnValueChanged((v) => {
			if (v) {
				LoadEarth("Earth-1 Polar Ice");
			}
		});

      UIToggleButton radio3 = new UIToggleButton(radioGroup, "Natural", fontSize: controlFontSize);
		radio3.AddLayoutElement(preferredWidth: 200); 
		radioGroup.AddToggleButton(radio3);
		radio3.OnValueChanged((v) => {
			if (v) {
				LoadEarth("Earth-2");
			}
		});

      UIToggleButton radio4 = new UIToggleButton(radioGroup, "Natural, Ice", fontSize: controlFontSize);
		radio4.AddLayoutElement(preferredWidth: 200); 
		radioGroup.AddToggleButton(radio4);
		radio4.OnValueChanged((v) => {
			if (v) {
				LoadEarth("Earth-2 Polar Ice");
			}
		});

      UIToggleButton radio5 = new UIToggleButton(radioGroup, "Hypsometric", fontSize: controlFontSize);
		radio5.AddLayoutElement(preferredWidth: 200); 
		radioGroup.AddToggleButton(radio5);
		radio5.OnValueChanged((v) => {
			if (v) {
				LoadEarth("Earth-3");
			}
		});

      UIToggleButton radio6 = new UIToggleButton(radioGroup, "Hypsometric, Ice", fontSize: controlFontSize);
		radio6.AddLayoutElement(preferredWidth: 200); 
		radioGroup.AddToggleButton(radio6);
		radio6.OnValueChanged((v) => {
			if (v) {
				LoadEarth("Earth-3 Polar Ice");
			}
		});

		//-------------------------------------------------------------------
		// Parameter Panel
      UIPanel parmPanel = new UIPanel(contentPanel, "Parameter Panel");
		parmPanel.AddVerticalLayoutGroup(spacing: 0, padding: new RectOffset(5,0,5,5));   // left, right, top, bottom
		parmPanel.AddLayoutElement(preferredWidth: 200);

		UIText header2 = new UIText(parmPanel, "Parameters", fontSize: 14);		
		header2.AddLayoutElement(preferredWidth: 200, preferredHeight: 30);
		header2.SetAlignment(TextAnchor.MiddleCenter);

		UIHorizontalSlider slider1 = new UIHorizontalSlider(parmPanel);
		slider1.SetText("Earth Speed", controlFontSize); 
		slider1.SetValue(mEarth.rotationSpeed);
		slider1.OnValueChanged((v) => { 
			mEarth.rotationSpeed = v;
		});  

		UIHorizontalSlider slider2 = new UIHorizontalSlider(parmPanel);
		slider2.SetText("Sun Speed", controlFontSize); 
		slider2.SetValue(mSun.rotationSpeed);
		slider2.OnValueChanged((v) => { 
			mSun.rotationSpeed = v;
		});  

		UIHorizontalSlider slider3 = new UIHorizontalSlider(parmPanel);
		slider3.SetText("Cloud Intensity", controlFontSize); 
		slider3.SetValue(mEarth.cloudIntensity);
		slider3.OnValueChanged((v) => { 
			mEarth.cloudIntensity = v;
		});  

		UIHorizontalSlider slider4 = new UIHorizontalSlider(parmPanel);
		slider4.SetText("Cloud Speed", controlFontSize); 
		slider4.SetValue(mEarth.cloudSpeed);
		slider4.OnValueChanged((v) => { 
			mEarth.cloudSpeed = v;
		});  

		UIHorizontalSlider slider5 = new UIHorizontalSlider(parmPanel);
		slider5.SetText("Earth Tilt", controlFontSize); 
		slider5.SetValue(mEarth.tilt);
		slider5.OnValueChanged((v) => { 
			mEarth.tilt = v;
		});  

		UIHorizontalSlider citySlider = new UIHorizontalSlider(parmPanel);
		citySlider.SetText("Cities", controlFontSize); 
		citySlider.SetMinMaxValue(-1, 8);   // -1 means no cities
		citySlider.SetValue(mMaxCityRank);
		citySlider.OnValueChanged((v) => { 
			mMaxCityRank = v;
			EnableCities();
		});  

		//-------------------------------------------------------------------
		// Info Panel
      UIPanel infoPanel = new UIPanel(contentPanel, "Info Panel");
		infoPanel.AddVerticalLayoutGroup(spacing: 0, padding: new RectOffset(2,5,5,5));   // left, right, top, bottom
		infoPanel.AddLayoutElement(preferredWidth: 210);

		UIText infoText1 = new UIText(infoPanel, "There are many more parameters!", fontSize: 12);		
		infoText1.AddLayoutElement(preferredWidth: 200, preferredHeight: 20);
		infoText1.SetAlignment(TextAnchor.MiddleCenter);

		UIText infoText2 = new UIText(infoPanel, "Please refer to the PDF manual.", fontSize: 12);		
		infoText2.AddLayoutElement(preferredWidth: 200, preferredHeight: 20);
		infoText2.SetAlignment(TextAnchor.MiddleCenter);

		//-------------------------------------------------------------------
		// Asset Store Panel
      UIPanel storePanel = new UIPanel(contentPanel, "Store Panel");
		storePanel.AddVerticalLayoutGroup(spacing: 0, padding: new RectOffset(2,5,5,5));   // left, right, top, bottom
		storePanel.AddLayoutElement(preferredWidth: 210);
 
		UIText storeText = new UIText(storePanel, "Available from the Unity Asset Store:", fontSize: 12);		
		storeText.AddLayoutElement(preferredWidth: 200, preferredHeight: 40);
		storeText.SetAlignment(TextAnchor.MiddleCenter);

		UIButton storeButton = new UIButton(storePanel, "http://www.assetstore.unity3d.com", fontSize: 11);		
		storeButton.AddLayoutElement(preferredWidth: 200, preferredHeight: 40);
		storeButton.OnValueChanged(() => { 
	 		Application.ExternalEval("window.open('https://www.assetstore.unity3d.com', '_blank')");
		});  

		//-------------------------------------------------------------------
		// Website Panel
      UIPanel sitePanel = new UIPanel(contentPanel, "Website Panel");
		sitePanel.AddVerticalLayoutGroup(spacing: 0, padding: new RectOffset(2,5,5,5));   // left, right, top, bottom
		sitePanel.AddLayoutElement(preferredWidth: 210);

		UIButton siteButton = new UIButton(sitePanel, "(C) Michael Schmeling\nhttp://www.aridocean.com", fontSize: 11);		
		siteButton.AddLayoutElement(preferredWidth: 200, preferredHeight: 55);
		siteButton.OnValueChanged(() => { 
	 		Application.ExternalEval("window.open('http://www.aridocean.com', '_blank')");
		});  
	}

	private void LoadEarth(string prefabName)
	{            
		float tilt = 0.5f; 
		Quaternion rot = Quaternion.identity;
		if (mEarth != null) {
			tilt = mEarth.tilt;			
			rot = mEarth.rot;
			mEarth.Remove();
		}
		
		mEarth = new Earth(prefabName);
		mEarth.tilt = tilt; 
		mEarth.rot = rot;
		SetCityParents(mEarth.GetObject());
	}
   
   private void SetCityParents(GameObject parent)
   {       
   	foreach (CityMarker city in mCities) {
   		city.SetParent(parent);
   	}
   }
   
   private void EnableCities()
   {       
   	foreach (CityMarker city in mCities) {
   		if (city.GetScaleRank() <= mMaxCityRank) {
   			city.SetEnabled(true);
   		}
   		else {
   			city.SetEnabled(false);
   		}
   	}
   }
	
	private void LoadCities()
	{                 
		mCities = new List<CityMarker>();
		
		if (cityFile == null) {
			throw new System.Exception("Error: city coordinates missing."); 
		}          
		string[] cities = cityFile.text.Split("\n"[0]);	

		// Skip first line containing field names
		for (int i = 1; i < cities.Length; i++) 
		{
			string[] fields = SplitCsvLine(cities[i]); 
			
			// For detailed description see www.naturalearthdata.com/downloads/110m-cultural-vectors, Populated Places
			
			// Data exported from 'ne_110m_populated_places.shp' with the following GDAL command (www.gdal.org):
			// ogr2ogr -lco ENCODING=UTF-8  -select "NAME,LONGITUDE,LATITUDE,SCALERANK,LABELRANK,FEATURECLA,ISO_A2,POP_MAX" -f csv populated_places_110m.csv ne_110m_populated_places.shp

			// Fields: NAME,LONGITUDE,LATITUDE,SCALERANK,LABELRANK,FEATURECLA,ISO_A2,POP_MAX 
			// SCALERANK: relative rank of city: 0-8
			// LABELRANK: relative rank for label size: 0-8
			// FEATURECLA: class of city: 
			//		'Admin-0 capital': 			state capital, e.g. 'Paris'
			//		'Admin-0 capital alt':		alternate state capital, e.g 'The Hague' 
			//		'Admin-0 region capital'	regional capital, e.g. 'Hong Kong'
			// 	'Admin-1 capital'				capital of US state or lower administrative unit, e.g. 'Atlanta'
			//		'Admin-1 region capital'	regional capital of lower administrative unit, e.g. 'Osaka'
			//		'Populated place'				other large city 
			// ISO_A2: ISO code of country
			// POP_MAX: estimated population 
			
			if (fields.Length >= 5) { 
				string name = fields[0];
				float lon = float.Parse(fields[1]);
				float lat = float.Parse(fields[2]); 
				float scaleRank = float.Parse(fields[3]); 
				float labelRank = float.Parse(fields[4]); 
				// Other fields are not used
				
				// Only use subset of most important cities 
				mCities.Add(new CityMarker(name, lon, lat, scaleRank, labelRank));
			}
		}
	}
}