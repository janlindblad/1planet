//------------------------------------------------------------------------------
//            UltimaEarth 2.0
// Copyright © 2014 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIToggleButton : UIToggleBase
{ 
	public UIToggleButton(UIElement parent, string text="", int fontSize=14) : base(parent)
	{ 	
		LoadPrefab(parent, "ToggleButton");
		SetState(false);
		SetText(text, fontSize);
	}
}
