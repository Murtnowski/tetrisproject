/*
 * Project: Tetris Project
 * Primary Author: Matthew Urtnowski
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

        public override void Initialize()
        {
            base.Initialize();

            this.GraphicsDevice.RenderState.DepthBufferEnable = true;
            this.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            this.GraphicsDevice.RenderState.AlphaTestEnable = false;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameScreen gameScreen in this.gameScreens)
            {
                if (!gameScreen.isDisabled)
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
