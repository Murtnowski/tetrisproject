using System;
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
        private String NewGameText = "New Game";
        private String OptionText = "Options";
        private String ExitText = "Exit";

        private Texture2D backGround;
        private Texture2D cursor;

        private SpriteFont menuFont;

        private MainMenuOptions HighlightedOption = MainMenuOptions.NewGame;

        private int numberOfTetrisMainMenuOptions = 3;

        public MainMenuScreen(Microsoft.Xna.Framework.Game game) : base(game)
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
            this.screenManager.batch.DrawString(this.menuFont, this.NewGameText, new Vector2(575, 400), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.OptionText, new Vector2(575, 425), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.ExitText, new Vector2(575, 450), Color.White);
            switch (this.HighlightedOption)
            {
                case MainMenuOptions.NewGame: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 405), Color.White); break;
                case MainMenuOptions.Options: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 430), Color.White); break;
                case MainMenuOptions.Quit: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 455), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
