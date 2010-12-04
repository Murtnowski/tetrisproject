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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Tetris3D
{
    public abstract class GameScreen
    {
        public ContentManager content;

        public ScreenManager screenManager;
        public bool isHidden;
        public bool isDisabled;
        public AudioBank audio;
        public Song backgroundMusic;
        

        public GameScreen(Microsoft.Xna.Framework.Game game)
        {
            this.content = new ContentManager(game.Services, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime);

        public abstract void Update(GameTime gameTime);
    }
}
