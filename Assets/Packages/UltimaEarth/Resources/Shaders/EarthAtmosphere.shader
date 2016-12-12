Shader "Earth Atmosphere" {
	Properties {   
		_GlowColor ("Glow Color", Color) = (0.5, 0.5, 1,0.0)
		_GlowIntensity ("Glow Intensity", Range(0.0,12.0)) = 4
		_GlowPower ("Glow Power", Range(1,12.0)) = 6
	}
 
 SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "ForceNoShadowCasting"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	AlphaTest Greater .001
	ColorMask RGB
	Cull Back Lighting Off ZWrite Off Fog { Color (0,0,0,0) } 
	   
   CGPROGRAM
   #pragma surface surf Atmosphere 

   fixed  _GlowIntensity;
   fixed  _GlowPower;
   fixed3 _GlowColor;
   
   fixed4 LightingAtmosphere (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
		half4 c;
		fixed vn = dot(normalize(viewDir), s.Normal);
		fixed ln = dot(normalize(lightDir), -s.Normal);
		fixed glow = clamp(ln*pow(1-vn, _GlowPower),0,2);

		c.rgb = clamp(_GlowIntensity*_GlowColor*glow,0,1);  
		c.a = 1;
		
		return c;
   }
   
   struct Input {
    fixed dummy;
   };
   
   void surf (Input IN, inout SurfaceOutput o) { 
   	o.Albedo = fixed3(0,0,0);
   }
   ENDCG
 
 } 
}