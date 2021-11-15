// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/IntersectionHighlightShaderOLD"
{
    Properties
    {
        _Color ("Color", Color) = (1,0,0,.2)
        _HighlightColor ("Highlight Color", Color) = (1,0,0,1)
		_HighlightThreshold("Highlight Threshold", Float) = 1
    }
    SubShader
    {
        Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}

		Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
 
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _Color;
            uniform float4 _HighlightColor;
			uniform float _HighlightThreshold;
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD1; //Screen position of pos
            };
 
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.projPos = ComputeScreenPos(o.pos);
 
                return o;
            }
 
            half4 frag(v2f i) : COLOR
            {
                float4 finalColor = _Color;
 
                //Get the distance to the camera from the depth buffer for this point
                float sceneZ = LinearEyeDepth (tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)).r);
 
                //Actual distance to the camera
                float partZ = i.projPos.z;
 
                //If the two are similar, then there is an object intersecting with our object
                float diff = (abs(sceneZ - partZ))/_HighlightThreshold;
 
                if(diff <= 1)
                {
                    finalColor = lerp(_HighlightColor, _Color, float4(diff, diff, diff, diff));
                }
 
                half4 c;
                c.r = finalColor.r;
                c.g = finalColor.g;
                c.b = finalColor.b;
                c.a = finalColor.a;
 
                return c;
            }
 
            ENDCG
        }
    }
    FallBack "VertexUnlit"
}
