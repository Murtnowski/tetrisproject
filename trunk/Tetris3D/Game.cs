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
    /// <summary>
    /// The state the game is currently in
    /// </summary>
    enum GameState
    {
        Title,
        Playing,
    }

    /// <summary>
    /// This class represents the Tetris Game and need heavy work
    /// </summary>
    // TODO: Create a game controller so we can remove all the game logic from this class
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region fields
        private GraphicsDeviceManager graphics;
        private BasicEffect cubeEffect;

        //Base and pieces
        private List<BasicShape> foundation = new List<BasicShape>();
        private TetrisSession tetrisSession;

        //Screens
        internal const int SCREEN_WIDTH = 1200;
        internal const int SCREEN_HEIGHT = 900;
        private SpriteBatch spriteBatch;
        private GameState gameState;
        private SpriteFont mainFont;
        private SpriteFont italicFont;

        //Background
        TetrisMainMenu tetrisMainMenu;
        private ScrollingBackground scrollingBackground;

        //Movement
        double totalTime = 0;
        double ElapsedRealTime = 0;

        //Components
        private InputHandler input;
        private Camera camera;
        #endregion

        //Audio
        public AudioBank audio;
        Song backgroundMusic;
        
        /// <summary>
        /// Constructs a new Game object
        /// </summary>
        /// TODO: Ensure this constructor uses a Singleton pattern
        public Game()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            this.graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            this.Content.RootDirectory = "Content";

            this.input = new InputHandler(this);
            this.Components.Add(input);

            this.camera = new Camera(this);
            this.Components.Add(camera);

            this.tetrisSession = new TetrisSession(new Vector2(10, 24)); 
        }

        #region load
        /// <summary>
        /// Initialize game services and data
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.Initialize();
            this.gameState = GameState.Title;
            this.tetrisMainMenu = new TetrisMainMenu(this);
        }

        /// <summary>
        /// Load the games media content
        /// </summary>
        protected override void LoadContent()
        {
            initializeWorld();
            //load fonts
            mainFont = Content.Load<SpriteFont>(@"Textures\Kootenay");
            italicFont = Content.Load<SpriteFont>(@"Textures\Italic");
            //load sound effects
            audio = new AudioBank();
            audio.LoadContent(Content);
            Services.AddService(typeof(AudioBank), audio);
            //load music
            backgroundMusic = Content.Load<Song>(@"Audio\bigButtz");
            //load background
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scrollingBackground = new ScrollingBackground();
            Texture2D backgroundTexture = Content.Load<Texture2D>(@"Textures\stars");
            scrollingBackground.Load(GraphicsDevice, backgroundTexture);
        }

        /// <summary>
        /// Initialize the 3D environment
        /// </summary>
        private void initializeWorld()
        {
            cubeEffect = new BasicEffect(GraphicsDevice, null);
            cubeEffect.TextureEnabled = true;
            cubeEffect.Texture = this.Content.Load<Texture2D>("Textures\\leather");

            cubeEffect.Parameters["World"].SetValue(camera.getRotationMatrix);
            cubeEffect.Parameters["View"].SetValue(camera.View);
            cubeEffect.Parameters["Projection"].SetValue(camera.Projection);

            cubeEffect.LightingEnabled = true;
            cubeEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            cubeEffect.PreferPerPixelLighting = true;

            cubeEffect.DirectionalLight0.Direction = new Vector3(1, -1, 0);
            cubeEffect.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
            cubeEffect.DirectionalLight0.Enabled = true;

            cubeEffect.DirectionalLight1.Enabled = true;
            cubeEffect.DirectionalLight1.DiffuseColor = Color.White.ToVector3();
            cubeEffect.DirectionalLight1.Direction = new Vector3(0, 1, 1);

            cubeEffect.DirectionalLight2.Enabled = true;
            cubeEffect.DirectionalLight2.DiffuseColor = Color.White.ToVector3();
            cubeEffect.DirectionalLight2.Direction = new Vector3(1, 0, -1);

            cubeEffect.SpecularPower = 32;
    
            //Draw Border
            for (int i = -1; i <= this.tetrisSession.GameBoard.GetLength(0); i++) //Bottom Row
            {
                BasicShape cube = new BasicShape(Vector3.One, new Vector3(i-4, -1, 0), TetrisColors.Gray);
                foundation.Add(cube);
            }

            for (int i = 0; i < this.tetrisSession.GameBoard.GetLength(1) - TetrisSession.GameOverRange; i++) //Containment Box 
            {
                BasicShape cubeLeft = new BasicShape(Vector3.One, new Vector3(-1-4, i, 0), TetrisColors.Gray);
                foundation.Add(cubeLeft);

                BasicShape cubeRight = new BasicShape(Vector3.One, new Vector3(this.tetrisSession.GameBoard.GetLength(0)-4, i, 0), TetrisColors.Gray);
                foundation.Add(cubeRight);
            }
        }

        #endregion


        /// <summary>
        /// Update the game states, inputs, and components
        /// </summary>
        /// <param name="gameTime">The time since last update</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsedBackground = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (this.gameState)
            {
                case GameState.Title:
                    this.tetrisMainMenu.Update(gameTime);

                    if (this.tetrisMainMenu.WasOptionSelected)
                    {
                        switch (this.tetrisMainMenu.SelectedOption)
                        {
                            case TetrisMainMenuOptions.NewGame: this.gameState = GameState.Playing; break;
                            case TetrisMainMenuOptions.Options: break;
                            case TetrisMainMenuOptions.Quit: this.Exit(); break;
                            default: throw new NotImplementedException();
                        }
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
                if (!this.tetrisSession.moveCurrentPieceDown())
                {
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        this.gameState = GameState.Title;
                        this.tetrisSession = new TetrisSession(new Vector2(this.tetrisSession.GameBoard.GetLength(0), this.tetrisSession.GameBoard.GetLength(1)));
                        this.tetrisMainMenu.WasOptionSelected = false;
                    }
                    audio.PlayClearLineSound();
                    this.tetrisSession.clearCompletedLines();
                }
            }
            if (this.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.tetrisSession.slamCurrentPiece();
                if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                {
                    this.gameState = GameState.Title;
                    this.tetrisSession = new TetrisSession(new Vector2(this.tetrisSession.GameBoard.GetLength(0), this.tetrisSession.GameBoard.GetLength(1)));
                    this.tetrisMainMenu.WasOptionSelected = false;
                }
                audio.PlayClearLineSound();
                this.tetrisSession.clearCompletedLines();
            }
            if (this.input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                audio.PlayRotateSound();
                this.tetrisSession.rotateCurrentPieceClockwise();
            }

            if (totalTime > (1000 - (this.tetrisSession.CurrentLevel * 100)))
            {
               this.totalTime = 0;

               if (!this.tetrisSession.isBlocksBelowCurrentPieceClear())
               {
                  if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                  {
                      this.gameState = GameState.Title;
                      this.tetrisSession = new TetrisSession(new Vector2(this.tetrisSession.GameBoard.GetLength(0), this.tetrisSession.GameBoard.GetLength(1)));
                      this.tetrisMainMenu.WasOptionSelected = false;
                  }
                  audio.PlayClearLineSound();
                  this.tetrisSession.clearCompletedLines();
               }
               else
               {
                  this.tetrisSession.moveCurrentPieceDown();
               }
            }
         }
         scrollingBackground.Update(elapsedBackground * 100);
         base.Update(gameTime);
      }

        /// <summary>
        /// Draws the games components
        /// </summary>
        /// <param name="gameTime">The time since last drawtime</param>
        protected override void Draw(GameTime gameTime)
        {
         GraphicsDevice.RenderState.DepthBufferEnable = true;
         GraphicsDevice.RenderState.AlphaBlendEnable = false;
         GraphicsDevice.RenderState.AlphaTestEnable = false;

         if (gameState == GameState.Title)
         {
            spriteBatch.Begin();
            this.tetrisMainMenu.Draw(gameTime);
            spriteBatch.End();
         }

         if (gameState == GameState.Playing)
         {
             //this.tetrisSession.Draw(gameTime, this.spriteBatch, this.GraphicsDevice);

             graphics.GraphicsDevice.Clear(Color.Gray);
             // TODO : Make it so you draw background.. then pieces.. then HUD without them conflicting graphically
             spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
             scrollingBackground.Draw(spriteBatch);
             spriteBatch.End();

             cubeEffect.World = camera.getRotationMatrix;
             cubeEffect.Begin();

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
               pass.Begin();

               for (int x = 0; x < this.tetrisSession.GameBoard.GetLength(0); x++)//This block draws all the pieces on the gameboard, will not draw pieces until they enter its range
               {                                                            //Use code block below to see them before they enter the gamefield range
                  for (int y = 0; y < this.tetrisSession.GameBoard.GetLength(1) - TetrisSession.GameOverRange; y++)
                  {
                     if (tetrisSession.GameBoard.GetValue(x, y) != null)
                     {
                        BasicShape cube = new BasicShape(Vector3.One, new Vector3(x-4, y, 0), tetrisSession.GameBoard[x,y].TetrisColor);
                        cube.RenderShape(this.GraphicsDevice);
                     }

                  }
               }

               foreach (BasicShape cube in foundation)//Draw containment cubes
               {
                  cube.RenderShape(GraphicsDevice);
               }
               pass.End();
            }

            cubeEffect.End();

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            spriteBatch.DrawString(italicFont, "Level: " + this.tetrisSession.CurrentLevel, new Vector2(75f, 200f), Color.Red);
            spriteBatch.DrawString(italicFont, "Lines: " + this.tetrisSession.CurrentNumberOfClearedLines, new Vector2(75f, 230f), Color.Red);
            spriteBatch.DrawString(italicFont, "Score: " + this.tetrisSession.CurrentScore, new Vector2(75f, 260f), Color.Red);
            spriteBatch.End();


         }
         base.Draw(gameTime);
      }
    }
}