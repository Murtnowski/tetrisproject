using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris3D
{
    public enum TetrisColors { Red, Magenta, Yellow, Cyan, Green, Blue, Orange, Black };

    public class TetrisBlock
    {
        private TetrisColors tetrisColor;

        public TetrisColors TetrisColor
        {
            get
            {
                return this.tetrisColor;
            }
        }

        public TetrisBlock(TetrisColors tetrisColor)
        {
            this.tetrisColor = tetrisColor;
        }
    }
}
