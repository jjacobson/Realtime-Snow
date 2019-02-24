Shader "Hidden/SnowAccumulation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			// https://answers.unity.com/questions/399751/randomity-in-cg-shaders-beginner.html 
			// pseudo random numbers
			float rand(float3 co)
			{
				return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _SnowflakeCount, _SnowflakeOpacity;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float rValue = ceil(rand(float3(i.uv.x, i.uv.y, 0) * _Time.x) - (1 - _SnowflakeCount));

				// clamp 0 - 1
				return saturate(col - (rValue * _SnowflakeOpacity));
			}
			ENDCG
		}
	}
}
