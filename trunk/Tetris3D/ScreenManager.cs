﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XELibrary;

namespace Tetris3D
{
    public class ScreenManager : DrawableGameComponent
    {
        List<GameScreen> gameScreens = new List<GameScreen>();
        List<GameScreen> screensToBeAdded = new List<GameScreen>();
        List<GameScreen> screensToBeRemoved = new List<GameScreen>();
        public IInputHandler input;
        public SpriteBatch batch;

        public ScreenManager(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.input = (IInputHandler)this.Game.Services.GetService(typeof(IInputHandler));
            this.batch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameScreen gameScreen in this.gameScreens)
            {
                if (!gameScreen.isDiabled)
                {
                    gameScreen.Update(gameTime);
                }
            }

            foreach (GameScreen gameScreen in this.screensToBeAdded)
            {
                gameScreen.screenManager = this;
                gameScreen.LoadContent();
                this.gameScreens.Add(gameScreen);
            }

            this.screensToBeAdded.Clear();

            foreach (GameScreen gameScreen in this.screensToBeRemoved)
            {
                gameScreen.UnloadContent();
                this.gameScreens.Remove(gameScreen);
            }

            this.screensToBeRemoved.Clear();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen gameScreen in this.gameScreens)
            {
                if (!gameScreen.isHidden)
                {
                    gameScreen.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public void addScreen(GameScreen gameScreen)
        {
            this.screensToBeAdded.Add(gameScreen);
        }

        public void removeScreen(GameScreen gameScreen)
        {
            this.screensToBeRemoved.Add(gameScreen);
        }
    }
}