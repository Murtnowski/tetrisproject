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
           this.pieceLocations =  this.verticalOrentation();
       }

       protected override void orentateEast()
       {
           this.pieceLocations = this.horizontalOrentation();
       }

       protected override void orentateSouth()
       {
           this.pieceLocations = this.verticalOrentation();
       }

       protected override void orentateWest()
       {
           this.pieceLocations = this.horizontalOrentation();
       }

       private Point[] verticalOrentation()
       {
           Point[] pieceVerticalLocation = new Point[4];
           pieceVerticalLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);
           pieceVerticalLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);
           pieceVerticalLocation[2] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 2);
           pieceVerticalLocation[3] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 3);

           return pieceVerticalLocation;
       }

       private Point[] horizontalOrentation()
       {
           Point[] pieceVerticalLocation = new Point[4];
           pieceVerticalLocation[0] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);
           pieceVerticalLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y);
           pieceVerticalLocation[2] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);
           pieceVerticalLocation[3] = new Point(this.referanceLocation.X + 2, this.referanceLocation.Y);

           return pieceVerticalLocation;
       }
   }
}
