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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris3D
{
    //the main scrolldown options
    public enum MainOptionsOptions {BackgroundMusic, FXMusic, Accept};
    //the sub scroll-horizontal options within the main scrolldown options
    public enum VolumeOptions { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten };
    //the 3 modes that options may take
    public enum Mode { Selection, BackgroundMode, FXMode };

    //the options menu controls a few changable options that the user may want to capitalize on.  It incorporates menus with submenus which save changes for the
    //rest of the time that the program is open.  Changes will be lost once the program exits and restored to their defaults.
    public class OptionsMenuScreen : GameScreen
    {
        //the background used in most of the menu screens
        private Texture2D backGround;
        //the cursor used in most of the menu screens
        private Texture2D cursor;
        //a display of the options menu
        private Texture2D options;
        //the sub-cursor used to show volume levels
        private Texture2D tetrisCursor;
        //a way of keeping track of which mode is currently selected
        private Mode currentMode = Mode.Selection;
        //the option that the user is currently highlighting
        private MainOptionsOptions highlightedOption = MainOptionsOptions.BackgroundMusic;
        //the options that the user is currently highlighting on the right of the main option highlighted.  2 Suboptions
        private VolumeOptions backgroundMusicOption; //1
        private VolumeOptions fxMusicOption;         //2

        //the number of MainOptions (not suboptions) within the options screen.
        private int numberOfMainOptionsMenuOptions = 3;

        public OptionsMenuScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backgroundMusicOption = (VolumeOptions)(this.screenManager.audioController.Volume / 10);
            this.fxMusicOption = (VolumeOptions)(this.screenManager.audio.GetAllSoundEffectsVolume());

            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");
            this.options = this.content.Load<Texture2D>(@"Textures\Menus\OptionsDisplay");
            this.tetrisCursor = this.content.Load<Texture2D>(@"Textures\Menus\OptionsCursor");
            //play a sound when entering the options menu
            this.screenManager.audio.PlayMenuForwardSound();
        }

        public override void Update(GameTime gameTime)
        {
            //depending on which mode is currently selected...
            switch (this.currentMode)
            {
                //handle the selections accordingly, each with inputs of their own
                case Mode.Selection: this.HandleSelection(); break;
                case Mode.BackgroundMode: this.HandleBackgroundMode(); break;
                case Mode.FXMode: this.HandleFxMode(); break;
                default: throw new NotImplementedException();
            }
        }

        public void HandleSelection()
        {
            //if UP was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                //decrement the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (MainOptionsOptions)Math.Max((int)this.highlightedOption, 0);
            }
            //if DOWN was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                //increment the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (MainOptionsOptions)Math.Min((int)this.highlightedOption, this.numberOfMainOptionsMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                //depending on which option is highlighted...
                switch (this.highlightedOption)
                {
                    //change the current mode to the highlighted mode OR go back which essentially accepts the changes performed in Background or FX modes
                    case MainOptionsOptions.BackgroundMusic: this.currentMode = Mode.BackgroundMode; break;
                    case MainOptionsOptions.FXMusic: this.currentMode = Mode.FXMode; break;
                    case MainOptionsOptions.Accept: this.screenManager.audioController.Volume = (int)this.backgroundMusicOption * 10; 
                        this.screenManager.audio.SetAllSoundEffectsVolume((int)this.fxMusicOption);
                        this.screenManager.removeScreen(this); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public void HandleBackgroundMode()
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Left))
            {
                //decrement the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.backgroundMusicOption--;
                this.backgroundMusicOption = (VolumeOptions)Math.Max((int)this.backgroundMusicOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Right))
            {
                //increment the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.backgroundMusicOption++;
                this.backgroundMusicOption = (VolumeOptions)Math.Min((int)this.backgroundMusicOption, 9);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                //accept a new selection for the given mode
                this.currentMode = Mode.Selection;
            }
        }

        public void HandleFxMode()
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Left))
            {
                //decrement the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.fxMusicOption--;
                this.fxMusicOption = (VolumeOptions)Math.Max((int)this.fxMusicOption, 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Right))
            {
                //increment the highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.fxMusicOption++;
                this.fxMusicOption = (VolumeOptions)Math.Min((int)this.fxMusicOption, 10);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                //accept a new selection for the given mode
                this.currentMode = Mode.Selection;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //begin drawing
            this.screenManager.batch.Begin();
            //draw the background and the display for the options
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.Draw(this.options, new Vector2(525, 425), Color.White);
            //depending on which is the current mode...
            switch (this.currentMode)
            {
                //draw  the cursors in the given positions that are based upon the highlighted options and the background(or fx) options selections
                case Mode.Selection: 
                    this.screenManager.batch.Draw(this.cursor, new Vector2(490, 430 + (int)this.highlightedOption * 72), Color.White);
                    this.screenManager.batch.Draw(this.tetrisCursor, new Vector2(694 + (int)this.backgroundMusicOption * 34, 430), Color.Gray);
                    this.screenManager.batch.Draw(this.tetrisCursor, new Vector2(660 + (int)this.fxMusicOption * 34, 502), Color.Gray);
                    break;
                //draw  the cursors in the given positions that are based upon the highlighted options and the background(or fx) options selections
                case Mode.BackgroundMode: 
                    this.screenManager.batch.Draw(this.cursor, new Vector2(490, 430 + (int)this.highlightedOption * 72), Color.Gray);
                    this.screenManager.batch.Draw(this.tetrisCursor, new Vector2(694 + (int)this.backgroundMusicOption * 34, 430), Color.White);
                    this.screenManager.batch.Draw(this.tetrisCursor, new Vector2(660 + (int)this.fxMusicOption * 34, 502), Color.Gray);
                    break;
                //draw  the cursors in the given positions that are based upon the highlighted options and the background(or fx) options selections
                case Mode.FXMode: 
                    this.screenManager.batch.Draw(this.cursor, new Vector2(490, 430 + (int)this.highlightedOption * 72), Color.Gray);
                    this.screenManager.batch.Draw(this.tetrisCursor, new Vector2(694 + (int)this.backgroundMusicOption * 34, 430), Color.Gray);
                    this.screenManager.batch.Draw(this.tetrisCursor, new Vector2(660 + (int)this.fxMusicOption * 34, 502), Color.White);
                    break;
            }
            //finished drawing
            this.screenManager.batch.End();
        }
    }
}
