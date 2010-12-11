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
    class Challenge3TetrisScreen : TetrisScreen
    {
        public override string GameType
        {
            get
            {
                return "Tool Box";
            }
        }

        private List<BasicShape> foundation = new List<BasicShape>();

        public Challenge3TetrisScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            TetrisBlock[,] board = new TetrisBlock[10, 24];

            board[0, 5] = new TetrisBlock(TetrisColors.Gray);
            board[1, 5] = new TetrisBlock(TetrisColors.Gray);
            board[2, 5] = new TetrisBlock(TetrisColors.Gray); //cog
            board[1, 6] = new TetrisBlock(TetrisColors.Gray);
            board[1, 4] = new TetrisBlock(TetrisColors.Gray);

            board[5, 6] = new TetrisBlock(TetrisColors.Gray);
            board[6, 6] = new TetrisBlock(TetrisColors.Gray);
            board[7, 6] = new TetrisBlock(TetrisColors.Gray);
            board[8, 6] = new TetrisBlock(TetrisColors.Gray);
            board[9, 6] = new TetrisBlock(TetrisColors.Gray);
            board[6, 7] = new TetrisBlock(TetrisColors.Gray);//star
            board[7, 7] = new TetrisBlock(TetrisColors.Gray);
            board[8, 7] = new TetrisBlock(TetrisColors.Gray);
            board[6, 5] = new TetrisBlock(TetrisColors.Gray);
            board[7, 5] = new TetrisBlock(TetrisColors.Gray);
            board[8, 5] = new TetrisBlock(TetrisColors.Gray);
            board[7, 8] = new TetrisBlock(TetrisColors.Gray);
            board[7, 4] = new TetrisBlock(TetrisColors.Gray);


            board[3, 15] = new TetrisBlock(TetrisColors.Gray);
            board[4, 15] = new TetrisBlock(TetrisColors.Gray);
            board[5, 16] = new TetrisBlock(TetrisColors.Gray);
            board[5, 14] = new TetrisBlock(TetrisColors.Gray);//wrench
            board[6, 16] = new TetrisBlock(TetrisColors.Gray);
            board[6, 14] = new TetrisBlock(TetrisColors.Gray);

            board[0, 12] = new TetrisBlock(TetrisColors.Gray);
            board[1, 12] = new TetrisBlock(TetrisColors.Gray); //ring
            board[1, 11] = new TetrisBlock(TetrisColors.Gray);
            board[1, 10] = new TetrisBlock(TetrisColors.Gray);
            board[0, 10] = new TetrisBlock(TetrisColors.Gray);


            this.tetrisSession = new TetrisSession(board);

            this.camera = new Camera(this.screenManager.Game, this.screenManager.GraphicsDevice);
            this.camera.Initialize();

            this.initializeWorld();

            this.IPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\I");
            this.JPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\J");
            this.LPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\L");
            this.OPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\O");
            this.SPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\S");
            this.TPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\T");
            this.ZPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\Z");

            uiFont = this.content.Load<SpriteFont>(@"Textures\UIFont");

            this.tetrisUI = this.content.Load<Texture2D>(@"Textures\TetrisUI");

            scrollingBackground = new ScrollingBackground();
            Texture2D backgroundTexture = this.content.Load<Texture2D>(@"Textures\stars");
            scrollingBackground.Load(this.screenManager.GraphicsDevice, backgroundTexture);

            this.screenManager.audio.PlayBeginSound(true);
        }

        public override void UnloadContent()
        {
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

            //Set UI text
            this.gameTypeText = new TextBox(this, new Vector2(873, 241f), new Vector2(147, 25), @"Textures\UIFont", this.GameType);
            this.gameTypeText.TextAlign = TextBox.TextAlignOption.MiddleCenter;
            this.gameTypeText.ForeColor = Color.Yellow;

            this.gameTimeText = new TextBox(this, new Vector2(873, 278f), new Vector2(147, 25), @"Textures\UIFont", "");
            this.gameTimeText.TextAlign = TextBox.TextAlignOption.MiddleCenter;
            this.gameTimeText.ForeColor = Color.Yellow;

            this.gameScoreText = new TextBox(this, new Vector2(891, 533f), new Vector2(115, 25), @"Textures\UIFont", this.tetrisSession.CurrentScore.ToString());
            this.gameScoreText.TextAlign = TextBox.TextAlignOption.MiddleCenter;
            this.gameScoreText.ForeColor = Color.Yellow;

            this.gameLevelText = new TextBox(this, new Vector2(891, 613f), new Vector2(115, 25), @"Textures\UIFont", this.tetrisSession.CurrentLevel.ToString());
            this.gameLevelText.TextAlign = TextBox.TextAlignOption.MiddleCenter;
            this.gameLevelText.ForeColor = Color.Yellow;

            this.gameLinesText = new TextBox(this, new Vector2(891, 693f), new Vector2(115, 25), @"Textures\UIFont", this.tetrisSession.CurrentNumberOfClearedLines.ToString());
            this.gameLinesText.TextAlign = TextBox.TextAlignOption.MiddleCenter;
            this.gameLinesText.ForeColor = Color.Yellow;
        }

        public override void Update(GameTime gameTime)
        {
            this.timer = this.timer.Add(gameTime.ElapsedGameTime);
            //update UI text
            gameTimeText.Text = this.timer.Minutes + ":" + this.timer.Seconds.ToString("00");

            this.timeSinceLastTick += gameTime.ElapsedGameTime.Milliseconds;

            this.camera.Update(gameTime);

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
                    this.screenManager.audio.PlaySlamSound();
                    numberOfLinesCleared = this.tetrisSession.clearCompletedLines();
                    gameLinesText.Text = this.tetrisSession.CurrentNumberOfClearedLines.ToString();
                    gameScoreText.Text = this.tetrisSession.CurrentScore.ToString();
                    gameLevelText.Text = this.tetrisSession.CurrentLevel.ToString();
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        this.screenManager.addScreen(new GameOverScreen(this.screenManager.Game, this));
                    }
                    if (numberOfLinesCleared == 4)
                    {
                        this.screenManager.audio.PlayTetrisSound();
                        numberOfLinesCleared = 0;
                    }
                    else if (numberOfLinesCleared >= 1)
                    {
                        this.screenManager.audio.PlayClearLineSound();
                        numberOfLinesCleared = 0;
                    }
                }
            }

            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                this.screenManager.audio.PlaySlamSound();
                this.tetrisSession.slamCurrentPiece();
                numberOfLinesCleared = this.tetrisSession.clearCompletedLines();
                gameScoreText.Text = this.tetrisSession.CurrentScore.ToString();
                gameLevelText.Text = this.tetrisSession.CurrentLevel.ToString();
                gameLinesText.Text = this.tetrisSession.CurrentNumberOfClearedLines.ToString();
                if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                {
                    this.screenManager.addScreen(new GameOverScreen(this.screenManager.Game, this));
                }
                if (numberOfLinesCleared == 4)
                {
                    this.screenManager.audio.PlayTetrisSound();
                    numberOfLinesCleared = 0;
                }
                else if (numberOfLinesCleared >= 1)
                {
                    this.screenManager.audio.PlayClearLineSound();
                    numberOfLinesCleared = 0;
                }
            }
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.screenManager.audio.PlayRotateSound();
                this.tetrisSession.rotateCurrentPieceClockwise();
            }

            if (this.timeSinceLastTick > (1000 - (this.tetrisSession.CurrentLevel * 100)))
            {
                this.timeSinceLastTick = 0;

                if (!this.tetrisSession.isBlocksBelowCurrentPieceClear())
                {
                    this.screenManager.audio.PlaySlamSound();
                    numberOfLinesCleared = this.tetrisSession.clearCompletedLines();
                    gameLinesText.Text = this.tetrisSession.CurrentNumberOfClearedLines.ToString();
                    gameScoreText.Text = this.tetrisSession.CurrentScore.ToString();
                    gameLevelText.Text = this.tetrisSession.CurrentLevel.ToString();
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece())
                    {
                        this.screenManager.addScreen(new GameOverScreen(this.screenManager.Game, this));
                    }
                    if (numberOfLinesCleared == 4)
                    {
                        this.screenManager.audio.PlayTetrisSound();
                        numberOfLinesCleared = 0;
                    }
                    else if (numberOfLinesCleared >= 1)
                    {
                        this.screenManager.audio.PlayClearLineSound();
                        numberOfLinesCleared = 0;
                    }
                }
                else
                {
                    this.tetrisSession.moveCurrentPieceDown();
                }
            }

            scrollingBackground.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 100);

        }

        public override void Draw(GameTime gameTime)
        {
            this.screenManager.GraphicsDevice.RenderState.DepthBufferEnable = true;
            this.screenManager.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            this.screenManager.GraphicsDevice.RenderState.AlphaTestEnable = false;

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
            this.screenManager.batch.Draw(this.tetrisUI, new Vector2(850, 200), Color.White);
            this.gameTypeText.Draw(this.screenManager.batch);
            this.gameTimeText.Draw(this.screenManager.batch);
            this.gameScoreText.Draw(this.screenManager.batch);
            this.gameLevelText.Draw(this.screenManager.batch);
            this.gameLinesText.Draw(this.screenManager.batch);

            //Draw Next Piece
            switch (this.tetrisSession.NextTetrisPiece().Type)
            {
                case TetrisPieces.IBlock: this.screenManager.batch.Draw(this.IPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.JBlock: this.screenManager.batch.Draw(this.JPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.LBlock: this.screenManager.batch.Draw(this.LPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.OBlock: this.screenManager.batch.Draw(this.OPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.SBlock: this.screenManager.batch.Draw(this.SPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.TBlock: this.screenManager.batch.Draw(this.TPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.ZBlock: this.screenManager.batch.Draw(this.ZPieceTexture, new Vector2(870, 350), Color.White); break;
                default: throw new NotImplementedException();
            }

            this.screenManager.batch.End();
        }
    }
}
