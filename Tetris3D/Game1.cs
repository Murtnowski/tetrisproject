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

         logic = new PieceLogic(this);
         Components.Add(logic);

         camera = new Camera(this);
         Components.Add(camera);
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
         game = new GameLogic();

         logic.getPieceCount = 0;

         cubeEffect = new BasicEffect(GraphicsDevice, null);
         cubeEffect.TextureEnabled = true;

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

         cubeEffect.Parameters["World"].SetValue(camera.getRotationMatrix);
         cubeEffect.Parameters["View"].SetValue(camera.View);
         cubeEffect.Parameters["Projection"].SetValue(camera.Projection);

         for (int i = -4; i < 4; i++)
         {
            for (int j = -4; j < 4; j++)
            {
               Cube cube = new Cube(new Vector3(j, 0, i));
               cube.getShapeTexture = Content.Load<Texture2D>("Textures\\ColorMap");
               cubeEffect.Texture = cube.getShapeTexture;
               foundation.Add(cube);
            }
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
                  currentShape = game.newShape();
                  logic.getShapes.Add(currentShape);
                  logic.getPieceCount += 1;
               }
               break;
            case GameState.Playing:

               break;
         }

         if (gameState == GameState.Playing)
         {
            totalTime += gameTime.ElapsedGameTime.Milliseconds;
            ElapsedRealTime += gameTime.ElapsedRealTime.Milliseconds;

            if (logic.checkCollisionsBelow() == true)
            {
               currentShape = nextShape;
               nextShape = game.newShape();
               logic.getShapes.Add(nextShape);
               logic.getPieceCount += 1;
            }

            if (totalTime > 1000)
            {
               logic.MovePieces("Down");
               totalTime = 0;
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

               foreach (Cube block in foundation)
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
      private List<Vector3> positions = new List<Vector3>();
      private List<Cube> foundation = new List<Cube>();
      Shape currentShape;
      Shape nextShape;

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
      GameLogic game;
      PieceLogic logic;

      //Components
      private InputHandler input;
      private Camera camera;

      #endregion
      //TODO: Make sure each move does not collide with another peice in respect to x & z
      //TODO: Make sure each move is within the the playing field
      //TODO: When the maximum amount of blocks occupy a y plane remove all blocks
      //TODO: Create a base structure
   }
}
