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
    class OPiece : TetrisPiece
    {
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Yellow;
            }
        }

        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.IBlock;
            }
        }

        public OPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        public OPiece(Point referanceLocation, Orentations orentation)
            : base(referanceLocation, orentation)
        {
        }

        protected override Point[] pointsForNorthOrentation()
        {
            return this.singularOrentation();
        }

        protected override Point[] pointsForEastOrentation()
        {
            return this.singularOrentation(); ;
        }

        protected override Point[] pointsForSouthOrentation()
        {
            return this.singularOrentation();
        }

        protected override Point[] pointsForWestOrentation()
        {
            return this.singularOrentation();
        }

        private Point[] singularOrentation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);
            newLocation[1] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);
            newLocation[2] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);
            newLocation[3] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 1);

            return newLocation;
        }
    }
}