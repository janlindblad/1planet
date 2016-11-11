//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UISliderBase : UIElement
{ 
	public UISliderBase(UIElement parent, string prefabName) : base(parent, prefabName)
	{ 	
	}
	
	public void SetText(string text, int fontSize=14)
	{
		Text t = GetObject().GetComponentInChildren<Text>();
		t.text = text;
		t.fontSize = fontSize;
	}
	
	public void SetMinMaxValue(float min, float max)
	{
		Slider sl = GetObject().GetComponentInChildren<Slider>();
		sl.minValue = min;
		sl.maxValue = max;
	}
	
	public void SetValue(float v)
	{
		Slider sl = GetObject().GetComponentInChildren<Slider>();
		sl.value = v;
	}

	public void UpdateLabel(float value)
	{
		SetText(Mathf.RoundToInt(value*100)+"%");
	}

	public void OnValueChanged(UnityAction<float> callback)
	{
		GetObject().GetComponentInChildren<Slider>().onValueChanged.AddListener(callback);
	} 
}
