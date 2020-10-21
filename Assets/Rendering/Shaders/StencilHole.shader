Shader "Custom/StencilHole" {
	SubShader{
		//the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType"="Transparent" "Queue"="Geometry-3"}

        //stencil operation
		Stencil{
			Ref 2
			Comp Always
			Pass Replace
		}

		ColorMask 0
        ZWrite Off

		Pass{
//            //don't draw color or depth
//			Blend Zero One
//		//	AlphaTest Always
//			//ZWrite Off
//
//			CGPROGRAM
//			#include "UnityCG.cginc"
//
//			#pragma vertex vert
//			#pragma fragment frag
//
//			struct appdata{
//				float4 vertex : POSITION;
//			};
//
//			struct v2f{
//				float4 position : SV_POSITION;
//			};
//
//			v2f vert(appdata v){
//				v2f o;
//				//calculate the position in clip space to render the object
//				o.position = UnityObjectToClipPos(v.vertex);
//				return o;
//			}
//
//			fixed4 frag(v2f i) : SV_TARGET{
//				return half4(0, 0, 0, 0);
//			}
//
//			ENDCG
		}
	}
}