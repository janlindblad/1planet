Shader "Earth Surface" {
	Properties {
		_MainTex ("World Texture(RGB) Spec(A)", 2D) = "white" {}
		_BumpMap ("Elevation Bumpmap", 2D) = "bump" {}
		_CityTex ("City Lights(RGB)", 2D) = "black" {}
		_CityIntensity ("City Light Intensity", Range(0,1.0)) = 0.2
		_CityLightTime ("City Light Time", Range(1,12)) = 12
		_CityColor ("City Light Color", Color) = (1,1,0.82,0.0)
		_Shininess ("Ocean Shininess Power", Range (5, 1000)) = 900
		_Gloss ("Ocean Gloss", Range (0.0, 10)) = 10
	}
 
 SubShader {
   Tags { "RenderType" = "Opaque" }
   
   CGPROGRAM
   #pragma surface surf Earth 
   
   float _Shininess;  
   float _Gloss;  
   float _CityIntensity;
   float _CityLightTime;
   sampler2D _MainTex;
   sampler2D _CityTex;
   sampler2D _BumpMap;
   float3 _CityColor;
   fixed3 _Cities;

	fixed4 LightingEarth(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
	{  
		// This simplified computation of specular highlight avoids a register warning
		//fixed3 h = normalize(lightDir + viewDir);
		fixed3 h = normalize(lightDir);
		fixed diffuse = max(0, dot(s.Normal, normalize(lightDir)));

		fixed nh = max(0, dot(s.Normal, h));
		fixed specular = _Gloss*s.Gloss*pow(nh, s.Specular);
		
		fixed4 c;

	   c.rgb = s.Albedo*_LightColor0.rgb*(diffuse+specular)*2*atten;
		c.rgb = lerp(c.rgb, _CityColor, _CityIntensity*_Cities*pow(1-diffuse, _CityLightTime));  
		c.a = 1;

		return c;
	} 
   
   struct Input {
    float2 uv_MainTex;
   };
   
   void surf (Input IN, inout SurfaceOutput o) { 
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		_Cities = tex2D (_CityTex, IN.uv_MainTex).rgb;
		o.Albedo = tex.rgb;
		o.Gloss = tex.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		
		// Fix faulty normals at poles, use together with higher aniso level!
		if (IN.uv_MainTex.y > 0.95 || IN.uv_MainTex.y < 0.01) {
			o.Normal = fixed3(0,0,1);
		} 
   }
   ENDCG
 
 } 
 Fallback "Specular"
}