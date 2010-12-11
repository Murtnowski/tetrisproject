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
using XELibrary;

namespace Tetris3D
{
    public abstract class TetrisScreen : GameScreen
    {
        protected Texture2D IPieceTexture;
        protected Texture2D JPieceTexture;
        protected Texture2D LPieceTexture;
        protected Texture2D OPieceTexture;
        protected Texture2D SPieceTexture;
        protected Texture2D TPieceTexture;
        protected Texture2D ZPieceTexture;

        protected SpriteFont uiFont;

        protected int numberOfLinesCleared;

        protected TimeSpan timer = new TimeSpan();
        protected double timeSinceLastTick = 0;
        protected double timeSinceLastYMovement = 0;
        protected double timeSinceLastXMovement = 0;

        protected Texture2D tetrisUI;

        protected BasicEffect cubeEffect;
        protected Camera camera;

        protected TetrisSession tetrisSession;

        protected ScrollingBackground scrollingBackground;

        protected TextBox gameTypeText;
        protected TextBox gameTimeText;
        protected TextBox gameScoreText;
        protected TextBox gameLevelText;
        protected TextBox gameLinesText;

        public TetrisSession Session
        {
            get
            {
                return this.tetrisSession;
            }
        }

        public abstract string GameType
        {
            get;
        }

        public TetrisScreen(Microsoft.Xna.Framework.Game game) : base(game)
        {
        }
    }
}
