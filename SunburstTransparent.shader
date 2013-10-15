Shader "Custom/SunburstTransparent" {
	Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        
        Cull off
		
		CGPROGRAM
		#pragma surface surf Lambert alpha
        
        float4 _Color;

		struct Input {
            half dummy;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
