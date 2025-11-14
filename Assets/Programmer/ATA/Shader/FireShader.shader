Shader "Custom/FireShader"
{
    Properties
    {
        _MainTex("Fire Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _Speed("Flow Speed", Float) = 1
        _Distortion("Distortion Amount", Float) = 0.1
        _Color("Fire Color", Color) = (1, 0.5, 0, 1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend One One  // ADDITIVE FIRE BLENDING
        ZWrite Off     // Ateş arkadan görülsün
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _Speed;
            float _Distortion;
            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                // UV'yi zamanla yukarı doğru kaydır (fire effect)
                o.uv = v.uv + float2(0, _Time.y * _Speed);

                return o;
            }

           fixed4 frag (v2f i) : SV_Target
{
    float noise = tex2D(_NoiseTex, i.uv * 1.5 + _Time.y * 0.2).r;
    float2 distortedUV = i.uv + (noise - 0.5) * _Distortion;

    fixed4 col = tex2D(_MainTex, distortedUV);

    // ORİJİNAL ALPHA'YI KULLAN → kare görünmeyi çözer
    col.a = col.a;

    return col * _Color;
}

            ENDCG
        }
    }
}
