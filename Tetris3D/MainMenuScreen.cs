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
    public enum MainMenuOptions { NewGame, Options, Quit };

    public class MainMenuScreen : GameScreen
    {
        //the background used in most of the menu screens
        private Texture2D backGround;
        //the cursor used in most of the menu screens
        private Texture2D cursor;
        //the 3 visuals of the user's options
        private Texture2D menuOptionNewGame;//1
        private Texture2D menuOptionOptions;//2
        private Texture2D menuOptionExit;   //3
        //the option that the user is currently highlighting
        private MainMenuOptions highlightedOption = MainMenuOptions.NewGame;
        //the number of options given to the user in the main menu
        private int numberOfTetrisMainMenuOptions = 3;

        public MainMenuScreen(Microsoft.Xna.Framework.Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");
            this.menuOptionNewGame = this.content.Load<Texture2D>(@"Textures\Menus\NewGame");
            this.menuOptionOptions = this.content.Load<Texture2D>(@"Textures\Menus\Options");
            this.menuOptionExit = this.content.Load<Texture2D>(@"Textures\Menus\Exit");
            //Plays the beginSound without the vocal saying "begin"
            this.screenManager.audio.PlayBeginSound(false);
        }

        public override void Update(GameTime gameTime)
        {
            //if UP was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                //decrememnt highlighted option and play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption--;
                this.highlightedOption = (MainMenuOptions)Math.Max((int)this.highlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                //increment highlighted option ad play a sound
                this.screenManager.audio.PlayMenuScrollSound();
                this.highlightedOption++;
                this.highlightedOption = (MainMenuOptions)Math.Min((int)this.highlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                //depending on which option is highlighted...
                switch (this.highlightedOption)
                {
                    //remove this screen and add the screen depending on the highlighted option
                        // or quit the game
                    case MainMenuOptions.NewGame: 
                        this.screenManager.removeScreen(this); 
                        this.screenManager.addScreen(new ModeMenuScreen(this.screenManager.Game));
                        break;
                    case MainMenuOptions.Options:
                        this.screenManager.removeScreen(this);
                        this.screenManager.addScreen(new OptionsMenuScreen(this.screenManager.Game)); break;
                    case MainMenuOptions.Quit: this.screenManager.Game.Exit(); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //begin drawing
            this.screenManager.batch.Begin();
            //draw the background and the 3 menu options
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.Draw(this.menuOptionNewGame, new Vector2(550, 415), Color.White);
            this.screenManager.batch.Draw(this.menuOptionOptions, new Vector2(550, 470), Color.White);
            this.screenManager.batch.Draw(this.menuOptionExit, new Vector2(550, 530), Color.White); 
            //depending on the highlighted option...
            switch (this.highlightedOption)
            {
                //draw a cursor over the highlighted option
                case MainMenuOptions.NewGame: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White); break;
                case MainMenuOptions.Options: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White); break;
                case MainMenuOptions.Quit: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 532), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
