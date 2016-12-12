//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright � 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;

public class Earth : UltimaObject 
{             
	private GameObject mClouds;
	private float mTilt;
		
	public Earth(string prefabName)
	{ 	        
		mTilt = 0;
		mObj = LoadPrefab(prefabName);
		mClouds = CloudObject();
		mClouds.GetComponent<Renderer>().material.SetFloat("_CloudIntensity", 0.2f);
	}   

	public Quaternion rot
	{
		get { 
			return GetObject().transform.rotation;
		}
		set { 
			GetObject().transform.rotation = value;			
		}
	}		

	public float rotationSpeed
	{
		get { 
			EarthRotation earthScript = GetObject().GetComponent<EarthRotation>();	
			return earthScript.earthSpeed; 
		}
		set { 
			EarthRotation earthScript = GetObject().GetComponent<EarthRotation>();	
			earthScript.earthSpeed = value; 
		}
	}		

	public float tilt
	{
		get { 
			return Mathf.InverseLerp(-90, +90, mTilt);
		}
		set { 
			float tilt = Mathf.Lerp(-90, +90, value); 
			if (tilt != mTilt) {
				GetObject().transform.Rotate(mTilt-tilt,0,0,Space.World);
				mTilt = tilt;
			}
		}
	}		

	public float cloudIntensity
	{
		get { 
			return mClouds.GetComponent<Renderer>().material.GetFloat("_CloudIntensity");
		}
		set { 
			mClouds.GetComponent<Renderer>().material.SetFloat("_CloudIntensity", value);
		}
	}		

	public float cloudSpeed
	{
		get { 
			CloudRotation script = mClouds.GetComponent<CloudRotation>();	
			return script.cloudSpeedH; 
		}
		set { 
			CloudRotation script = mClouds.GetComponent<CloudRotation>();	
			script.cloudSpeedH = value; 
		}
	}

	public GameObject CloudObject()
	{
		return GetObject().transform.Find("Clouds").gameObject;
	}
			
}
