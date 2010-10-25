/*
 * Project: Tetris Project
 * Authors: Damon Chastain & Matthew Urtnowski 
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   class LPiece : Shape
   {
      public LPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y + 2, z));
         blocks[1] = new Cube(new Vector3(x, y + 1, z));    //  0
         blocks[2] = new Cube(new Vector3(x, y, z));        //  1
         blocks[3] = new Cube(new Vector3(x + 1, y, z));    //  2 3
         blocks[0].getCubeTexture = txBlue;
         blocks[1].getCubeTexture = txBlue;
         blocks[2].getCubeTexture = txBlue;
         blocks[3].getCubeTexture = txBlue;
      }
   }
}
