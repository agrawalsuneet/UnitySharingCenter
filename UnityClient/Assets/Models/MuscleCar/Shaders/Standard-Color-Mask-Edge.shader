Shader "Standard Color Mask Edged"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
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
		sampler2D _Metallic;
		sampler2D _Normal;
		sampler2D _Occlusion;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
			fixed mask = round(tex2D(_ColorMask, IN.uv_MainTex).r + _MaskEdge);
			fixed4 metal = tex2D(_Metallic, IN.uv_MainTex);
			fixed3 normal = UnpackScaleNormal(tex2D(_Normal, IN.uv_MainTex), 1);
			fixed4 occ = tex2D(_Occlusion, IN.uv_MainTex);
			o.Albedo = lerp(col.rgb, col.rgb * _Color, mask.r);
			o.Metallic = metal.r;
			o.Smoothness = metal.a;
			o.Normal = normal;
			o.Occlusion = occ.rgb;
		}

		ENDCG
	}

	Fallback "Standard"
}