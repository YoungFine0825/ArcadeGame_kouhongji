// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Unlit Color"
{
    Properties{
        _MainTex("Texture", 2D) = "black"{}
        _Color ("Add Color", float) = (1,1,1,1)
    }
    
    SubShader{
        LOD 100
        Pass{
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
            
            sampler2D _MainTex;
            fixed4 _MainTex_ST;
            fixed4 _Color;
            
            struct vIn{
                half4 vertex:POSITION;
                float2 texcoord:TEXCOORD0;
                fixed4 color:COLOR;
            };
            
            struct vOut{
                half4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
                fixed4 color:COLOR;
            };
            
            vOut vert(vIn v){
                vOut o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }
            
            fixed4 frag(vOut i):COLOR{
                fixed4 tex = tex2D(_MainTex, i.uv);
                return tex * (i.color * _Color);
            }
            ENDCG
        }
    }
}
