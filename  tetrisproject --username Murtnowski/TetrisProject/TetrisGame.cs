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

namespace TetrisProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TetrisGame : Microsoft.Xna.Framework.Game
    {

        BasicEffect cubeEffect;

        //world matrix
        Matrix worldMatrix;
        Matrix cameraMatrix;
        Matrix projectionMatrix;

        //background
        Color background;
        Color blueBackground = new Color(10, 40, 125, 0);

        //screen
        internal const int SCREEN_WIDTH = 1200;
        internal const int SCREEN_HEIGHT = 900;

        //angles used in camera rotations
        int angle1 = 0;
        int angle2 = 0;
        int angle3 = 0;
        float scale = 1.0f;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH; //set screen width to 1200
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT; //set screen height to 900
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            initializeWorld(); //initializes world matrix, cube effect, camera
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //world rotation
            angle1 += 1;
            angle2 += 3;
            scale += .001f;
            if (scale > 10)
                scale = 0;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.RenderState.DepthBufferEnable = true;
            GraphicsDevice.RenderState.AlphaBlendEnable = false;
            GraphicsDevice.RenderState.AlphaTestEnable = false;

            graphics.GraphicsDevice.Clear(blueBackground);
            //world matrix alterations are used with variables to save previous changes
            worldMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(angle1)) *
                Matrix.CreateRotationX(MathHelper.ToRadians(angle2)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(angle3)) *
                Matrix.CreateScale(scale);
            cubeEffect.World = worldMatrix;

            cubeEffect.Begin();
            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                //begin rendering cube shapes

                Cube cube = new Cube(new Vector3(0, 0, 0));
                cube.RenderShape(GraphicsDevice);
                cube.shapeTexture = Content.Load<Texture2D>("Textures\\pearl");
                cubeEffect.Texture = cube.shapeTexture;
                pass.End();
            }
            cubeEffect.End();
            base.Draw(gameTime);
        }

        public void initializeWorld()
        {
            //first vector controls x,y,z depth, second moves position laterally, third flips board
            cameraMatrix = Matrix.CreateLookAt(
                new Vector3(9, 7, 9), new Vector3(-1, -1, -1), new Vector3(0, 1, 0));

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height, 1.0f, 100.0f);

            //  float tilt = MathHelper.ToRadians(23.0f); //tilt value to use for x & y
            //  worldMatrix = Matrix.CreateRotationX(tilt) * Matrix.CreateRotationY(tilt);
            cubeEffect = new BasicEffect(GraphicsDevice, null);
            // cubeEffect.World = worldMatrix;
            cubeEffect.View = cameraMatrix;
            cubeEffect.Projection = projectionMatrix;
            cubeEffect.TextureEnabled = true;
            // cubeEffect.EnableDefaultLighting();

        }
    }
}
