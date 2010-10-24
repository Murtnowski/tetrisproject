using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class JPiece : Shape
   {
      public JPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x + 1, y + 2, z));  //
         blocks[1] = new Cube(new Vector3(x + 1, y + 1, z));  //    0
         blocks[2] = new Cube(new Vector3(x + 1, y, z));      //    1
         blocks[3] = new Cube(new Vector3(x, y, z));          //  3 2
         blocks[0].getCubeTexture = txOrange;
         blocks[1].getCubeTexture = txOrange;
         blocks[2].getCubeTexture = txOrange;
         blocks[3].getCubeTexture = txOrange;
      }
   }
}
