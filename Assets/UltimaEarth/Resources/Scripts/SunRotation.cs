//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;

public class SunRotation : MonoBehaviour
{
	public float sunSpeed = 0.5f;
	
	public void Update()
	{
		transform.RotateAround(Vector3.zero, Vector3.up, 40.0f*sunSpeed*Time.deltaTime);
	}
}
