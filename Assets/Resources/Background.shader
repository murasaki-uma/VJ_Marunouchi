Shader "Unlit/Background"
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            

            float3 pal( float t, float3 a,float3 b, float3 c, float3 d )
            {
                return a + b*cos( 6.28318*(c*t+d) );
            }

            float random(float2 co) {
    return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453123);
}

float random(float3 co) {
    return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 56.787))) * 43758.5453123);
}

float noise(float2 pos) {
    float2 i = floor(pos);
    float2 f = frac(pos);
    float a = random(i);
    float b = random(i + float2(1.0, 0.0));
    float c = random(i + float2(0.0, 1.0));
    float d = random(i + float2(1.0, 1.0));

    float2 u = f * f * (3.0 - 2.0 * f);

    return lerp(a, b, u.x) +
            (c - a) * u.y * (1.0 - u.x) +
            (d - b) * u.x * u.y;
}

float noise(float3 pos) {
    float3 ip = floor(pos);
    float3 fp = smoothstep(0, 1, frac(pos));
    float4 a = float4(
        random(ip + float3(0, 0, 0)),
        random(ip + float3(1, 0, 0)),
        random(ip + float3(0, 1, 0)),
        random(ip + float3(1, 1, 0)));
    float4 b = float4(
        random(ip + float3(0, 0, 1)),
        random(ip + float3(1, 0, 1)),
        random(ip + float3(0, 1, 1)),
        random(ip + float3(1, 1, 1)));
    a = lerp(a, b, fp.z);
    a.xy = lerp(a.xy, a.zw, fp.y);
    return lerp(a.x, a.y, fp.x);
}

//pseudo perlin noise
float perlin(float3 pos) {
    return  (noise(pos) * 32 +
            noise(pos * 2 ) * 16 +
            noise(pos * 4) * 8 +
            noise(pos * 8) * 4 +
            noise(pos * 16) * 2 +
            noise(pos * 32) ) / 63;
}

float perlin(float2 pos) {
    return  (noise(pos) * 32 +
            noise(pos * 2 ) * 16 +
            noise(pos * 4) * 8 +
            noise(pos * 8) * 4 +
            noise(pos * 16) * 2 +
            noise(pos * 32) ) / 63;
}

//fractal brownian motion
#define OCTAVES 5
float fbm(float2 pos) {
    float value = 0.0;
    float amplitude = .3;
    float frequency = 0.;

    for(int i = 0; i < OCTAVES; i++) {
        value += amplitude * noise(pos);
        pos *= 2.;
        amplitude *= .7;
    }
    return value;
}

            fixed4 frag (v2f i) : SV_Target
            {
                float2 p = i.uv;
                p.x = fbm(i.uv.xy*0.2 + float2(_Time.x,0));
                float3 col = pal( p.x, float3(0.5,0.5,0.5),float3(0.5,0.5,0.5),float3(1.0,1.0,1.0),float3(0.0,0.33,0.67) );
    
//                float f = frac(p.y*7.0);
                

                // sample the texture
//                fixed4 c = tex2D(_MainTex, i.uv);
//                col += (1.0/255.0)*c.xyz;
                // apply fog
//                UNITY_APPLY_FOG(i.fogCoord, col);
                return float4(col,1);
            }
            ENDCG
        }
    }
}
