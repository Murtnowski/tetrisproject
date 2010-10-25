﻿/*
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
    //This class represents the I shape and is the color aqua
   class IPiece: Shape
   {
      public IPiece(int x, int y, int z) 
      {
         blocks[0] = new Cube(new Vector3(x, y+3, z));  //  0
         blocks[1] = new Cube(new Vector3(x, y+2, z));  //  1
         blocks[2] = new Cube(new Vector3(x, y+1, z));  //  2
         blocks[3] = new Cube(new Vector3(x, y, z));    //  3
         blocks[0].getCubeTexture = txAqua;
         blocks[1].getCubeTexture = txAqua;
         blocks[2].getCubeTexture = txAqua;
         blocks[3].getCubeTexture = txAqua;
      }
   }
}
