/*
 * Project: Tetris Project
 * Authors: Damon Chastain & Matthew Urtnowski 
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace XELibrary
{
    /* This class is the the camera which manages matrix projections to transform the 3D Gamefield so it can
     * be viewed from different angles.
     * It extends the XNA GameComponent so the XNA game cycle and manage it and update it.
     */
    public partial class Camera : Microsoft.Xna.Framework.GameComponent
    {
        //Stores the input handler, so the user can move the camera angles
        protected IInputHandler input;

        //Stores the Graphics Device, so the Graphics can be changed
        private GraphicsDeviceManager graphics;

        //The Matrices used to transform the 3D Gamefield based on rotation, orientation, and perspective
        private Matrix projection;
        private Matrix view;
        private Matrix rotationMatrix;

        //Stores where the camera currently is in three dimensional space
        protected Vector3 cameraPosition = new Vector3(18, 18, 18); //to zoom out of FOV +1 each vector component
        //Stores where the camera is pointing
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = Vector3.Up;

        private Vector3 cameraReference = new Vector3(0.0f, 0.0f, -1.0f);

        //Stores the current angles the camera is using
        private float cameraYaw = 40.0f;   //should be set to 0 once testing is done
        private float cameraPitch = 0.0f;

        //Stores the current movement vector of the camera
        protected Vector3 movement = Vector3.Zero;
        
        //Assigns the spin and movement rate when the user wants to move the camera
        private const float spinRate = 120.0f;
        private const float moveRate = 120.0f;

        private int playerIndex = 0;
        private Viewport? viewport;

        //A contructor for the camera than invokes the graphics and input services
        public Camera(Game game)
            : base(game)
        {
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            input = (IInputHandler)game.Services.GetService(typeof(IInputHandler));
        }

        //As a XNA game component, this is called from the constructor
        public override void Initialize()
        {
            base.Initialize();
            InitializeCamera();
        }

        //This creates the initial view and setup for the camera
        private void InitializeCamera()
        {
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width /
                (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
                1.0f, 10000.0f, out projection);

            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget,
                ref cameraUpVector, out view);
        }
        
        //Returns the View Matrix
        public Matrix View
        {
            get { return view; }
        }

        //Returns the Rotation Matrix
        public Matrix getRotationMatrix
        {
           get { return rotationMatrix; }
        }

        //Returns the Projection Matrix
        public Matrix Projection
        {
            get { return projection; }
        }

        //Return the current player number
        public PlayerIndex PlayerIndex
        {
            get { return ((PlayerIndex)playerIndex); }
            set { playerIndex = (int)value; }
        }

        //Returns the position of the camera
        public Vector3 Position
        {
            get { return (cameraPosition); }
            set { cameraPosition = value; }
        }

        //Returns how the camera is orientated
        public Vector3 Orientation
        {
            get { return (cameraReference); }
            set { cameraReference = value; }
        }

        //Returns where the camera is pointing
        public Vector3 Target
        {
            get { return (cameraTarget); }
            set { cameraTarget = value; }
        }

        //3D graphics are projected onto a viewport
        public Viewport Viewport
        {
            get
            {
                if (viewport == null)
                    viewport = graphics.GraphicsDevice.Viewport;

                return ((Viewport)viewport);
            }
            set
            {
                viewport = value;
                InitializeCamera();
            }
        }

        /* As an XNA Game Component this is called by the XNA Game Cycle to update the
         * camera.  Often the camera needs to be updated by user input
         */
        public override void Update(GameTime gameTime)
        {
            //Time since last input collected
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //cameraYaw += .2f;

            //If Q or the right stick is moved right hit spin right
            if (input.KeyboardState.IsKeyDown(Keys.Q) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.X < 0))
            {
               cameraYaw += (spinRate * timeDelta);
            }
            //If W or the right stick is moved left spin left
            if (input.KeyboardState.IsKeyDown(Keys.W) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.X > 0))
            {
               cameraYaw -= (spinRate * timeDelta);
            }

            //If A or the righstick is moved up spin up
            if (input.KeyboardState.IsKeyDown(Keys.A) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.Y < 0))
            {
               cameraPitch -= (spinRate * timeDelta);
            }
            //If S or the rightstick is moved down spin down
            if (input.KeyboardState.IsKeyDown(Keys.S) ||
                (input.GamePads[playerIndex].ThumbSticks.Right.Y > 0))
            {
               cameraPitch += (spinRate * timeDelta);
            }

            //If the mouse is pressed and moving right spin right
            if ((input.PreviousMouseState.X > input.MouseState.X) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraYaw += (spinRate * timeDelta);
            }
            //If the mouse is pressed and moving left spin left
            else if ((input.PreviousMouseState.X < input.MouseState.X) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraYaw -= (spinRate * timeDelta);
            }

            //If the mouse is pressed and moving up spin up
            if ((input.PreviousMouseState.Y > input.MouseState.Y) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraPitch += (spinRate * timeDelta);
            }
            //If the mouse is pressed and moving down spin down
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
            
           //Calulated the movement rate based on speed * elapsed time
            movement *= (moveRate * timeDelta);

            //A temporary variable used to mangage rotations
            Vector3 transformedReference;

            //Rotate the camera along the Y axis
            Matrix.CreateRotationY(MathHelper.ToRadians(cameraYaw), out rotationMatrix);

            //If the camera is moving transfrom the current matrix and change the camera position
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


