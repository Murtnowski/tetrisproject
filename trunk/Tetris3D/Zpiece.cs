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
    class ZPiece : TetrisPiece
    {
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Red;
            }
        }

        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.IBlock;
            }
        }

        public ZPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        public ZPiece(Point referanceLocation, Orientations orientation)
            : base(referanceLocation, orientation)
        {
        }

        protected override Point[] pointsForNorthOrientation()
        {
            return this.horizontalOrientation();
        }

        protected override Point[] pointsForEastOrientation()
        {
            return this.verticalOrientation();
        }

        protected override Point[] pointsForSouthOrientation()
        {
            return this.horizontalOrientation();
        }

        protected override Point[] pointsForWestOrientation()
        {
            return this.verticalOrientation();
        }

        private Point[] horizontalOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);         //3  2
            newLocation[1] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);     //   0  1
            newLocation[2] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);     //  
            newLocation[3] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y + 1); //  

            return newLocation;
        }

        private Point[] verticalOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);         //    3
            newLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);     //  1 2  
            newLocation[2] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 1); //  0
            newLocation[3] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 2); //  

            return newLocation;
        }
    }
}
