/*
 * Project: Tetris Project
 * Authors: Matthew Urtnowski & Damon Chastain
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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using XELibrary;

namespace Tetris3D
{
    /// <summary>
    /// This class represents a single Tetris session.  A session is when a player plays a single Tetris round from start till game over
    /// </summary>
    public class TetrisSession
    {
        /// <summary>
        /// The number of rows from the top of the board that if a piece ends up in the game is over
        /// </summary>
        public const int GameOverRange = 4;

        /// <summary>
        /// A referance point that a piece the player controls is built around when a new piece is needed at the top of the board
        /// </summary>
        private Point newCurrentPieceGenerationPoint;

        /// <summary>
        /// A two dimension array that represents the gameboard.
        /// </summary>
        /// The game board is arranged into collection of points in a multidimensional array
        private TetrisBlock[,] gameBoard;

        /// <summary>
        /// The current Tetris peice the player controls
        /// </summary>
        private TetrisPiece currentTetrisPiece;

        /// <summary>
        /// The next piece the player will control
        /// </summary>
        private TetrisPiece nextTetrisPiece;

        /// <summary>
        /// The current level
        /// </summary>
        private int currentLevel;


        /// <summary>
        /// The current score
        /// </summary>
        private int currentScore;

        /// <summary>
        /// The number of lines that has been cleared in this session
        /// </summary>
        private int currentNumberOfClearedLines;

        /// <summary>
        /// A random number generator used to generate the next Tetris piece
        /// </summary>
        private Random randomGenerator = new Random();

        /// <summary>
        /// The points earned when a line is cleared
        /// </summary>
        private int lineScore = 100;

        /// <summary>
        /// The bounus points earned when the player completes a Tetris
        /// </summary>
        private int tetrisBonus = 700;

        /// <summary>
        /// A two dimension array that represents the gameboard.
        /// </summary>
        public TetrisBlock[,] GameBoard
        {
            get
            {
                return this.gameBoard;
            }
        }

        /// <summary>
        /// The current Tetris piece the player controls
        /// </summary>
        /// <returns>Returns the tetris piece the player controls</returns>
        public TetrisPiece CurrentTetrisPiece()
        {
            return this.currentTetrisPiece;
        }

        /// <summary>
        /// The next Tetris piece the player will control
        /// </summary>
        /// <returns>Returns the tetris piece the player controls</returns>
        public TetrisPiece NextTetrisPiece()
        {
            return this.nextTetrisPiece;
        }

        /// <summary>
        /// An array of points the player's piece occupies on the game board
        /// </summary>
        public Point[] CurrentPiecePointLocations
        {
            get
            {
                return this.currentTetrisPiece.PieceLocations;
            }
        }

        /// <summary>
        /// The current level
        /// </summary>
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

        /// <summary>
        /// The current score
        /// </summary>
        public int CurrentScore
        {
            get
            {
                return this.currentScore;
            }
        }

        /// <summary>
        /// The current number of lines that has been cleared in this Tetris session
        /// </summary>
        public int CurrentNumberOfClearedLines
        {
            get
            {
                return this.currentNumberOfClearedLines;
            }
        }

        /// <summary>
        /// Constructs a new Tetris Session
        /// </summary>
        /// <param name="gameBoardSize">Defines the size of the gameboard the tetris session uses</param>
        public TetrisSession(Vector2 gameBoardSize)
        {
            this.gameBoard = new TetrisBlock[(int)gameBoardSize.X, (int)gameBoardSize.Y];

            //Calculate the point which new pieces will be created around
            this.newCurrentPieceGenerationPoint = new Point(this.gameBoard.GetLength(0) / 2, this.gameBoard.GetLength(1) - GameOverRange);

            //Initialize the pieces the users controls
            this.nextTetrisPiece = this.getRandomTetrisPiece();

            this.GenerateNewCurrentTetrisPiece();
        }

        // Constructs a Tetris Session with a board intialized by the parameter gameBoard
        /// <summary>
        /// Constructs a new Tetris Session
        /// </summary>
        /// <param name="gameBoard">Initialize the gameboard</param>
        public TetrisSession(TetrisBlock[,] gameBoard)
        {
            this.gameBoard = gameBoard;

            //Calculate the point which new pieces will be created around
            this.newCurrentPieceGenerationPoint = new Point(this.gameBoard.GetLength(0) / 2, this.gameBoard.GetLength(1) - GameOverRange);

            //Initialize the pieces the users controls
            this.nextTetrisPiece = this.getRandomTetrisPiece();

            this.GenerateNewCurrentTetrisPiece();
        }

        /// <summary>
        /// Gets a new Tetris piece at random
        /// </summary>
        /// <returns>A new tetris piece</returns>
        private TetrisPiece getRandomTetrisPiece()
        {
            switch (this.randomGenerator.Next(TetrisPiece.NUMBER_OF_SUPPORTED_TETRIS_PIECES))
            {
                case (int)TetrisPieces.IBlock: return new IPiece(this.newCurrentPieceGenerationPoint);
                case (int)TetrisPieces.JBlock: return new JPiece(this.newCurrentPieceGenerationPoint);
                case (int)TetrisPieces.LBlock: return new LPiece(this.newCurrentPieceGenerationPoint);
                case (int)TetrisPieces.SBlock: return new SPiece(this.newCurrentPieceGenerationPoint);
                case (int)TetrisPieces.OBlock: return new OPiece(this.newCurrentPieceGenerationPoint);
                case (int)TetrisPieces.TBlock: return new TPiece(this.newCurrentPieceGenerationPoint);
                case (int)TetrisPieces.ZBlock: return new ZPiece(this.newCurrentPieceGenerationPoint);
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Generate a new Tetris piece the user will control.
        /// </summary>
        /// <returns>Returns true if a new Tetris piece is constructed, if the last Tetris piece resulted in a game over then a new Tetris piece is not constructed and false is returned</returns>
        public bool GenerateNewCurrentTetrisPiece()
        {
            //If the game is not over generate a new piece
            if (!this.isGameOver())
            {
                this.currentTetrisPiece = this.nextTetrisPiece;

                this.addBlocksToGameBoard(this.currentTetrisPiece.PieceLocations, this.currentTetrisPiece.Color);

                this.nextTetrisPiece = this.getRandomTetrisPiece();

                //A new piece was successfully generated, so return true
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the current Tetris piece occupies a particular point
        /// </summary>
        /// <param name="point">The point compared against the Tetris piece</param>
        /// <returns>A bool conresponding to wether or not part of the Tetris piece occupies a particular point</returns>
        public bool isCurrentPieceAtLocation(Point point)
        {
            // TODO: One point of return from a method.

            //Foreach of the currentPieces point locations compare it to the specified point
            foreach (Point currentPoint in this.currentTetrisPiece.PieceLocations)
            {
                if (point == currentPoint)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check to see if the current Tetris piece can move down
        /// </summary>
        /// <returns>If the space below the current tetris piece is clear return true</returns>
        public bool isBlocksBelowCurrentPieceClear()
        {
            //Check each of the points of the current piece
            foreach (Point point in this.currentTetrisPiece.PieceLocations)
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

        /// <summary>
        /// Check to see if the current Tetris piece can move left
        /// </summary>
        /// <returns>If the space left of the current tetris piece is clear return true</returns>
        public bool isBlocksLeftOfCurrentPieceClear()
        {
            //Check each of the points of the current piece
            foreach (Point point in this.CurrentPiecePointLocations)
            {
                /* if the block has reach the left side of the board or the space below the block has another block then
                * return false since the space is not clear.  Do not fail the check if the space the block is part of
                * the current piece
                */
                //TODO: Overload this to throw in an X and a Y instead of creating a new point
                if ((point.X - 1) < 0 || (!this.isCurrentPieceAtLocation(new Point(point.X - 1, point.Y)) &&
                    this.GameBoard[(point.X - 1), point.Y] != null))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check to see if the current Tetris piece can move right
        /// </summary>
        /// <returns>If the space right of the current tetris piece is clear return true</returns>
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

        /// <summary>
        /// Check to see if the current Tetris piece can rotate clockwise
        /// </summary>
        /// <returns>If the Tetris piece can rotate clockwise return true</returns>
        public bool isCurrentPieceAbleToRotateClockwise()
        {
            Point[] points = this.currentTetrisPiece.pointsForClockwiseRotation();

            foreach (Point point in points)
            {
                if ((point.X < 0 || point.X >= this.gameBoard.GetLength(0) || point.Y < 0 || point.Y >= this.gameBoard.GetLength(1)) || (this.gameBoard[point.X, point.Y] != null && !this.isCurrentPieceAtLocation(point)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check to see if the current Tetris piece can rotate counterclockwise
        /// </summary>
        /// <returns>If the Tetris piece can rotate counterclockwise return true</returns>
        public bool isCurrentPieceAbleToRotateCounterClockwise()
        {
            Point[] points = this.currentTetrisPiece.pointsForCounterClockwiseRotation();

            foreach (Point point in points)
            {
                if ((point.X < 0 || point.X >= this.gameBoard.GetLength(0) || point.Y < 0 || point.Y >= this.gameBoard.GetLength(1)) || (this.gameBoard[point.X, point.Y] != null && !this.isCurrentPieceAtLocation(point)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Removes a set of blocks from the gameboard
        /// </summary>
        /// <param name="pointsToBeRemove">The position of the blocks to be removed</param>
        private void removeBlocksFromGameboard(Point[] pointsToBeRemove)
        {
            foreach (Point p in pointsToBeRemove)
            {
                this.gameBoard[p.X, p.Y] = null;
            }
        }

        /// <summary>
        /// Move the current Tetris piece down
        /// </summary>
        /// <returns>If the Tetris piece was successfully moved down return true</returns>
        public bool moveCurrentPieceDown()
        {
            //If the space below the current piece is clear then move down
            if (this.isBlocksBelowCurrentPieceClear())
            {

                this.removeBlocksFromGameboard(this.currentTetrisPiece.PieceLocations);

                this.currentTetrisPiece.moveDown();

                this.addBlocksToGameBoard(this.currentTetrisPiece.PieceLocations, this.currentTetrisPiece.Color);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Move the current Tetris piece left
        /// </summary>
        /// <returns>If the Tetris piece was successfully moved left return true</returns>
        public bool moveCurrentPieceLeft()
        {
            if (this.isBlocksLeftOfCurrentPieceClear())
            {
                this.removeBlocksFromGameboard(this.currentTetrisPiece.PieceLocations);

                this.currentTetrisPiece.moveLeft();

                this.addBlocksToGameBoard(this.currentTetrisPiece.PieceLocations, this.currentTetrisPiece.Color);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Move the current Tetris piece right
        /// </summary>
        /// <returns>If the Tetris piece was successfully moved right return true</returns>
        public bool moveCurrentPieceRight()
        {
            if (this.isBlocksRightOfCurrentPieceClear())
            {
                this.removeBlocksFromGameboard(this.currentTetrisPiece.PieceLocations);

                this.currentTetrisPiece.moveRight();

                this.addBlocksToGameBoard(this.currentTetrisPiece.PieceLocations, this.currentTetrisPiece.Color);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Rotate the current tetris piece clockwise
        /// </summary>
        /// <returns>If the Tetris piece was successfully rotated return true</returns>
        public bool rotateCurrentPieceClockwise()
        {
            if (this.isCurrentPieceAbleToRotateClockwise())
            {
                this.removeBlocksFromGameboard(this.currentTetrisPiece.PieceLocations);

                this.currentTetrisPiece.rotateClockwise();

                this.addBlocksToGameBoard(this.currentTetrisPiece.PieceLocations, this.currentTetrisPiece.Color);
            }
            else
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Rotate the current tetris piece counterclockwise
        /// </summary>
        /// <returns>If the Tetris piece was successfully rotated return true</returns>
        public bool rotateCurrentPieceCounterclockwise()
        {
            //TODO: Implement bool rotateCurrentPieceCounterclockwise()
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a tetris block at specific points on the gameboard
        /// </summary>
        /// <param name="pointsToBeAdded">Location of the block to be added</param>
        /// <param name="tetrisColor">The color of the tetris blcok to be added</param>
        private void addBlocksToGameBoard(Point[] pointsToBeAdded, TetrisColors tetrisColor)
        {
            foreach (Point p in pointsToBeAdded)
            {
                this.gameBoard[p.X, p.Y] = new TetrisBlock(tetrisColor);
            }
        }

        //The current algorithm is very inefficent
        /// <summary>
        /// Clears all of the currently completed lines
        /// </summary>
        /// <returns>Returns the number of lines that were cleared</returns>
        public int clearCompletedLines()
        {
            /* For each row see if the line is completed.  If it is then clear the line restructure the board then start
             * looking from the beginning for cleared lines.
             */
            int linesCleared = 0;
            for (int lineIndex = this.gameBoard.GetLength(1) - 1; lineIndex >= 0; lineIndex--)
            {
                if(this.isLineCompleted(lineIndex))
                {
                    this.clearLine(lineIndex);

                    this.restructureGameboard(lineIndex);

                    linesCleared++;
                }
            }

            if (linesCleared == 4)
            {
                this.currentScore += ((linesCleared * this.lineScore) + this.tetrisBonus);
            }
            else
            {
                this.currentScore += (linesCleared * this.lineScore);
            }

            this.currentNumberOfClearedLines += linesCleared;
            this.currentLevel = this.currentNumberOfClearedLines / 10;

            return linesCleared;
        }

        /// <summary>
        /// Lowers all blocks above a lineIndex by one
        /// </summary>
        /// <param name="lineIndex">The base line all other lines above will be lowered</param>
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

        /// <summary>
        /// Clears a line at a particular line index
        /// </summary>
        /// <param name="lineIndex">The index of the line to be removed</param>
        private void clearLine(int lineIndex)
        {
            for (int x = 0; x < this.gameBoard.GetLength(0); x++)
            {
                this.gameBoard[x, lineIndex] = null;
            }
        }

        //Returns the current number of completed lines
        /// <summary>
        /// The current number of lines that are complete
        /// </summary>
        /// <returns>An integer count of the number of completed lines</returns>
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

        /// <summary>
        /// Checks to see if a line at an index in clear
        /// </summary>
        /// <param name="lineIndex">The line to be checked</param>
        /// <returns>If all the blocks at a line index are occupied return true</returns>
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

        /// <summary>
        /// Checks to see if the current game is over
        /// </summary>
        /// <returns>If any line is the board is fully occupied return true</returns>
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

        /// <summary>
        /// Instantly moves the piece to the bottom of the game board
        /// </summary>
        public void slamCurrentPiece()
        {
            if (this.moveCurrentPieceDown())
            {
                this.slamCurrentPiece();
            }
        }
    }
}
