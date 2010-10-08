using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris3D
{
   class Cube
   {
      private Vector3 shapePosition;
      private VertexPositionNormalTexture[] shapeVertices;
      private VertexBuffer vertexBuffer;
      private Texture2D shapeTexture;

      public Vector3 getShapePosition
      {
         get { return shapePosition; }
         set { shapePosition = value; }
      }

      public Texture2D getShapeTexture
      {
         get { return shapeTexture; }
         set { shapeTexture = value; }
      }

      public Vector2[] getCubeTexture
      {
         get { return cubeTexture; }
         set { cubeTexture = value; }
      }

      public Cube(Vector3 position)
      {
         this.shapePosition = position;
      }

      private static short[] triangleIndices = 
		{
            0,1,2,
            2,3,0,
            4,5,6,
            4,6,7,
            8,11,10,
            8,10,9,
            12,13,14,
            12,14,15,
            16,19,17,
            19,18,17,
            20,21,22,
            20,22,23,
		};

      private static Vector3[] corner =
        {
                new Vector3(-0.5f, 0.5f, 0.0f),      //front TL
                new Vector3(0.5f, 0.5f, 0.0f),       //front TR
                new Vector3(0.5f, -0.5f, 0.0f),      //front BR
                new Vector3(-0.5f, -0.5f, 0.0f),     //front BL
                new Vector3(-0.5f, 0.5f, -1.0f),     //back TL
                new Vector3(0.5f, 0.5f, -1.0f),      //back TR
                new Vector3(0.5f, -0.5f, -1.0f),     //back BR
                new Vector3(-0.5f, -0.5f, -1.0f)     //back BL
        };

      private static Vector3[] cubeCorner =
        {
            corner[0],corner[1],corner[2],corner[3],    //cube1 front
            corner[1],corner[5],corner[6],corner[2],    //cube2 right
            corner[1],corner[5],corner[4],corner[0],    //cube3 top
            corner[7],corner[3],corner[2],corner[6],    //cube4 bottom
            corner[0],corner[4],corner[7],corner[3],    //cube5 left
            corner[7],corner[6],corner[5],corner[4]     //cube6 back
        };

      private static Vector3[] lighting =
        {
                new Vector3(0.0f, 0.0f, 1.0f),      //front
                new Vector3(1.0f, 0.0f, 0.0f),      //right
                new Vector3(0.0f, 1.0f, 0.0f),      //top
                new Vector3(0.0f, -1.0f, 0.0f),     //bottom
                new Vector3(-1.0f, 0.0f, 0.0f),     //left
                new Vector3(0.0f, 0.0f, -1.0f)      //back
        };

      private static short[] pos =
        {
                0,0,0,0,
                1,1,1,1,
                2,2,2,2,
                3,3,3,3,
                4,4,4,4,
                5,5,5,5,
                6,6,6,6
        };

      private static Vector2[] tx =
        {
            new Vector2(.91f, 0f),   
            new Vector2(1f, 0f), 
            new Vector2(1f, 1f), 
            new Vector2(.91f, 1f),
        };

      private Vector2[] cubeTexture =
        {
            tx[0],tx[1],tx[2],tx[3],    
            tx[0],tx[1],tx[2],tx[3],    
            tx[0],tx[1],tx[2],tx[3],    
            tx[0],tx[1],tx[2],tx[3],    
            tx[0],tx[1],tx[2],tx[3],    
            tx[0],tx[1],tx[2],tx[3]    
        };



      private void BuildShape()
      {
         shapeVertices = new VertexPositionNormalTexture[24];

         for (int i = 0; i < 24; i++)
         {
            shapeVertices[i] = new VertexPositionNormalTexture(shapePosition +
                cubeCorner[i], lighting[pos[i]], cubeTexture[i]);
         }
      }

      public void RenderShape(GraphicsDevice device)
      {
         BuildShape();

         vertexBuffer = new VertexBuffer(device,
             VertexPositionNormalTexture.SizeInBytes * shapeVertices.Length,
             BufferUsage.WriteOnly);

         vertexBuffer.SetData(shapeVertices);

         device.VertexDeclaration = new VertexDeclaration(
            device, VertexPositionNormalTexture.VertexElements);

         device.Vertices[0].SetSource(vertexBuffer, 0,
             VertexPositionNormalTexture.SizeInBytes);

         device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, shapeVertices, 0, 24, triangleIndices, 0, 12);
      }
   }
}
