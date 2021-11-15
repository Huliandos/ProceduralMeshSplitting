// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/IntersectionHighlightShader"
{
    Properties
	{
		_Color("Color", COLOR) = (0,0,0,0)

		_IntersectionColor("Intersection Color", COLOR) = (0,0,0,0)
		_IntersectIntensity("Intersection Intensity", float) = 10.0
		_IntersectExponent("Intersection Falloff Exponent", float) = 6.0
	}
	SubShader
	{
		Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}
		Cull Off
		Blend SrcAlpha One

		Pass
		{
			HLSLPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD2;
				float depth : TEXCOORD3; 
			};

			float4 _Color, _IntersectionColor;

			sampler2D _CameraDepthNormalsTexture;
			float _IntersectIntensity, _IntersectExponent;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				o.depth = -mul(UNITY_MATRIX_MV, v.vertex).z * _ProjectionParams.w; 
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float diff = DecodeFloatRG(tex2D(_CameraDepthNormalsTexture, i.screenPos.xy / i.screenPos.w).zw) - i.depth;
				float intersectGradient = 1 - min(diff / _ProjectionParams.w, 1.0f);

				fixed4 intersectTerm = _IntersectionColor * pow(intersectGradient, _IntersectExponent) * _IntersectIntensity;

				return fixed4(_Color.rgb + intersectTerm, _Color.a);
			}

			ENDHLSL
		}
	}
}
