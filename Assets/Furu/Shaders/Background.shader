Shader "Custom/Background" {
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    float rand(float2 st)
    {
        return frac(sin(dot(st, float2(12.9898, 78.233))) * 43758.5453);
    }

    float circle(float2 st, float r)
    {
        float d = distance(float2(0.5, 0.5), st);
        return step(r, d);
    }

    // ノコギリ波
    float saw(float t)
    {
        return frac(t - floor(t) - 1);
    }

    // ノコギリ波もどき
    float wave(float t)
    {
        float PI = radians(180);
        float sx = abs(sin(t / 2 * PI)) * step(0, sin((t + 0.0) * PI));
        float cx = abs(cos(t / 2 * PI)) * step(0, cos((t + 0.5) * PI));
        return sx + cx;
    }

    // 参考: https://hacchi-man.hatenablog.com/entry/2020/04/09/220000
    float2 uv_scroll(float2 st, float t)
    {
        return fixed2(frac(st.x), frac(st.y + 1 * t));
    }

    float4 frag(v2f_img i) : SV_Target
    {
        float n = 50; // 縦横の繰り返し数
        float t = _Time.y * 0.5;
        // float t = _Time.y * 0.5 + rand(floor(i.uv * n));

        float2 st = frac(i.uv * n);

        float2 scroll_st = uv_scroll(st, t);
        
        float outer = circle(scroll_st, 0.4 * wave(t));
        float inner = circle(scroll_st, 0.4 * saw(t));

        return lerp(
            float4(0.35, 0.7, 0.8, 0.0),
            float4(0.5, 0.7, 0.8, 1.0),
            inner - outer
        );
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}