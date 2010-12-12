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
        private TetrisScreen finishedTetrisScreen;

        private Texture2D background;
        private Texture2D cursor;
        private Texture2D gameOverMenu;
        private Texture2D highScoreBorder;

        private TextBox HighScoreScoreBox;
        private TextBox HighScoreInitialBox;

        private float fadeInBackground = 0f;
        private float fadeInOptions = 0f;

        private GameOverOptions highlightedOption = GameOverOptions.PlayAgain;

        private int numberOfTetrisGameOverOptions = 2;

        private HighScoreManager highScore;

        public GameOverScreen(Microsoft.Xna.Framework.Game game, TetrisScreen finishedTetrisScreen)
            : base(game)
        {
            this.finishedTetrisScreen = finishedTetrisScreen;
            this.finishedTetrisScreen.isDisabled = true;
            this.highScore = new HighScoreManager(finishedTetrisScreen.GameType);

            if (this.finishedTetrisScreen.tetrisSession.CurrentScore >= this.highScore.highscoreEntries[this.highScore.highscoreEntries.Length - 1].ScoreInt)
            {
                HighScoreForm foo = new HighScoreForm();
                foo.ShowDialog();
                String initals = foo.initialsBox.Text;
                this.highScore.Insert(initals, this.finishedTetrisScreen.tetrisSession.CurrentScore);
                this.highScore.Publish();
            }
        }

        public override void LoadContent()
        {
            this.background = this.content.Load<Texture2D>(@"Textures\Menus\GameOverScreen");
            this.cursor = this.content.Load<Texture2D>(@"Textures\cursor");
            this.gameOverMenu = this.content.Load<Texture2D>(@"Textures\Menus\GameOverMenu");
            this.highScoreBorder = this.content.Load<Texture2D>(@"Textures\Menus\HighScoresBorder");

            this.HighScoreInitialBox = new TextBox(this, new Vector2(873, 240), new Vector2(80, 410), @"Textures\UIFont", "");
            this.HighScoreInitialBox.TextAlign = TextBox.TextAlignOption.TopLeft;

            this.HighScoreScoreBox = new TextBox(this, new Vector2(1004, 240), new Vector2(100, 410), @"Textures\UIFont", "");
            this.HighScoreScoreBox.TextAlign = TextBox.TextAlignOption.TopRight;


            this.HighScoreInitialBox.Text = this.highScore.HighScoreInitialText;
            this.HighScoreInitialBox.spriteFont.LineSpacing = 41;
            this.HighScoreInitialBox.TextAlign = TextBox.TextAlignOption.TopCenter;
            this.HighScoreScoreBox.Text = this.highScore.HighScoreScoreText;
            this.HighScoreScoreBox.TextAlign = TextBox.TextAlignOption.TopCenter;

            this.screenManager.audio.PlayGameOverSound(0f);
            this.screenManager.audioController.Stop();
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
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down) && fadeInOptions >= .35f)
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (GameOverOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisGameOverOptions - 1);
            }
            //controls locked until options are visible
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter) && fadeInOptions >= .35f)
            {
                this.screenManager.removeScreen(this.finishedTetrisScreen);
                this.screenManager.removeScreen(this);
                switch (this.highlightedOption)
                {
                    case GameOverOptions.PlayAgain: this.screenManager.addScreen(this.getNewGame()); this.screenManager.audioController.Play(); break;
                    case GameOverOptions.Quit: this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.HighScoreInitialBox.ForeColor = new Color(Color.SkyBlue, this.fadeInOptions);
            this.HighScoreScoreBox.ForeColor = new Color(Color.SkyBlue, this.fadeInOptions);

            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.background, new Rectangle(0, 0, 1200, 900), new Color(Color.MistyRose, fadeInBackground));
            this.screenManager.batch.Draw(this.gameOverMenu, new Vector2(155, 375), new Color(Color.White, fadeInOptions));
            this.screenManager.batch.Draw(this.highScoreBorder, new Vector2(825, 135), new Color(Color.White, fadeInOptions));
            switch (this.highlightedOption)
            {
                case GameOverOptions.PlayAgain: this.screenManager.batch.Draw(this.cursor, new Vector2(120, 384),
                    new Color(Color.White, fadeInOptions)); break;
                case GameOverOptions.Quit: this.screenManager.batch.Draw(this.cursor, new Vector2(120, 493),
                    new Color(Color.White, fadeInOptions)); break;
                default: throw new NotImplementedException();
            }

            this.HighScoreInitialBox.Draw(this.screenManager.batch);
            this.HighScoreScoreBox.Draw(this.screenManager.batch);

            this.screenManager.batch.End();
        }

        private GameScreen getNewGame()
        {
            if (this.finishedTetrisScreen is ClassicTetrisScreen)
                return new ClassicTetrisScreen(this.screenManager.Game);
            else if (this.finishedTetrisScreen is Challenge1TetrisScreen)
                return new Challenge1TetrisScreen(this.screenManager.Game);
            else if (this.finishedTetrisScreen is Challenge2TetrisScreen)
                return new Challenge2TetrisScreen(this.screenManager.Game);
            else if (this.finishedTetrisScreen is Challenge3TetrisScreen)
                return new Challenge3TetrisScreen(this.screenManager.Game);
            else if (this.finishedTetrisScreen is Challenge4TetrisScreen)
                return new Challenge4TetrisScreen(this.screenManager.Game);
            else if (this.finishedTetrisScreen is MarathonTetrisScreen)
                return new MarathonTetrisScreen(this.screenManager.Game);
            else if (this.finishedTetrisScreen is TimeTrialScreen)
                return new TimeTrialScreen(this.screenManager.Game);
            else
                throw new NotImplementedException();

        }
    }
}
