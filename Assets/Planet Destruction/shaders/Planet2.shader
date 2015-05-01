Shader "PlanetShader2" {
   Properties {
      _MainTex ("Texture Image", 2D) = "white" {} 
      _MaskTex ("Mask Texture Image", 2D) = "white" {} 
	  _ShellColor("Color of outere layer", Color)= (0.2,0.7,0,1)
	  _SkyColor("Color of Horizon Line", Color)= (0,0.7,0.9,1)
	  Radius("Radius",float)=10

   }
    SubShader {
      Pass {	
         GLSLPROGRAM
 
         uniform sampler2D _MainTex;	
         uniform sampler2D _MaskTex;	
		 uniform vec4 _ShellColor;
		 uniform vec4 _SkyColor;
		 uniform float Radius;
         varying vec2 textureCoordinates; 
         #ifdef VERTEX
         void main()
         {
            textureCoordinates = gl_Vertex.xy;
            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
         }
         #endif
 
         #ifdef FRAGMENT
         void main()
         {     
			//(0,0,1)=>sky
			//(0,1,0)=>Planet core
			//(1,0,0)=>planet outer layer
			float u=(textureCoordinates.x/Radius)/2+0.5;
			float v=(textureCoordinates.y/Radius)/2+0.5;
			vec2 uv=vec2(u,v);
			vec4 contourID=texture2D(_MaskTex, uv);
			 
			gl_FragColor=texture2D(_MainTex, textureCoordinates)*contourID.y+_SkyColor*contourID.z+_ShellColor*contourID.x;
			if(contourID.x>0&&contourID.y>0)
			{
				gl_FragColor=(1-contourID.x)*texture2D(_MainTex,textureCoordinates);//+_ShellColor*contourID.x;
			}
         }

         #endif
 
         ENDGLSL
      }
   }
   // The definition of a fallback shader should be commented out 
   // during development:
    Fallback "Unlit/Texture"
}