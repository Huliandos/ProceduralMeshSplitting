
Shader "Custom/IntersectionHighlightShaderTEST"
{
    Properties
    {
		_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,0,0,.2)
        _HighlightColor ("Highlight Color", Color) = (1,0,0,1)
		_Fresnel("Fresnel Intensity", Range(0,200)) = 3.0
		_FresnelWidth("Fresnel Width", Range(0,2)) = 3.0
		_IntersectionThreshold("Highlight Threshold", range(0,1)) = .1 
    }
    SubShader
    {
        Tags{ "IgnoreProjector" = "True" "RenderType" = "Transparent" "Queue" = "Transparent" }

		Pass
		{
			Lighting Off ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				fixed4 vertex : POSITION;
				fixed4 normal: NORMAL;
				fixed3 uv : TEXCOORD0;
			};

			struct v2f
			{
				fixed2 uv : TEXCOORD0;
				fixed4 vertex : SV_POSITION;
				fixed3 rimColor :TEXCOORD1;
				fixed4 screenPos: TEXCOORD2;
			};

			sampler2D _MainTex, _CameraDepthTexture;
			fixed4 _MainTex_ST, _Color, _HighlightColor;
			fixed _Fresnel, _FresnelWidth, _IntersectionThreshold;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				//fresnel 
				fixed3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				fixed dotProduct = 1 - saturate(dot(v.normal, viewDir));
				o.rimColor = smoothstep(1 - _FresnelWidth, 1.0, dotProduct) * .5f;
				o.screenPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.screenPos.z);//eye space depth of the vertex 
				return o;
			}

			fixed4 frag (v2f i,fixed face : VFACE) : SV_Target
			{
				//intersection
				fixed intersect = saturate((abs(LinearEyeDepth(tex2Dproj(_CameraDepthTexture,i.screenPos).r) - i.screenPos.z)) / _IntersectionThreshold);

				fixed3 main = tex2D(_MainTex, i.uv);

				//intersect hightlight
				i.rimColor *= intersect * clamp(0,1,face);
				main *= _HighlightColor * pow(_Fresnel,i.rimColor) ;
				
				//lerp distort color & fresnel color
				main = lerp(_Color, main, i.rimColor.r);
				main += (1 - intersect) * (face > 0 ? .03:.3) * _HighlightColor * _Fresnel;
				return fixed4(main,1);
			}
			ENDCG
		}
    }
    FallBack "VertexUnlit"
}
