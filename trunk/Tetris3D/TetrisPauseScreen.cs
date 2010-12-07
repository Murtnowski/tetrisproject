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

    public class TetrisPauseScreen : GameScreen
    {        
        private GameScreen screenToPause;

        private Texture2D Background;
        private Texture2D Cursor;
        private Texture2D PauseMenu;

        private TetrisPauseOptions highlightedOption = TetrisPauseOptions.Resume;

        private int numberOfTetrisMainMenuOptions = 2;

        public TetrisPauseScreen(Microsoft.Xna.Framework.Game game, GameScreen screenToPause) : base(game)
        {
            this.screenToPause = screenToPause;
            this.screenToPause.isDisabled = true;
            this.screenToPause.pauseAudio();
        }

        public override void LoadContent()
        {
            this.Background = this.content.Load<Texture2D>(@"Textures\blank");
            this.Cursor = this.content.Load<Texture2D>(@"Textures\cursor");
            this.PauseMenu = this.content.Load<Texture2D>(@"Textures\Menus\PauseMenu");
            //audio = new AudioBank();
            //audio.LoadContent(this.content);

            audio.PlayPauseSound();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (TetrisPauseOptions)Math.Max((int)this.highlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (TetrisPauseOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.highlightedOption)
                {
                    case TetrisPauseOptions.Resume: this.screenManager.removeScreen(this); this.screenToPause.isDisabled = false; this.screenToPause.resumeAudio(); break;
                    case TetrisPauseOptions.Quit: this.screenManager.removeScreen(this); this.screenManager.removeScreen(this.screenToPause); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.Background, new Rectangle(0, 0, 1200, 900), new Color(Color.White, 100));
            this.screenManager.batch.Draw(this.PauseMenu, new Vector2(555, 375), Color.White);
            switch (this.highlightedOption)
            {
                case TetrisPauseOptions.Resume: this.screenManager.batch.Draw(this.Cursor, new Vector2(520, 385), Color.White); break;
                case TetrisPauseOptions.Quit: this.screenManager.batch.Draw(this.Cursor, new Vector2(520, 455), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
