/*
 * Project: Tetris Project
 * Primary Author: Damon Chastain
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
    public enum GameOverOptions { PlayAgain, Quit };

    class GameOverScreen : GameScreen
    {
        private GameScreen finishedGameplayScreen;

        private Texture2D background;
        private Texture2D cursor;
        private Texture2D gameOverMenu;

        private float fadeInBackground = 0f;
        private float fadeInOptions = 0f;

        private GameOverOptions highlightedOption = GameOverOptions.PlayAgain;

        private int numberOfTetrisGameOverOptions = 2;

        public GameOverScreen(Microsoft.Xna.Framework.Game game, GameScreen finishedGameplayScreen) : base(game)
        {
            this.finishedGameplayScreen = finishedGameplayScreen;
            this.finishedGameplayScreen.isDisabled = true;
        }

        public override void LoadContent()
        {
            this.background = this.content.Load<Texture2D>(@"Textures\Menus\GameOverScreen");
            this.cursor = this.content.Load<Texture2D>(@"Textures\cursor");
            this.gameOverMenu = this.content.Load<Texture2D>(@"Textures\Menus\GameOverMenu");

            this.screenManager.audio.PlayGameOverSound(0f);
            this.screenManager.audioController.Pause();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (fadeInBackground <= 1.50f) //background fades in first
                fadeInBackground += 0.003f;
            if (fadeInBackground >= 1.00f && fadeInOptions == 0f) // only plays once as the options are appearing
                this.screenManager.audio.PlaySecondGameOverSound();
            if (fadeInOptions <= 1.50f && fadeInBackground >= 1.00f) //options fade in next. must happen after fadeInBackground
                fadeInOptions += 0.004f;
            if (fadeInOptions >= 0.5f && fadeInBackground <= 2.0f)
            {
                this.screenManager.audio.PlayGameOverSound(-1f);
                fadeInBackground = 2.1f; //ensures this sound wont happen again
            }

            //controls locked until options are visible
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up) && fadeInOptions >= .35f)
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (GameOverOptions)Math.Max((int)this.highlightedOption, 0);
            }
            //controls locked until options are visible
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down)&& fadeInOptions >= .35f)
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (GameOverOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisGameOverOptions - 1);
            }
            //controls locked until options are visible
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter) && fadeInOptions >= .35f)
            {
                this.screenManager.removeScreen(this.finishedGameplayScreen);
                this.screenManager.removeScreen(this); 
                switch (this.highlightedOption)
                {
                    case GameOverOptions.PlayAgain: this.screenManager.addScreen(this.getLastGameType()); this.screenManager.audioController.Play(); break;
                    case GameOverOptions.Quit: this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.background, new Rectangle(0, 0, 1200, 900), new Color(Color.MistyRose, fadeInBackground));
            this.screenManager.batch.Draw(this.gameOverMenu, new Vector2(855, 375), new Color(Color.White, fadeInOptions));
            switch (this.highlightedOption)
            {
                case GameOverOptions.PlayAgain: this.screenManager.batch.Draw(this.cursor, new Vector2(820, 384), 
                    new Color(Color.White, fadeInOptions)); break;
                case GameOverOptions.Quit: this.screenManager.batch.Draw(this.cursor, new Vector2(820, 493), 
                    new Color(Color.White, fadeInOptions)); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
        private GameScreen getLastGameType()
        {
            if (this.finishedGameplayScreen is ClassicTetrisScreen)
                return new ClassicTetrisScreen(this.screenManager.Game);
            else if (this.finishedGameplayScreen is Challenge1TetrisScreen)
                return new Challenge1TetrisScreen(this.screenManager.Game);
            else if (this.finishedGameplayScreen is Challenge2TetrisScreen)
                return new Challenge2TetrisScreen(this.screenManager.Game);
            else if (this.finishedGameplayScreen is Challenge3TetrisScreen)
                return new Challenge3TetrisScreen(this.screenManager.Game);
            else if (this.finishedGameplayScreen is Challenge4TetrisScreen)
                return new Challenge4TetrisScreen(this.screenManager.Game);
            else if (this.finishedGameplayScreen is MarathonTetrisScreen)
                return new MarathonTetrisScreen(this.screenManager.Game);
            else if (this.finishedGameplayScreen is TimeTrialScreen)
                return new TimeTrialScreen(this.screenManager.Game);
            else
                throw new NotImplementedException();

        }
    }
}
