Shader "Custom/RedTintShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RedAmount ("Red Amount", Range(0, 1)) = 0
        _TintColor ("Tint Color", Color) = (1,0,0,1) // Color del tinte
        _CenterIntensity ("Center Intensity", Range(0, 1)) = 0.5 // Intensidad del tinte en el centro
        _EdgeIntensity ("Edge Intensity", Range(0, 1)) = 1.0 // Intensidad del tinte en los bordes
        _NoiseTex ("Noise Texture", 2D) = "white" {}


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
            float _RedAmount;
            float4 _TintColor; // Nueva variable para el color del tinte
            float _CenterIntensity; // Nueva variable para la intensidad en el centro
            float _EdgeIntensity; // Nueva variable para la intensidad en los bordes
            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;


            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

fixed4 frag (v2f i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv);
    float distanceToCenter = length(i.uv - 0.5);
    float redTint = _RedAmount * smoothstep(_CenterIntensity, _EdgeIntensity, distanceToCenter);
    // Obtener el valor de ruido
    fixed noiseValue = tex2D(_NoiseTex, i.uv * _NoiseTex_ST.xy).r;
    // Mezclar el ruido con el color del tinte
    col.rgb = lerp(col.rgb, _TintColor.rgb * noiseValue, redTint);
    return col;
}

            ENDCG
        }
    }
    FallBack "Diffuse"
}
