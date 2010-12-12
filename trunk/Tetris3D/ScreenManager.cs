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
    //ScreenManager keeps track of the current screens, enabled or disabled, and allows the game to have multiple instances of screens happening at one time.
    //For instance, a pause screen may overlay a ClassicTetrisScreen.
    public class ScreenManager : DrawableGameComponent
    {
        //the current list of gamescreens
        List<GameScreen> gameScreens = new List<GameScreen>();
        //the list of screens that are going to be added
        List<GameScreen> screensToBeAdded = new List<GameScreen>();
        //the list of screens that are going to be removed
        List<GameScreen> screensToBeRemoved = new List<GameScreen>();
        //input for each screen
        public IInputHandler input;
        //spritebatch for each screen
        public SpriteBatch batch;
        //music for each screen
        public AudioController audioController;
        //sfx for each screen
        public AudioBank audio;

        public ScreenManager(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.input = (IInputHandler)this.Game.Services.GetService(typeof(IInputHandler));
            this.batch = spriteBatch;
            this.audioController = new AudioController(this.Game);
            this.audio = new AudioBank();
            this.audio.LoadContent(this.Game.Content);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //updates the music
            this.audioController.Update(gameTime);

            //updates the gametime for each screen that is currently enabled
            foreach (GameScreen gameScreen in this.gameScreens)
            {
                if (!gameScreen.isDisabled)
                {
                    gameScreen.Update(gameTime);
                }
            }
            //unloads the content of each screen to be removed and removes them
            foreach (GameScreen gameScreen in this.screensToBeRemoved)
            {
                gameScreen.UnloadContent();
                this.gameScreens.Remove(gameScreen);
            }
            //clears the list of screens to be removed for future input
            this.screensToBeRemoved.Clear();
            //adds the screens that are in the list to be added
            foreach (GameScreen gameScreen in this.screensToBeAdded)
            {
                gameScreen.screenManager = this;
                gameScreen.LoadContent();
                this.gameScreens.Add(gameScreen);
            }
            //clears the list of screens to be added
            this.screensToBeAdded.Clear();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //draw the gamescreens that aren't meant to be hidden
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
