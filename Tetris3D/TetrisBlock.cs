/*
 * Project: Tetris Project
 * Primary Author: Damon Chastain
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris3D
{
    /// <summary>
    /// An enumeration of the tetris piece colors
    /// </summary>
    public enum TetrisColors { Red, Magenta, Yellow, Cyan, Green, Blue, Orange, Gray};

    /// <summary>
    /// This class represents an individual cell on the tetris board
    /// </summary>
    public class TetrisBlock
    {
        /// <summary>
        /// The color of the tetris block
        /// </summary>
        private TetrisColors tetrisColor;

        /// <summary>
        /// The color of the tetris block
        /// </summary>
        public TetrisColors TetrisColor
        {
            get
            {
                return this.tetrisColor;
            }
        }

        /// <summary>
        /// Constructs a new Tetris block
        /// </summary>
        /// <param name="tetrisColor">The color of the tetris block</param>
        public TetrisBlock(TetrisColors tetrisColor)
        {
            this.tetrisColor = tetrisColor;
        }
    }
}
