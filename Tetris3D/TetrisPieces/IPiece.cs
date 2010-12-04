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
    /// This class represents the I Tetris piece
    /// </summary>
    class IPiece : TetrisPiece
    {
        /// <summary>
        /// Returns the color of the Tetris piece
        /// </summary>
        public override TetrisColors Color
        {
            get
            {
                return TetrisColors.Cyan;
            }
        }

        /// <summary>
        /// Returns the type of Tetris piece
        /// </summary>
        public override TetrisPieces Type
        {
            get
            {
                return TetrisPieces.IBlock;
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
                    case Orientations.East: return 1;
                    case Orientations.North: return 4;
                    case Orientations.South: return 4;
                    case Orientations.West: return 1;
                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Constructs a new J Tetris piece
        /// </summary>
        /// <param name="referanceLocation">The referance point used to position the Tetris piece</param>
        public IPiece(Point referanceLocation)
            : base(referanceLocation)
        {
        }

        /// <summary>
        /// Constructs a new J Tetris piece
        /// </summary>
        /// <param name="referanceLocation">The referance point used to position the Tetris piece</param>
        /// <param name="orientation">The orientation of the Tetris piece</param>
        public IPiece(Point referanceLocation, Orientations orientation)
            : base(referanceLocation, orientation)
        {
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated north
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated north</returns>
        protected override Point[] pointsForNorthOrientation()
        {
            return this.verticalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated east
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated east</returns>
        protected override Point[] pointsForEastOrientation()
        {
            return this.horizontalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated south
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated south</returns>
        protected override Point[] pointsForSouthOrientation()
        {
            return this.verticalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated west
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated west</returns>
        protected override Point[] pointsForWestOrientation()
        {
            return this.horizontalOrientation();
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated north or south
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated north or south</returns>
        private Point[] verticalOrientation()
        {
            Point[] pieceVerticalLocation = new Point[4];
            pieceVerticalLocation[0] = new Point(this.referanceLocation.X, this.referanceLocation.Y);
            pieceVerticalLocation[1] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);
            pieceVerticalLocation[2] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 2);
            pieceVerticalLocation[3] = new Point(this.referanceLocation.X, this.referanceLocation.Y + 3);

            return pieceVerticalLocation;
        }

        /// <summary>
        /// The location the Tetris piece occupies when orientated west or east
        /// </summary>
        /// <returns>An array of the points the Tetris piece occupies when orientated west or east</returns>
        private Point[] horizontalOrientation()
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
