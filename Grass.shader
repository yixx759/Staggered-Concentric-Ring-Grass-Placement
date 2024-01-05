Shader "Unlit/Grass"
{
    Properties
    {
        _Back ("Back", 2D) = "white" {}
        _Middle ("Middle", 2D) = "white" {}
        _Front ("Front", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"RenderType"="Transparent" "Queue" = "Transparent+500" }
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
     

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
               
                float4 vertex : SV_POSITION;
            };

            sampler2D _Front;
            sampler2D _Middle;
            sampler2D _Back;
            float4 _Front_ST;
            float4 _Middle_ST;
            float4 _Back_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Front);
             
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 Layer1 = tex2D(_Front, i.uv);
                float4 Layer2 = tex2D(_Middle, i.uv);
                float4 Layer3 = tex2D(_Back, i.uv);
                float4 col = Layer3;
                col = lerp(col,Layer2, Layer2.a);
                col = lerp(col,Layer1, Layer1.a);
                
              
                return col;
            }
            ENDCG
        }
    }
}
