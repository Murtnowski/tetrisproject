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

        public OPiece(Point referanceLocation, Orientations orientation)
            : base(referanceLocation, orientation)
        {
        }

        protected override Point[] pointsForNorthOrientation()
        {
            return this.singularOrientation();
        }

        protected override Point[] pointsForEastOrientation()
        {
            return this.singularOrientation(); ;
        }

        protected override Point[] pointsForSouthOrientation()
        {
            return this.singularOrientation();
        }

        protected override Point[] pointsForWestOrientation()
        {
            return this.singularOrientation();
        }

        private Point[] singularOrientation()
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