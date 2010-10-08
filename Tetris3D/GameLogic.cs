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
         switch (number)
         {
            case 1:
               currentShape = new TriPiece(0, 10, 0);//new Shape(ShapeType.TRI);
               break;
            case 2:
               currentShape = new LnPiece(0, 10, 0);//new Shape(ShapeType.LN);
               break;
            case 3:
               currentShape =  new SqrPiece(0,10,0);//new Shape(ShapeType.SQR);
               break;
            case 4:
               currentShape = new LlPiece(0,10,0);//new Shape(ShapeType.LL);
               break;
            case 5:
               currentShape = new RlPiece(0,10,0);//new Shape(ShapeType.RL);
               break;
            case 6:
               currentShape = new Zpiece(0,10,0);//new Shape(ShapeType.Z);
               break;
            case 7:
               currentShape = new Spiece(0, 10, 0);//new Shape(ShapeType.S);
               break;
         }
         return currentShape;
      }
   }
}
