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


namespace Tetris3D
{
    enum GameState
    {
        Title,
        Playing,
    }

    enum PieceType
    {
        T, O, I
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
        }

        #region load
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.Initialize();
            keyboardState = new KeyboardState();
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
            game = new Game();
            pieceCount = 0;

            cameraMatrix = Matrix.CreateLookAt(
                new Vector3(15, 11, 30), new Vector3(-1, -1, -1), new Vector3(0, 1, 0));

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height, 1.0f, 100.0f);

            cubeEffect = new BasicEffect(GraphicsDevice, null);
            cubeEffect.View = cameraMatrix;
            cubeEffect.Projection = projectionMatrix;
            cubeEffect.TextureEnabled = true;
            cubeEffect.EnableDefaultLighting();

            for (int i = -10; i < 10; i++)//create the board  Tetris board = 10x20
            {
                for (int j = -6; j <= 6; j++)
                {
                    Cube cube = new Cube(new Vector3(j, i, 0));
                    cube.shapeTexture = Content.Load<Texture2D>("Textures\\pete_text");
                    cubeEffect.Texture = cube.shapeTexture;
                    board.Add(cube);
                    if (i != -10) //basically skips filling the inside of the board unless it's placed at the base (
                        j += 11;
                }
            }
        }

        #endregion

        protected override void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            switch (this.gameState)
            {
                case GameState.Title:
                    if (KeyP(Keys.Enter) == true)
                    {
                        this.gameState = GameState.Playing;
                        currentShape = game.newShape();
                        shapes.Add(currentShape);
                        pieceCount += 1;
                    }
                    break;
                case GameState.Playing:

                    break;

            }
#if !XBOX
            if (gameState == GameState.Playing)
            {
                totalTime += gameTime.ElapsedGameTime.Milliseconds;
                ElapsedRealTime += gameTime.ElapsedRealTime.Milliseconds;

                if (boardBenath() == true)
                {
                    currentShape = nextShape;
                    nextShape = game.newShape();
                    shapes.Add(nextShape);
                    pieceCount += 1;
                }

                if (totalTime > 1000)
                {
                    MovePieces("Down");
                    totalTime = 0;
                }

                angle1 += 1;
                if (KeyP(Keys.Up) == true)
                {
                    //MovePieces("Back");
                }

                if (KeyP(Keys.Down) == true)
                {
                    MovePieces("Down");
                }

                if (KeyP(Keys.Left) == true)
                {
                    MovePieces("Left");
                }

                if (KeyP(Keys.Right) == true)
                {
                    MovePieces("Right");
                }

                //key presses to rotate/scale world view
                if (keyboardState.IsKeyDown(Keys.F))//Rotators
                {
                    angle1 += 2;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {

                    angle1 -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.E))//Pitch
                {
                    angle2 += 2;
                }
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    angle2 -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.T))//Yaw
                {
                    angle3 += 2;
                }
                if (keyboardState.IsKeyDown(Keys.G))
                {
                    angle3 -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.O)) //Scalars
                {
                    scale += 0.015f;
                }
                if (keyboardState.IsKeyDown(Keys.P))
                {
                    scale -= 0.015f;
                }
                if (KeyP(Keys.Delete) == true)
                {
                    this.Exit();
                }
            }
#endif
            base.Update(gameTime);
        }

        private void MovePieces(String direction)
        {
            /** 
             * need to use an XBOX 360 controller to handle world movement
             * bumpers for world rotation, y is not used here but will when the 
             * world rotates every 90 degress the direction arrows or joystick
             * will have to update to the new world perspective
             */
            int x = 0;
            int y = 0;
            int z = 0;

            cube1 = shapes.ElementAt(pieceCount - 1).blocks[0].shapePosition;
            cube2 = shapes.ElementAt(pieceCount - 1).blocks[1].shapePosition;
            cube3 = shapes.ElementAt(pieceCount - 1).blocks[2].shapePosition;
            cube4 = shapes.ElementAt(pieceCount - 1).blocks[3].shapePosition;

            if (direction == "Back")
                z = -1;
            else if (direction == "Forward")
                z = +1;
            else if (direction == "Left")
                x = -1;
            else if (direction == "Right")
                x = +1;
            else
                y = -1;


            shapes.ElementAt(pieceCount - 1).blocks[0].shapePosition = new Vector3(cube1.X + x, cube1.Y + y, cube1.Z + z);
            shapes.ElementAt(pieceCount - 1).blocks[1].shapePosition = new Vector3(cube2.X + x, cube2.Y + y, cube2.Z + z);
            shapes.ElementAt(pieceCount - 1).blocks[2].shapePosition = new Vector3(cube3.X + x, cube3.Y + y, cube3.Z + z);
            shapes.ElementAt(pieceCount - 1).blocks[3].shapePosition = new Vector3(cube4.X + x, cube4.Y + y, cube4.Z + z);
        }
        /**
         * Check for board beneath, need to add foreach loop to check for other
         * pieces as well
         * 
         * */
        private bool boardBenath()
        {
            if (shapes.ElementAt(pieceCount - 1).blocks[0].shapePosition.Y - 1 == -10 ||
            shapes.ElementAt(pieceCount - 1).blocks[1].shapePosition.Y - 1 == -10 ||
            shapes.ElementAt(pieceCount - 1).blocks[2].shapePosition.Y - 1 == -10 ||
            shapes.ElementAt(pieceCount - 1).blocks[3].shapePosition.Y - 1 == -10)
                return true;

            return false;
        }

        /**
         * Method to determine when a key has been pressed and depressed
         * @params k - key to check if pressed
         * */
        private bool KeyP(Keys k)
        {
            if (keyboardState.IsKeyDown(k) && oldKeyboardState.IsKeyUp(k))
            {
                return true;
            }
            else
                return false;
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

                worldMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(angle1)) *
                                Matrix.CreateRotationX(MathHelper.ToRadians(angle2)) *
                                Matrix.CreateRotationZ(MathHelper.ToRadians(angle3)) *
                                Matrix.CreateScale(scale);

                cubeEffect.World = worldMatrix;

                cubeEffect.Begin();
                foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    foreach (Cube block in board)
                    {
                        block.RenderShape(GraphicsDevice);
                    }

                    foreach (Shape cur in shapes)
                    {
                        cur.blocks[0].RenderShape(GraphicsDevice);
                        cur.blocks[1].RenderShape(GraphicsDevice);
                        cur.blocks[2].RenderShape(GraphicsDevice);
                        cur.blocks[3].RenderShape(GraphicsDevice);
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
        private KeyboardState keyboardState, oldKeyboardState;

        //World Matrix
        private Matrix worldMatrix;
        private Matrix cameraMatrix;
        private Matrix projectionMatrix;
        private int angle1 = 0;
        private int angle2 = 0;
        private int angle3 = 0;
        private float scale = 1.0f;

        //Base and piecs
        private List<Vector3> positions = new List<Vector3>();
        private List<Cube> board = new List<Cube>();
        private List<Shape> shapes = new List<Shape>();
        private Vector3 cube1;
        private Vector3 cube2;
        private Vector3 cube3;
        private Vector3 cube4;
        private int pieceCount;
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

        //Game
        Game game;
        #endregion
    }
}
