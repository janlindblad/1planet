Shader "Earth Clouds" {
	Properties {   
		_CloudTex ("Clouds(RGB)", 2D) = "black" {}
		_BumpMap ("Cloud Bumpmap", 2D) = "bump" {}
		_CloudIntensity ("Cloud Intensity", Range(0,1.0)) = 0.3
		_CloudNightIntensity ("Cloud Intensity at Night", Range(0,1.0)) = 0.1
		_CloudColor ("Cloud Color", Color) = (1,1,1,0.0)
	}
 
 SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "ForceNoShadowCasting"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	AlphaTest Greater .001
	ColorMask RGB
	Cull Back Lighting Off ZWrite Off Fog { Color (0,0,0,0) } 
	   
   CGPROGRAM
   #pragma surface surf Clouds 

   sampler2D _CloudTex;
   sampler2D _BumpMap;
   fixed _CloudIntensity;
   fixed _CloudNightIntensity;
   fixed3 _CloudColor;
   fixed3 _Clouds;
   fixed  _GlowIntensity;
   fixed  _GlowPower;
   fixed3 _GlowColor;
   
   fixed4 LightingClouds(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
		half4 c;
		fixed diffuse = max(0, dot(s.Normal, normalize(lightDir)));

		c.rgb = _CloudIntensity*_CloudColor*_Clouds*clamp((_CloudNightIntensity+diffuse),0,1);  
		c.a = 1;
		
		return c;
   }
   
   struct Input {
    float2 uv_CloudTex;
   };
   
   void surf (Input IN, inout SurfaceOutput o) { 
   	o.Albedo = fixed3(0,0,0);
		_Clouds = tex2D(_CloudTex, IN.uv_CloudTex).rgb; 
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_CloudTex));
   }
   ENDCG
 
 } 
}