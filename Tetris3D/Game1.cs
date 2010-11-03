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

            //logic = new PieceLogic(this);
        }

        #region load
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            this.tetrisGamefieldRenderer = new TetrisGamefieldRenderer(this);
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
            //game = new GameLogic();

            //logic.getPieceCount = 0;
        }

        #endregion

        protected override void Update(GameTime gameTime)
        {
            this.tetrisGamefieldRenderer.Update(gameTime);

            switch (this.gameState)
            {
                case GameState.Title:
                    if (input.KeyboardState.WasKeyPressed(Keys.Enter))
                    {
                        this.gameState = GameState.Playing;
                        //currentShape = game.newShape();
                        //logic.getShapes.Add(currentShape);
                        //logic.getPieceCount += 1;
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

                this.tetrisGamefieldRenderer.Draw(gameTime, this.tetrisSession.GameBoard);

                /*
                cubeEffect.World = camera.getRotationMatrix;
                cubeEffect.Begin();

                foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    foreach (Cube block in board)
                    {
                        block.RenderShape(GraphicsDevice);
                    }

                    foreach (Shape cur in logic.getShapes)
                    {
                        cur.getBlock[0].RenderShape(GraphicsDevice);
                        cur.getBlock[1].RenderShape(GraphicsDevice);
                        cur.getBlock[2].RenderShape(GraphicsDevice);
                        cur.getBlock[3].RenderShape(GraphicsDevice);                  
                    }

                    pass.End();
                }
                cubeEffect.End();
                */

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

        //Base and piecs
        private List<Vector3> positions = new List<Vector3>();

        private TetrisSession tetrisSession = new TetrisSession(new Vector2(10, 24));
        //Shape currentShape;
        //Shape nextShape;

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

        //Game & Logic
        //GameLogic game;
        //PieceLogic logic;

        //Components
        private InputHandler input;
        private TetrisGamefieldRenderer tetrisGamefieldRenderer;

        #endregion
        //TODO: Make sure each move does not collide with another peice in respect to x & z
        //TODO: Make sure each move is within the the playing field
        //TODO: When the maximum amount of blocks occupy a y plane remove all blocks
        //TODO: Create a base structure
    }
}
