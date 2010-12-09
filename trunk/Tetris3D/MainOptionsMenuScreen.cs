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
    public enum MainOptionsOptions {BackgroundMusic, FXMusic, Accept, Cancel};
    public enum VolumeOptions { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten };

    public class MainOptionsMenuScreen : GameScreen
    {
        private Texture2D backGround;
        private Texture2D cursor;

        private MainOptionsOptions highlightedOption = MainOptionsOptions.BackgroundMusic;
        private VolumeOptions backgroundMusicOption = VolumeOptions.Five;
        private VolumeOptions fxMusicOptoin = VolumeOptions.Five;

        private int numberOfTetrisMainMenuOptions = 5;

        public MainOptionsMenuScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");

            audio = new AudioBank();
            audio.LoadContent(this.content);

            audio.PlayMenuForwardSound();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (MainOptionsOptions)Math.Max((int)this.highlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (MainOptionsOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.highlightedOption)
                {
                    /*case ModeMenuOptions.Classic: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ClassicTetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.Marathon: this.screenManager.removeScreen(this); this.screenManager.addScreen(new MarathonTetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.TimeTrial: this.screenManager.removeScreen(this); this.screenManager.addScreen(new TimeTrialScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.Challenges: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ChallengesMenuScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.Back: this.screenManager.removeScreen(this); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;*/
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);

            switch (this.highlightedOption)
            {
                /*case ModeMenuOptions.Classic: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White);
                    //TODO: Draw text boxes by each game mode to give a short description
                    this.classicDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.Marathon: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White);
                    this.marathonDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.TimeTrial: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 527), Color.White);
                    this.timeTrialDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.Challenges: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 582), Color.White);
                    this.challengesDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 637), Color.White); break;*/
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
