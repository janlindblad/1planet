//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

// This script is attached to a 2D text element which acts as a city label 
// It sets the position of label to screen coords of the associated city marker
public class CityLabel : MonoBehaviour
{            
	// Reference to the city marker associated with this label;
	public GameObject cityMarker; 

	// Minimim and maximum camera distances for label fading in/out	
	private float minDist = 11.0f;
	private float maxDist = 13.0f; 
	                  
	// Width and height of label	                  
	private float labelWidth = 220;
	private float labelHeight = 40;
	
	private Camera cam;
	private RectTransform rect; 
	private Text text;
	
	void Awake()
	{           
		cam = Camera.main;
		rect = GetComponent<RectTransform>();	
		text = GetComponent<Text>();	
	}
		
	void Update()
	{                  
		// Obtain screen coordinates of city marker associated with this label;
		Vector3 screenCoords = cam.WorldToScreenPoint(cityMarker.transform.position); 

		// Fade the alpha value with distance from camera
		Color c = text.color;
		c.a = Mathf.SmoothStep(1, 0, (screenCoords.z-minDist)/(maxDist-minDist));
		text.color = c;

		// Set position of label to screen coords of city marker
		rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, screenCoords.x, labelWidth);		
		rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, screenCoords.y, labelHeight);		
	}
}