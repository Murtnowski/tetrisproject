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
    public enum ChallengesMenuOptions { Challenge1, Challenge2, Challenge3, Challenge4, Back };

    //The menu used to display the different Challenges the user may select from.  It offers a visual representation of the highlighted challenge as well as a high score of each item.
    public class ChallengesMenuScreen : GameScreen
    {
        //the background used in most of the menu screens
        private Texture2D backGround;
        //the cursor used in most of the menu screens
        private Texture2D cursor;
        //in this menu, the challenges were cropped in from separate files so that the menu may expand if needed. 
        //Each menu option is followed by a visual example of the challenge
        private Texture2D menuOption1;
        private Texture2D menuExampleOption1;
        private Texture2D menuOption2;
        private Texture2D menuExampleOption2;
        private Texture2D menuOption3;
        private Texture2D menuExampleOption3;
        private Texture2D menuOption4;
        private Texture2D menuExampleOption4;
        private Texture2D menuOptionBack;
        //the option that the user is currently highlighting
        private ChallengesMenuOptions highlightedOption = ChallengesMenuOptions.Challenge1;
        //the number of options placed within the given menu
        private int numberOfTetrisMainMenuOptions = 5;

        public ChallengesMenuScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");
            this.menuOption1 = this.content.Load<Texture2D>(@"Textures\Menus\Challenge1");
            this.menuOption2 = this.content.Load<Texture2D>(@"Textures\Menus\Challenge2");
            this.menuOption3 = this.content.Load<Texture2D>(@"Textures\Menus\Challenge3");
            this.menuOption4 = this.content.Load<Texture2D>(@"Textures\Menus\Challenge4");
            this.menuOptionBack = this.content.Load<Texture2D>(@"Textures\Menus\Back");
            this.menuExampleOption1 = this.content.Load<Texture2D>(@"Textures\Menus\Smile");
            this.menuExampleOption2 = this.content.Load<Texture2D>(@"Textures\Menus\Stairway");
            this.menuExampleOption3 = this.content.Load<Texture2D>(@"Textures\Menus\Toolbox");
            this.menuExampleOption4 = this.content.Load<Texture2D>(@"Textures\Menus\Tsunami");

            this.screenManager.audio.PlayMenuForwardSound();
        }

        public override void Update(GameTime gameTime)
        {
            //if Up was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                //play a sound, decrement the highlighted option
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (ChallengesMenuOptions)Math.Max((int)this.highlightedOption, 0);
            }
            //if Down was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                //play a sound, incrememnt the highlight option
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (ChallengesMenuOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            //if Enter was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                //find where enter was pressed
                switch (this.highlightedOption)
                {
                    //remove the menu screen, add new screen and play music accordingly
                    case ChallengesMenuOptions.Challenge1: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge1TetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ChallengesMenuOptions.Challenge2: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge2TetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ChallengesMenuOptions.Challenge3: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge3TetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ChallengesMenuOptions.Challenge4: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge4TetrisScreen(this.screenManager.Game)); this.screenManager.audioController.Play(); break;
                    case ChallengesMenuOptions.Back: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ModeMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException(); //if a menu selection was chosen that isn't supported by our game
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //begin drawing
            this.screenManager.batch.Begin();
            //draw the background and options
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.Draw(this.menuOption1, new Vector2(550, 415), Color.White);
            this.screenManager.batch.Draw(this.menuOption2, new Vector2(550, 470), Color.White);
            this.screenManager.batch.Draw(this.menuOption3, new Vector2(550, 525), Color.White);
            this.screenManager.batch.Draw(this.menuOption4, new Vector2(550, 580), Color.White);
            this.screenManager.batch.Draw(this.menuOptionBack, new Vector2(550, 635), Color.White);

            //based on which option is highlighted...
            switch (this.highlightedOption)
            {
                //draw a cursor AND an example of the highlighted challenge
                case ChallengesMenuOptions.Challenge1: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White);
                    this.screenManager.batch.Draw(this.menuExampleOption1, new Vector2(350, 417), Color.White);  break;
                case ChallengesMenuOptions.Challenge2: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White);
                    this.screenManager.batch.Draw(this.menuExampleOption2, new Vector2(350, 417), Color.White); break;
                case ChallengesMenuOptions.Challenge3: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 527), Color.White);
                    this.screenManager.batch.Draw(this.menuExampleOption3, new Vector2(350, 417), Color.White); break;
                case ChallengesMenuOptions.Challenge4: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 582), Color.White);
                    this.screenManager.batch.Draw(this.menuExampleOption4, new Vector2(350, 417), Color.White); break;
                case ChallengesMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 637), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
