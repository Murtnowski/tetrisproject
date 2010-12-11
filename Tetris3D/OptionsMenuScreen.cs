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
    public enum MainOptionsOptions {BackgroundMusic, FXMusic, Accept};
    public enum VolumeOptions { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten };
    public enum Mode { Selection, BackgroundMode, FXMode };

    public class OptionsMenuScreen : GameScreen
    {
        private Texture2D backGround;
        private Texture2D cursor;
        private Texture2D options;
        private Texture2D tetrisCursor;

        private Mode currentMode = Mode.Selection;
        private MainOptionsOptions highlightedOption = MainOptionsOptions.BackgroundMusic;
        private VolumeOptions backgroundMusicOption;
        private VolumeOptions fxMusicOption;

        private int numberOfMainOptionsMenuOptions = 3;

        public OptionsMenuScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backgroundMusicOption = (VolumeOptions)(this.screenManager.audioController.Volume / 10);
            this.fxMusicOption = (VolumeOptions)(this.screenManager.audio.getAllSoundEffectsVolume());

            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");
            this.options = this.content.Load<Texture2D>(@"Texture\Menu\OptionsDisplay");
            this.tetrisCursor = this.content.Load<Texture2D>(@"Texture\Menu\OptionsCursor");

            this.screenManager.audio.PlayMenuForwardSound();
        }

        public override void Update(GameTime gameTime)
        {
            switch (this.currentMode)
            {
                case Mode.Selection: this.HandleSelection(); break;
                case Mode.BackgroundMode: this.HandleBackgroundMode(); break;
                case Mode.FXMode: this.HandleFxMode(); break;
                default: throw new NotImplementedException();
            }
        }

        public void HandleSelection()
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (MainOptionsOptions)Math.Max((int)this.highlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (MainOptionsOptions)Math.Min((int)this.highlightedOption, this.numberOfMainOptionsMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.highlightedOption)
                {
                    case MainOptionsOptions.BackgroundMusic: this.currentMode = Mode.BackgroundMode; break;
                    case MainOptionsOptions.FXMusic: this.currentMode = Mode.FXMode; break;
                        //TODO: SET FX MUSIC
                    case MainOptionsOptions.Accept: this.screenManager.audioController.Volume = (int)this.backgroundMusicOption * 10; this.screenManager.removeScreen(this); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public void HandleBackgroundMode()
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Left))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.backgroundMusicOption--;
                this.backgroundMusicOption = (VolumeOptions)Math.Max((int)this.backgroundMusicOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Right))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.backgroundMusicOption++;
                this.backgroundMusicOption = (VolumeOptions)Math.Min((int)this.backgroundMusicOption, 9);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                this.currentMode = Mode.Selection;
            }
        }

        public void HandleFxMode()
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Left))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.fxMusicOption--;
                this.fxMusicOption = (VolumeOptions)Math.Max((int)this.fxMusicOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Right))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.fxMusicOption++;
                this.fxMusicOption = (VolumeOptions)Math.Min((int)this.fxMusicOption, 9);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                this.currentMode = Mode.Selection;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);

            switch (this.currentMode)
            {
                case Mode.Selection: 
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515, 300 + (int)this.highlightedOption * 100), Color.White);
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515 + (int)this.backgroundMusicOption * 25, 350), Color.Gray);
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515 + (int)this.fxMusicOption * 25, 450), Color.Gray);
                    break;

                case Mode.BackgroundMode: 
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515, 300 + (int)this.highlightedOption * 100), Color.Gray);
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515 + (int)this.backgroundMusicOption * 25, 350), Color.White);
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515 + (int)this.fxMusicOption * 25, 450), Color.Gray);
                    break;
                case Mode.FXMode: 
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515, 300 + (int)this.highlightedOption * 100), Color.Gray);
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515 + (int)this.backgroundMusicOption * 25, 350), Color.Gray);
                    this.screenManager.batch.Draw(this.cursor, new Vector2(515 + (int)this.fxMusicOption * 25, 450), Color.White);
                    break;
            }

            this.screenManager.batch.End();
        }
    }
}
