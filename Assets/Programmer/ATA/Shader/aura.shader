Shader "Custom/aura"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _AuraColor ("Aura Color", Color) = (0.3, 0.8, 1, 1)
        _AuraIntensity ("Aura Intensity", Range(0,10)) = 2
        _AuraPower ("Aura Sharpness", Range(0.1,10)) = 4
        _EmissionStrength ("Emission Strength", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _AuraColor;
            float _AuraIntensity;
            float _AuraPower;
            float _EmissionStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Texture base color (optional)
                float4 baseCol = tex2D(_MainTex, i.uv);

                // Fresnel aura effect
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, i.worldNormal)), _AuraPower);
                float aura = fresnel * _AuraIntensity;

                float4 auraCol = _AuraColor * aura * _EmissionStrength;
                auraCol.a = saturate(aura); // transparency control

                return baseCol + auraCol;
            }
            ENDCG
        }
    }
}
