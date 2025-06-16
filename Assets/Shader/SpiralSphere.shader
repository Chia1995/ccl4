Shader "Custom/SpiralSphere"
{
    Properties
    {
        _TimeScale("Time Scale", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" }
        Cull Front
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float _TimeScale;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 dir : TEXCOORD0;
            };

            v2f vert(float4 vertex : POSITION)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.dir = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, vertex.xyz));
                return o;
            }

            float3 linColor(float value)
            {
                value = fmod(value * 6.0, 6.0);
                float3 color;
                color.r = 1.0 - clamp(value - 1.0, 0.0, 1.0) + clamp(value - 4.0, 0.0, 1.0);
                color.g = clamp(value, 0.0, 1.0) - clamp(value - 3.0, 0.0, 1.0);
                color.b = clamp(value - 2.0, 0.0, 1.0) - clamp(value - 5.0, 0.0, 1.0);
                return color;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.dir);
                float angle = atan2(dir.y, dir.x);
                float len = length(dir.xz);

                float tauTime = fmod(_Time.y * _TimeScale, 6.28318530718);
                float powLen = 1.0 / sqrt(len + 0.0001);
                float arms = 4.0;

                float sine = sin(powLen * 16.0 + angle * arms - tauTime * 8.0);
                sine = abs(sine);
                sine = sqrt(sine);

                float fractTime = frac(_Time.y * _TimeScale);
                float3 col = linColor(powLen - fractTime) * sine;

                return float4(col, 1.0);
            }
            ENDCG
        }
    }
}
