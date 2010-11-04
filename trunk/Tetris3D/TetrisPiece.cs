using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Tetris3D
{
    //This is an enumeration of the currently supported tetris blocks.  If you change it be sure to update NUMBER_OF_SUPPORTED_TETRIS_PIECES
    public enum TetrisPieces { TBlock = 0, SBlock, ZBlock, IBlock, JBlock, LBlock, OBlock };

    public enum Orientations { North, West, South, East };

    public abstract class TetrisPiece
    {
        public const int NUMBER_OF_SUPPORTED_TETRIS_PIECES = 7;

        private Orientations orientation = Orientations.North;

        protected Point referanceLocation;

        protected Point[] pieceLocations;

        public Orientations Orientation
        {
            get
            {
                return this.orientation;
            }
        }

        public abstract TetrisPieces Type
        {
            get;
        }

        public abstract TetrisColors Color
        {
            get;
        }

        public Point[] PieceLocations
        {
            get
            {
                return this.pieceLocations.ToArray();
            }
        }

        protected TetrisPiece(Point referanceLocation)
        {
            this.referanceLocation = referanceLocation;

            this.updatePieceLocations();
        }

        protected TetrisPiece(Point referanceLocation, Orientations orientation)
        {
            this.referanceLocation = referanceLocation;
            this.orientation = orientation;

            this.updatePieceLocations();
        }

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

        public Point moveRight()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

        public Point moveLeft()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

        public Point moveDown()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X, this.referanceLocation.Y - 1);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

        public Point moveUp()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);

            this.updatePieceLocations();

            return this.referanceLocation;
        }

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

        private void orientateNorth()
        {
            this.pieceLocations = this.pointsForNorthOrientation();
        }
        private void orientateEast()
        {
            this.pieceLocations = this.pointsForEastOrientation();
        }
        private void orientateSouth()
        {
            this.pieceLocations = this.pointsForSouthOrientation();
        }
        private void orientateWest()
        {
            this.pieceLocations = this.pointsForWestOrientation();
        }

        protected abstract Point[] pointsForNorthOrientation();
        protected abstract Point[] pointsForEastOrientation();
        protected abstract Point[] pointsForSouthOrientation();
        protected abstract Point[] pointsForWestOrientation();
    }
}
