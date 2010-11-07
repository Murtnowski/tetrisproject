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
    /// <summary>
    /// This class represents the S Tetris piece
    /// </summary>
    class SPiece : TetrisPiece
    {
        /// <summary>
        /// Returns the color of the Tetris piece
        /// </summary>
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Green;
            }
        }

        /// <summary>
        /// Returns the type of Tetris piece
        /// </summary>
        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.SBlock;
            }
        }

        /// <summary>
        /// Constructs a new S Tetris piece
        /// </summary>
        /// <param name="referanceLocation">The referance point used to position the Tetris piece</param>
        public SPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        /// <summary>
        /// Constructs a new S Tetris piece
        /// </summary>
        /// <param name="referanceLocation">The referance point used to position the Tetris piece</param>
        /// <param name="orientation">The orientation of the Tetris piece</param>
        public SPiece(Point referanceLocation, Orientations orientation)
            : base(referanceLocation, orientation)
        {
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated north
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated north</returns>
        protected override Point[] pointsForNorthOrientation()
        {
            return this.horizontalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated east
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated east</returns>
        protected override Point[] pointsForEastOrientation()
        {
            return this.verticalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated south
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated south</returns>
        protected override Point[] pointsForSouthOrientation()
        {
            return this.horizontalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated west
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated west</returns>
        protected override Point[] pointsForWestOrientation()
        {
            return this.verticalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated west or east
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated north or south</returns>
        private Point[] horizontalOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);        //   2 3
            newLocation[1] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);    // 1 0
            newLocation[2] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);    
            newLocation[3] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 1);

            return newLocation;
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated north or south
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated north or south</returns>
        private Point[] verticalOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);            //   3 
            newLocation[1] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y + 1);    //   1 2
            newLocation[2] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);        //     0
            newLocation[3] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y + 2);

            return newLocation;
        }
    }
}
