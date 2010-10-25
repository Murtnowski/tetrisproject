/*
 * Project: Tetris Project
 * Authors: Damon Chastain & Matthew Urtnowski 
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */
using System;

namespace Tetris3D
{
   static class Program
   {
      /// The main entry point for the application.
      static void Main(string[] args)
      {
         using (Game1 game = new Game1())
         {
            game.Run();
         }
      }
   }
}

