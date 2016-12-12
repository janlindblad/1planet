//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

// Puts a red dot and a label at the position of a city
public class CityMarker : UltimaObject 
{ 
	private float size = 0.12f;  // size of marker
	private float r = 6.35f;  	  // radius of earth = 6.37 (6371 km), the marker is placed a little inside the sphere
	
	private float minLabelSize = 20;	// min size of labels
	private float maxLabelSize = 30; // max size of labels
	
	private GameObject mLabel;
	private float mScaleRank;
	
	public CityMarker(string name, float lon, float lat, float scaleRank,  float labelRank)
	{ 	
		mScaleRank = scaleRank;
		mObj = LoadPrefab("CityMarker");  
		mObj.name = name;
		mObj.transform.localScale = new Vector3(size, size, size);	
		mObj.transform.position = TransformCoordinates(r, lon, lat);  

		// The Canvas 'Label Canvas' is used as parent for all 2D labels
		GameObject labelCanvas = LoadFromScene("Label Canvas"); 
		if (labelCanvas != null) {
			CreateLabel(labelCanvas, name, labelRank);
		}
	}   

	public float GetScaleRank()
	{
		return mScaleRank;
	}

	public void SetParent(GameObject parent)
	{
		mObj.transform.SetParent(parent.transform);
	}

	public void SetEnabled(bool enable)
	{
		mObj.SetActive(enable);
		mLabel.SetActive(enable);
	}
	
	// Create a 2D text at the position of the city marker
	// The text is placed under a special Canvas that acts as the parent for all text labels
	protected void CreateLabel(GameObject canvas, string name, float labelRank)
	{
		mLabel = LoadPrefab("CityLabel");  
		mLabel.name = name;
		mLabel.transform.SetParent(canvas.transform);
		Text text = mLabel.GetComponent<Text>();	
		text.text = name;
		// set the label size according to the labelRank of the city (max. labelRank is 8)
		text.fontSize = Mathf.RoundToInt(Mathf.Lerp(maxLabelSize, minLabelSize, labelRank/8.0f));

		RectTransform rect = mLabel.GetComponent<RectTransform>();	
		rect.pivot = Vector2.zero;

		CityLabel label = mLabel.GetComponent<CityLabel>();	
		label.cityMarker = mObj; // place a reference to the marker in the label's component
	}
	       
	// Transform longitude and latitude values to x,y,z on the sphere	
	protected Vector3 TransformCoordinates(float r, float lon, float lat)
	{
		float lonDeg = lon*Mathf.Deg2Rad;		
		float latDeg = lat*Mathf.Deg2Rad;		
		float x = r*Mathf.Cos(latDeg)*Mathf.Cos(lonDeg);
		// y,z are reversed
		float z = r*Mathf.Cos(latDeg)*Mathf.Sin(lonDeg);
		float y = r*Mathf.Sin(latDeg);
		return new Vector3(x,y,z);
	}
}
