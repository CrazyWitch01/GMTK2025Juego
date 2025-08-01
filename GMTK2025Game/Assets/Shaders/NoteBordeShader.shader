Shader "Sprite/NoteBordeShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _BordeColor ("Borde Color", Color) = (1,1,1,1)
        _BordeSize ("Borde Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _BordeColor;
            float _BordeSize;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 pixelSize = float2(_BordeSize / _ScreenParams.x, _BordeSize / _ScreenParams.y);
                float alpha = tex2D(_MainTex, i.uv).a;

                // Muestreo en 8 direcciones
                float outline = 0.0;
                outline += tex2D(_MainTex, i.uv + float2(pixelSize.x, 0)).a;
                outline += tex2D(_MainTex, i.uv - float2(pixelSize.x, 0)).a;
                outline += tex2D(_MainTex, i.uv + float2(0, pixelSize.y)).a;
                outline += tex2D(_MainTex, i.uv - float2(0, pixelSize.y)).a;
                outline += tex2D(_MainTex, i.uv + pixelSize).a;
                outline += tex2D(_MainTex, i.uv - pixelSize).a;
                outline += tex2D(_MainTex, i.uv + float2(pixelSize.x, -pixelSize.y)).a;
                outline += tex2D(_MainTex, i.uv + float2(-pixelSize.x, pixelSize.y)).a;

                outline = step(0.01, outline) * step(alpha, 0.01); // Si hay vecinos opacos y el actual es transparente

                float4 texColor = tex2D(_MainTex, i.uv) * _Color;
                float4 finalColor = lerp(texColor, _BordeColor, outline);
                finalColor.a = max(texColor.a, outline); // Asegura que el borde sea visible

                return finalColor;
            }
            ENDCG
        }
    }
}