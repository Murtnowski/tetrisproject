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
    //This class represents the J shape and is the color orange
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
