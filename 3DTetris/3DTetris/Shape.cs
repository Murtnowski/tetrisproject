using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Tetris3D
{
    class Shape
    {
        ShapeType name;
        public Cube[] blocks;

        public Shape(ShapeType type)
        {
            this.blocks = new Cube[4];
            if (type == ShapeType.NONE)
            {

            }
            //Arbitraty start positions used right now to display separate pieces
            if (type == ShapeType.T)
            {
                blocks[0] = new Cube(new Vector3(2, 5, 0));
                blocks[1] = new Cube(new Vector3(3, 5, 0));
                blocks[2] = new Cube(new Vector3(3, 6, 0));
                blocks[3] = new Cube(new Vector3(4, 5, 0));
            }

            else if (type == ShapeType.O)
            {
                blocks[0] = new Cube(new Vector3(-2, 6, 0));
                blocks[1] = new Cube(new Vector3(-2, 5, 0));
                blocks[2] = new Cube(new Vector3(-1, 6, 0));
                blocks[3] = new Cube(new Vector3(-1, 5, 0));
            }

            else if (type == ShapeType.I)
            {
                blocks[0] = new Cube(new Vector3(0, 8, 0));
                blocks[1] = new Cube(new Vector3(0, 7, 0));
                blocks[2] = new Cube(new Vector3(0, 6, 0));
                blocks[3] = new Cube(new Vector3(0, 5, 0));
            }
            else if (type == ShapeType.S)
            {
                blocks[0] = new Cube(new Vector3(-4, 6, 0));
                blocks[1] = new Cube(new Vector3(-3, 6, 0));
                blocks[2] = new Cube(new Vector3(-3, 7, 0));
                blocks[3] = new Cube(new Vector3(-2, 7, 0));
            }
            else if (type == ShapeType.J)
            {
                blocks[0] = new Cube(new Vector3(2, 6, 0));
                blocks[1] = new Cube(new Vector3(2, 5, 0));
                blocks[2] = new Cube(new Vector3(2, 4, 0));
                blocks[3] = new Cube(new Vector3(1, 4, 0));
            }
            else if (type == ShapeType.L)
            {
                blocks[0] = new Cube(new Vector3(1, 7, 0));
                blocks[1] = new Cube(new Vector3(1, 6, 0));
                blocks[2] = new Cube(new Vector3(1, 5, 0));
                blocks[3] = new Cube(new Vector3(2, 5, 0));
            }
            else if (type == ShapeType.Z)
            {
                blocks[0] = new Cube(new Vector3(1, 7, 0));
                blocks[1] = new Cube(new Vector3(2, 7, 0));
                blocks[2] = new Cube(new Vector3(2, 6, 0));
                blocks[3] = new Cube(new Vector3(3, 6, 0));
            }
            this.name = type;
        }

        public ShapeType Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
    }
}
