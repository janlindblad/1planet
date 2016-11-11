//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright � 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;

public class CloudRotation : MonoBehaviour
{
	public float cloudSpeedH = 0.5f;
	public float cloudSpeedV = 0.0f;
	private float speedMultiplier = 0.003f;
	
	public void Update()
	{
		Vector2 cloudOffset = new Vector2(5.0f*cloudSpeedH, -cloudSpeedV);
		GetComponent<Renderer>().material.SetTextureOffset("_CloudTex", speedMultiplier*Time.time*cloudOffset); 
	}
}
