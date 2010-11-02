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
    class SPiece : TetrisPiece
    {
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Green;
            }
        }

        public SPiece(Point referanceLocation)
            : base(TetrisPieces.SBlock, referanceLocation)
        {
        }

        public SPiece(Point referanceLocation, Orentations orentation)
            : base(TetrisPieces.SBlock, referanceLocation, orentation)
        {
        }

        protected override void updatePieceLocations()
        {
            List<Point> newPieceLocations = new List<Point>();

            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X - 1, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 1));
            newPieceLocations.Add(new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 1));

            this.pieceLocations = newPieceLocations;
        }
    }
}
