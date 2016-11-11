//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class UIElement 
{ 
	private GameObject mObj;
	
	public UIElement(GameObject g)
	{
		mObj = g; 	
	}
	
	public UIElement(UIElement parent, string prefabName="", string objName="")
	{ 	
		LoadPrefab(parent, prefabName, objName);
	}   

	public void SetParent(UIElement parent)
	{                         
		if (parent != null) {
			GetObject().transform.SetParent(parent.mObj.transform, false);	
		}
	}

	public void LoadPrefab(UIElement parent, string prefabName, string objName="")
	{                         
		if (!string.IsNullOrEmpty(prefabName)) {
	 		Object prefab = Resources.Load("Prefabs/"+prefabName);
	 		if (prefab == null) {
				throw new System.Exception("Prefab "+prefabName+" not found!"); 				 
			}
			mObj = Object.Instantiate(prefab) as GameObject;
			if (string.IsNullOrEmpty(objName)) {
				mObj.name = prefabName;
			}
			else {
				mObj.name = objName;
			}	
			SetParent(parent);
		}
	}
		
	public GameObject GetObject()
	{
		return mObj;
	}
	
	public void SetFixedSize(float width, float height)
	{
		RectTransform r = GetObject().GetComponent<RectTransform>();
		r.anchoredPosition = new Vector2(0,0);
		r.anchorMin = new Vector2(0,1);
		r.anchorMax = new Vector2(0,1);
		r.pivot = new Vector2(0,1);
		r.sizeDelta = new Vector2(width, height); 
	}
	
	public VerticalLayoutGroup AddVerticalLayoutGroup(bool expandWidth=false, bool expandHeight=false, RectOffset padding=null, float spacing=0)
	{
		VerticalLayoutGroup g = GetObject().GetComponent<VerticalLayoutGroup>();                             
		if (g == null) {
			g = mObj.AddComponent<VerticalLayoutGroup>();                             
		}
		g.childForceExpandWidth = expandWidth;
		g.childForceExpandHeight = expandHeight;
		g.spacing = spacing;  
		if (padding != null) {
			g.padding = padding;
		}
		return g;
	}
	
	public HorizontalLayoutGroup AddHorizontalLayoutGroup(bool expandWidth=false, bool expandHeight=false, RectOffset padding=null, float spacing=0)
	{
		HorizontalLayoutGroup g = GetObject().GetComponent<HorizontalLayoutGroup>();                             
		if (g == null) {
			g = mObj.AddComponent<HorizontalLayoutGroup>();                             
		}
		g.childForceExpandWidth = expandWidth;
		g.childForceExpandHeight = expandHeight;
		g.spacing = spacing;  
		if (padding != null) {
			g.padding = padding;
		}
		return g;
	}

	public LayoutElement AddLayoutElement(float minWidth=-1, float minHeight=-1, float preferredWidth=-1, float preferredHeight=-1, bool flexibleWidth=false, bool flexibleHeight=false)
	{
		LayoutElement l = GetObject().GetComponent<LayoutElement>(); 
		
		if (l == null) {
			l = mObj.AddComponent<LayoutElement>();
		}
		
		if (minWidth >= 0) {
			l.minWidth = minWidth;
		}
		if (minHeight >= 0) {
			l.minHeight = minHeight;
		}
		if (preferredWidth >= 0) {
			l.preferredWidth = preferredWidth;
		}
		if (preferredHeight >= 0) {
			l.preferredHeight = preferredHeight;
		}
		l.flexibleWidth = (flexibleWidth ? 1 : 0);
		l.flexibleHeight = (flexibleHeight ? 1 : 0); 

		return l;
	}

	public ContentSizeFitter AddContentSizeFitter()
	{
		ContentSizeFitter c = GetObject().GetComponent<ContentSizeFitter>(); 
		
		if (c == null) {
			c = mObj.AddComponent<ContentSizeFitter>();
		}
		
		c.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		c.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		return c;
	}

	public void AddToggleButton(UIElement button)
	{
		ToggleGroup g = GetObject().GetComponent<ToggleGroup>();
		if (g == null) {
			g = GetObject().AddComponent<ToggleGroup>();                             
		}
		button.GetObject().GetComponent<Toggle>().group = g;
	}
}
