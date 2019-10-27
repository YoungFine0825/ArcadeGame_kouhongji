// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/CircleCut" 
{
	Properties 
	{
		_Radius("_Radius",Range(0,1.2))=0.5
		_MainTex("_MainTex",2D)="white"{}
	}
	SubShader 
	{
		Tags{ "Queue"="Transparent" "IngnoreProjector"="True" "RenderType"="Transparent" }
 
		Pass 
		{
 
			Tags{ "LightMode" = "ForwardBase" }
 
			//必须加入才能实现透明效果
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
 
			CGPROGRAM
 
			#include "UnityCG.cginc"
			#include "Lighting.cginc"  
 
			#pragma vertex vert
			#pragma fragment frag
 
			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			float _Radius;
 
			struct a2v
			{
				float4 position:POSITION ;
				float4 texcoord:TEXCOORD0 ; 
			};
 
			struct v2f
			{
				float4 position:SV_POSITION ;
				float2 texcoord:TEXCOORD0 ; 
			};
 
			v2f vert(a2v v)
			{
				v2f f;
				f.position=UnityObjectToClipPos(v.position) ;
 
				//获取该顶点下的纹理坐标
				f.texcoord=v.texcoord.xy*_MainTex_ST.xy+_MainTex_ST.zw;
 
				return f;
			}
 
			fixed4 frag(v2f f):SV_Target
			{
				//获取纹理坐标下的颜色值
				fixed4 color = tex2D(_MainTex,f.texcoord);
 
				//左上方区域
				if(f.texcoord.x<0.5 && f.texcoord.y>0.5)
				{
					float2 r;
					r.x=0.5-f.texcoord.x;
					r.y=f.texcoord.y-0.5;
					if(length(r)>_Radius)//以r.x、r.y为两直角边长度，计算斜边长度
					{
						return fixed4(1,1,1,0);
					}
					else
					{
						return color;
					}
				}
				//左下方区域
				else if(f.texcoord.x<0.5 && f.texcoord.y<0.5)
				{
					float2 r;
					r.x=0.5-f.texcoord.x;
					r.y=0.5-f.texcoord.y;
					if(length(r)>_Radius)
					{
						return fixed4(1,1,1,0);
					}
					else
					{
						return color;
					}
				}
				//右上方区域
				else if(f.texcoord.x>0.5 && f.texcoord.y>0.5)
				{
					float2 r;
					r.x=f.texcoord.x-0.5;
					r.y=f.texcoord.y-0.5;
					if(length(r)>_Radius)
					{
						return fixed4(1,1,1,0);
					}
					else
					{
						return color;
					}
				}
				//右下方区域
				else if(f.texcoord.x>0.5 && f.texcoord.y<0.5)
				{
					float2 r;
					r.x=f.texcoord.x-0.5;
					r.y=0.5-f.texcoord.y;
					if(length(r)>_Radius)
					{
						return fixed4(1,1,1,0);
					}
					else
					{
						return color;
					}
				}
 
				return color;
			}
 
			ENDCG
		}
	}
 
	Fallback "Diffuse"
}