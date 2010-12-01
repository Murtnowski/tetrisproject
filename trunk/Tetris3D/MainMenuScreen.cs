﻿using System;
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
        private Texture2D backGround;
        private Texture2D cursor;
        private Texture2D menuOptionNewGame;
        private Texture2D menuOptionOptions;
        private Texture2D menuOptionExit;

        private MainMenuOptions HighlightedOption = MainMenuOptions.NewGame;

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
        }

        public override void Update(GameTime gameTime)
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.HighlightedOption--;
                this.HighlightedOption = (MainMenuOptions)Math.Max((int)this.HighlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.HighlightedOption++;
                this.HighlightedOption = (MainMenuOptions)Math.Min((int)this.HighlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.HighlightedOption)
                {
                    case MainMenuOptions.NewGame: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ModeMenuScreen(this.screenManager.Game)); break;
                    case MainMenuOptions.Options: break;
                    case MainMenuOptions.Quit: this.screenManager.Game.Exit(); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.Draw(this.menuOptionNewGame, new Vector2(550, 415), Color.White);
            this.screenManager.batch.Draw(this.menuOptionOptions, new Vector2(550, 470), Color.White);
            this.screenManager.batch.Draw(this.menuOptionExit, new Vector2(550, 530), Color.White); 
            switch (this.HighlightedOption)
            {
                case MainMenuOptions.NewGame: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White); break;
                case MainMenuOptions.Options: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White); break;
                case MainMenuOptions.Quit: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 532), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}