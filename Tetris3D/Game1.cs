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
   enum GameState //GameStates successfully divide all gameplay.  If Gamestate = menu then the user is in the menu
   {
      Title,
      Playing,
   }

   public class Game1 : Microsoft.Xna.Framework.Game
   {
      public Game1()
      {
          //setting neccessary defaults
         graphics = new GraphicsDeviceManager(this);
         graphics.PreferredBackBufferWidth = SCREEN_WIDTH; //set screen width
         graphics.PreferredBackBufferHeight = SCREEN_HEIGHT; //set screen height
         Content.RootDirectory = "Content";

         input = new InputHandler(this);
         Components.Add(input); //enable the use of user input

         logic = new PieceLogic(this);
         Components.Add(logic); //enable an instance of pieceLogic

         camera = new Camera(this);
         Components.Add(camera); //setup a camera
      }

      #region load
      protected override void Initialize() //used to initialize the game.  Only called once
      {
         spriteBatch = new SpriteBatch(graphics.GraphicsDevice); //declare the spritebatch
         base.Initialize(); 
         this.gameState = GameState.Title; //set the gamestate to be Title
      }

      protected override void LoadContent()
      {
         initializeWorld(); //initialize the world
         titleTexture = Content.Load<Texture2D>(@"Textures\Title"); //initialize textures
         mainFont = Content.Load<SpriteFont>(@"Textures\Kootenay"); //initialize font
         italicFont = Content.Load<SpriteFont>(@"Textures\Italic"); //iinitialize font
      }

      private void initializeWorld() //initializes the world where gameplay takes place
      {
         game = new GameLogic(); //creates an instance of GameLogic for use

         logic.getPieceCount = 0; //initializes the piece count (aka number of pieces on the board) to 0

         cubeEffect = new BasicEffect(GraphicsDevice, null); //initialize cubeeffect and set basic properties for it
         cubeEffect.TextureEnabled = true;

         cubeEffect.DiffuseColor = new Vector3(5.0f, 3.0f, 4.0f);
         cubeEffect.SpecularColor = new Vector3(.35f, 0.35f, 0.35f);
         cubeEffect.SpecularPower = 10.0f;
          // below is me toying with the idea of directional light instead of using default lighting (to gain a better understanding)
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
          // toying with directional light, finished
         cubeEffect.LightingEnabled = true;

         cubeEffect.Parameters["World"].SetValue(camera.getRotationMatrix); //setting matrices to be world, view and projection.  All commonly used
         cubeEffect.Parameters["View"].SetValue(camera.View);
         cubeEffect.Parameters["Projection"].SetValue(camera.Projection);
          
         for (int j = -10; j < 10; j++) //creation of the board cubes.
         {
                 for (int i = -6; i < 6; i++)
                 {
                         Cube cube = new Cube(new Vector3(i, j, 0)); 
                         cube.getShapeTexture = Content.Load<Texture2D>("Textures\\ColorMap");
                         cubeEffect.Texture = cube.getShapeTexture;
                         board.Add(cube);
                         if (j != -10 && i!=5) //leave a gap if it's not the base of the board
                             i = 4; //reach the other side of the board and place 1 more
                 }
         }
      }

      #endregion

      protected override void Update(GameTime gameTime) //this is used to update each tick of the game.  Everyhing done here impacts the next frame displayed on the scree.
      {
         switch (this.gameState) //depending on the gamestate...
         {
            case GameState.Title:
               if (input.KeyboardState.WasKeyPressed(Keys.Enter)) //if enter was pressed..
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
            totalTime += gameTime.ElapsedGameTime.Milliseconds; //timers used within the game
            ElapsedRealTime += gameTime.ElapsedRealTime.Milliseconds;

            if (logic.checkCollisionsBelow() == true)
            {
                if (logic.isGameOver()) //if pieces have reached the top of the board
                {
                    this.Exit();  //exit the game
                }
               currentShape = nextShape;
               nextShape = game.newShape(); //create a new piece to play
               logic.getShapes.Add(nextShape);
               logic.getPieceCount += 1;
            }

            if (totalTime > 1000) //1000 = 1 "tick" which is used to move the piece down. The tick will be a variable later on depending on level
            {
               logic.MovePieces("Down");
               totalTime = 0;
            }
         }
         base.Update(gameTime);
      }

      protected override void Draw(GameTime gameTime) //this method is used to draw images to the screen.  Typically in sync with the update
      {
         GraphicsDevice.RenderState.DepthBufferEnable = true; //presets 
         GraphicsDevice.RenderState.AlphaBlendEnable = false;
         GraphicsDevice.RenderState.AlphaTestEnable = false;

         if (gameState == GameState.Title) //draw the title screen
         {
            spriteBatch.Begin();
            DrawTitleScreen();
            spriteBatch.End();
         }

         if (gameState == GameState.Playing)
         {
            graphics.GraphicsDevice.Clear(Color.Gray); //gray background

            cubeEffect.World = camera.getRotationMatrix;
            cubeEffect.Begin();

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes) //draw the cubes
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
         }
         base.Draw(gameTime);
      }

      private void DrawTitleScreen() //used to display the title image onto the screen
      {
         spriteBatch.Draw(titleTexture, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
         spriteBatch.DrawString(italicFont, "Press Enter", new Vector2(75f, 200f), Color.Red);
      }

      #region fields
      private GraphicsDeviceManager graphics;
      private BasicEffect cubeEffect;

      //Base and piecs
      private List<Vector3> positions = new List<Vector3>();
      private List<Cube> board = new List<Cube>();
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
