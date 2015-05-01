Shader "PlanetShader" {
   Properties {
      _MainTex ("Texture Image", 2D) = "white" {} 
      _ShellTex ("Shell Texture Image", 2D) = "white" {} 
      _MaskTex ("Mask Texture Image", 2D) = "white" {} 
	  Radius("Radius",float)=10
	  NumOfSegments("Number of Segments ",float)=5
   }
   SubShader {
      Pass {	
         GLSLPROGRAM
 
         uniform sampler2D _MainTex;	
         uniform sampler2D _ShellTex;	
         uniform sampler2D _MaskTex;	
		 uniform float Radius;
		 uniform float NumOfSegments;
         varying vec4 textureCoordinates; 
		 float PI=3.141593;
		 float PI2=6.2831853;
         #ifdef VERTEX
         void main()
         {
            textureCoordinates = gl_Vertex;
            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
         }
         #endif
 
         #ifdef FRAGMENT
         void main()
         {
		    vec2 uv=vec2(textureCoordinates);
			float sqrDistance=textureCoordinates.x*textureCoordinates.x+textureCoordinates.y*textureCoordinates.y;
			if(sqrDistance>Radius*Radius){
				//recalculate uv
				float v=sqrt(sqrDistance)-Radius;//-Readius if you set the texture wrap mode to clamp if not remove it
				//float u=(atan(textureCoordinates.y,textureCoordinates.x)/(PI2))*NumOfSegments;
				float u=mod(float((atan(textureCoordinates.y,textureCoordinates.x)/(PI2))*NumOfSegments),float(0.9));
				//float u=(textureCoordinates.x/Radius)/2+0.5;
				//float v=(textureCoordinates.y/Radius)/2+0.5;
				uv=vec2(u,v);
				gl_FragColor = texture2D(_ShellTex, uv);	         
			}
            else 
				gl_FragColor = texture2D(_MainTex, uv);	   

			float u=(textureCoordinates.x/Radius)/2+0.5;
			float v=(textureCoordinates.y/Radius)/2+0.5;
			uv=vec2(u,v);
			//gl_FragColor = texture2D(_MaskTex, uv);      
			vec4 contour=texture2D(_MaskTex, uv); 
			vec4 shellColor=vec4(0,0,0,0);
			vec4 innerColor=vec4(0,0,0,0);
			float factor=1;
			if(contour.x>=0.5)
			{
				//outer shell
				//float v=sqrt(sqrDistance)-Radius;//-Readius if you set the texture wrap mode to clamp if not remove it
				//float u=mod(float((atan(textureCoordinates.y,textureCoordinates.x)/(PI2))*NumOfSegments),float(0.9));
				//uv=vec2(u,v);
				//gl_FragColor = texture2D(_ShellTex, uv);	
				//gl_FragColor = vec4(0,0.7,0,0);	
				shellColor=vec4(0.3,0.64,0,0);	
				gl_FragColor=shellColor;
			}
			else
			{
				if(contour.y>=0.1){
					//inner layer
					//gl_FragColor = texture2D(_MainTex, vec2(textureCoordinates));      
					innerColor = texture2D(_MainTex, vec2(textureCoordinates)); 
					//shellColor=vec4(0,0,0,0);	
					gl_FragColor=contour.x*shellColor+(1-contour.x)*innerColor;
					//gl_FragColor=(1-contour.x)*innerColor;
				}
				else
				{
					//outer layer
					//vec4 skyColor = vec4(0.2,0.77,1,0); 
					//shellColor=vec4(0.3,0.64,0,0);	
					//gl_FragColor=contour.x*shellColor*factor+(1-contour.x)*skyColor*factor;
					//gl_FragColor=skyColor;
				}
			}
			gl_FragColor=contour.x*shellColor+(1-contour.x)*innerColor;

         }

         #endif
 
         ENDGLSL
      }
   }
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Unlit/Texture"
}