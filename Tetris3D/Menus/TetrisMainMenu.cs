using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using XELibrary;


namespace Tetris3D
{
    /// <summary>
    /// An enumeration of the options this menu has
    /// </summary>
    public enum TetrisMainMenuOptions { NewGame, Options, Quit };

    /// <summary>
    /// This represents the main menu of the Tetris game
    /// </summary>
    public class TetrisMainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /// <summary>
        /// Access to the input service
        /// </summary>
        protected IInputHandler input;

        private int numberOfTetrisMainMenuOptions = 3;

        /// <summary>
        /// The background texture
        /// </summary>
        private Texture2D backGround;

        /// <summary>
        /// The menu cursor texture
        /// </summary>
        private Texture2D cursor;

        /// <summary>
        /// The Spritebatch used to draw the 2D textures
        /// </summary>
        private SpriteBatch batch;

        /// <summary>
        /// The font of the menu
        /// </summary>
        private SpriteFont menuFont;

        /// <summary>
        /// The current highlighted option
        /// </summary>
        private TetrisMainMenuOptions HighlightedOption = TetrisMainMenuOptions.NewGame;

        private bool wasOptionSelected = false;

        /// <summary>
        /// The last selected option
        /// </summary>
        private TetrisMainMenuOptions selectedOption;

        /// <summary>
        /// Determines weather an options was selected
        /// </summary>
        public bool WasOptionSelected
        {
            get
            {
                return this.wasOptionSelected;
            }
            set
            {
                this.wasOptionSelected = value;
            }
        }

        /// <summary>
        /// The last selected tetris option
        /// </summary>
        public TetrisMainMenuOptions SelectedOption
        {
            get
            {
                return this.selectedOption;
            }
        }

        /// <summary>
        /// Constructs a new Tetirs Main Menu
        /// </summary>
        /// <param name="game"></param>
        public TetrisMainMenu(Game game)
            : base(game)
        {
            this.Initialize();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            input = (IInputHandler)this.Game.Services.GetService(typeof(IInputHandler));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per initalization and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.batch = new SpriteBatch(GraphicsDevice);
            this.backGround = Game.Content.Load<Texture2D>(@"Textures\Title");
            this.cursor = Game.Content.Load<Texture2D>(@"Textures\Cursor");
            this.menuFont = Game.Content.Load<SpriteFont>(@"Textures\Kootenay");
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (this.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.HighlightedOption--;
                this.HighlightedOption = (TetrisMainMenuOptions)Math.Max((int)this.HighlightedOption, 0);
            }
            else if (this.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                this.HighlightedOption++;
                this.HighlightedOption = (TetrisMainMenuOptions)Math.Min((int)this.HighlightedOption, this.numberOfTetrisMainMenuOptions - 1);
            }
            else if (this.input.KeyboardState.WasKeyPressed(Keys.Enter))
            {
                this.wasOptionSelected = true;
                switch (this.HighlightedOption)
                {
                    case TetrisMainMenuOptions.NewGame: this.selectedOption = TetrisMainMenuOptions.NewGame;  break;
                    case TetrisMainMenuOptions.Options: this.selectedOption = TetrisMainMenuOptions.Options; break;
                    case TetrisMainMenuOptions.Quit: this.selectedOption = TetrisMainMenuOptions.Quit; break;
                    default: throw new NotImplementedException();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            this.batch.Begin();
            //this.batch.Draw(this.backGround,Vector2.Zero, new Rectangle(0,0, 1200, 900), Color.White);
            this.batch.Draw(this.backGround, new Rectangle(0, 0, 1200, 900), Color.White);
            //this.batch.Draw(this.backGround, Vector2.Zero, Color.White);
            this.batch.DrawString(this.menuFont, "New Game", new Vector2(575, 400), Color.White);
            this.batch.DrawString(this.menuFont, "Options", new Vector2(575, 425), Color.White);
            this.batch.DrawString(this.menuFont, "Exit", new Vector2(575, 450), Color.White);
            switch (this.HighlightedOption)
            {
                case TetrisMainMenuOptions.NewGame: this.batch.Draw(this.cursor, new Vector2(540, 405), Color.White); break;
                case TetrisMainMenuOptions.Options: this.batch.Draw(this.cursor, new Vector2(540, 430), Color.White); break;
                case TetrisMainMenuOptions.Quit: this.batch.Draw(this.cursor, new Vector2(540, 455), Color.White); break;
                default: throw new NotImplementedException();
            }
            this.batch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Called when graphics resources need to be unloaded.
        /// </summary>
        protected override void UnloadContent()
        {
            this.batch.Dispose();

            base.UnloadContent();
        }
    }
}