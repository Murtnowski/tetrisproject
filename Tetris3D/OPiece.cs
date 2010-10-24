using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Tetris3D
{
   class OPiece : Shape
   {
      public OPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y + 1, z));
         blocks[1] = new Cube(new Vector3(x, y, z));            // 0 2
         blocks[2] = new Cube(new Vector3(x + 1, y + 1, z));    // 1 3
         blocks[3] = new Cube(new Vector3(x + 1, y, z));
         blocks[0].getCubeTexture = txYellow;
         blocks[1].getCubeTexture = txYellow;
         blocks[2].getCubeTexture = txYellow;
         blocks[3].getCubeTexture = txYellow;
      }
   }
}
