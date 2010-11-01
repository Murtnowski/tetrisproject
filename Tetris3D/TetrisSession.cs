/*
 * Project: Tetris Project
 * Authors: Matthew Urtnowski 
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
    //This is an enumeration of the currently supported tetris blocks.  If you change it be sure to update NumberOfSupportedTetrisPieces
    public enum TetrisPieces { TBlock = 0, SBlock, ZBlock, IBlock, JBlock, LBlock };

    /* This class represents a single Tetris session.  A session is when a player plays
     * a single Tetris round from start until Game Over
     */
    public class TetrisSession
    {
        //NumberOfSupportedTetrisPieces needs to be equal to the number of entries in the TetrisPieces enumeration
        public const int NumberOfSupportedTetrisPieces = 6;

        //GameOverRange is the number of rows from the top of the board that if a piece ends up in the game is over.
        public const int GameOverRange = 4;

        //A referance point that new piece the player controls is built around when a new piece is needed at the top of the board
        private Point newCurrentPieceGenerationPoint;

        /* A two dimension array that represents the gameboard.  The game board is arranged into collection of points in a 
         * multidimensional array
         */
        private TetrisBlock[,] gameBoard;

        //A collection of points that is used to keep track of the current space the piece the player controls is occuping
        private Point[] currentPiecePointLocations;

        //The current Tetris peice the player controls
        private TetrisPieces currentTetrisPiece;

        //The next piece the player will control
        private TetrisPieces nextTetrisPiece;

        //The current level
        private int currentLevel;

        //The current score
        private int currentScore;

        //A random number generator used to generate the next Tetris piece
        private Random randomGenerator = new Random();

        //Accessor for the Gameboard
        public TetrisBlock[,] GameBoard
        {
            get
            {
                return this.gameBoard;
            }
        }

        //Accessor for the current Tetris piece the player controls
        public TetrisPieces CurrentTetrisPiece()
        {
            return this.currentTetrisPiece;
        }

        //Accessor for the next Tetris piece the player controls
        public TetrisPieces NextTetrisPiece()
        {
            return this.nextTetrisPiece;
        }

        //Accessor for the collection of points the player's piece occupies on the game board
        public Point[] CurrentPiecePointLocations
        {
            get
            {
                return this.currentPiecePointLocations;
            }
        }

        //Accessor for the current level
        public int CurrentLevel
        {
            get
            {
                return this.currentLevel;
            }
            set
            {
                this.currentLevel = value;
            }
        }

        //Accessor for the current score
        public int CurrentScore
        {
            get
            {
                return this.currentScore;
            }
        }

        // Constructs a Tetris Session with a new board of the size defined by gameBoardSize
        public TetrisSession(Vector2 gameBoardSize)
        {
            this.gameBoard = new TetrisBlock[(int)gameBoardSize.X, (int)gameBoardSize.Y];

            //Calculate the point which new pieces will be created around
            this.newCurrentPieceGenerationPoint = new Point(this.gameBoard.GetLength(0) / 2, this.gameBoard.GetLength(1) - GameOverRange);

            //Initialize the pieces the users controls
            this.nextTetrisPiece =(TetrisPieces)this.randomGenerator.Next(NumberOfSupportedTetrisPieces);

            this.GenerateNewCurrentTetrisPiece();
        }

        // Constructs a Tetris Session with a board intialized by the parameter gameBoard
        public TetrisSession(TetrisBlock[,] gameBoard)
        {
            this.gameBoard = gameBoard;

            //Calculate the point which new pieces will be created around
            this.newCurrentPieceGenerationPoint = new Point(this.gameBoard.GetLength(0) / 2, this.gameBoard.GetLength(1) - GameOverRange);

            //Initialize the pieces the users controls
            this.nextTetrisPiece = (TetrisPieces)this.randomGenerator.Next(NumberOfSupportedTetrisPieces);

            this.GenerateNewCurrentTetrisPiece();
        }

        /* Generate a new Tetris piece the user will control.  If a new piece is generated true is returned.
         * If tetris blocks are exist with the Game Over area at the top of the board, a new piece will not be 
         * generated and false will be returned
         */
        public bool GenerateNewCurrentTetrisPiece()
        {
            //If the game is not over generate a new piece
            if (!this.isGameOver())
            {
                this.currentTetrisPiece = this.nextTetrisPiece;

                switch (this.currentTetrisPiece)
                {
                    case TetrisPieces.TBlock: this.generateNewTBlock(); break;
                    case TetrisPieces.SBlock: this.generateNewSBlock(); break;
                    case TetrisPieces.ZBlock: this.generateNewZBlock(); break;
                    case TetrisPieces.IBlock: this.generateNewIBlock(); break;
                    case TetrisPieces.JBlock: this.generateNewJBlock(); break;
                    case TetrisPieces.LBlock: this.generateNewLBlock(); break;
                }

                this.nextTetrisPiece = (TetrisPieces)this.randomGenerator.Next(NumberOfSupportedTetrisPieces);

                //A new piece was successfully generated, so return true
                return true;
            }
            else
            {
                return false;
            }
        }

        // Generate a new T block around the newCurrentPieceGenerationPoint
        private void generateNewTBlock()
        {
            //Set the locations of the current block then add them to the gameboard

            this.currentPiecePointLocations = new Point[4];
            this.currentPiecePointLocations[0] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[1] = new Point(this.newCurrentPieceGenerationPoint.X + 1, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[2] = new Point(this.newCurrentPieceGenerationPoint.X - 1, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[3] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 1);

            this.addBlocksToGameBoard(this.currentPiecePointLocations, TetrisPieces.TBlock);
        }

        // Generate a new S block around the newCurrentPieceGenerationPoint
        private void generateNewSBlock()
        {
            //Set the locations of the current block then add them to the gameboard
            this.currentPiecePointLocations = new Point[4];
            this.currentPiecePointLocations[0] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[1] = new Point(this.newCurrentPieceGenerationPoint.X - 1, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[2] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 1);
            this.currentPiecePointLocations[3] = new Point(this.newCurrentPieceGenerationPoint.X + 1, this.newCurrentPieceGenerationPoint.Y + 1);

            this.addBlocksToGameBoard(this.currentPiecePointLocations, TetrisPieces.SBlock);
        }

        // Generate a new Z block around the newCurrentPieceGenerationPoint
        private void generateNewZBlock()
        {
            //Set the locations of the current block then add them to the gameboard

            this.currentPiecePointLocations = new Point[4];
            this.currentPiecePointLocations[0] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[1] = new Point(this.newCurrentPieceGenerationPoint.X + 1, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[2] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 1);
            this.currentPiecePointLocations[3] = new Point(this.newCurrentPieceGenerationPoint.X - 1, this.newCurrentPieceGenerationPoint.Y + 1);

            this.addBlocksToGameBoard(this.currentPiecePointLocations, TetrisPieces.ZBlock);
        }

        // Generate a new I block around the newCurrentPieceGenerationPoint
        private void generateNewIBlock()
        {
            //Set the locations of the current block then add them to the gameboard

            this.currentPiecePointLocations = new Point[4];
            this.currentPiecePointLocations[0] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[1] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 1);
            this.currentPiecePointLocations[2] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 2);
            this.currentPiecePointLocations[3] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 3);

            this.addBlocksToGameBoard(this.currentPiecePointLocations, TetrisPieces.IBlock);
        }

        // Generate a new J block around the newCurrentPieceGenerationPoint
        private void generateNewJBlock()
        {
            //Set the locations of the current block then add them to the gameboard

            this.currentPiecePointLocations = new Point[4];
            this.currentPiecePointLocations[0] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[1] = new Point(this.newCurrentPieceGenerationPoint.X - 1, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[2] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 1);
            this.currentPiecePointLocations[3] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 2);

            this.addBlocksToGameBoard(this.currentPiecePointLocations, TetrisPieces.JBlock);
        }

        // Generate a new L block around the newCurrentPieceGenerationPoint
        private void generateNewLBlock()
        {
            //Set the locations of the current block then add them to the gameboard

            this.currentPiecePointLocations = new Point[4];
            this.currentPiecePointLocations[0] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[1] = new Point(this.newCurrentPieceGenerationPoint.X + 1, this.newCurrentPieceGenerationPoint.Y);
            this.currentPiecePointLocations[2] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 1);
            this.currentPiecePointLocations[3] = new Point(this.newCurrentPieceGenerationPoint.X, this.newCurrentPieceGenerationPoint.Y + 2);

            this.addBlocksToGameBoard(this.currentPiecePointLocations, TetrisPieces.LBlock);
        }

        //Returns true if the part of the current piece occupies a point on the game board
        public bool isCurrentPieceAtLocation(Point point)
        {
            //Foreach of the currentPieces point locations compare it to the specified point
            foreach (Point currentPoint in this.currentPiecePointLocations)
            {
                if (point == currentPoint)
                {
                    return true;
                }
            }

            return false;
        }

        //Returns true if the space below the current piece is clear.  Another way of thinking about it can the current piece move down
        public bool isBlocksBelowCurrentPieceClear()
        {
            //Check each of the points of the current piece
            foreach (Point point in this.currentPiecePointLocations)
            {
                /* if the block has reach the bottom of the board or the space below the block has another block then
                 * return false since the space is not clear.  Do not fail the check if the space the block is part of
                 * the current piece
                 */
                if ((point.Y - 1) < 0 || (!this.isCurrentPieceAtLocation(new Point(point.X, point.Y - 1)) && this.GameBoard[point.X, (point.Y - 1)] != null))
                {
                    return false;
                }
            }

            return true;
        }

        //Returns true if the space to the left of the current piece is clear.  Another way of thinking about it can the current piece move right
        public bool isBlocksLeftOfCurrentPieceClear()
        {
            //Check each of the points of the current piece
            foreach (Point point in this.CurrentPiecePointLocations)
            {
                /* if the block has reach the left side of the board or the space below the block has another block then
                * return false since the space is not clear.  Do not fail the check if the space the block is part of
                * the current piece
                */
                if ((point.X - 1) < 0 || (!this.isCurrentPieceAtLocation(new Point(point.X - 1, point.Y)) && this.GameBoard[(point.X - 1), point.Y] != null))
                {
                    return false;
                }
            }

            return true;
        }

        //Returns true if the space to the right of the current piece is clear.  Another way of thinking about it can the current piece move right
        public bool isBlocksRightOfCurrentPieceClear()
        {
            //Check each of the points of the current piece
            foreach (Point point in this.CurrentPiecePointLocations)
            {
                /* if the block has reach the right side of the board or the space below the block has another block then
                * return false since the space is not clear.  Do not fail the check if the space the block is part of
                * the current piece
                */
                if ((point.X + 1) >= this.GameBoard.GetLength(0) || (!this.isCurrentPieceAtLocation(new Point(point.X + 1, point.Y)) && this.GameBoard[(point.X + 1), point.Y] != null))
                {
                    return false;
                }
            }

            return true;
        }

        //Checks to see if the piece can rotate clockwise
        //NOT YET IMPLEMENTED
        public bool isCurrentPieceAbleToRotateClockwise()
        {
            return false;
        }

        //Checks to see if the piece can rotate counterclockwise.
        //NOT YET IMPLEMENTED
        public bool isCurrentPieceAbleToRotateCounterClockwise()
        {
            return false;
        }

        //Move the current piece down.  If succesful return true, otherwise return false
        public bool moveCurrentPieceDown()
        {
            //If the space below the current piece is clear then move down
            if (this.isBlocksBelowCurrentPieceClear())
            {
                //A temporary list to store the new current piece locations that are offset one downwards
                List<Point> newCurrentPieceLocations = new List<Point>();

                foreach (Point p in this.currentPiecePointLocations)
                {
                    newCurrentPieceLocations.Add(new Point(p.X, p.Y - 1));
                }

                this.replaceCurrentPiece(newCurrentPieceLocations.ToArray(), this.currentTetrisPiece);

                this.currentPiecePointLocations = newCurrentPieceLocations.ToArray();

                return true;
            }
            else
            {
                return false;
            }
        }

        //Move the current piece left.  If succesful return true, otherwise return false;
        public bool moveCurrentPieceLeft()
        {
            if (this.isBlocksLeftOfCurrentPieceClear())
            {
                //A temporary list to store the new current piece locations that are offset one left
                List<Point> newCurrentPieceLocations = new List<Point>();

                foreach (Point p in this.currentPiecePointLocations)
                {
                    newCurrentPieceLocations.Add(new Point(p.X - 1, p.Y));
                }

                this.replaceCurrentPiece(newCurrentPieceLocations.ToArray(), this.currentTetrisPiece);

                this.currentPiecePointLocations = newCurrentPieceLocations.ToArray();

                return true;
            }
            else
            {
                return false;
            }
        }

        //Move the current piece right.  If succesful return true, otherwise return false;
        public bool moveCurrentPieceRight()
        {
            if (this.isBlocksRightOfCurrentPieceClear())
            {
                //A temporary list to store the new current piece locations that are offset one right
                List<Point> newCurrentPieceLocations = new List<Point>();

                foreach (Point p in this.currentPiecePointLocations)
                {
                    newCurrentPieceLocations.Add(new Point(p.X + 1, p.Y));
                }

                this.replaceCurrentPiece(newCurrentPieceLocations.ToArray(), this.currentTetrisPiece);

                this.currentPiecePointLocations = newCurrentPieceLocations.ToArray();

                return true;
            }
            else
            {
                return false;
            }
        }

        //Rotate the current piece clockwise.  If succesful return true, otherwise return false
        //NOT YET IMPLEMENTED
        public bool rotateCurrentPieceClockwise()
        {
            return false;
        }

        //Move the current piece counterclockwise.  If succesful return true, otherwise return false;
        //NOT YET IMPLEMENTED
        public bool rotateCurrentPieceCounterclockwise()
        {
            return false;
        }

        //Replaces the current Tetris piece with a new piece at a new location
        private void replaceCurrentPiece(Point[] newCurrentPieceLocations, TetrisPieces tetrisPiece)
        {
            foreach (Point p in this.currentPiecePointLocations)
            {
                this.gameBoard[p.X, p.Y] = null;
            }

            this.addBlocksToGameBoard(newCurrentPieceLocations, tetrisPiece);
        }

        //Adds a tetris piece at specific points on the gameboard
        private void addBlocksToGameBoard(Point[] pointsToBeAdded, TetrisPieces tetrisPiece)
        {
            foreach (Point p in pointsToBeAdded)
            {
                switch (tetrisPiece)
                {
                    case TetrisPieces.TBlock: this.gameBoard[p.X, p.Y] = new TetrisBlock(TetrisColors.Magenta); break;
                    case TetrisPieces.SBlock: this.gameBoard[p.X, p.Y] = new TetrisBlock(TetrisColors.Red); break;
                    case TetrisPieces.ZBlock: this.gameBoard[p.X, p.Y] = new TetrisBlock(TetrisColors.Orange); break;
                    case TetrisPieces.IBlock: this.gameBoard[p.X, p.Y] = new TetrisBlock(TetrisColors.Cyan); break;
                    case TetrisPieces.JBlock: this.gameBoard[p.X, p.Y] = new TetrisBlock(TetrisColors.Green); break;
                    case TetrisPieces.LBlock: this.gameBoard[p.X, p.Y] = new TetrisBlock(TetrisColors.Yellow); break;
                }
            }
        }

        //Clears all of the currently completed lines
        //The current algorithm is very inefficent
        public void clearCompletedLines()
        {
            /* For each row see if the line is completed.  If it is then clear the line restructure the board then start
             * looking from the beginning for cleared lines.
             */
            for (int lineIndex = 0; lineIndex < this.GameBoard.GetLength(1); lineIndex++)
            {
                if (this.isLineCompleted(lineIndex))
                {
                    this.clearLine(lineIndex);

                    this.restructureGameboard(lineIndex);

                    this.clearCompletedLines();

                    return;
                }
            }
        }

        //Lowers all blocks above a lineIndex by one
        private void restructureGameboard(int lineIndex)
        {
            //For all of the rows above the lineIndex move each of the occupied blocks down
            for (int y = lineIndex + 1; y < this.gameBoard.GetLength(1); y++)
            {
                for (int x = 0; x < this.gameBoard.GetLength(0); x++)
                {
                    if (this.gameBoard[x, y] != null && !this.isCurrentPieceAtLocation(new Point(x, y)))
                    {
                        this.gameBoard[x, y - 1] = new TetrisBlock(this.gameBoard[x, y].TetrisColor);
                        this.gameBoard[x, y] = null;
                    }
                }
            }
        }

        //Clears a line at a particular line index
        private void clearLine(int lineIndex)
        {
            for (int x = 0; x < this.gameBoard.GetLength(0); x++)
            {
                this.gameBoard[x, lineIndex] = null;
            }
        }

        //Returns the current number of completed lines
        public int currentNumerOfCompletedLines()
        {
            int currentNumberOfCompletedLines = 0;

            for (int lineIndex = 0; lineIndex < this.GameBoard.GetLength(1); lineIndex++)
            {
                if (this.isLineCompleted(lineIndex))
                {
                    currentNumberOfCompletedLines++;
                }
            }

            return currentNumberOfCompletedLines;
        }

        //Returns true if the line is completed at a particular line index
        public bool isLineCompleted(int lineIndex)
        {
            for (int x = 0; x < this.GameBoard.GetLength(0); x++)
            {
                try
                {
                    if (this.GameBoard[x, lineIndex] == null || this.isCurrentPieceAtLocation(new Point(x, lineIndex)))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        //Return true if a tetris block occupies the game over area at the top of the board;
        public bool isGameOver()
        {
            //For all of the rows in the Game Over range check to see if they have any blocks in them
            for (int y = this.gameBoard.GetLength(1) - 1; y >= (this.gameBoard.GetLength(1) - GameOverRange); y--)
            {
                for (int x = 0; x < this.gameBoard.GetLength(0); x++)
                {
                    if (this.gameBoard[x, y] != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
