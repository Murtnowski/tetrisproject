using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class RlPiece : Shape
   {
      public RlPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y + 2, z));
         blocks[1] = new Cube(new Vector3(x, y + 1, z));
         blocks[2] = new Cube(new Vector3(x, y, z));
         blocks[3] = new Cube(new Vector3(x + 1, y, z));
         blocks[0].getCubeTexture = txBlue;
         blocks[1].getCubeTexture = txBlue;
         blocks[2].getCubeTexture = txBlue;
         blocks[3].getCubeTexture = txBlue;
      }
   }
}
