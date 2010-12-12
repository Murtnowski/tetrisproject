#region File Description
//-----------------------------------------------------------------------------
// InputHandler.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// Borrowed from Microsoft XNA GameStudios 3.1 Unleashed (Textbook)
// Edited to fit the purposes of Tetris3D by Damon Chastain
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace XELibrary
{
    public partial class Camera : Microsoft.Xna.Framework.GameComponent
    {
        protected IInputHandler input;
        private GraphicsDevice graphics;

        private Matrix projection;
        private Matrix view;
        private Matrix rotationMatrix;

        protected Vector3 cameraPosition = new Vector3(23, 23, 23); //to zoom out of FOV +1 each vector component
        private Vector3 cameraTarget = new Vector3(0,10,0);
        private Vector3 cameraUpVector = Vector3.Up;

        private Vector3 cameraReference = new Vector3(0.0f, 0.0f, -1.0f);

        private float cameraYaw = 47.0f;   //should be set to 0 once testing is done
        private float cameraPitch = 3.0f;

        protected Vector3 movement = Vector3.Zero;
        
        private const float spinRate = 120.0f;
        private const float moveRate = 120.0f;

        private int playerIndex = 0;
        private Viewport? viewport;

        public Camera(Game game, GraphicsDevice graphicsDevice)
            : base(game)
        {
            //graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            this.graphics = graphicsDevice;
            input = (IInputHandler)game.Services.GetService(typeof(IInputHandler));
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializeCamera();
        }

        private void InitializeCamera()
        {
            float aspectRatio = (float)this.graphics.Viewport.Width /
                (float)this.graphics.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
                1.0f, 10000.0f, out projection);

            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget,
                ref cameraUpVector, out view);
        }
        
        public Matrix View
        {
            get { return view; }
        }

        public Matrix getRotationMatrix
        {
           get { return rotationMatrix; }
        }

        public Matrix Projection
        {
            get { return projection; }
        }

        public PlayerIndex PlayerIndex
        {
            get { return ((PlayerIndex)playerIndex); }
            set { playerIndex = (int)value; }
        }

        public Vector3 Position
        {
            get { return (cameraPosition); }
            set { cameraPosition = value; }
        }

        public Vector3 Orientation
        {
            get { return (cameraReference); }
            set { cameraReference = value; }
        }

        public Vector3 Target
        {
            get { return (cameraTarget); }
            set { cameraTarget = value; }
        }

        public Viewport Viewport
        {
            get
            {
                if (viewport == null)
                    viewport = this.graphics.Viewport;

                return ((Viewport)viewport);
            }
            set
            {
                viewport = value;
                InitializeCamera();
            }
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //cameraYaw += .2f;

            if (input.KeyboardState.IsKeyDown(Keys.Q) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.X < 0))
            {
               cameraYaw += (spinRate * timeDelta);              
            }
            if (input.KeyboardState.IsKeyDown(Keys.W) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.X > 0))
            {
               cameraYaw -= (spinRate * timeDelta);
            }

            if (input.KeyboardState.IsKeyDown(Keys.A) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.Y < 0))
            {
               cameraPitch -= (spinRate * timeDelta);
            }
            if (input.KeyboardState.IsKeyDown(Keys.S) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.Y > 0))
            {
               cameraPitch += (spinRate * timeDelta);
            }

            if ((input.PreviousMouseState.X > input.MouseState.X) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraYaw += (spinRate * timeDelta);
            }
            else if ((input.PreviousMouseState.X < input.MouseState.X) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraYaw -= (spinRate * timeDelta);
            }

            if ((input.PreviousMouseState.Y > input.MouseState.Y) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraPitch += (spinRate * timeDelta);
            }
            else if ((input.PreviousMouseState.Y < input.MouseState.Y) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraPitch -= (spinRate * timeDelta);
            }

            //reset camera angle if needed
            if (cameraYaw > 360)
                cameraYaw -= 360;
            else if (cameraYaw < 0)
                cameraYaw += 360;

            //keep camera from rotating a full 90 degrees in either direction
            if (cameraPitch > 89)
                cameraPitch = 89;
            if (cameraPitch < -89)
                cameraPitch = -89;

            movement *= (moveRate * timeDelta);

            Vector3 transformedReference;

            Matrix.CreateRotationY(MathHelper.ToRadians(cameraYaw), out rotationMatrix);

            if (movement != Vector3.Zero)
            {
                Vector3.Transform(ref movement, ref rotationMatrix, out movement);
                cameraPosition += movement;
            }

            //add in pitch to the rotation
            rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(cameraPitch)) * rotationMatrix;

            // Create a vector pointing the direction the camera is facing.
            Vector3.Transform(ref cameraReference, ref rotationMatrix,
                out transformedReference);
            // Calculate the position the camera is looking at.
            Vector3.Add(ref cameraPosition, ref transformedReference, out cameraTarget);

            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector,
                out view);

            base.Update(gameTime);
        }
    }
}


