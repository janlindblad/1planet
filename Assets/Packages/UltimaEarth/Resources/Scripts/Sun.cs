//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;

public class Sun : UltimaObject 
{ 
	public Sun()
	{ 	
		mObj = LoadFromScene("Sun");
	}   

	public float rotationSpeed
	{
		get { 
			SunRotation script = GetObject().GetComponent<SunRotation>();	
			return script.sunSpeed; 
		}
		set { 
			SunRotation script = GetObject().GetComponent<SunRotation>();	
			script.sunSpeed = value; 
		}
	}		
}
