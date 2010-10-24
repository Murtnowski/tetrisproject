using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class TPiece : Shape
   {
       private int rotationPosition; //position of rotation

      public TPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y, z));          
         blocks[1] = new Cube(new Vector3(x+1, y, z));        //       2
         blocks[2] = new Cube(new Vector3(x+1, y+1, z));      //     0 1 3
         blocks[3] = new Cube(new Vector3(x+2, y, z));        
         blocks[0].getCubeTexture = txPink;
         blocks[1].getCubeTexture = txPink;
         blocks[2].getCubeTexture = txPink;
         blocks[3].getCubeTexture = txPink;

         rotationPosition = 0;
         type = ShapeType.T;
      }
       public bool Rotate(ref Vector3 cube0, ref Vector3 cube1, ref Vector3 cube2, ref Vector3 cube3)
       { //each piece has 4 rotations it may go through
           if (rotationPosition == 0) //                                         2
           {                          //   INITIAL POSITION IS THIS ->         0 1 3
               cube0.X += 1;    //  0
               cube0.Y += 1;    //  1 2
               cube2.X += 1;    //  3          <-     newly changed position
               cube2.Y -= 1;
               cube3.X -= 1;
               cube3.Y -= 1;

               rotationPosition = 1;
               return true;
           }
           else if (rotationPosition == 1)
           {
               cube0.X += 1;    //  
               cube0.Y -= 1;    //  3 1 0
               cube2.X -= 1;    //    2
               cube2.Y -= 1;
               cube3.X -= 1;
               cube3.Y += 1;

               rotationPosition = 2;
               return true;
           }
           else if (rotationPosition == 2)
           {
               cube0.X -= 1;    //    3
               cube0.Y -= 1;    //  2 1 
               cube2.X -= 1;    //    0
               cube2.Y += 1;
               cube3.X += 1;
               cube3.Y += 1;

               rotationPosition = 3;
               return true;
           }
           else if (rotationPosition == 3)
           {
               cube0.X -= 1;    //    2
               cube0.Y += 1;    //  0 1 3
               cube2.X += 1;
               cube2.Y += 1;
               cube3.X += 1;
               cube3.Y -= 1;

               rotationPosition = 0;
               return true;
           }
           else
               return false; //something went wrong
       }
   }
}
