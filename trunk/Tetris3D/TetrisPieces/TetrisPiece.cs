
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Tetris3D
{
    /// <summary>
    /// An enumeration of the type a Tetris piece may be
    /// </summary>
    /// If you change it be sure to update NUMBER_OF_SUPPORTED_TETRIS_PIECES
    public enum TetrisPieces { TBlock = 0, SBlock, ZBlock, IBlock, JBlock, LBlock, OBlock };

    /// <summary>
    /// An enumeration of the orientations a Tetris piece may face
    /// </summary>
    public enum Orientations { North, West, South, East };

    /// <summary>
    /// The base class of a Tetris piece
    /// </summary>
    public abstract class TetrisPiece
    {
        /// <summary>
        /// The number of Tetris piece types
        /// </summary>
        /// The number needs to be equal to the number of entires in TetrisPiece.TetrisPieces
        public const int NUMBER_OF_SUPPORTED_TETRIS_PIECES = 7;

        /// <summary>
        /// The orientation of the Tetris piece
        /// </summary>
        private Orientations orientation = Orientations.North;

        /// <summary>
        /// The point used as a referance location to define which points the Tetris piece occupies
        /// </summary>
        protected Point referanceLocation;

        /// <summary>
        /// An array of points the Tetris piece occupies
        /// </summary>
        protected Point[] pieceLocations;

        /// <summary>
        /// The orientation of the Tetris piece
        /// </summary>
        public Orientations Orientation
        {
            get
            {
                return this.orientation;
            }
        }

        /// <summary>
        /// The type of the Tetris piece
        /// </summary>
        public abstract TetrisPieces Type
        {
            get;
        }

        /// <summary>
        /// The color of the Tetris piece
        /// </summary>
        public abstract TetrisColors Color
        {
            get;
        }

        /// <summary>
        /// An array of points the Tetris piece occupies
        /// </summary>
        public Point[] PieceLocations
        {
            get
            {
                return this.pieceLocations.ToArray();
            }
        }

        /// <summary>
        /// Constructs a new Tetris piece
        /// </summary>
        /// <param name="referanceLocation">A referance point location used to define which points the Tetris piece occupies</param>
        protected TetrisPiece(Point referanceLocation)
        {
            this.referanceLocation = referanceLocation;

            this.updatePieceLocations();
        }

        /// <summary>
        /// Constructs a new Tetris piece
        /// </summary>
        /// <param name="referanceLocation">A referance point location used to define which points the Tetris piece occupies</param>
        /// <param name="orientation">The orientation of the Tetris piece</param>
        protected TetrisPiece(Point referanceLocation, Orientations orientation)
        {
            this.referanceLocation = referanceLocation;
            this.orientation = orientation;

            this.updatePieceLocations();
        }

        /// <summary>
        /// Rotates the Tetris piece clockwise
        /// </summary>
        /// <returns>Returns the new orientation</returns>
        public Orientations rotateClockwise()
        {
            switch (this.orientation)
            {
                case Orientations.North: this.orientation = Orientations.East; break;
                case Orientations.West: this.orientation = Orientations.North; break;
                case Orientations.South: this.orientation = Orientations.West; break;
                case Orientations.East: this.orientation = Orientations.South; break;
            }

            this.updatePieceLocations();

            return this.orientation;
        }

        /// <summary>
        /// Rotates the Tetris piece counterclockwise
        /// </summary>
        /// <returns>Returns the new orientation</returns>
        public Orientations rotateCounterClockwise()
        {
            switch (this.orientation)
            {
                case Orientations.North: this.orientation = Orientations.West; break;
                case Orientations.West: this.orientation = Orientations.South; break;
                case Orientations.South: this.orientation = Orientations.East; break;
                case Orientations.East: this.orientation = Orientations.North; break;
            }

            this.updatePieceLocations();

            return this.orientation;
        }

        /// <summary>
        /// The points the Tetris piece would occupy if rotated clockwise
        /// </summary>
        /// <returns>An array of points defining where the Tetris piece would be located</returns>
        public Point[] pointsForClockwiseRotation()
        {
            switch (this.orientation)
            {
                case Orientations.North: return this.pointsForEastOrientation();
                case Orientations.West: return this.pointsForNorthOrientation();
                case Orientations.South: return this.pointsForWestOrientation();
                case Orientations.East: return this.pointsForSouthOrientation();
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// The points the Tetris piece would occupy if rotated counterwise
        /// </summary>
        /// <returns>An array of points defining where the Tetris piece would be located</returns>
        public Point[] pointsForCounterClockwiseRotation()
        {
            switch (this.orientation)
            {
                case Orientations.North: return this.pointsForWestOrientation();
                case Orientations.West: return this.pointsForSouthOrientation();
                case Orientations.South: return this.pointsForEastOrientation();
                case Orientations.East: return this.pointsForNorthOrientation();
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Move the Tetris piece right
        /// </summary>
        /// <returns>Returns the new referance point</returns>
        public Point moveRight()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

        /// <summary>
        /// Move the Tetris piece left
        /// </summary>
        /// <returns>Returns the new referance point</returns>
        public Point moveLeft()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

        /// <summary>
        /// Move the Tetris piece down
        /// </summary>
        /// <returns>Returns the new referance point</returns>
        public Point moveDown()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X, this.referanceLocation.Y - 1);

            this.updatePieceLocations();

            return this.referanceLocation;
        }
        /// <summary>
        /// Move the Tetris piece up
        /// </summary>
        /// <returns>Returns the new referance point</returns>
        public Point moveUp()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

        /// <summary>
        /// Updates the Tetris pieceLocations to reflect the orientation and referance point
        /// </summary>
        private void updatePieceLocations()
        {
            switch (this.orientation)
            {
                case Orientations.North: this.orientateNorth(); break;
                case Orientations.East: this.orientateEast(); break;
                case Orientations.South: this.orientateSouth(); break;
                case Orientations.West: this.orientateWest(); break;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Orientates the Tetris piece north
        /// </summary>
        private void orientateNorth()
        {
            this.pieceLocations = this.pointsForNorthOrientation();
        }

        /// <summary>
        /// Orientates the Tetris piece east
        /// </summary>
        private void orientateEast()
        {
            this.pieceLocations = this.pointsForEastOrientation();
        }

        /// <summary>
        /// Orientates the Tetris piece south
        /// </summary>
        private void orientateSouth()
        {
            this.pieceLocations = this.pointsForSouthOrientation();
        }

        /// <summary>
        /// Orientates the Tetris piece west
        /// </summary>
        private void orientateWest()
        {
            this.pieceLocations = this.pointsForWestOrientation();
        }

        /// <summary>
        /// The points the Tetris piece would occupy if orientated north
        /// </summary>
        /// <returns>An array of points defining the Tetris piece's location when orientated north</returns>
        protected abstract Point[] pointsForNorthOrientation();

        /// <summary>
        /// The points the Tetris piece would occupy if orientated east
        /// </summary>
        /// <returns>An array of points defining the Tetris piece's location when orientated east</returns>
        protected abstract Point[] pointsForEastOrientation();

        /// <summary>
        /// The points the Tetris piece would occupy if orientated south
        /// </summary>
        /// <returns>An array of points defining the Tetris piece's location when orientated south</returns>
        protected abstract Point[] pointsForSouthOrientation();

        /// <summary>
        /// The points the Tetris piece would occupy if orientated west
        /// </summary>
        /// <returns>An array of points defining the Tetris piece's location when orientated west</returns>
        protected abstract Point[] pointsForWestOrientation();
    }
}
