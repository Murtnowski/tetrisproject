using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris3D
{
    public enum TetrisColors { Red, Magenta, Yellow, Cyan, Green, Blue, Orange, Black };

    public class TetrisBlock
    {
        public TetrisColors TetrisColor;

        public TetrisBlock(TetrisColors tetrisColor)
        {
            this.TetrisColor = tetrisColor;
        }
    }
}
