using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Tetris3D
{
    public enum TetrisPauseOptions { Resume, Quit };

    public class TetrisPauseScreen : GameScreen
    {
        private GameScreen screenToPause;

        private String ResumeText = "Resume";
        private String QuitText = "Quit";

        private Texture2D backGround;
        private Texture2D cursor;

        private SpriteFont menuFont;

        private TetrisPauseOptions HighlightedOption = TetrisPauseOptions.Resume;

        private int numberOfTetrisMainMenuOptions = 2;

        public TetrisPauseScreen(Microsoft.Xna.Framework.Game game, GameScreen screenToPause) : base(game)
        {
            this.screenToPause = screenToPause;
            this.screenToPause.isDiabled = true;
        }

        public override void LoadContent()
        {
            this.backGround = this.content.Load<Texture2D>(@"Textures\blank");
            this.menuFont = this.content.Load<SpriteFont>(@"Textures\Kootenay");
            this.cursor = this.content.Load<Texture2D>(@"Textures\cursor");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.HighlightedOption--;
                this.HighlightedOption = (TetrisPauseOptions)Math.Max((int)this.HighlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.HighlightedOption++;
                this.HighlightedOption = (TetrisPauseOptions)Math.Min((int)this.HighlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.HighlightedOption)
                {
                    case TetrisPauseOptions.Resume: this.screenManager.removeScreen(this); this.screenToPause.isDiabled = false; break;
                    case TetrisPauseOptions.Quit: this.screenManager.removeScreen(this); this.screenManager.removeScreen(this.screenToPause); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), new Color(Color.White, 100));
            this.screenManager.batch.DrawString(this.menuFont, this.ResumeText, new Vector2(575, 400), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.QuitText, new Vector2(575, 425), Color.White);
            switch (this.HighlightedOption)
            {
                case TetrisPauseOptions.Resume: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 405), Color.White); break;
                case TetrisPauseOptions.Quit: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 430), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
