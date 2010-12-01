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
        private Texture2D backGround;
        private Texture2D cursor;
        private Texture2D menuOptionClassic;
        private Texture2D menuOptionChallenges;
        private Texture2D menuOptionTimeTrial;
        private Texture2D menuOptionMarathon;
        private Texture2D menuOptionBack;

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
            this.menuOptionClassic = this.content.Load<Texture2D>(@"Textures\Menus\Classic");
            this.menuOptionTimeTrial = this.content.Load<Texture2D>(@"Textures\Menus\TimeTrial");
            this.menuOptionMarathon = this.content.Load<Texture2D>(@"Textures\Menus\Marathon");
            this.menuOptionChallenges = this.content.Load<Texture2D>(@"Textures\Menus\Challenges");
            this.menuOptionBack = this.content.Load<Texture2D>(@"Textures\Menus\Back");
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
            this.screenManager.batch.Draw(this.menuOptionClassic, new Vector2(550, 415), Color.White);
            this.screenManager.batch.Draw(this.menuOptionMarathon, new Vector2(550, 470), Color.White);
            this.screenManager.batch.Draw(this.menuOptionTimeTrial, new Vector2(550, 525), Color.White);
            this.screenManager.batch.Draw(this.menuOptionChallenges, new Vector2(550, 580), Color.White);
            this.screenManager.batch.Draw(this.menuOptionBack, new Vector2(550, 635), Color.White);

            switch (this.HighlightedOption)
            {
                case ModeMenuOptions.Classic: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 417), Color.White); break;
                case ModeMenuOptions.Marathon: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 472), Color.White); break;
                case ModeMenuOptions.TimeTrial: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 527), Color.White); break;
                case ModeMenuOptions.Challenges: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 582), Color.White); break;
                case ModeMenuOptions.Back: this.screenManager.batch.Draw(this.cursor, new Vector2(515, 637), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.screenManager.batch.End();
        }
    }
}
