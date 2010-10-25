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
using System.Linq;

namespace Tetris3D
{
   /**
    * The Game class is for level, score and generating random pieces
    */
   class GameLogic
   {
       //A random generator used to create random blocks
       private Random generator;
      
       //The current games level
       private int level;
       //The current games score
       private int score;

       //Constructs a new game logic controller on level 1
      public GameLogic()
      {
         generator = new Random();
         this.score = 0;
         this.level = 1;
      }

       //Returns the current level
      public int whichLevel
      {
         get
         { return level; }
         set
         { this.level = value; }
      }

       //Returns the current score
      public int currentScore
      {
         get
         { return score; }
         set
         { this.score = value; }
      }

       //The is called when a new shape needs to be created
      public Shape newShape()
      {
          //First a random number is rolled to determine the next shape
         int number = generator.Next(7) + 1;
         Shape currentShape = new Shape();

          //The current shape number is then sent through a switch to determine which shape was created
         switch (1)
         {
            case 1:
               currentShape = new TPiece(0, 10, 0);//new Shape(ShapeType.T);
               break;
            case 2:
               currentShape = new IPiece(0, 10, 0);//new Shape(ShapeType.I);
               break;
            case 3:
               currentShape =  new OPiece(0,10,0);//new Shape(ShapeType.O);
               break;
            case 4:
               currentShape = new JPiece(0,10,0);//new Shape(ShapeType.J);
               break;
            case 5:
               currentShape = new LPiece(0,10,0);//new Shape(ShapeType.L);
               break;
            case 6:
               currentShape = new Zpiece(0,10,0);//new Shape(ShapeType.Z);
               break;
            case 7:
               currentShape = new SPiece(0, 10, 0);//new Shape(ShapeType.S);
               break;
         }
         return currentShape;
      }
   }
}
