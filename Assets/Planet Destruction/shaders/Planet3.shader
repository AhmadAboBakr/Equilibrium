Shader "PlanetShader3" {
   Properties {
      _MainTex ("Texture Image", 2D) = "white" {} 
      _CoreTex ("Texture core", 2D) = "white" {} 
      _MaskTex ("Mask Texture Image", 2D) = "white" {} 
	  _ShellColor("Color of outere layer", Color)= (0.2,0.7,0,1)
	  _SkyColor("Color of Horizon Line", Color)= (0,0.7,0.9,1)
	  Radius("Radius",float)=10
	  CoreRadius("Core Radius",float)=5

   }
   SubShader {
      Pass {	
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 

		 uniform sampler2D _MainTex;		
         uniform sampler2D _MaskTex;	
		 uniform float4 _MainTex_ST;
         uniform sampler2D _CoreTex;
		 uniform float4 _CoreTex_ST;

		 uniform float Radius;
		 uniform float CoreRadius;
		 uniform float4 _SkyColor;
		 uniform float4 _ShellColor;
         //varying vec2 textureCoordinates; 
		            // a uniform variable refering to the property above
            // (in fact, this is just a small integer specifying a 
            // "texture unit", which has the texture image "bound" 
            // to it; Unity takes care of this).
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.tex = input.vertex;
               // Unity provides default longitude-latitude-like 
               // texture coordinates at all vertices of a 
               // sphere mesh as the input parameter 
               // "input.texcoord" with semantic "TEXCOORD0".
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
         float4 frag(vertexOutput input) : COLOR
         {
			
			float u=(input.tex.x/Radius)/2+0.5;
			float v=(input.tex.y/Radius)/2+0.5;
			float2 uv= float2(u,v);
			//gl_FragColor = tex2D(_MaskTex, uv);      
			float4 contour=tex2D(_MaskTex, uv); 
			
			float factor=1;
			if(contour.z==0){
				float4 shellColor=float4(0,0,0,0);
				float4 innerColor=float4(0,0,0,0);
				if(contour.x>=0.5)
				{
					shellColor=_ShellColor;	
				}
				else
				{
				if(distance(input.tex.xy,float2(0,0))<CoreRadius){
					innerColor = tex2D(_CoreTex, input.tex*_CoreTex_ST.xy+_CoreTex_ST.zw);
				}
				else{
					innerColor = tex2D(_MainTex, input.tex*_MainTex_ST.xy+_MainTex_ST.zw);
					} 
				}
				return  contour.x*shellColor+(1-contour.x)*innerColor;
			}
			else
			{
				return _SkyColor*contour.z+_ShellColor*contour.x;	
			}
         }
 
         ENDCG
      }
   }
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Unlit/Texture"
   }