using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Tetris3D
{
   class LnPiece: Shape
   {
      public LnPiece(int x, int y, int z) 
      {
         blocks[0] = new Cube(new Vector3(x, y+3, z));
         blocks[1] = new Cube(new Vector3(x, y+2, z));
         blocks[2] = new Cube(new Vector3(x, y+1, z));
         blocks[3] = new Cube(new Vector3(x, y, z));
         blocks[0].getCubeTexture = txAqua;
         blocks[1].getCubeTexture = txAqua;
         blocks[2].getCubeTexture = txAqua;
         blocks[3].getCubeTexture = txAqua;
      }
   }
}
