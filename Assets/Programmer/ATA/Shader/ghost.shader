Shader "Custom/ghost"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _TintColor ("Ghost Tint Color", Color) = (0.6, 0.9, 1, 1)
        _Opacity ("Transparency", Range(0,1)) = 0.5
        _FresnelPower ("Fresnel Sharpness", Range(0.1,10)) = 3
        _FresnelIntensity ("Fresnel Intensity", Range(0,5)) = 1.5
        _EmissionColor ("Emission Color", Color) = (0.5, 1, 1, 1)
        _EmissionStrength ("Emission Strength", Range(0,10)) = 2
        _Distortion ("Distortion Strength", Range(0,1)) = 0.05
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _TintColor;
            float _Opacity;
            float _FresnelPower;
            float _FresnelIntensity;
            float4 _EmissionColor;
            float _EmissionStrength;
            float _Distortion;

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
                // Base color
                fixed4 baseCol = tex2D(_MainTex, i.uv) * _TintColor;

                // Fresnel highlight
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, normalize(i.worldNormal))), _FresnelPower);
                fresnel *= _FresnelIntensity;

                // Slight UV distortion for ghosty wobble
                float2 distortedUV = i.uv + (i.worldNormal.xy * _Distortion * sin(_Time.y * 3));

                baseCol.rgb = tex2D(_MainTex, distortedUV).rgb * _TintColor.rgb;
                baseCol.rgb += _EmissionColor.rgb * fresnel * _EmissionStrength;

                // Final transparency
                baseCol.a = _Opacity * (0.5 + 0.5 * fresnel);

                return baseCol;
            }
            ENDCG
        }
    }
}
