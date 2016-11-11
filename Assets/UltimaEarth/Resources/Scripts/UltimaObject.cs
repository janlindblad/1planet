//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;

public class UltimaObject 
{ 
	protected GameObject mObj;
	
	public UltimaObject()
	{ 	
	}   
		
	public GameObject GetObject()
	{
		return mObj;
	}

	public GameObject LoadPrefab(string prefabName)
	{                         
 		Object prefab = Resources.Load("Prefabs/"+prefabName);
 		if (prefab == null) {
			throw new System.Exception("Prefab "+prefabName+" not found!"); 				 
		}
		GameObject obj = Object.Instantiate(prefab) as GameObject;
		obj.name = prefabName;
		return obj;
	}

	public GameObject LoadFromScene(string name)
	{ 
		return GameObject.Find(name);	
	}

	public void Remove()
	{                         
		MonoBehaviour.Destroy(mObj);
	}
}
