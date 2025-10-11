Shader "UI/TransparentHole"
{
    Properties
    {
        _Color ("Overlay Color", Color) = (0,0,0,1)
        _HoleCenter ("Hole Center (UV)", Vector) = (0.5, 0.5, 0, 0)
        _HoleSize ("Hole Size (UV)", Vector) = (0.2, 0.2, 0, 0)
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

            float4 _Color;
            float2 _HoleCenter;
            float2 _HoleSize;

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

                // 1 — непрозрачный, 0 — прозрачный
                float insideX = step(minR.x, uv.x) * step(uv.x, maxR.x);
                float insideY = step(minR.y, uv.y) * step(uv.y, maxR.y);
                float inside = insideX * insideY;

                // Если внутри "дырки", делаем альфу = 0
                float alpha = (1 - inside) * _Color.a;

                return fixed4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}
