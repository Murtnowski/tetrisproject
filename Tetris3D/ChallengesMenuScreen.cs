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

    public class ChallengesMenuScreen : GameScreen
    {
        private Texture2D backGround;
        private Texture2D cursor;
        private Texture2D menuOption1;
        private Texture2D menuOption2;
        private Texture2D menuOption3;
        private Texture2D menuOption4;
        private Texture2D menuOptionBack;

        private ChallengesMenuOptions HighlightedOption = ChallengesMenuOptions.Challenge1;

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
        }

        public override void Update(GameTime gameTime)
        {

            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.HighlightedOption--;
                this.HighlightedOption = (ChallengesMenuOptions)Math.Max((int)this.HighlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.HighlightedOption++;
                this.HighlightedOption = (ChallengesMenuOptions)Math.Min((int)this.HighlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.HighlightedOption)
                {
                    case ChallengesMenuOptions.Challenge1: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge1TetrisScreen(this.screenManager.Game)); break;
                    case ChallengesMenuOptions.Challenge2: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge2TetrisScreen(this.screenManager.Game)); break;
                    case ChallengesMenuOptions.Challenge3: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge3TetrisScreen(this.screenManager.Game)); break; ;
                    case ChallengesMenuOptions.Challenge4: break;
                    case ChallengesMenuOptions.Back: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ModeMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.Draw(this.menuOption1, new Vector2(550, 415), Color.White);
            this.screenManager.batch.Draw(this.menuOption2, new Vector2(550, 470), Color.White);
            this.screenManager.batch.Draw(this.menuOption3, new Vector2(550, 525), Color.White);
            this.screenManager.batch.Draw(this.menuOption4, new Vector2(550, 580), Color.White);
            this.screenManager.batch.Draw(this.menuOptionBack, new Vector2(550, 635), Color.White);


            switch (this.HighlightedOption)
            {
                case ChallengesMenuOptions.Challenge1: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White); break;
                case ChallengesMenuOptions.Challenge2: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White); break;
                case ChallengesMenuOptions.Challenge3: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 527), Color.White); break;
                case ChallengesMenuOptions.Challenge4: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 582), Color.White); break;
                case ChallengesMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 637), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
