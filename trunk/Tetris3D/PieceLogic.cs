/*
 * Project: Tetris Project
 * Authors: Damon Chastain & Matthew Urtnowski 
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XELibrary;

namespace Tetris3D
{
    /* This class represents the logic used to determine how a tetris piece behaves and interacts with other
     * Tetris Pieces
     */
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

        //This returns weather or not a piece is below the current piece or the current piece has reached the bottom of the board.
        public bool checkCollisionsBelow()
        {
        if (checkShapesPosition(-9) || pieceBeneath()) 
            return true;
        else
            return false;
      }

        //This returns weather or not a peice is below the current piece
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

       //Determines weather a piece has exceeded the top of the permisable gamefield
      public bool isGameOver()
      {
          bool gameover = false;
          for (int z = 0; z < shapes.Count - 1; z++)
          {
              {
                  for (int i = 0; i < 4; i++)
                  {
                      if (shapes.ElementAt(z).getBlock[i].getShapePosition.Y > 10)
                      {
                          return true;
                      }
                  }
              }
          }
          return gameover;
      }

       //used to check the position of the shapes for collisions
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
       //used to check the position of the shapes for collisions
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

       //used to move the pieces, logically, within the board
      public void MovePieces(String direction)
      {
         int x = 0;
         int y = 0;
         int z = 0;

         cube1 = shapes.ElementAt(pieceCount - 1).getBlock[0].getShapePosition;
         cube2 = shapes.ElementAt(pieceCount - 1).getBlock[1].getShapePosition;
         cube3 = shapes.ElementAt(pieceCount - 1).getBlock[2].getShapePosition;
         cube4 = shapes.ElementAt(pieceCount - 1).getBlock[3].getShapePosition;

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

      public void RotatePieces(String direction) //not working properly at the moment!  --Damon
      { //PRESS SPACEBAR TO ROTATE
          bool rotationSuccess = false;  //this can be used later to identify if a wall bump is needed while rotating

          cube1 = shapes.ElementAt(pieceCount - 1).getBlock[0].getShapePosition;  //save Vector3 positions of each cube to be rotated
          cube2 = shapes.ElementAt(pieceCount - 1).getBlock[1].getShapePosition;
          cube3 = shapes.ElementAt(pieceCount - 1).getBlock[2].getShapePosition;
          cube4 = shapes.ElementAt(pieceCount - 1).getBlock[3].getShapePosition;

          Shape copy = shapes.ElementAt(pieceCount - 1); //used to pass in an instance of the shape to Rotation... not absolutely needed

          if (direction == "clockwise") //room for expansion if needed to create a counterclockwise rotation
          {
              if (shapes.ElementAt(pieceCount - 1).Rotation(copy, ref cube1, ref cube2, ref cube3, ref cube4)) //calls shape.rotation while passing in a reference of cube1-4 (which alters cube1-4
                  rotationSuccess = true; //the rotation was a success
              else
                  return; //the rotation failed (shouldn't happen, yet)
          }
          if (rotationSuccess) //if the rotation worked that means that cube1-4 were altered and now may change the location of the blocks
          {
              shapes.ElementAt(pieceCount - 1).getBlock[0].getShapePosition = new Vector3(cube1.X, cube1.Y, cube1.Z); //change the location of the blocks to match that of the new rotation 
              shapes.ElementAt(pieceCount - 1).getBlock[1].getShapePosition = new Vector3(cube2.X, cube2.Y, cube2.Z);
              shapes.ElementAt(pieceCount - 1).getBlock[2].getShapePosition = new Vector3(cube3.X, cube3.Y, cube3.Z);
              shapes.ElementAt(pieceCount - 1).getBlock[3].getShapePosition = new Vector3(cube4.X, cube4.Y, cube4.Z);
          }
      }

      public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
      {

         if (WasPressed(Keys.Down))
          { MovePieces("Down"); }

         if (WasPressed(Keys.Left))
         { MovePieces("Left"); }

         if (WasPressed(Keys.Right))
         { MovePieces("Right"); }

         if (WasPressed(Keys.Space))  
         { RotatePieces("clockwise"); }

         base.Update(gameTime);
      }
       //determines if a key was pressed
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
