using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using XELibrary;

namespace Tetris3D
{
    public class ClassicTetrisScreen : GameScreen
    {
        private String GameType = "Classic";
  
        private BasicEffect cubeEffect;
        private Camera camera;

        private List<BasicShape> foundation = new List<BasicShape>();
        private TetrisSession tetrisSession;

        private ScrollingBackground scrollingBackground;

        private SpriteFont UIFont;

        public AudioBank audio;
        Song backgroundMusic;

        double totalTime = 0;
        double ElapsedRealTime = 0;

        private Texture2D TetrisUI;

        public ClassicTetrisScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            this.tetrisSession = new TetrisSession(new Vector2(10, 24)); 

            this.camera = new Camera(this.screenManager.Game, this.screenManager.GraphicsDevice);
            this.camera.Initialize();

            this.initializeWorld();

            UIFont = this.content.Load<SpriteFont>(@"Textures\UIFont");

            audio = new AudioBank();
            audio.LoadContent(this.content);
            backgroundMusic = this.content.Load<Song>(@"Audio\STG-MajorTom");

            this.TetrisUI = this.content.Load<Texture2D>(@"Textures\TetrisUI");

            scrollingBackground = new ScrollingBackground();
            Texture2D backgroundTexture = this.content.Load<Texture2D>(@"Textures\stars");
            scrollingBackground.Load(this.screenManager.GraphicsDevice, backgroundTexture);
        }

        public override void UnloadContent()
        {
            MediaPlayer.Stop();
            base.UnloadContent();
        }

        /// <summary>
        /// Initialize the 3D environment
        /// </summary>
        private void initializeWorld()
        {
            cubeEffect = new BasicEffect(this.screenManager.GraphicsDevice, null);
            cubeEffect.TextureEnabled = true;
            cubeEffect.Texture = this.content.Load<Texture2D>("Textures\\leather");

            cubeEffect.Parameters["World"].SetValue(camera.getRotationMatrix);
            cubeEffect.Parameters["View"].SetValue(camera.View);
            cubeEffect.Parameters["Projection"].SetValue(camera.Projection);

            cubeEffect.LightingEnabled = true;
            cubeEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            cubeEffect.PreferPerPixelLighting = true;

            cubeEffect.DirectionalLight0.Direction = new Vector3(1, -1, 0);
            cubeEffect.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
            cubeEffect.DirectionalLight0.Enabled = true;

            cubeEffect.DirectionalLight1.Enabled = true;
            cubeEffect.DirectionalLight1.DiffuseColor = Color.White.ToVector3();
            cubeEffect.DirectionalLight1.Direction = new Vector3(0, 1, 1);

            cubeEffect.DirectionalLight2.Enabled = true;
            cubeEffect.DirectionalLight2.DiffuseColor = Color.White.ToVector3();
            cubeEffect.DirectionalLight2.Direction = new Vector3(1, 0, -1);

            cubeEffect.SpecularPower = 32;

            //Draw Border
            for (int i = -1; i <= this.tetrisSession.GameBoard.GetLength(0); i++) //Bottom Row
            {
                BasicShape cube = new BasicShape(Vector3.One, new Vector3(i - 4, -1, 0), TetrisColors.Gray);
                foundation.Add(cube);
            }

            for (int i = 0; i < this.tetrisSession.GameBoard.GetLength(1) - TetrisSession.GameOverRange; i++) //Containment Box 
            {
                BasicShape cubeLeft = new BasicShape(Vector3.One, new Vector3(-1 - 4, i, 0), TetrisColors.Gray);
                foundation.Add(cubeLeft);

                BasicShape cubeRight = new BasicShape(Vector3.One, new Vector3(this.tetrisSession.GameBoard.GetLength(0) - 4, i, 0), TetrisColors.Gray);
                foundation.Add(cubeRight);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(this.backgroundMusic);
            }
            this.camera.Update(gameTime);
            float elapsedBackground = (float)gameTime.ElapsedGameTime.TotalSeconds;

            totalTime += gameTime.ElapsedGameTime.Milliseconds;
            ElapsedRealTime += gameTime.ElapsedRealTime.Milliseconds;

            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Escape))
            {
                this.screenManager.addScreen(new TetrisPauseScreen(this.screenManager.Game, this));
            }

            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Left))
            {
                this.tetrisSession.moveCurrentPieceLeft();
            }

            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Right))
            {
                this.tetrisSession.moveCurrentPieceRight();
            }
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {
                if (!this.tetrisSession.moveCurrentPieceDown())
                {
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        this.screenManager.removeScreen(this);
                        this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game));
                        //TODO: GAMEOVER LOGIC
                    }
                    audio.PlayClearLineSound();
                    this.tetrisSession.clearCompletedLines();
                }
            }
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.tetrisSession.slamCurrentPiece();
                if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                {
                    this.screenManager.removeScreen(this);
                    this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game));
                    //TODO: GAME OVER LOGIC
                }
                audio.PlayClearLineSound();
                this.tetrisSession.clearCompletedLines();
            }
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                audio.PlayRotateSound();
                this.tetrisSession.rotateCurrentPieceClockwise();
            }

            if (totalTime > (1000 - (this.tetrisSession.CurrentLevel * 100)))
            {
                this.totalTime = 0;

                if (!this.tetrisSession.isBlocksBelowCurrentPieceClear())
                {
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        //TODO: GAMEOVER LOGIC
                        this.screenManager.removeScreen(this);
                        this.screenManager.addScreen(new MainMenuScreen(this.screenManager.Game));
                    }
                    audio.PlayClearLineSound();
                    this.tetrisSession.clearCompletedLines();
                }
                else
                {
                    this.tetrisSession.moveCurrentPieceDown();
                }
            }

            scrollingBackground.Update(elapsedBackground * 100);
        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.GraphicsDevice.RenderState.DepthBufferEnable = true;
            this.screenManager.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            this.screenManager.GraphicsDevice.RenderState.AlphaTestEnable = false;

            //this.tetrisSession.Draw(gameTime, this.spriteBatch, this.GraphicsDevice);
            this.screenManager.GraphicsDevice.Clear(Color.Pink);

            // TODO : Make it so you draw background.. then pieces.. then HUD without them conflicting graphically
            this.screenManager.batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            scrollingBackground.Draw(this.screenManager.batch);
            this.screenManager.batch.End();

            cubeEffect.World = camera.getRotationMatrix;
            cubeEffect.Begin();

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                for (int x = 0; x < this.tetrisSession.GameBoard.GetLength(0); x++)//This block draws all the pieces on the gameboard, will not draw pieces until they enter its range
                {                                                            //Use code block below to see them before they enter the gamefield range
                    for (int y = 0; y < this.tetrisSession.GameBoard.GetLength(1) - TetrisSession.GameOverRange; y++)
                    {
                        if (tetrisSession.GameBoard.GetValue(x, y) != null)
                        {
                            BasicShape cube = new BasicShape(Vector3.One, new Vector3(x - 4, y, 0), tetrisSession.GameBoard[x, y].TetrisColor);
                            cube.RenderShape(this.screenManager.GraphicsDevice);
                        }

                    }
                }

                foreach (BasicShape cube in foundation)//Draw containment cubes
                {
                    cube.RenderShape(this.screenManager.GraphicsDevice);
                }
                pass.End();
            }

            cubeEffect.End();
            
            this.screenManager.batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            this.screenManager.batch.Draw(this.TetrisUI, new Vector2(850, 200), Color.White);
            this.screenManager.batch.DrawString(UIFont, this.GameType, new Vector2(895, 238f), Color.Yellow);
            this.screenManager.batch.DrawString(UIFont, "" + this.tetrisSession.CurrentScore, new Vector2(922f, 528f), Color.Yellow);
            this.screenManager.batch.DrawString(UIFont, "" + this.tetrisSession.CurrentLevel, new Vector2(942f, 608f), Color.Yellow);
            this.screenManager.batch.DrawString(UIFont, "" + this.tetrisSession.CurrentNumberOfClearedLines, new Vector2(942f, 688f), Color.Yellow);

            this.screenManager.batch.End();
        }
    }
}
