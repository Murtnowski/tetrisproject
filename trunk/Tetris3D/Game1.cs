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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XELibrary;


namespace Tetris3D
{
   enum GameState
   {
      Title,
      Playing,
   }

   public class Game1 : Microsoft.Xna.Framework.Game
   {
      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
         graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
         Content.RootDirectory = "Content";

         input = new InputHandler(this);
         Components.Add(input);

         camera = new Camera(this);
         Components.Add(camera);

         gamefield = new TetrisGameState(this);
         Components.Add(gamefield);

         tetrisSession = new TetrisSession(new Vector2(10, 24)); 
      }

      #region load
      protected override void Initialize()
      {
         spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
         base.Initialize();
         this.gameState = GameState.Title;
      }

      protected override void LoadContent()
      {
         initializeWorld();
         titleTexture = Content.Load<Texture2D>(@"Textures\Title");
         mainFont = Content.Load<SpriteFont>(@"Textures\Kootenay");
         italicFont = Content.Load<SpriteFont>(@"Textures\Italic");
      }

      private void initializeWorld()
      {
         cubeEffect = new BasicEffect(GraphicsDevice, null);
         cubeEffect.TextureEnabled = true;
         cubeEffect.Texture = this.Content.Load<Texture2D>("Textures\\ColorMap");

         cubeEffect.Parameters["World"].SetValue(camera.getRotationMatrix);
         cubeEffect.Parameters["View"].SetValue(camera.View);
         cubeEffect.Parameters["Projection"].SetValue(camera.Projection);

         cubeEffect.DiffuseColor = new Vector3(5.0f, 3.0f, 4.0f);
         cubeEffect.SpecularColor = new Vector3(.35f, 0.35f, 0.35f);
         cubeEffect.SpecularPower = 10.0f;

         cubeEffect.DirectionalLight0.Enabled = true;
         cubeEffect.DirectionalLight0.DiffuseColor = Vector3.One;
         cubeEffect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(1.0f, -1.0f, -1.0f));
         cubeEffect.DirectionalLight0.SpecularColor = Vector3.One;

         cubeEffect.DirectionalLight1.Enabled = true;
         cubeEffect.DirectionalLight1.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
         cubeEffect.DirectionalLight1.Direction = Vector3.Normalize(new Vector3(-1.0f, -1.0f, 1.0f));
         cubeEffect.DirectionalLight1.SpecularColor = new Vector3(0.5f, 0.5f, 0.5f);

         cubeEffect.DirectionalLight2.Enabled = true;
         cubeEffect.DirectionalLight2.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
         cubeEffect.DirectionalLight2.Direction = Vector3.Normalize(new Vector3(-1.0f, 1.0f, -1.0f));
         cubeEffect.DirectionalLight2.SpecularColor = new Vector3(0.5f, 0.5f, 0.5f);

         cubeEffect.LightingEnabled = true;

         //Draw Border
         for (int i = -1; i <= gamefield.getGameField.GetLength(0); i++) //Bottom Row
         {
            BasicShape cube = new BasicShape(Vector3.One, new Vector3(i, -1, 0), TetrisColors.Black);
            foundation.Add(cube);
         }

         for (int i = 0; i < gamefield.getGameField.GetLength(1); i++) //Containment Box 
         {
            BasicShape cubeLeft = new BasicShape(Vector3.One, new Vector3(-1, i, 0), TetrisColors.Black);
            foundation.Add(cubeLeft);

            BasicShape cubeRight = new BasicShape(Vector3.One, new Vector3(gamefield.getGameField.GetLength(0), i, 0), TetrisColors.Black);
            foundation.Add(cubeRight);
         }
      }

      #endregion

      protected override void Update(GameTime gameTime)
      {
         switch (this.gameState)
         {
            case GameState.Title:
               if (input.KeyboardState.WasKeyPressed(Keys.Enter))
               {
                  this.gameState = GameState.Playing;
               }
               break;
            case GameState.Playing:
               break;
         }

         if (gameState == GameState.Playing)
         {
            totalTime += gameTime.ElapsedGameTime.Milliseconds;
            ElapsedRealTime += gameTime.ElapsedRealTime.Milliseconds;

            if (this.input.KeyboardState.WasKeyPressed(Keys.Left))
            {
               this.tetrisSession.moveCurrentPieceLeft();
            }

            if (this.input.KeyboardState.WasKeyPressed(Keys.Right))
            {
               this.tetrisSession.moveCurrentPieceRight();
            }
            if (this.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
               this.tetrisSession.moveCurrentPieceDown();
            }
            if (this.input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                this.tetrisSession.rotateCurrentPieceClockwise();
            }

            if (totalTime > (1000 - (this.tetrisSession.CurrentLevel + 1) * 100))
            {
               totalTime = 0;

               if (!this.tetrisSession.isBlocksBelowCurrentPieceClear())
               {
                  if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                  {
                     this.Exit();
                  }
                  this.tetrisSession.clearCompletedLines();
               }
               else
               {
                  this.tetrisSession.moveCurrentPieceDown();
               }
            }
         }
         base.Update(gameTime);
      }

      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.RenderState.DepthBufferEnable = true;
         GraphicsDevice.RenderState.AlphaBlendEnable = false;
         GraphicsDevice.RenderState.AlphaTestEnable = false;

         if (gameState == GameState.Title)
         {
            spriteBatch.Begin();
            DrawTitleScreen();
            spriteBatch.End();
         }

         if (gameState == GameState.Playing)
         {
            graphics.GraphicsDevice.Clear(Color.Gray);

            cubeEffect.World = camera.getRotationMatrix;

            cubeEffect.Begin();

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
               pass.Begin();

               //This will no render because it needs to be passed in from the current tetrisSession

               ////Draw Tetris Pieces
               //for (int x = 0; x < gamefield.getGameField.GetLength(0); x++)
               //{
               //   for (int y = 0; y < gamefield.getGameField.GetLength(1); y++)
               //   {
               //      if (gamefield.getGameField[x, y] != null)
               //      {
               //         BasicShape cube = new BasicShape(Vector3.One, new Vector3(x, y, 0), TetrisBlock.getColorOfTetrisPiece(gamefield.getGameField[x, y].TetrisPiece));
               //         cube.RenderShape(this.GraphicsDevice); 
               //      }
               //   }
               //}

               for (int x = 0; x < gamefield.getGameField.GetLength(0); x++)//This block draws all the pieces on the gameboard, will not draw pieces until they enter its range
               {                                                            //Use code block below to see them before they enter the gamefield range
                  for (int y = 0; y < gamefield.getGameField.GetLength(1); y++)
                  {
                     if (tetrisSession.GameBoard.GetValue(x, y) != null)
                     {
                        BasicShape cube = new BasicShape(Vector3.One, new Vector3(x, y, 0), tetrisSession.GameBoard[x,y].TetrisColor);
                        cube.RenderShape(this.GraphicsDevice);
                     }

                  }
               }

               //This block draws the individual pieces

               //foreach (Point points in tetrisSession.CurrentPiecePointLocations
               //{
               //   BasicShape cube = new BasicShape(Vector3.One, new Vector3(points.X, points.Y, 0), TetrisBlock.getColorOfTetrisPiece(tetrisSession.GameBoard[x,y].getPiece));
               //   cube.RenderShape(this.GraphicsDevice);
               //}

               foreach (BasicShape cube in foundation)//Draw containment cubes
               {
                  cube.RenderShape(GraphicsDevice);
               }
               pass.End();

            }
            cubeEffect.End();
         }
         base.Draw(gameTime);
      }

      private void DrawTitleScreen()
      {
         spriteBatch.Draw(titleTexture, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
         spriteBatch.DrawString(italicFont, "Press Enter", new Vector2(75f, 200f), Color.Red);
      }

      #region fields
      private GraphicsDeviceManager graphics;
      private BasicEffect cubeEffect;

      //Base and piecs
      private List<BasicShape> foundation = new List<BasicShape>();
      private TetrisSession tetrisSession;

      //Screens
      internal const int SCREEN_WIDTH = 1200;
      internal const int SCREEN_HEIGHT = 900;
      private SpriteBatch spriteBatch;
      private GameState gameState;
      private SpriteFont mainFont;
      private SpriteFont italicFont;
      private Texture2D titleTexture;

      //Movement
      double totalTime = 0;
      double ElapsedRealTime = 0;

      //Components
      private InputHandler input;
      private TetrisGameState gamefield;
      private Camera camera;
      #endregion
   }
}