using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris3D
{
    //This is an enumeration of the currently supported tetris blocks.  If you change it be sure to update NumberOfSupportedTetrisPieces
    public enum TetrisPieces { TBlock = 0, SBlock, ZBlock, IBlock, JBlock, LBlock, SquareBlock };

    public class TetrisBlock
    {
        public const int NUMBEROFSUPPORTEDTETRISPIECES = 7;

        public TetrisPieces TetrisPiece;

        public TetrisBlock(TetrisPieces tetrisPiece)
        {
            this.TetrisPiece = tetrisPiece;
        }

        public static TetrisColors getColorOfTetrisPiece(TetrisPieces tetrisPiece)
        {
            switch (tetrisPiece)
            {
                case TetrisPieces.IBlock: return TetrisColors.Cyan;
                case TetrisPieces.JBlock: return TetrisColors.Green;
                case TetrisPieces.LBlock: return TetrisColors.Yellow;
                case TetrisPieces.SBlock: return TetrisColors.Red;
                case TetrisPieces.ZBlock: return TetrisColors.Orange;
                case TetrisPieces.SquareBlock: return TetrisColors.Magenta;
                case TetrisPieces.TBlock: return TetrisColors.Blue;
                default: throw new ArgumentOutOfRangeException("tetrisPiece", tetrisPiece, "");
            }
        }
    }
}
