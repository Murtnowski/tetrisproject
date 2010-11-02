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

        private TetrisPieces type;

        protected TetrisColors color;

        protected Point referanceLocation;

        protected List<Point> pieceLocations;

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

        public void moveRight()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X + 1, this.referanceLocation.Y);

            this.updatePieceLocations();
        }

        public void moveLeft()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X - 1, this.referanceLocation.Y);

            this.updatePieceLocations();
        }

        public void moveDown()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X, this.referanceLocation.Y - 1);

            this.updatePieceLocations();
        }

        public void moveUp()
        {
            List<Point> newPieceLocations = new List<Point>();

            this.referanceLocation = new Point(this.referanceLocation.X, this.referanceLocation.Y + 1);

            this.updatePieceLocations();
        }

        private void updatePieceLocations()
        {
            switch (this.orentation)
            {
                case Orentations.North: this.orentateNorth();
                case Orentations.East: this.orentateEast();
                case Orentations.South: this.orentateSouth();
                case Orentations.West: this.orentateWest();
                default: throw new NotImplementedException();
            }
        }

        protected abstract void orentateNorth();
        protected abstract void orentateEast();
        protected abstract void orentateSouth();
        protected abstract void orentateWest();
    }
}
