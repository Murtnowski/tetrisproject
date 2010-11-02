using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XELibrary;

namespace Tetris3D
{
    public class TetrisGamefieldRenderer
    {
        private IInputHandler input;

        private Matrix worldMatrix;
        private Matrix cameraMatrix;
        private Matrix projectionMatrix;

        private Game game;

        private BasicEffect cubeEffect;

        private float xAngle = 0.0f;
        private float yAngle = 0.0f;
        private float zAngle = 0.0f;

        private float rotationSpeed = 0.05f;
        private float maxRotationSpeed = 10.0f;

        public TetrisGamefieldRenderer(Game game)
        {
            this.game = game;
            input = (IInputHandler)game.Services.GetService(typeof(IInputHandler));
            this.initializeWorld();
        }

        private void initializeWorld()
        {
            cameraMatrix = Matrix.CreateLookAt(
                new Vector3(25, 25, 25), new Vector3(5, 10, 0), Vector3.Up);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                this.game.Window.ClientBounds.Width / this.game.Window.ClientBounds.Height, 1.0f, 100.0f);

            float tilt = MathHelper.ToRadians(0.0f);
            worldMatrix = Matrix.CreateRotationX(tilt) * Matrix.CreateRotationY(tilt);


            cubeEffect = new BasicEffect(this.game.GraphicsDevice, null);

            cubeEffect.World = worldMatrix;
            cubeEffect.View = cameraMatrix;
            cubeEffect.Projection = projectionMatrix;

            cubeEffect.TextureEnabled = true;
            cubeEffect.Texture = this.game.Content.Load<Texture2D>("Textures\\ColorMap");
        }

        public void Update(GameTime gameTime)
        {
            if (input.KeyboardState.IsKeyDown(Keys.Q) ||
                (input.GamePads[0].ThumbSticks.Right.X < 0))
            {
                if (this.xAngle < 0)
                {
                    this.xAngle = 0;
                }
                this.xAngle += this.rotationSpeed;
                this.xAngle = Math.Min(this.maxRotationSpeed, this.xAngle);
                worldMatrix *= Matrix.CreateRotationX(MathHelper.ToRadians(xAngle));

                cubeEffect.World = worldMatrix;
            }
            else
            {
                if (this.xAngle > 0)
                {
                    this.xAngle = 0;
                }
            }

            if (input.KeyboardState.IsKeyDown(Keys.W) ||
                (input.GamePads[0].ThumbSticks.Right.X > 0))
            {
                if (this.xAngle > 0)
                {
                    this.xAngle = 0;
                }

                this.xAngle -= this.rotationSpeed;
                this.xAngle = Math.Max(-this.maxRotationSpeed, this.xAngle);
                worldMatrix *= Matrix.CreateRotationX(MathHelper.ToRadians(xAngle));

                cubeEffect.World = worldMatrix;
            }
            else
            {
                if (this.xAngle < 0)
                {
                    this.xAngle = 0;
                }
            }

            if (input.KeyboardState.IsKeyDown(Keys.A) ||
                (input.GamePads[0].ThumbSticks.Right.Y < 0))
            {
                if (this.yAngle < 0)
                {
                    this.yAngle = 0;
                }

                this.yAngle += this.rotationSpeed;
                this.yAngle = Math.Min(this.maxRotationSpeed, this.yAngle);
                worldMatrix *= Matrix.CreateRotationY(MathHelper.ToRadians(yAngle));

                cubeEffect.World = worldMatrix;
            }
            else
            {
                if (this.yAngle > 0)
                {
                    this.yAngle = 0;
                }
            }

            if (input.KeyboardState.IsKeyDown(Keys.S) ||
                (input.GamePads[0].ThumbSticks.Right.Y > 0))
            {
                if (this.yAngle > 0)
                {
                    this.yAngle = 0;
                }

                this.yAngle -= this.rotationSpeed;
                this.yAngle = Math.Max(-this.maxRotationSpeed, this.yAngle);
                worldMatrix *= Matrix.CreateRotationY(MathHelper.ToRadians(yAngle));

                cubeEffect.World = worldMatrix;
            }
            else
            {
                if (this.yAngle < 0)
                {
                    this.yAngle = 0;
                }
            }
        }

        public void Draw(GameTime gameTime, TetrisBlock[,] gamefield)
        {
            cubeEffect.Begin();

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                //Draw Boarder
                for (int i = -1; i <= gamefield.GetLength(0); i++)
                {
                    BasicShape cube = new BasicShape(Vector3.One, new Vector3(i, -1, 0), TetrisColors.Black);

                    cube.RenderShape(this.game.GraphicsDevice);
                }

                for (int i = 0; i < gamefield.GetLength(1); i++)
                {
                    BasicShape cubeLeft = new BasicShape(Vector3.One, new Vector3(-1, i, 0), TetrisColors.Black);
                    cubeLeft.RenderShape(this.game.GraphicsDevice);

                    BasicShape cubeRight = new BasicShape(Vector3.One, new Vector3(gamefield.GetLength(0), i, 0), TetrisColors.Black);
                    cubeRight.RenderShape(this.game.GraphicsDevice);
                }

                //Draw Tetris Pieces
                for (int x = 0; x < gamefield.GetLength(0); x++)
                {
                    for (int y = 0; y < gamefield.GetLength(1); y++)
                    {
                        if (gamefield[x, y] != null)
                        {
                            BasicShape cube = new BasicShape(Vector3.One, new Vector3(x, y, 0), gamefield[x, y].TetrisColor);

                            cube.RenderShape(this.game.GraphicsDevice);
                        }
                    }
                }

                pass.End();
            }

            cubeEffect.End();
        }
    }
}