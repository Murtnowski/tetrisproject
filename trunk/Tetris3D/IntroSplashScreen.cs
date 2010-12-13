/*
 * Project: Tetris Project
 * Primary Author: Damon Chastain
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
    class IntroSplashScreen : GameScreen
    {
        //the background 
        private Texture2D backGround;
        //personalized text displayed on the screen
        private Texture2D createdBy;
        //the credits displayed
        private TextBox credits;
        private TextBox credits2;
        private TextBox credits3;
        //fader variable
        private float fader, fader1, fader2;


        public IntroSplashScreen(Microsoft.Xna.Framework.Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\Space");
            this.createdBy = this.content.Load<Texture2D>(@"Textures\Menus\StartScreenText");
            //initializing the values of the textboxes


            this.credits = new TextBox(this, new Vector2(535, 500), new Vector2(300, 75), @"Textures\Kootenay",
                "CSULB", true);
            this.credits.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.credits.ForeColor = Color.Aqua;

            this.credits2 = new TextBox(this, new Vector2(475, 575), new Vector2(300, 75), @"Textures\Kootenay",
                "Professor Monge", true);
            this.credits2.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.credits2.ForeColor = Color.AliceBlue;

            this.credits3 = new TextBox(this, new Vector2(515, 650), new Vector2(300, 75), @"Textures\Kootenay",
                "CECS 491", true);
            this.credits3.TextAlign = TextBox.TextAlignOption.TopLeft;
            this.credits3.ForeColor = Color.Aquamarine;

            fader = 0f;
            fader1 = 0f;
            fader2 = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (fader <= 1.50f) //background fades in first
                fader += 0.009f;
            if (fader >= 1.00f && fader1 <= 1.50f)
                fader1 += 0.006f;
            if (fader1 >= 1.00f)
                fader2 += 0.009f; //fader 2 has no limits

            //update the draw for the credits
            this.credits.ForeColor = new Color(Color.DeepSkyBlue, fader2);
            this.credits2.ForeColor = new Color(Color.DeepSkyBlue, fader2);
            this.credits3.ForeColor = new Color(Color.DeepSkyBlue, fader2);

            if (fader2 >= 1.7f)
            {
                this.screenManager.removeScreen(this);
                this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //begin drawing
            this.screenManager.batch.Begin();
            //draw the background and the 3 menu options
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), new Color(Color.White, fader));
            this.screenManager.batch.Draw(this.createdBy, new Vector2(0, 0), new Color(Color.White, fader1));

            this.credits.Draw(this.screenManager.batch);
            this.credits2.Draw(this.screenManager.batch);
            this.credits3.Draw(this.screenManager.batch);

            this.screenManager.batch.End();
        }
    }
}
