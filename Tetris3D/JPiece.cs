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
    class JPiece : TetrisPiece
    {
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Blue;
            }
        }

        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.IBlock;
            }
        }

        public JPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        public JPiece(Point referanceLocation, Orentations orentation)
            : base(referanceLocation, orentation)
        {
        }

        protected override void updatePieceLocations()
        {
            List<Point> newPieceLocations = new List<Point>();

            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X - 1, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 1));
            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 2));

            this.pieceLocations = newPieceLocations;
        }
    }
}
