Shader "Unlit/AudioReactiveCircle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineWidth("Line Width", float) = 0.1
    }
    SubShader
    {
       Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            
           

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Forces[10];
            float _LineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = float4(0,0,0,1);
                
                for(int num = 0; num < 10; num++)
                {
                    float2 v = normalize(i.uv - float2(0.5,0.5));
                    float2 th = float2(0.5,0.5) + v * fmod(_Forces[num],0.5);
                    float dist = distance(th, float2(0.5,0.5));
                    float current = distance(i.uv,float2(0.5,0.5));
                    if(dist > current && current > dist-_LineWidth)
                    {
                        float a= 1-min((dist-current),_LineWidth)/_LineWidth;
                        col = float4(1,1,1,a);
                    }
                }
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
