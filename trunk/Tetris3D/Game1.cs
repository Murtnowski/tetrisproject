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
       private Texture2D titleTexture;

       //Background
       private ScrollingBackground scrollingBackground;

       //Movement
       double totalTime = 0;
       double ElapsedRealTime = 0;

       //Components
       private InputHandler input;
       private TetrisGameState gamefield;
       private Camera camera;
       #endregion

       //Audio
       public AudioBank audio;
       Song backgroundMusic;

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
          //load fonts
         titleTexture = Content.Load<Texture2D>(@"Textures\Title");
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
         for (int i = -1; i <= gamefield.getGameField.GetLength(0); i++) //Bottom Row
         {
            BasicShape cube = new BasicShape(Vector3.One, new Vector3(i-4, -1, 0), TetrisColors.Gray);
            foundation.Add(cube);
         }

         for (int i = 0; i < gamefield.getGameField.GetLength(1); i++) //Containment Box 
         {
            BasicShape cubeLeft = new BasicShape(Vector3.One, new Vector3(-1-4, i, 0), TetrisColors.Gray);
            foundation.Add(cubeLeft);

            BasicShape cubeRight = new BasicShape(Vector3.One, new Vector3(gamefield.getGameField.GetLength(0)-4, i, 0), TetrisColors.Gray);
            foundation.Add(cubeRight);
         }
      }

      #endregion

      protected override void Update(GameTime gameTime)
      {
         float elapsedBackground = (float)gameTime.ElapsedGameTime.TotalSeconds;
         switch (this.gameState)
         {
            case GameState.Title:
               if (input.KeyboardState.WasKeyPressed(Keys.Enter))
               {
                  //MediaPlayer.Play(backgroundMusic);  //!!!!!!!!!!!!!!! COMMENT THIS OUT TO MAKE IT LOAD FASTER!!!!!!!!!!!!!!!
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
                if (!this.tetrisSession.moveCurrentPieceDown())
                {
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        this.Exit();
                    }
                    audio.PlayClearLineSound();
                    this.tetrisSession.clearCompletedLines();
                }
            }
            if (this.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                if (!this.tetrisSession.slamCurrentPiece())
                {
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        this.Exit();
                    }
                    audio.PlayClearLineSound();
                    this.tetrisSession.clearCompletedLines();
                }
            }
            if (this.input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                audio.PlayRotateSound();
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
             // TODO : Make it so you draw background.. then pieces.. then HUD without them conflicting graphically
             spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
             scrollingBackground.Draw(spriteBatch);
             spriteBatch.End();

             cubeEffect.World = camera.getRotationMatrix;
             cubeEffect.Begin();

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
               pass.Begin();

               for (int x = 0; x < gamefield.getGameField.GetLength(0); x++)//This block draws all the pieces on the gameboard, will not draw pieces until they enter its range
               {                                                            //Use code block below to see them before they enter the gamefield range
                  for (int y = 0; y < gamefield.getGameField.GetLength(1); y++)
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
         }
         base.Draw(gameTime);
      }

      private void DrawTitleScreen()
      {
         spriteBatch.Draw(titleTexture, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
         spriteBatch.DrawString(italicFont, "Press Enter", new Vector2(75f, 200f), Color.Red);
      }
   }
}