using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Tetris3D
{
    //This is an enumeration of the currently supported tetris blocks.  If you change it be sure to update NumberOfSupportedTetrisPieces
    public enum TetrisPieces { TBlock = 0, SBlock, ZBlock, IBlock, JBlock, LBlock, OBlock };

    public enum Orentations { North, West, South, East };

    public abstract class TetrisPiece
    {
        public const int NUMBEROFSUPPORTEDTETRISPIECES = 7;

        private Orentations orentation = Orentations.North;

        protected Point referanceLocation;

        protected Point[] pieceLocations;

        public Orentations Orentation
        {
            get
            {
                return this.orentation;
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

        protected TetrisPiece(Point referanceLocation, Orentations orentation)
        {
            this.referanceLocation = referanceLocation;
            this.orentation = orentation;

            this.updatePieceLocations();
        }

        public Orentations rotateClockwise()
        {
            switch (this.orentation)
            {
                case Orentations.North: this.orentation = Orentations.East; break;
                case Orentations.West: this.orentation = Orentations.North; break;
                case Orentations.South: this.orentation = Orentations.West; break;
                case Orentations.East: this.orentation = Orentations.South; break;
            }

            this.updatePieceLocations();

            return this.orentation;
        }

        public Orentations rotateCounterClockwise()
        {
            switch (this.orentation)
            {
                case Orentations.North: this.orentation = Orentations.West; break;
                case Orentations.West: this.orentation = Orentations.South; break;
                case Orentations.South: this.orentation = Orentations.East; break;
                case Orentations.East: this.orentation = Orentations.North; break;
            }

            this.updatePieceLocations();

            return this.orentation;
        }

        public Point[] pointsForClockwiseRotation()
        {
            switch (this.orentation)
            {
                case Orentations.North: return this.pointsForEastOrentation();
                case Orentations.West: return this.pointsForNorthOrentation();
                case Orentations.South: return this.pointsForWestOrentation();
                case Orentations.East: return this.pointsForSouthOrentation();
                default: throw new NotImplementedException();
            }
        }

        public Point[] pointsForCounterClockwiseRotation()
        {
            throw new NotImplementedException();
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
            switch (this.orentation)
            {
                case Orentations.North: this.orentateNorth(); break;
                case Orentations.East: this.orentateEast(); break;
                case Orentations.South: this.orentateSouth(); break;
                case Orentations.West: this.orentateWest(); break;
                default: throw new NotImplementedException();
            }
        }

        private void orentateNorth()
        {
            this.pieceLocations = this.pointsForNorthOrentation();
        }
        private void orentateEast()
        {
            this.pieceLocations = this.pointsForEastOrentation();
        }
        private void orentateSouth()
        {
            this.pieceLocations = this.pointsForSouthOrentation();
        }
        private void orentateWest()
        {
            this.pieceLocations = this.pointsForWestOrentation();
        }

        protected abstract Point[] pointsForNorthOrentation();
        protected abstract Point[] pointsForEastOrentation();
        protected abstract Point[] pointsForSouthOrentation();
        protected abstract Point[] pointsForWestOrentation();
    }
}
