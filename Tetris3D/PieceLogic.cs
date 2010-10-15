using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XELibrary;

namespace Tetris3D
{
   class PieceLogic : Microsoft.Xna.Framework.DrawableGameComponent
   {
      public PieceLogic(Game game)
         : base(game)
      {
         input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
      }

      public List<Shape> getShapes
      {
         get { return shapes; }
         set { shapes = value; }
      }

      public int getPieceCount
      {
         get { return pieceCount; }
         set { pieceCount = value; }
      }

      public bool checkCollisionsBelow()
      {
         if (checkShapesPosition(-9) || pieceBeneath()) return true;
         else
            return false;
      }

      public bool pieceBeneath()
      {
         bool collision = false;
         for (int z = 0; z < shapes.Count - 1; z++)
         {
            {
               for (int i = 0; i < 4; i++)
               {
                  if (checkShapesPosition(((int)shapes.ElementAt(z).getBlock[i].getShapePosition.X),
                  ((int)shapes.ElementAt(z).getBlock[i].getShapePosition.Y + 1),
                  ((int)shapes.ElementAt(z).getBlock[i].getShapePosition.Z)))
                     collision = true;
               }
            }
         }
         return collision;
      }

      private bool checkShapesPosition(int xPosition, int yPosition, int zPosition)
      {
         bool collision = false;
         for (int i = 0; i < 4; i++)
         {
            if (shapes.ElementAt(pieceCount - 1).getBlock[i].getShapePosition.X == xPosition &&
                shapes.ElementAt(pieceCount - 1).getBlock[i].getShapePosition.Y == yPosition &&
                shapes.ElementAt(pieceCount - 1).getBlock[i].getShapePosition.Z == zPosition)
               collision = true;
         }
         return collision;
      }

      private bool checkShapesPosition(int yPosition)
      {
         bool collision = false;
         for (int i = 0; i < 4; i++)
         {
            if (shapes.ElementAt(pieceCount - 1).getBlock[i].getShapePosition.Y == yPosition)
               collision = true;
         }
         return collision;
      }


      public void MovePieces(String direction)
      {
         int x = 0;
         int y = 0;
         int z = 0;

         cube1 = shapes.ElementAt(pieceCount - 1).getBlock[0].getShapePosition;
         cube2 = shapes.ElementAt(pieceCount - 1).getBlock[1].getShapePosition;
         cube3 = shapes.ElementAt(pieceCount - 1).getBlock[2].getShapePosition;
         cube4 = shapes.ElementAt(pieceCount - 1).getBlock[3].getShapePosition;

         //if (direction == "Back")
         //   z = -1;
         //else if (direction == "Forward")
         //   z = +1;
         if (direction == "Down")
             y = -1;
         else if (direction == "Left")
         {
             if (cube1.X != -5 && cube2.X != -5 && cube3.X != -5 && cube4.X != -5) //ensures no cubes pass through the board
                 x = -1;
         }
         else if (direction == "Right")
         {
             if (cube1.X != 4 && cube2.X != 4 && cube3.X != 4 && cube4.X != 4) //ensures no cubes pass through the board
                 x = +1;
         }
         else
             y = -1;

         shapes.ElementAt(pieceCount - 1).getBlock[0].getShapePosition = new Vector3(cube1.X + x, cube1.Y + y, cube1.Z + z);
         shapes.ElementAt(pieceCount - 1).getBlock[1].getShapePosition = new Vector3(cube2.X + x, cube2.Y + y, cube2.Z + z);
         shapes.ElementAt(pieceCount - 1).getBlock[2].getShapePosition = new Vector3(cube3.X + x, cube3.Y + y, cube3.Z + z);
         shapes.ElementAt(pieceCount - 1).getBlock[3].getShapePosition = new Vector3(cube4.X + x, cube4.Y + y, cube4.Z + z);
      }

      public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
      {
         //if (WasPressed(Keys.Up))
         //{ MovePieces("Back"); }

         //if (WasPressed(Keys.Down))
         //{ MovePieces("Forward"); }

         if (WasPressed(Keys.Down))
          { MovePieces("Down"); }

         if (WasPressed(Keys.Left))
         { MovePieces("Left"); }

         if (WasPressed(Keys.Right))
         { MovePieces("Right"); }

         base.Update(gameTime);
      }

      private bool WasPressed(Keys keys)
      {
         if (input.KeyboardState.WasKeyPressed(keys))
            return (true);
         else
            return (false);
      }

      private static int pieceCount;
      private static List<Shape> shapes = new List<Shape>();
      private Vector3 cube1;
      private Vector3 cube2;
      private Vector3 cube3;
      private Vector3 cube4;
      private InputHandler input;
   }
}
