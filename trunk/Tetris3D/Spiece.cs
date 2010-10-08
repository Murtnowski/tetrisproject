using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class Spiece : Shape
   {
      public Spiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y, z));
         blocks[1] = new Cube(new Vector3(x + 1, y, z));
         blocks[2] = new Cube(new Vector3(x + 1, y + 1, z));
         blocks[3] = new Cube(new Vector3(x + 2, y + 1, z));
         blocks[0].getCubeTexture = txGreen;
         blocks[1].getCubeTexture = txGreen;
         blocks[2].getCubeTexture = txGreen;
         blocks[3].getCubeTexture = txGreen;
      }
   }
}
