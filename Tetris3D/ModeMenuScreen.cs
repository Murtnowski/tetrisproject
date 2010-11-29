using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris3D
{
    public enum ModeMenuOptions { Classic, Marathon, TimeTrial, Challenges, Back };

    public class ModeMenuScreen : GameScreen
    {
        private String ClassicText = "Classic Tetris";
        private String MarathonText = "Marathon Tetris";
        private String TimeText = "Time Trial Tetris";
        private String ChallengesText = "Challenges Tetris";
        private String BackText = "Back";

        private Texture2D backGround;
        private Texture2D cursor;

        private SpriteFont menuFont;

        private ModeMenuOptions HighlightedOption = ModeMenuOptions.Classic;

        private int numberOfTetrisMainMenuOptions = 5;

        public ModeMenuScreen(Microsoft.Xna.Framework.Game game)
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
                this.HighlightedOption = (ModeMenuOptions)Math.Max((int)this.HighlightedOption, 0);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.HighlightedOption++;
                this.HighlightedOption = (ModeMenuOptions)Math.Min((int)this.HighlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                switch (this.HighlightedOption)
                {
                    case ModeMenuOptions.Classic: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ClassicTetrisScreen(this.screenManager.Game)); break;
                    case ModeMenuOptions.Marathon: this.screenManager.removeScreen(this); this.screenManager.addScreen(new MarathonTetrisScreen(this.screenManager.Game)); break;
                    case ModeMenuOptions.TimeTrial: break;
                    case ModeMenuOptions.Challenges: this.screenManager.removeScreen(this); this.screenManager.addScreen(new ChallengesMenuScreen(this.screenManager.Game)); break;
                    case ModeMenuOptions.Back: this.screenManager.removeScreen(this); this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game)); break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.batch.Begin();
            this.screenManager.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.ClassicText, new Vector2(575, 400), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.MarathonText, new Vector2(575, 425), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.TimeText, new Vector2(575, 450), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.ChallengesText, new Vector2(575, 475), Color.White);
            this.screenManager.batch.DrawString(this.menuFont, this.BackText, new Vector2(575, 500), Color.White);

            switch (this.HighlightedOption)
            {
                case ModeMenuOptions.Classic: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 405), Color.White); break;
                case ModeMenuOptions.Marathon: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 430), Color.White); break;
                case ModeMenuOptions.TimeTrial: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 455), Color.White); break;
                case ModeMenuOptions.Challenges: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 470), Color.White); break;
                case ModeMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(540, 485), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
