//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;

public class EarthRotation : MonoBehaviour
{
	public float earthSpeed = 0.5f;
	
	public void Update()
	{
		transform.Rotate(Vector3.down*20.0f*earthSpeed*Time.deltaTime);
	}
}
