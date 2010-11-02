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
    //This class represents the I shape and is the color aqua
    class IPiece : TetrisPiece
    {
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Cyan;
            }
        }

        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.IBlock;
            }
        }

      public IPiece(Point referanceLocation)
           : base(referanceLocation)
      {
      }

       public IPiece(Point referanceLocation, Orentations orentation)
           : base(referanceLocation, orentation)
       {
       }

       protected override void orentateNorth()
       {
           List<Point> newPieceLocations = new List<Point>();

           newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y));
           newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 1));
           newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 2));
           newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 3));

           this.pieceLocations = newPieceLocations;
       }

       protected override void orentateEast()
       {
           throw new NotImplementedException();
       }

       protected override void orentateSouth()
       {
           throw new NotImplementedException();
       }

       protected override void orentateWest()
       {
           throw new NotImplementedException();
       }
   }
}
