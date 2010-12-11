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
    public enum ModeMenuOptions { Classic, Marathon, TimeTrial, Challenges, Back };

    public class ModeMenuScreen : GameScreen
    {
        private Texture2D backGround;
        private Texture2D cursor;
        private Texture2D menuOptionClassic;
        private Texture2D menuOptionChallenges;
        private Texture2D menuOptionTimeTrial;
        private Texture2D menuOptionMarathon;
        private Texture2D menuOptionBack;

        private TextBox classicDescriptionText;
        private TextBox marathonDescriptionText;
        private TextBox timeTrialDescriptionText;
        private TextBox challengesDescriptionText;

        private ModeMenuOptions highlightedOption = ModeMenuOptions.Classic;

        private int numberOfTetrisMainMenuOptions = 5;

        public ModeMenuScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");
            this.menuOptionClassic = this.content.Load<Texture2D>(@"Textures\Menus\Classic");
            this.menuOptionTimeTrial = this.content.Load<Texture2D>(@"Textures\Menus\TimeTrial");
            this.menuOptionMarathon = this.content.Load<Texture2D>(@"Textures\Menus\Marathon");
            this.menuOptionChallenges = this.content.Load<Texture2D>(@"Textures\Menus\Challenges");
            this.menuOptionBack = this.content.Load<Texture2D>(@"Textures\Menus\Back");

            this.classicDescriptionText = new TextBox(this, new Vector2(75, 410f), new Vector2(450, 400), @"Textures\Kootenay", 
                "Objective: Get the highest score possible without stacking over the top of the playing field", true);
            this.classicDescriptionText.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.classicDescriptionText.ForeColor = Color.Turquoise;
            this.marathonDescriptionText = new TextBox(this, new Vector2(75, 410f), new Vector2(450, 400), @"Textures\Kootenay",
                "Objective: Keep getting score until you can't play any longer.  Pause and level cap are disabled.  Get comfortable because you may be here for a while", true);
            this.marathonDescriptionText.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.marathonDescriptionText.ForeColor = Color.Thistle;
            this.timeTrialDescriptionText = new TextBox(this, new Vector2(75, 410f), new Vector2(450, 400), @"Textures\Kootenay",
                "Objective: Get the highest score possible with the 3 minutes provided.  Time will be added upon clearing a line", true);
            this.timeTrialDescriptionText.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.timeTrialDescriptionText.ForeColor = Color.Turquoise;
            this.challengesDescriptionText = new TextBox(this, new Vector2(75, 410f), new Vector2(450, 400), @"Textures\Kootenay",
                "Objective: Clear all of the pre-placed blocks on the board to achieve victory", true);
            this.challengesDescriptionText.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.challengesDescriptionText.ForeColor = Color.Thistle;

            this.screenManager.audio.PlayMenuForwardSound();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (ModeMenuOptions)Math.Max((int)this.highlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (ModeMenuOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.highlightedOption)
                {
                    case ModeMenuOptions.Classic: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ClassicTetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.Marathon: this.screenManager.removeScreen(this); this.screenManager.addScreen(new MarathonTetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.TimeTrial: this.screenManager.removeScreen(this); this.screenManager.addScreen(new TimeTrialScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ModeMenuOptions.Challenges: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ChallengesMenuScreen(this.screenManager.Game)); break;
                    case ModeMenuOptions.Back: this.screenManager.removeScreen(this); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.Draw(this.menuOptionClassic, new Vector2(550, 415), Color.White);
            this.screenManager.batch.Draw(this.menuOptionMarathon, new Vector2(550, 470), Color.White);
            this.screenManager.batch.Draw(this.menuOptionTimeTrial, new Vector2(550, 525), Color.White);
            this.screenManager.batch.Draw(this.menuOptionChallenges, new Vector2(550, 580), Color.White);
            this.screenManager.batch.Draw(this.menuOptionBack, new Vector2(550, 635), Color.White);

            switch (this.highlightedOption)
            {
                case ModeMenuOptions.Classic: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White);
                    //TODO: Draw text boxes by each game mode to give a short description
                    this.classicDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.Marathon: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White);
                    this.marathonDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.TimeTrial: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 527), Color.White);
                    this.timeTrialDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.Challenges: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 582), Color.White);
                    this.challengesDescriptionText.Draw(this.screenManager.batch); break;
                case ModeMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 637), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
