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
    class TPiece : TetrisPiece
    {
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Magenta;
            }
        }

        public TPiece(Point referanceLocation)
            : base(TetrisPieces.TBlock, referanceLocation)
        {
        }

        public TPiece(Point referanceLocation, Orentations orentation)
            : base(TetrisPieces.TBlock, referanceLocation, orentation)
        {
        }

        protected override void updatePieceLocations()
        {
            List<Point> newPieceLocations = new List<Point>();

            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X - 1, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X + 1, this.referanceLocation.Y));
            newPieceLocations.Add(new Point(this.referanceLocation.X, this.referanceLocation.Y + 1));

            this.pieceLocations = newPieceLocations;
        }
    }
}
