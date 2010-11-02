﻿/*
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

        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.IBlock;
            }
        }

        public TPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        public TPiece(Point referanceLocation, Orentations orentation)
            : base(referanceLocation, orentation)
        {
        }

        protected override void orentateNorth()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);
            newLocation[1] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);
            newLocation[2] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);
            newLocation[3] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);

            this.pieceLocations = newLocation;
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
