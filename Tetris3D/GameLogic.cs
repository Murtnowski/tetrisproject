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
      private Random generator;
      private int level;
      private int score;

      public GameLogic()
      {
         generator = new Random();
         this.score = 0;
         this.level = 1;
      }

      public int whichLevel
      {
         get
         { return level; }
         set
         { this.level = value; }
      }

      public int currentScore
      {
         get
         { return score; }
         set
         { this.score = value; }
      }

      public Shape newShape()
      {
         int number = generator.Next(7) + 1;
         Shape currentShape = new Shape();
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
