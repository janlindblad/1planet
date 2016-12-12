//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIToggleBase : UIElement
{ 
	public UIToggleBase(UIElement parent, string text="", int fontSize=14) : base(parent)
	{ 	
	}
	
	public void SetText(string text, int fontSize=14)
	{
		Text t = GetObject().GetComponentInChildren<Text>();
		t.text = text;
		t.fontSize = fontSize;
	}     
	     
	public void SetState(bool isOn)
	{
		GetObject().GetComponent<Toggle>().isOn = isOn;
	}

	public void OnValueChanged(UnityAction<bool> callback)
	{
		GetObject().GetComponent<Toggle>().onValueChanged.AddListener(callback);
	}
	
}
