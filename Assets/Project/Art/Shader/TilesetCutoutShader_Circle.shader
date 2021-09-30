Shader "Infinity Code/Online Maps/Tileset Cutout Circle"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_OverlayBackTex("Overlay Back Texture", 2D) = "black" {}
		_OverlayBackAlpha("Overlay Back Alpha", Range(0, 1)) = 1
		_TrafficTex("Traffic Texture", 2D) = "black" {}
		_OverlayFrontTex("Overlay Front Texture", 2D) = "black" {}
		_OverlayFrontAlpha("Overlay Front Alpha", Range(0, 1)) = 1
		_Radius("Radius", Float) = 0.5
	}

	SubShader
	{
		Tags {"Queue" = "AlphaTest-300" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
		LOD 200
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alphatest 
		#pragma target 3.5

		sampler2D _MainTex;
		sampler2D _OverlayBackTex;
		half _OverlayBackAlpha;
		sampler2D _TrafficTex;
		sampler2D _OverlayFrontTex;
		half _OverlayFrontAlpha;
		fixed4 _Color;
		float _Radius;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_OverlayBackTex;
			float2 uv_TrafficTex;
			float2 uv_OverlayFrontTex;
			float3 my_vertPos;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.my_vertPos = v.vertex;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

			fixed4 t = tex2D(_OverlayBackTex, IN.uv_OverlayBackTex);
			fixed3 ct = lerp(c.rgb, t.rgb, t.a * _OverlayBackAlpha);

			t = tex2D(_TrafficTex, IN.uv_TrafficTex);
			ct = lerp(ct, t.rgb, t.a);

			t = tex2D(_OverlayFrontTex, IN.uv_OverlayFrontTex);
			ct = lerp(ct, t.rgb, t.a * _OverlayFrontAlpha);

			ct = ct * _Color;
			o.Albedo = ct;

			float alpha = -1;
			float len = length(float3(IN.my_vertPos.x, 0, IN.my_vertPos.z) + float3(_Radius / 2, 0, -_Radius / 2));
			if (len < _Radius / 2)
				alpha = 1;
			else
				alpha = -1;

			clip(alpha);

		}
		ENDCG

	}
	Fallback "Transparent/Cutout/Diffuse"
}
