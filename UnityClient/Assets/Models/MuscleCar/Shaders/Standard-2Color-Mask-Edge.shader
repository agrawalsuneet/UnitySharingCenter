Shader "Standard 2 Color Mask Edged"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_Color2 ("Secondary Color", Color) = (1,1,1,1)
		_ColBlend ("Color Blend", Range(0.1, 10.0)) = 2.0
		_MainTex ("Albedo", 2D) = "white" {}
		_ColorMask ("Grayscale Color Mask", 2D) = "white" {}
		_MaskEdge ("Mask Edge Shift", Range(-0.49, 0.49)) = 0.0
		_Metallic ("Metallic and Smoothness", 2D) = "gray" {}
		_Normal ("Normal", 2D) = "bump" {}
		_Occlusion ("Occlusion", 2D) = "white" {}
	}

	SubShader
	{   
        Tags
        {
        	"Queue" = "Geometry"
            "RenderType" = "Opaque"
        }
         
		CGPROGRAM

 		#include "UnityPBSLighting.cginc"
		#pragma surface surf Standard

		sampler2D _MainTex;
		sampler2D _ColorMask;
		fixed _MaskEdge;
		fixed _ColBlend;
		sampler2D _Metallic;
		sampler2D _Normal;
		sampler2D _Occlusion;
		fixed4 _Color;
		fixed4 _Color2;

		struct Input
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
			fixed mask = round(tex2D(_ColorMask, IN.uv_MainTex).r + _MaskEdge);
			fixed4 metal = tex2D(_Metallic, IN.uv_MainTex);
			fixed3 normal = UnpackScaleNormal(tex2D(_Normal, IN.uv_MainTex), 1);
			o.Normal = normal;
			fixed4 occ = tex2D(_Occlusion, IN.uv_MainTex);
			half blend = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			fixed3 blendedCol = lerp(col.rgb * _Color, col.rgb * _Color2, pow(blend, _ColBlend));
			o.Albedo = lerp(col.rgb, blendedCol.rgb, mask.r);
			o.Metallic = metal.r;
			o.Smoothness = metal.a;
			o.Occlusion = occ.rgb;
		}

		ENDCG
	}

	Fallback "Standard"
}