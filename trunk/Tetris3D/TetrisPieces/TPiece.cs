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
    /// This class represents the T Tetris piece
    /// </summary>
    class TPiece : TetrisPiece
    {
        /// <summary>
        /// Returns the color of the Tetris piece
        /// </summary>
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Magenta;
            }
        }

        /// <summary>
        /// Returns the type of Tetris piece
        /// </summary>
        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.TBlock;
            }
        }

        /// <summary>
        /// Returns the vertical height of the Tetris piece as it is orientated
        /// </summary>
        public override int VerticalHeight
        {
            get
            {
                switch (this.Orientation)
                {
                    case Orientations.East: return 3;
                    case Orientations.North: return 2;
                    case Orientations.South: return 2;
                    case Orientations.West: return 3;
                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Constructs a new T Tetris piece
        /// </summary>
        /// <param name="referanceLocation">The referance point used to position the Tetris piece</param>
        public TPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        /// <summary>
        /// Constructs a new T Tetris piece
        /// </summary>
        /// <param name="referanceLocation">The referance point used to position the Tetris piece</param>
        /// <param name="orientation">The orientation of the Tetris piece</param>
        public TPiece(Point referanceLocation, Orientations orientation)
            : base(referanceLocation, orientation)
        {
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated north
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated north</returns>
        protected override Point[] pointsForNorthOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);           //   3
            newLocation[1] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);       // 1 0 2
            newLocation[2] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);       //
            newLocation[3] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);

            return newLocation;
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated east
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated east</returns>
        protected override Point[] pointsForEastOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);           //  3
            newLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);       //  1 2
            newLocation[2] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 1);   //  0 
            newLocation[3] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 2);       //  

            return newLocation;
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated south
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated south</returns
        protected override Point[] pointsForSouthOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);          // 3  1  2
            newLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);      //    0
            newLocation[2] = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y + 1);  
            newLocation[3] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y + 1);

            return newLocation;
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated west
        /// </summary>
        /// <returns>An array of points the Tetris piece occupies when orientated west</returns>
        protected override Point[] pointsForWestOrientation()
        {
            Point[] newLocation = new Point[4];
            newLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);          //    3
            newLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);      //  2 1
            newLocation[2] = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y + 1);  //    0
            newLocation[3] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 2);      

            return newLocation;
        }
    }
}
