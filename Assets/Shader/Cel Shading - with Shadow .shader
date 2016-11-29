
Shader "Toon Shadingz - with" 
{

	//Define the properties of the shader.
	Properties
	{
		_MainTex("Main Texture", 2D) = "texzture" {}
		_ShadowColor("Shadow's Color", Color) = (25,25,25,1)
		_UnlitColor("Unlit Color", Color) = (0.5,0.5,0.5,1)
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_DiffuseThreshold("Lighting Threshold", Range(-1.1,1)) = 0.1
		_Shininess("Shininess", Range(0.5,1)) = 1
		_OutlineThickness("Outline Thickness", Range(0,1)) = 0.1
	}

	SubShader
	{
		Pass 
		{   
			//This pass handles rendering of shadows on objects that has this shader attached to their material.
			Tags { "LightMode" = "ForwardBase" } 

			//Define an offset, to make sure the shadows are rendered on top of the shadow receiver.
			Offset -1.0, -2.0 
 
			//Start the CG code.
			CGPROGRAM
 	        #pragma vertex vert 
		    #pragma fragment frag
 
			/* Uniform variables get their initial values from an environment that is external to the shader code.
			 These are the colour or the shadows, and the transformation from world coordinates to the coordinate system of the plane*/
			uniform float4 _ShadowColor;
			uniform float4x4 _World2Receiver;
	
			//The vertex shader
			float4 vert(float4 vertexPos : POSITION) : SV_POSITION
			{
				//Get the object coordinates in world space and object space.
				//Calculate the viewmatrix as the multiplication of the object view matrix with the object space coordinates.
				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject; 
				float4x4 viewMatrix = mul(UNITY_MATRIX_MV, modelMatrixInverse);
 
				//Calculate the light direction.
				float4 lightDirection;
				if (0.0 != _WorldSpaceLightPos0.w) 
				{
					/*if the alpha of the world space light is not zero, calculate the light direction as the normalised 
					multiplication of the model matrix with the direction from the light to the vertex we're currently handling.*/
					lightDirection = normalize(mul(modelMatrix, vertexPos - _WorldSpaceLightPos0));
				} 
				else
				{
					//if it is, set the light direction to the negative normalisation of the directional light's direction.
					lightDirection = -normalize(_WorldSpaceLightPos0); 
				}
 
				//Calculate the position of the vertex in world space.
				float4 vertexInWorldSpace = mul(modelMatrix, vertexPos);

				//Get the first row of the world2Receiver
				float4 world2ReceiverRow1 = float4(_World2Receiver[1][0], _World2Receiver[1][1], _World2Receiver[1][2], _World2Receiver[1][3]);
				
				//Calculate the height of the shadow, over the plane.
				float distanceOfVertex = dot(world2ReceiverRow1, vertexInWorldSpace);  
				//And how far the light goes in the y direction.
				float lengthOfLightDirectionInY = dot(world2ReceiverRow1, lightDirection);
 

	            if (distanceOfVertex > 0.0 && lengthOfLightDirectionInY < 0.0)
		        {
					/*if the shadow is floating above the plane and the light in the y direction is less than 0,
					adjust the vertex.*/
					lightDirection = lightDirection * (distanceOfVertex / (-lengthOfLightDirectionInY));
				}
				else
				{
					//else, don't adjust it.
					lightDirection = float4(0.0, 0.0, 0.0, 0.0);
				}

				/*Return the result of the vertex shader: A multiplication of the object viewMatrix 
				with the sum of the vertex world coordinates and the light direction.*/
				return mul(UNITY_MATRIX_VP, vertexInWorldSpace + lightDirection);
			}
 
			//Colour the shadow in the colour that shadows have, from the lightsource in unity.
			float4 frag(void) : COLOR 
			{
				return _ShadowColor;
			}
 
			ENDCG 
		}

		Pass
		{
			//This pass handles the general Cel shading.
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			//== User defined ==//

			//TOON SHADING UNIFORMS
			uniform float4 _UnlitColor;
			uniform float _DiffuseThreshold;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float _OutlineThickness;


			//== UNITY defined ==//
			uniform float4 _LightColor0;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;

			struct vertexInput {

				//TOON SHADING VAR
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct vertexOutput {

				float4 pos : SV_POSITION;
				float3 normalDir : TEXCOORD1;
				float4 lightDir : TEXCOORD2;
				float3 viewDir : TEXCOORD3;
				float2 uv : TEXCOORD0;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				//normalDirection
				output.normalDir = normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);

				//World position
				float4 posWorld = mul(unity_ObjectToWorld, input.vertex);

				//view direction
				output.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz); 
				//vector from object to the camera

				//light direction
				float3 fragmentToLightSource = (_WorldSpaceCameraPos.xyz - posWorld.xyz);
				output.lightDir = float4(normalize(lerp(_WorldSpaceLightPos0.xyz , fragmentToLightSource, _WorldSpaceLightPos0.w)),lerp(1.0 , 1.0 / length(fragmentToLightSource), _WorldSpaceLightPos0.w)
				);

				//fragmentInput output;
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);

				//UV-Map
				output.uv = input.texcoord;

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{

				float nDotL = saturate(dot(input.normalDir, input.lightDir.xyz));

				//Diffuse threshold calculation
				float diffuseCutoff = saturate((max(_DiffuseThreshold, nDotL) - _DiffuseThreshold) * 1000);

				//Specular threshold calculation
				float specularCutoff = saturate(max(_Shininess, dot(reflect(-input.lightDir.xyz, input.normalDir), input.viewDir)) - _Shininess) * 1000;

				//Calculate Outlines
				float outlineStrength = saturate((dot(input.normalDir, input.viewDir) - _OutlineThickness) * 1000);


				float3 ambientLight = (1 - diffuseCutoff) * _UnlitColor.xyz; //adds general ambient illumination
				float3 diffuseReflection = (1 - specularCutoff) * tex2D(_MainTex, input.uv)* diffuseCutoff;
				float3 specularReflection = _SpecColor.xyz * specularCutoff;

				float3 combinedLight = (ambientLight + diffuseReflection) * outlineStrength + specularReflection;

				return float4(combinedLight, 1.0); //+tex2D(_MainTex, input.uv); // DELETE LINE COMMENTS & ';' TO ENABLE TEXTURE
			}
		ENDCG
		}
	}

    Fallback "VertexLit"
}
