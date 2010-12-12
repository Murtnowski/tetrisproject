/*
 * Project: Tetris Project
 * Authors: Matthew Urtnowski & Damon Chastain
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
    public enum TetrisPauseOptions { Resume, Quit };

    //TetrisPauseScreen is a screen that is implemented (IN-GAME) when the player needs to pause the game.  It displays a background image of a previously disabled screen, with a dulled
    //hue, and presents the player with the options of returning to the game or quitting it altogether.
    public class TetrisPauseScreen : GameScreen
    {        
        private GameScreen screenToPause;

        //the background of the pause screen
        private Texture2D Background;
        //the cursor used in most of the menu screens
        private Texture2D Cursor;
        //the visual of the options to choose from in the pause screen
        private Texture2D PauseMenu;
        //the option that the user is currently highlighting
        private TetrisPauseOptions highlightedOption = TetrisPauseOptions.Resume;
        //the number of options available in this screen
        private int numberOfTetrisMainMenuOptions = 2;

        public TetrisPauseScreen(Microsoft.Xna.Framework.Game game, GameScreen screenToPause) : base(game)
        {
            //preserves the screen that is getting disabled
            this.screenToPause = screenToPause;
            this.screenToPause.isDisabled = true;
        }

        public override void LoadContent()
        {
            this.Background = this.content.Load<Texture2D>(@"Textures\blank");
            this.Cursor = this.content.Load<Texture2D>(@"Textures\cursor");
            this.PauseMenu = this.content.Load<Texture2D>(@"Textures\Menus\PauseMenu");

            //play sound upon entering the pause screen as well as pausing the background music
            this.screenManager.audio.PlayPauseSound();
            this.screenManager.audioController.Pause();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //if UP was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                //decrement the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (TetrisPauseOptions)Math.Max((int)this.highlightedOption, 0);
            }
            //if DOWN was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                //increment the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (TetrisPauseOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            //if ENTER was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                //depending on which option is highlighted...
                switch (this.highlightedOption)
                {
                    //RESUME SELECTED:remove the pause screen, ENABLE the previously paused screen, play a sound indicating the game was resumed, resume the background music
                    case TetrisPauseOptions.Resume: this.screenManager.removeScreen(this); this.screenToPause.isDisabled = false;
                        this.screenManager.audio.PlayResumedSound(); this.screenManager.audioController.Resume();  break;
                    //QUIT SELECTED:remove the pause screen, remove the previously disabled screen, add a mainmenu screen, STOP the background music
                    case TetrisPauseOptions.Quit: this.screenManager.removeScreen(this); this.screenManager.removeScreen(this.screenToPause); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); this.screenManager.audioController.Stop(); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //begin drawing
            this.screenManager.batch.Begin();
            //draw the pause screen and the menu of pause selections
            this.screenManager.batch.Draw(this.Background, new Rectangle(0, 0, 1200, 900), new Color(Color.White, 100));
            this.screenManager.batch.Draw(this.PauseMenu, new Vector2(555, 375), Color.White);
            //depending on which option is highlighted...
            switch (this.highlightedOption)
            {
                //draw a cursor over the highlighted option
                case TetrisPauseOptions.Resume: this.screenManager.batch.Draw(this.Cursor, new Vector2(520, 385), Color.White); break;
                case TetrisPauseOptions.Quit: this.screenManager.batch.Draw(this.Cursor, new Vector2(520, 455), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
