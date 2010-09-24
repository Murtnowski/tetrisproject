using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TetrisProject
{
    class Cube
    {
        public Vector3 shapeSize;
        public Vector3 shapePosition;
        private VertexPositionNormalTexture[] shapeVertices;
        public IndexBuffer indexBuffer;
        private VertexBuffer vertexBuffer;
        public Texture2D shapeTexture;
        private VertexBuffer shapeBuffer; //holds the vertices to display

        public static short[] triangleIndices = 
		{
            0,1,2,2,3,0,
            4,5,6,4,6,7,
            8,11,10,8,10,9,
            12,13,14,12,14,15,
            16,19,17,19,18,17,
            20,21,22,20,22,23,
		};

        public static Vector2[] texture = 
            {
            new Vector2(.5f, 0f), // Green
            new Vector2(1f, 0f), 
            new Vector2(1f, .5f), 
            new Vector2(.5f, .5f),
            new Vector2(0f, .5f), // Blue
            new Vector2(.5f, .5f), 
            new Vector2(.5f, 1f), 
            new Vector2(0f, 1f),
            new Vector2(.5f, .5f), // White
            new Vector2(1f, .5f), 
            new Vector2(1f, 1f), 
            new Vector2(.5f, 1f),
            };
        public static Vector3[] normal =
            {
                new Vector3(0.0f, 0.0f, 1.0f), //front
                new Vector3(1.0f, 0.0f, 0.0f), //right
                new Vector3(0.0f, 1.0f, 0.0f), //top
                new Vector3(0.0f, -1.0f, 0.0f), //bottom
                new Vector3(-1.0f, 0.0f, 0.0f), //left
                new Vector3(0.0f, 0.0f, -1.0f) //back
            };
        public static Vector3[] corner =
            {
                new Vector3(-0.5f, 0.5f, 0.0f), //front TL
                new Vector3(0.5f, 0.5f, 0.0f), //front TR
                new Vector3(0.5f, -0.5f, 0.0f), //front BR
                new Vector3(-0.5f, -0.5f, 0.0f), //front BL
                new Vector3(-0.5f, 0.5f, -1.0f), //back TL
                new Vector3(0.5f, 0.5f, -1.0f), //back TR
                new Vector3(0.5f, -0.5f, -1.0f), //back BR
                new Vector3(-0.5f, -0.5f, -1.0f) //back BL
            };
        public static int[] cornerPos =
            {
                0,1,2,3, //front
                1,5,6,2, //right
                1,5,4,0, //top
                7,3,2,6, //bottom
                0,4,7,3, //left
                7,6,5,4, //back
            };
        public static int[] texturePos = 
            {
                0,1,2,3,4,5,
                6,7,8,9,10,
                11,8,9,10,11,4,
                5,6,7,0,1,2,3
            };
        public static int[] normalPos =
            {
                0,0,0,0,
                1,1,1,1,
                2,2,2,2,
                3,3,3,3,
                4,4,4,4,
                5,5,5,5,
                6,6,6,6
            };

        public Cube(Vector3 position)
        {
            shapePosition = position;
        }

        private void BuildShape()
        {
            shapeVertices = new VertexPositionNormalTexture[24];

            for (int i = 0; i < 24; i++)
                shapeVertices[i] = new VertexPositionNormalTexture(shapePosition +
                    corner[cornerPos[i]], normal[normalPos[i]], texture[texturePos[i]]);
        }

        public void RenderShape(GraphicsDevice device)
        {
            BuildShape();

            vertexBuffer = new VertexBuffer(device,                                         //does nothing
                VertexPositionNormalTexture.SizeInBytes * shapeVertices.Length,              //does nothing
                BufferUsage.WriteOnly);                                                      //does nothing
            vertexBuffer.SetData(shapeVertices);

            indexBuffer = new IndexBuffer(device,                                        //does nothing
                    typeof(short), 36, BufferUsage.WriteOnly);                              //does nothing
            this.indexBuffer.SetData(triangleIndices);                                       //does nothing


            device.Vertices[0].SetSource(vertexBuffer, 0,
                VertexPositionNormalTexture.SizeInBytes);
            device.VertexDeclaration = new VertexDeclaration(
                device, VertexPositionNormalTexture.VertexElements);
            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                shapeVertices, 0, shapeVertices.Length,
                triangleIndices, 0, triangleIndices.Length / 3);
        }
    }
}