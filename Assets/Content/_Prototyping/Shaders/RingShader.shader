Shader "Custom/RingShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _InnerRadius ("Inner Radius", Range(0,1)) = 0.3
        _OuterRadius ("Outer Radius", Range(0,1)) = 0.5
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
            float4 _Color;
            float _InnerRadius;
            float _OuterRadius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // Отбрасываем пиксели вне кольца
                if (dist < _InnerRadius || dist > _OuterRadius)
                    discard;

                // Получаем цвет из текстуры
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Умножаем текстурный цвет на _Color, включая альфа-канал
                fixed4 finalColor = texColor * _Color;

                // Применяем альфа-прозрачность
                finalColor.a *= _Color.a;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "UI/Default"
}
