using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class TriPiece : Shape
   {
      public TriPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y, z));
         blocks[1] = new Cube(new Vector3(x+1, y, z));
         blocks[2] = new Cube(new Vector3(x+1, y+1, z));
         blocks[3] = new Cube(new Vector3(x+2, y, z));
         blocks[0].getCubeTexture = txPink;
         blocks[1].getCubeTexture = txPink;
         blocks[2].getCubeTexture = txPink;
         blocks[3].getCubeTexture = txPink;
      }
   }
}
