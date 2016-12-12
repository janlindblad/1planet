//------------------------------------------------------------------------------
//            MilGuiSkin 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : UIElement
{ 
	public UIButton(UIElement parent, string text="", int fontSize=14, string prefabName="Button") : base(parent, prefabName)
	{ 	
		SetText(text, fontSize);
	}
	
	public void SetText(string text, int fontSize=14)
	{
		Text t = GetObject().GetComponentInChildren<Text>();
		t.text = text;
		t.fontSize = fontSize;
	}

	public void OnValueChanged(UnityAction callback)
	{
		GetObject().GetComponentInChildren<Button>().onClick.AddListener(callback);
	}
	
}
