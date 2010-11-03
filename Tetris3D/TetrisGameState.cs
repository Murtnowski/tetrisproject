using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
   public class TetrisGameState : Microsoft.Xna.Framework.GameComponent
    {
        public TetrisBlock[,] GameBoard = new TetrisBlock[10, 20];
        public List<Point> CurrentPiece = new List<Point>();

        public int Level;
        public int Score;

       public TetrisBlock[,] getGameField
       {
          get{return GameBoard;}

          set{this.GameBoard = value;}
       }

       public TetrisGameState(Game game)
          : base(game)
        {

        }

        public bool isCurrentPiece(Point point)
        {
            foreach (Point currentPoint in this.CurrentPiece)
            {
                if (point == currentPoint)
                {
                    return true;
                }
            }

            return false;
        }

        public bool BlockBelow()
        {
            foreach (Point point in this.CurrentPiece)
            {
                if ((point.Y - 1) < 0 || (!this.isCurrentPiece(new Point(point.X, point.Y - 1)) && this.GameBoard[point.X, (point.Y - 1)] != null))
                {
                    return true;
                }
            }

            return false;
        }

        public bool BlockLeft()
        {
            foreach (Point point in this.CurrentPiece)
            {
                if ((point.X - 1) < 0 || (!this.isCurrentPiece(new Point(point.X - 1, point.Y)) && this.GameBoard[(point.X - 1), point.Y] != null))
                {
                    return true;
                }
            }

            return false;
        }

        public bool BlockRight()
        {
            foreach (Point point in this.CurrentPiece)
            {
                if((point.X + 1) >= this.GameBoard.GetLength(0) || (!this.isCurrentPiece(new Point(point.X + 1, point.Y)) && this.GameBoard[(point.X + 1), point.Y] != null))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
