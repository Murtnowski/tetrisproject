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

        private int numberOfTetrisMainMenuOptions = 2;

        public GameOverScreen(Microsoft.Xna.Framework.Game game, GameScreen finishedGameplayScreen) : base(game)
        {
            this.finishedGameplayScreen = finishedGameplayScreen;
            this.finishedGameplayScreen.isDisabled = true;
            this.finishedGameplayScreen.pauseAudio();
        }

        public override void LoadContent()
        {
            this.background = this.content.Load<Texture2D>(@"Textures\Menus\GameOverScreen");
            this.cursor = this.content.Load<Texture2D>(@"Textures\cursor");
            this.gameOverMenu = this.content.Load<Texture2D>(@"Textures\Menus\GameOverMenu");
            audio = new AudioBank();
            audio.LoadContent(this.content);

            audio.PlayGameOverSound();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (fadeInBackground <= 1.50f) //background fades in first
                fadeInBackground = 0.002f + fadeInBackground;
            if (fadeInBackground >= 1.00f && fadeInOptions == 0f) // only plays once as the options are appearing
                audio.PlaySecondGameOverSound();
            if (fadeInOptions <= 1.50f && fadeInBackground >= 1.00f) //options fade in next. must happen after fadeInBackground
                fadeInOptions += 0.004f;

            //controls locked until options are visible
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up) && fadeInOptions >= .35f)
            {
                audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (GameOverOptions)Math.Max((int)this.highlightedOption, 0);
            }
            //controls locked until options are visible
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down)&& fadeInOptions >= .35f)
            {
                audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (GameOverOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            //controls locked until options are visible
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter) && fadeInOptions >= .35f)
            {
                this.screenManager.removeScreen(this.finishedGameplayScreen);
                this.screenManager.removeScreen(this); 
                switch (this.highlightedOption)
                {
                    case GameOverOptions.PlayAgain: this.screenManager.addScreen(new ClassicTetrisScreen(this.screenManager.Game)); break;
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
    }
}
