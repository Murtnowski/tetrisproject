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
    //This class represents the S shape and is the color green
   class SPiece : Shape
   {
      public SPiece(int x, int y, int z)
      {
         blocks[0] = new Cube(new Vector3(x, y, z));
         blocks[1] = new Cube(new Vector3(x + 1, y, z));        //    2  3
         blocks[2] = new Cube(new Vector3(x + 1, y + 1, z));    //  0 1
         blocks[3] = new Cube(new Vector3(x + 2, y + 1, z));
         blocks[0].getCubeTexture = txGreen;
         blocks[1].getCubeTexture = txGreen;
         blocks[2].getCubeTexture = txGreen;
         blocks[3].getCubeTexture = txGreen;
      }
   }
}
