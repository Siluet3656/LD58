Shader "UI/TransparentHoleGlow"
{
    Properties
    {
        _Color ("Overlay Color", Color) = (0,0,0,1)
        _HoleCenter ("Hole Center (UV)", Vector) = (0.5, 0.5, 0, 0)
        _HoleSize ("Hole Size (UV)", Vector) = (0.2, 0.2, 0, 0)

        // 🔹 Новые свойства
        _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
        _HighlightWidth ("Highlight Width", Range(0.001, 0.3)) = 0.05
        _HighlightIntensity ("Highlight Intensity", Range(0, 3)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "CanUseSpriteAtlas"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
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
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _Color;
            float2 _HoleCenter;
            float2 _HoleSize;

            // 🔹 Новые параметры подсветки
            fixed4 _HighlightColor;
            float _HighlightWidth;
            float _HighlightIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 halfSize = _HoleSize * 0.5;
                float2 minR = _HoleCenter - halfSize;
                float2 maxR = _HoleCenter + halfSize;

                // Проверка — внутри дырки или нет
                float insideX = step(minR.x, uv.x) * step(uv.x, maxR.x);
                float insideY = step(minR.y, uv.y) * step(uv.y, maxR.y);
                float inside = insideX * insideY;

                // Базовая альфа (0 внутри дырки)
                float alpha = (1 - inside) * _Color.a;

                // 🔹 Подсветка по расстоянию от границы окна
                float2 distToEdge = max(abs(uv - _HoleCenter) - halfSize, 0);
                float edgeDist = length(distToEdge); // 0 на границе
                float glow = exp(-pow(edgeDist / _HighlightWidth, 2.0)) * _HighlightIntensity;

                // 🔹 Смешиваем подсветку и фон
                fixed3 finalColor = lerp(_Color.rgb, _HighlightColor.rgb, saturate(glow));

                return fixed4(finalColor, alpha);
            }
            ENDCG
        }
    }
}
