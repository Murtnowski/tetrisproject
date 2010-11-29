using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris3D
{
    public enum ChallengesMenuOptions { Challange1, Challange2, Challange3, Challange4, Back };

    public class ChallengesMenuScreen : GameScreen
    {
        private String Challange1Text = "Challange 1";
        private String Challange2Text = "Challenge 2";
        private String Challange3Text = "Challenge 3";
        private String Challange4Text = "Challenge 4";
        private String BackText = "Back";

        private Texture2D backGround;
        private Texture2D cursor;

        private SpriteFont menuFont;

        private ChallengesMenuOptions HighlightedOption = ChallengesMenuOptions.Challange1;

        private int numberOfTetrisMainMenuOptions = 5;

        public ChallengesMenuScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\Title");
            this.cursor = this.content.Load<Texture2D>(@"Textures\Cursor");
            this.menuFont = this.content.Load<SpriteFont>(@"Textures\Kootenay");
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
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
                    case ChallengesMenuOptions.Challange1: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge1TetrisScreen(this.screenManager.Game)); break;
                    case ChallengesMenuOptions.Challange2: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge2TetrisScreen(this.screenManager.Game)); break;
                    case ChallengesMenuOptions.Challange3: this.screenManager.removeScreen(this); this.screenManager.addScreen(new Challenge3TetrisScreen(this.screenManager.Game)); break; ;
                    case ChallengesMenuOptions.Challange4: break;
                    case ChallengesMenuOptions.Back: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ModeMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.Challange1Text, new Vector2(575, 400), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.Challange2Text, new Vector2(575, 425), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.Challange3Text, new Vector2(575, 450), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.Challange4Text, new Vector2(575, 475), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.BackText, new Vector2(575, 500), Color.White);


            switch (this.HighlightedOption)
            {
                case ChallengesMenuOptions.Challange1: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 405), Color.White); break;
                case ChallengesMenuOptions.Challange2: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 430), Color.White); break;
                case ChallengesMenuOptions.Challange3: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 455), Color.White); break;
                case ChallengesMenuOptions.Challange4: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 470), Color.White); break;
                case ChallengesMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 485), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
