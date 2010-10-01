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

namespace Tetris3D
{
    class Cube
    {
        public Vector3 shapePosition;
        private VertexPositionNormalTexture[] shapeVertices;
        private VertexBuffer vertexBuffer;
        public Texture2D shapeTexture;

        /**Cube constructor
         * */
        public Cube(Vector3 position)
        {
            this.shapePosition = position;
        }

        public static short[] triangleIndices = 
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

        public static Vector3[] corner =
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

        public static Vector3[] cubeCorner =
        {
            corner[0],corner[1],corner[2],corner[3],    //cube1 front
            corner[1],corner[5],corner[6],corner[2],    //cube2 right
            corner[1],corner[5],corner[4],corner[0],    //cube3 top
            corner[7],corner[3],corner[2],corner[6],    //cube4 bottom
            corner[0],corner[4],corner[7],corner[3],    //cube5 left
            corner[7],corner[6],corner[5],corner[4]     //cube6 back
        };

        public static Vector3[] lighting =
        {
                new Vector3(0.0f, 0.0f, 1.0f),      //front
                new Vector3(1.0f, 0.0f, 0.0f),      //right
                new Vector3(0.0f, 1.0f, 0.0f),      //top
                new Vector3(0.0f, -1.0f, 0.0f),     //bottom
                new Vector3(-1.0f, 0.0f, 0.0f),     //left
                new Vector3(0.0f, 0.0f, -1.0f)      //back
        };

        public static short[] pos =
        {
                0,0,0,0,
                1,1,1,1,
                2,2,2,2,
                3,3,3,3,
                4,4,4,4,
                5,5,5,5,
                6,6,6,6
        };

        public static Vector2[] tx =
        { //texture mapping
            new Vector2(0f, 0f),   // Top left of texture
            new Vector2(1f, 0f),   // Top right
            new Vector2(1f, 1f),   // Bottom right 
            new Vector2(0f, 1f),   // Bottom left
        };

        Vector2[] cubeTexture =
        {
            tx[0],tx[1],tx[2],tx[3],    //front
            tx[0],tx[1],tx[2],tx[3],    //right
            tx[0],tx[1],tx[2],tx[3],    //top
            tx[0],tx[1],tx[2],tx[3],    //bottom
            tx[0],tx[1],tx[2],tx[3],    //left
            tx[0],tx[1],tx[2],tx[3]     //back
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
