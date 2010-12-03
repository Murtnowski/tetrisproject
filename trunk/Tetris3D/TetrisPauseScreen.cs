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

        private Texture2D Background;
        private Texture2D Cursor;
        private Texture2D PauseMenu;

        private TetrisPauseOptions HighlightedOption = TetrisPauseOptions.Resume;

        private int numberOfTetrisMainMenuOptions = 2;

        public TetrisPauseScreen(Microsoft.Xna.Framework.Game game, GameScreen screenToPause) : base(game)
        {
            this.screenToPause = screenToPause;
            this.screenToPause.isDiabled = true;
        }

        public override void LoadContent()
        {
            this.Background = this.content.Load<Texture2D>(@"Textures\blank");
            this.Cursor = this.content.Load<Texture2D>(@"Textures\cursor");
            this.PauseMenu = this.content.Load<Texture2D>(@"Textures\Menus\PauseMenu");
            audio = new AudioBank();
            audio.LoadContent(this.content);

            audio.PlayPauseSound();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                audio.PlayMenuScrollSound();
                this.HighlightedOption--;
                this.HighlightedOption = (TetrisPauseOptions)Math.Max((int)this.HighlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                audio.PlayMenuScrollSound();
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
            this.screenManager.batch.Draw(this.Background, new Rectangle(0, 0, 1200, 900), new Color(Color.White, 100));
            this.screenManager.batch.Draw(this.PauseMenu, new Vector2(555, 375), Color.White);
            switch (this.HighlightedOption)
            {
                case TetrisPauseOptions.Resume: this.screenManager.batch.Draw(this.Cursor, new Vector2(520, 385), Color.White); break;
                case TetrisPauseOptions.Quit: this.screenManager.batch.Draw(this.Cursor, new Vector2(520, 455), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
