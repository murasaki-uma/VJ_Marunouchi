Shader "Instanced/InstancedShader/TextAnim" {
	Properties{
		_PowerLevel ("", Range(0.01,10)) = 2
	}
	SubShader{

		Pass {

			Tags {"LightMode" = "ForwardBase" "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

            LOD 100
	
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha 
	
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			#pragma target 4.5

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "AutoLight.cginc"

			sampler2D _MainTex;
			sampler2D _SourceTex;
//			sampler2D _SourceTex02;

			int _CellSize;
			int _FrameCount;
			float _Cell_U;
			float _Cell_V;
			float _QuadScale;

			float _PowerLevel;

		#if SHADER_TARGET >= 45
			StructuredBuffer<float3> _PositionBuffer;
			StructuredBuffer<float2> _UVBuffer;
		#endif

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_sourceTex : TEXCOORD5;
			};


			v2f vert(appdata_full v, uint instanceID : SV_InstanceID)
			{
				float3 pos = _PositionBuffer[instanceID];
				float2 uv = _UVBuffer[instanceID];

float y = v.vertex.y;
float z = v.vertex.z;
v.vertex.y = z;
v.vertex.z = y;
//                 v.vertex.z += sin(v.vertex.x * 5.0 + _Time.x * 5.0) * cos(v.vertex.z * 5.0 + _Time.x * 10.0);
				float3 localPosition = v.vertex.xyz * _QuadScale;
				float3 worldPosition = pos.xyz + localPosition;
					

				v2f o;
				o.pos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
				o.uv_sourceTex = uv;
				o.uv = v.texcoord;
//				TRANSFER_SHADOW(o)
				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				int size = floor(_CellSize);
				float4 source = tex2D(_SourceTex, i.uv_sourceTex);
//				float4 source02 = tex2D(_SourceTex02, i.uv_sourceTex);
				float powerLevel = _PowerLevel;
				float whiteLevel_01 = (pow(source.x, powerLevel) + pow(source.y, powerLevel) + pow(source.z, powerLevel)) / 3.0;
//				float whiteLevel_02 = (pow(source02.x, powerLevel) + pow(source02.y, powerLevel) + pow(source02.z, powerLevel)) / 3.0;
				float whiteLevel = whiteLevel_01;

				float step = 1.0 / (size*size - 1);
				float whiteLevelCount = 0.0;
				while (whiteLevelCount < whiteLevel)
				{
					whiteLevelCount += step;
				}
				int frame = floor(whiteLevelCount / step);
				float offset_x = fmod(frame, size) / size;
				float offset_y = floor(frame / size) / size;
				
				fixed4 c = tex2D(_MainTex, i.uv / _CellSize + float2(offset_x, offset_y));
				if(frame == 0) c.a  = 0;
				return c;
			}

			ENDCG
		}
	}
}