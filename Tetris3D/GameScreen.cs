using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris3D
{
    public abstract class GameScreen
    {
        public ContentManager content;

        public ScreenManager screenManager;
        public bool isHidden;
        public bool isDiabled;

        public GameScreen(Microsoft.Xna.Framework.Game game)
        {
            this.content = new ContentManager(game.Services, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime);

        public abstract void Update(GameTime gameTime);
    }
}
