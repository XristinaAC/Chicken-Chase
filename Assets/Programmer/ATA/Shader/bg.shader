Shader "Unlit/bg"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeedX ("Scroll Speed X", Float) = 0.1 // X ekseni kaydırma hızı
        _ScrollSpeedY ("Scroll Speed Y", Float) = 0.0 // Y ekseni kaydırma hızı
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" } // UI için Transparent kuyruğu genellikle daha iyidir
        LOD 100

        // RawImage, UI olduğu için genellikle Transparency (Saydamlık) ayarlarını kullanırız
        // UI bileşenleri için en yaygın tag'leri ve ayarlamaları kullanmak önemlidir.
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha // Normal saydamlık

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScrollSpeedX;
            float _ScrollSpeedY;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                // UV koordinatlarını al
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                // Kaydırma (Scrolling) efektini ekle
                // _Time.y (t) zamanı verir. Unity, bunu saniyeler içinde artan bir float olarak günceller.
                o.uv.x += _Time.y * _ScrollSpeedX;
                o.uv.y += _Time.y * _ScrollSpeedY;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Texture'ı kaydırılmış UV koordinatları ile örnekle
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}