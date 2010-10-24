using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class Zpiece : Shape
   {
      public Zpiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y+1, z));
         blocks[1] = new Cube(new Vector3(x+1, y+1, z));    //  0 1
         blocks[2] = new Cube(new Vector3(x+1, y, z));      //    2 3
         blocks[3] = new Cube(new Vector3(x+2, y, z));
         blocks[0].getCubeTexture = txRed;
         blocks[1].getCubeTexture = txRed;
         blocks[2].getCubeTexture = txRed;
         blocks[3].getCubeTexture = txRed;
      }
   }
}
