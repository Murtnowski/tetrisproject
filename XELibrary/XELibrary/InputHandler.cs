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
using Microsoft.Xna.Framework.Input;
#endregion

namespace XELibrary
{
    //This is an interface used by classes to invoke the InputHandler
    public interface IInputHandler
    {
        //Returns the current state of the keyboard
        KeyboardHandler KeyboardState { get; }

        //Returns the current state of the game controller
        GamePadState[] GamePads { get; }
        //Returns the current state of the buttons on the game controller
        ButtonHandler ButtonHandler { get; }

        //Returns the current state of the Mouse
        MouseState MouseState { get; }

        /* Returns the previous state of the mouse.  The previous state is compared to the
         * compared to the current state to detect mouseclicks
         */
        MouseState PreviousMouseState { get; }
    };

    /* This class is the the input handler for the various I/O devices the user interfaces with.
     * It extends the XNA GameComponent so the XNA game cycle and manage it and update it.
     */
    public partial class InputHandler
        : Microsoft.Xna.Framework.GameComponent, IInputHandler
    {
        //The various buttons types
        public enum ButtonType { A, B, Back, LeftShoulder, LeftStick, RightShoulder, RightStick, Start, X, Y }

        //The current state of the keyboard
        private KeyboardHandler keyboard;
        //The current state of the game controller buttons
        private ButtonHandler gamePadHandler = new ButtonHandler();

        //An array of game controller that manages them seperatly if more than one controller is connected.
        private GamePadState[] gamePads = new GamePadState[4];

        //the current state of the Mouse
        private MouseState mouseState;

        /* the previous state of the mouse.  The previous state is compared to the
         * compared to the current state to detect mouseclicks
         */
        private MouseState prevMouseState;

        /* The constructor class for the InputHandler.  It registers itself with the game services so it can
         * be invoked easier by other classes.
         */
        public InputHandler(Game game)
            : base(game)
        {
            //Registers the InputHandler with the game services
            game.Services.AddService(typeof(IInputHandler), this);
            
            //Initalizeds the keyboard handler
            keyboard = new KeyboardHandler();

            //Sets the mouse to visible
            Game.IsMouseVisible = true;

            //Initializes the Mouses previous state
            prevMouseState = Mouse.GetState();
        }

        // Inherited from XNA gamecomponent to initialize any logic.  No initialization is needed
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Updates the keyboards state
            keyboard.Update();

            //Updates the gamepad buttons
            gamePadHandler.Update();

            //If the escape key is held down, end the game
            if (keyboard.IsKeyDown(Keys.Escape))
                Game.Exit();

            //If the back button on the gamepad is held down end the game
            if (gamePadHandler.WasButtonPressed(0, ButtonType.Back))
                Game.Exit();

            //Update the previous mouse state
            prevMouseState = mouseState;

            //Set the current mousestate
            mouseState = Mouse.GetState();

            //Update all four gamepads
            gamePads[0] = GamePad.GetState(PlayerIndex.One);
            gamePads[1] = GamePad.GetState(PlayerIndex.Two);
            gamePads[2] = GamePad.GetState(PlayerIndex.Three);
            gamePads[3] = GamePad.GetState(PlayerIndex.Four);

            base.Update(gameTime);
        }


        #region IInputHandler Members
        public KeyboardHandler KeyboardState
        {
            get { return (keyboard); }
        }

        public ButtonHandler ButtonHandler
        {
            get { return (gamePadHandler); }
        }

        public GamePadState[] GamePads
        {
            get { return(gamePads); }
        }

#if !XBOX360
        public MouseState MouseState
        {
            get { return(mouseState); }
        }

        public MouseState PreviousMouseState
        {
            get { return(prevMouseState); }
        }
#endif
        #endregion
    }

    public class ButtonHandler //: IButtonHandler
    {
        private GamePadState[] prevGamePadsState = new GamePadState[4];
        private GamePadState[] gamePadsState = new GamePadState[4];

        public ButtonHandler()
        {
            prevGamePadsState[0] = GamePad.GetState(PlayerIndex.One);
            prevGamePadsState[1] = GamePad.GetState(PlayerIndex.Two);
            prevGamePadsState[2] = GamePad.GetState(PlayerIndex.Three);
            prevGamePadsState[3] = GamePad.GetState(PlayerIndex.Four);
        }

        public void Update()
        {
            //set our previous state to our new state
            prevGamePadsState[0] = gamePadsState[0];
            prevGamePadsState[1] = gamePadsState[1];
            prevGamePadsState[2] = gamePadsState[2];
            prevGamePadsState[3] = gamePadsState[3];

            //get our new state
            //gamePadsState = GamePad.State .GetState();
            gamePadsState[0] = GamePad.GetState(PlayerIndex.One);
            gamePadsState[1] = GamePad.GetState(PlayerIndex.Two);
            gamePadsState[2] = GamePad.GetState(PlayerIndex.Three);
            gamePadsState[3] = GamePad.GetState(PlayerIndex.Four);
        }        

        public bool WasButtonPressed(int playerIndex, InputHandler.ButtonType button)
        {

            int pi = playerIndex;
            switch(button)
            {
                case InputHandler.ButtonType.A:
                    {
                        return (gamePadsState[pi].Buttons.A == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.A == ButtonState.Released);
                    }
                case InputHandler.ButtonType.B:
                    {
                        return (gamePadsState[pi].Buttons.B == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.B == ButtonState.Released);
                    }
                case InputHandler.ButtonType.Back:
                    {
                        return (gamePadsState[pi].Buttons.Back == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.Back == ButtonState.Released);
                    }
                case InputHandler.ButtonType.LeftShoulder:
                    {
                        return (gamePadsState[pi].Buttons.LeftShoulder == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.LeftShoulder == ButtonState.Released);
                    }
                case InputHandler.ButtonType.LeftStick:
                    {
                        return (gamePadsState[pi].Buttons.LeftStick == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.LeftStick == ButtonState.Released);
                    }
                case InputHandler.ButtonType.RightShoulder:
                    {
                        return (gamePadsState[pi].Buttons.RightShoulder == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.RightShoulder == ButtonState.Released);
                    }
                case InputHandler.ButtonType.RightStick:
                    {
                        return (gamePadsState[pi].Buttons.RightStick == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.RightStick == ButtonState.Released);
                    }
                case InputHandler.ButtonType.Start:
                    {
                        return (gamePadsState[pi].Buttons.Start == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.Start == ButtonState.Released);
                    }
                case InputHandler.ButtonType.X:
                    {
                        return (gamePadsState[pi].Buttons.X == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.X == ButtonState.Released);
                    }
                case InputHandler.ButtonType.Y:
                    {
                        return (gamePadsState[pi].Buttons.Y == ButtonState.Pressed &&
                            prevGamePadsState[pi].Buttons.Y == ButtonState.Released);
                    }
                default:
                    throw (new ArgumentException());
            }
        }
    }

    public class KeyboardHandler //: IKeyboardHandler
    {
        private KeyboardState prevKeyboardState;
        private KeyboardState keyboardState;


        public KeyboardHandler()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }

        public bool IsHoldingKey(Keys key)
        {
            return(keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key));
        }

        public bool HasReleasedKey(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key));
        }

        public void Update()
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
    }
}


