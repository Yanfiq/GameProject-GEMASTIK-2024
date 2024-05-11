Shader "Unlit/Shader1" {
    Properties { //input data
        _ColorA ("Color A", color) = (1, 0, 0, 1)
        _ColorB ("Color B", color) = (0, 0, 1, 1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _ColorA;
            float4 _ColorB;

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct interpolators {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            interpolators vert (MeshData v) {
                interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (interpolators i) : SV_Target {
                return lerp(_ColorA, _ColorB, i.uv.y);
            }
            ENDCG
        }
    }
}
