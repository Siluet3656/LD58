Shader "UI/MentalDistortion"
{
    Properties
    {
        [PerRendererData] _MainTex ("Main Texture", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Range(0, 0.1)) = 0.01
        _DistortionSpeed ("Distortion Speed", Range(0, 5)) = 1
        _NoiseScale ("Noise Scale", Range(0, 0.1)) = 0.05
        _ColorShift ("Color Shift", Range(0, 0.3)) = 0.1
        _AlphaFade ("Alpha Fade", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float _DistortionStrength;
            float _DistortionSpeed;
            float _NoiseScale;
            float _ColorShift;
            float _AlphaFade;

            // Простой шум для псевдослучайных значений
            float noise(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Базовые искажения UV
                float2 distortedUV = i.uv;
                
                // Добавляем волновые искажения
                float wave1 = sin(i.uv.y * 20 + _Time.y * _DistortionSpeed) * _DistortionStrength;
                float wave2 = cos(i.uv.x * 15 + _Time.y * _DistortionSpeed * 1.3) * _DistortionStrength;
                
                // Добавляем шумовые искажения
                float noiseVal = noise(i.uv * 10 + _Time.y) * _NoiseScale;
                
                distortedUV.x += wave1 + wave2 * 0.5 + noiseVal;
                distortedUV.y += wave2 + wave1 * 0.3 + noiseVal;

                // Хроматическая аберрация
                fixed4 col;
                col.r = tex2D(_MainTex, distortedUV + float2(_ColorShift, 0)).r;
                col.g = tex2D(_MainTex, distortedUV + float2(0, 0)).g;
                col.b = tex2D(_MainTex, distortedUV + float2(-_ColorShift, 0)).b;
                col.a = tex2D(_MainTex, i.uv).a * i.color.a;

                // Пульсирующая прозрачность
                col.a *= (1 - _AlphaFade) + sin(_Time.y * 3) * _AlphaFade;

                return col;
            }
            ENDCG
        }
    }
}