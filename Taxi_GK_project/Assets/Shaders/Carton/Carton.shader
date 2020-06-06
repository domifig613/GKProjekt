Shader "Holistic/Carton"{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("Outline Width", Range(.002, 0.1)) = .005
		_Color("ForwardColor", Color) = (0,0,0,0)
	}

		SubShader{
  
			Stencil
			{
				Ref 1
				Comp always
				Pass replace
			}	

			Tags { "Queue" = "Transparent" }
			LOD 100

			Pass
			{
				Cull Off
				ZWrite Off
				ZTest Greater

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
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _Color;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					return _Color;
				}

				ENDCG
			}



			ZWrite off
				
			CGPROGRAM
				#pragma surface surf Lambert vertex:vert

			struct Input {
				float2 uv_MainTex;
			};

			float _OutlineWidth;
			float4 _OutlineColor;

			void vert(inout appdata_full v) {
				v.vertex.xyz += v.normal * _OutlineWidth;
			}

			sampler2D _MainTex;
			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = _OutlineColor.rgb;
				o.Albedo = _OutlineColor.rgb;
			}

			ENDCG

			ZWrite On

			CGPROGRAM
			#pragma surface surf Lambert

			struct Input {
				float2 uv_MainTex;
			};


			sampler2D _MainTex;
			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			}

			ENDCG
		}
			Fallback "Diffuse"
}