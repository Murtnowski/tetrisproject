using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris3D
{
    /**
     * The Game class is for level, score and generating random pieces
     */
    class Game
    {
        private Random generator;
        private int level;
        private int score;

        public Game()
        {
            generator = new Random();
            this.score = 0;
            this.level = 1;
        }

        public int whichLevel
        {
            get
            {
                return level;
            }
            set
            {
                this.level = value;
            }
        }

        public int currentScore
        {
            get
            {
                return score;
            }
            set
            {
                this.score = value;
            }
        }

        public Shape newShape()
        {
            int number = generator.Next(7) + 1;
            Shape currentShape = new Shape(ShapeType.NONE);
            switch (number)
            {
                case 1:
                    currentShape = new Shape(ShapeType.T);
                    break;
                case 2:
                    currentShape = new Shape(ShapeType.I);
                    break;
                case 3:
                    currentShape = new Shape(ShapeType.O);
                    break;
                case 4:
                    currentShape = new Shape(ShapeType.J);
                    break;
                case 5:
                    currentShape = new Shape(ShapeType.L);
                    break;
                case 6:
                    currentShape = new Shape(ShapeType.Z);
                    break;
                case 7:
                    currentShape = new Shape(ShapeType.S);
                    break;
            }
            return currentShape;
        }
    }
}
