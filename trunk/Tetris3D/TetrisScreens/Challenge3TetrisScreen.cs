/*
 * Project: Tetris Project
 * Authors: Matthew Urtnowski & Damon Chastain
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

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
    public class Challenge3TetrisScreen : TetrisScreen
    {
        public override string GameName
        {
            get
            {
                return "Tool Box";
            }
        }

        public override TetrisModes GameType
        {
            get
            {
                return TetrisModes.Challenge3;
            }
        }
        //a list of the pieces contained as the board AKA foundation
        private List<BasicShape> foundation = new List<BasicShape>();


        public Challenge3TetrisScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            //creates a new session with a board thats 10x24 (even though the real board is 10x20 we like to display the piece as it falls in)
            TetrisBlock[,] board = new TetrisBlock[10, 24];

            for (int j=0; j < 18; j++)
            {
                for (int i=0; i < 10; i++)
                {
                    board[i, j] = new TetrisBlock(TetrisColors.Red);
                    if (i == 2)
                        i += 4;
                }
                j++;
            }

            this.tetrisSession = new TetrisSession(board);
            //initialize the camera
            this.camera = new Camera(this.screenManager.Game, this.screenManager.GraphicsDevice);
            this.camera.Initialize();
            //initialize the world
            this.initializeWorld();
            //initialize the textures
            this.IPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\I");
            this.JPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\J");
            this.LPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\L");
            this.OPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\O");
            this.SPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\S");
            this.TPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\T");
            this.ZPieceTexture = this.content.Load<Texture2D>(@"Textures\Pieces\Z");
            //initialize the fonts
            uiFont = this.content.Load<SpriteFont>(@"Textures\UIFont");
            //load in the User Interface
            this.tetrisUI = this.content.Load<Texture2D>(@"Textures\TetrisUI");
            //begin the scrolling background
            scrollingBackground = new ScrollingBackground();
            Texture2D backgroundTexture = this.content.Load<Texture2D>(@"Textures\stars");
            scrollingBackground.Load(this.screenManager.GraphicsDevice, backgroundTexture);
            //play a sound when classic tetris begins
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
            //below are the basic initializations that are extended from the LoadContent.
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
            //Containment Box 
            for (int i = 0; i < this.tetrisSession.GameBoard.GetLength(1) - TetrisSession.GameOverRange; i++)
            {
                BasicShape cubeLeft = new BasicShape(Vector3.One, new Vector3(-1 - 4, i, 0), TetrisColors.Gray);
                foundation.Add(cubeLeft);

                BasicShape cubeRight = new BasicShape(Vector3.One, new Vector3(this.tetrisSession.GameBoard.GetLength(0) - 4, i, 0), TetrisColors.Gray);
                foundation.Add(cubeRight);
            }

            //Set UI text defaults
            this.gameTypeText = new TextBox(this, new Vector2(873, 241f), new Vector2(147, 25), @"Textures\UIFont", this.GameName);
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
            //update the timer for the game
            this.timer = this.timer.Add(gameTime.ElapsedGameTime);
            //update UI text
            gameTimeText.Text = this.timer.Minutes + ":" + this.timer.Seconds.ToString("00");
            //the 3 below are the updates needed for smooth movement of the pieces though the controls
            this.timeSinceLastTick += gameTime.ElapsedGameTime.Milliseconds;
            this.timeSinceLastYMovement += gameTime.ElapsedGameTime.Milliseconds;
            this.timeSinceLastXMovement += gameTime.ElapsedGameTime.Milliseconds;
            //update the camera
            this.camera.Update(gameTime);
            //if ESCAPE was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Escape))
            {//PAUSE THE GAME
                this.screenManager.addScreen(new TetrisPauseScreen(this.screenManager.Game, this));
            }
            //if LEFT is being held
            if (this.screenManager.input.KeyboardState.IsHoldingKey(Keys.Left))
            {//and it hasn't been too soon since the last movement
                if (timeSinceLastXMovement > 100)
                {//move piece LEFT and reset the time since last movement
                    this.tetrisSession.moveCurrentPieceLeft();
                    timeSinceLastXMovement = 0;
                }
            }
            //else if LEFT was pressed 
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Left))
            {//move piece LEFT and reset the time since last movement
                this.tetrisSession.moveCurrentPieceLeft();
                timeSinceLastXMovement = 0;
            }
            //if RIGHT is being held
            if (this.screenManager.input.KeyboardState.IsHoldingKey(Keys.Right))
            {//and it hasn't been too soon since the last movement
                if (timeSinceLastXMovement > 100)
                {//move the piece RIGHT and reset the time since last movement
                    this.tetrisSession.moveCurrentPieceRight();
                    timeSinceLastXMovement = 0;
                }
            }
            //else if RIGHT was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Right))
            {//move piece RIGHT and reset the time since last movement
                this.tetrisSession.moveCurrentPieceRight();
                timeSinceLastXMovement = 0;
            }
            //if DOWN is being held
            if (this.screenManager.input.KeyboardState.IsHoldingKey(Keys.Down))
            {//and it hasn't been too soon since the last movement
                if (timeSinceLastYMovement > 50)
                {//move the piece DOWN and reset the time since last movement
                    if (this.tetrisSession.moveCurrentPieceDown())
                    {
                        timeSinceLastYMovement = 0;
                        this.timeSinceLastTick = 0; //do not increment ticks while holding piece down
                    }
                    else//if PIECE cannot be moved down make the auto-down tick exceed the amount so the next tick will be checked as a collision
                        this.timeSinceLastTick = 1001;
                }
            }
            //else if DOWN was pressed
            else if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Down))
            {//and the piece is not capable of moving down
                if (!this.tetrisSession.moveCurrentPieceDown())
                {//play a sound and perform a moveCurrentPieceDown set of actions which may cause score to change or prevent the next piece from generating (game over)
                    this.screenManager.audio.PlaySlamSound();
                    numberOfLinesCleared = this.tetrisSession.clearCompletedLines();
                    gameLinesText.Text = this.tetrisSession.CurrentNumberOfClearedLines.ToString();
                    gameScoreText.Text = this.tetrisSession.CurrentScore.ToString();
                    gameLevelText.Text = this.tetrisSession.CurrentLevel.ToString();
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece()) //if gameover
                    {
                        //GAME OVER. Stop music and add gameover screen
                        this.screenManager.audioController.Stop();
                        this.screenManager.addScreen(new GameOverScreen(this.screenManager.Game, this));
                    }
                    //if user got a tetris (cleared 4 lines)
                    if (numberOfLinesCleared == 4)
                    {//reward the user with an explosion sound and reset the number of lines cleared
                        this.screenManager.audio.PlayTetrisSound();
                        numberOfLinesCleared = 0;
                    }//if the user cleared at least 1 line but not 4
                    else if (numberOfLinesCleared >= 1)
                    {//reward the user with a sound and reset the number of lines cleared
                        this.screenManager.audio.PlayClearLineSound();
                        numberOfLinesCleared = 0;
                    }
                }
                else //reset time since last Y movement
                    timeSinceLastYMovement = 0;
            }


            //if SPACE was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Space)) //slam the piece
            {
                //play a sound
                this.screenManager.audio.PlaySlamSound();
                //slam the piece
                this.tetrisSession.slamCurrentPiece();
                //reflect slam changes onto score
                numberOfLinesCleared = this.tetrisSession.clearCompletedLines();
                gameScoreText.Text = this.tetrisSession.CurrentScore.ToString();
                gameLevelText.Text = this.tetrisSession.CurrentLevel.ToString();
                gameLinesText.Text = this.tetrisSession.CurrentNumberOfClearedLines.ToString();
                //if gameover
                if (!this.tetrisSession.GenerateNewCurrentTetrisPiece()) //if gameover
                {
                    //GAME OVER. Stop music and add gameover screen
                    this.screenManager.audioController.Stop();
                    this.screenManager.addScreen(new GameOverScreen(this.screenManager.Game, this));
                }
                //if user got a tetris (cleared 4 lines)
                if (numberOfLinesCleared == 4)
                {//reward the user with an explosion sound and reset the number of lines cleared
                    this.screenManager.audio.PlayTetrisSound();
                    numberOfLinesCleared = 0;
                }//if the user cleared at least 1 line but not 4
                else if (numberOfLinesCleared >= 1)
                {//reward the user with a sound and reset the number of lines cleared
                    this.screenManager.audio.PlayClearLineSound();
                    numberOfLinesCleared = 0;
                }
            }
            //if UP was pressed
            if (this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Up) || this.screenManager.input.KeyboardState.WasKeyPressed(Keys.Z))
            {//play a rotate sound and rotate the piece clockwise
                this.screenManager.audio.PlayRotateSound();
                this.tetrisSession.rotateCurrentPieceClockwise();
            }
            //if enough time has passed since the piece was moved (Based on current level)
            if (this.timeSinceLastTick > (1000 - (this.tetrisSession.CurrentLevel * 100)))
            {
                //reset the time since last tick
                this.timeSinceLastTick = 0;
                //piece moves down.
                if (!this.tetrisSession.isBlocksBelowCurrentPieceClear())
                {
                    //play a sound and reflect the changes
                    this.screenManager.audio.PlaySlamSound();
                    numberOfLinesCleared = this.tetrisSession.clearCompletedLines();
                    gameLinesText.Text = this.tetrisSession.CurrentNumberOfClearedLines.ToString();
                    gameScoreText.Text = this.tetrisSession.CurrentScore.ToString();
                    gameLevelText.Text = this.tetrisSession.CurrentLevel.ToString();
                    if (!this.tetrisSession.GenerateNewCurrentTetrisPiece()) //if gameover
                    {
                        //GAME OVER. Stop music and add gameover screen
                        this.screenManager.audioController.Stop();
                        this.screenManager.addScreen(new GameOverScreen(this.screenManager.Game, this));
                    }
                    //if user got a tetris (cleared 4 lines)
                    if (numberOfLinesCleared == 4)
                    {//reward the user with an explosion sound and reset the number of lines cleared
                        this.screenManager.audio.PlayTetrisSound();
                        numberOfLinesCleared = 0;
                    }//if the user cleared at least 1 line but not 4
                    else if (numberOfLinesCleared >= 1)
                    {//reward the user with a sound and reset the number of lines cleared
                        this.screenManager.audio.PlayClearLineSound();
                        numberOfLinesCleared = 0;
                    }
                }
                else
                {
                    //simply move the piece down without collisions
                    this.tetrisSession.moveCurrentPieceDown();
                }
            }
            //update the scrolling background
            scrollingBackground.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 100);

        }

        public override void Draw(GameTime gameTime)
        {
            //setting the depth buffer to being working in the order we state
            //disabling alpha blend is important to not have 2 screen's colors blend
            this.screenManager.GraphicsDevice.RenderState.DepthBufferEnable = true;
            this.screenManager.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            this.screenManager.GraphicsDevice.RenderState.AlphaTestEnable = false;
            //begin drawing
            this.screenManager.batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            //scrolling background takes the back layer
            scrollingBackground.Draw(this.screenManager.batch);
            //end drawing
            this.screenManager.batch.End();

            //receive the rotation matrix and the diffused color and then collecting data
            cubeEffect.World = camera.getRotationMatrix;
            cubeEffect.DiffuseColor = Color.White.ToVector3();
            cubeEffect.Begin();

            bool nearGameOver = false;
            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                //This block draws all the pieces on the gameboard, will not draw pieces until they enter its range
                for (int x = 0; x < this.tetrisSession.GameBoard.GetLength(0); x++)
                {
                    //Use code block below to see them before they enter the gamefield range                                   
                    for (int y = 0; y < this.tetrisSession.GameBoard.GetLength(1); y++)
                    {
                        //if the point on the board is not empty
                        if (tetrisSession.GameBoard.GetValue(x, y) != null)
                        {
                            //check if the player is near game over
                            if (y >= (this.tetrisSession.GameBoard.GetLength(1) - TetrisSession.GameOverRange - 4) && !this.tetrisSession.isCurrentPieceAtLocation(new Point(x, y)))
                            {
                                nearGameOver = true;
                            }
                            //draw the selected cubes
                            BasicShape cube = new BasicShape(Vector3.One, new Vector3(x - 4, y, 0), tetrisSession.GameBoard[x, y].TetrisColor);
                            cube.RenderShape(this.screenManager.GraphicsDevice);
                        }

                    }
                }
                pass.End();
            }
            if (nearGameOver && !isDisabled)
            {
                //diffuse red into the diffused light which gives the appearance of a red alarm going off onto the board
                cubeEffect.DiffuseColor = new Color(1.0f, (gameTime.TotalGameTime.Milliseconds / 1000.0f) * 1.0f, (gameTime.TotalGameTime.Milliseconds / 1000.0f) * 1.0f).ToVector3();
            }

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                //Draw the board AKA Containment Cubes
                foreach (BasicShape cube in foundation)
                {
                    cube.RenderShape(this.screenManager.GraphicsDevice);
                }
                pass.End();
            }

            cubeEffect.End();
            //SaveStateMode.SaveState is used to mesh 2D graphics with 3D graphics
            this.screenManager.batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            //draw the user interface onto the board on the right side along with the 5 textboxes that go inside of it
            this.screenManager.batch.Draw(this.tetrisUI, new Vector2(850, 200), Color.White);
            this.gameTypeText.Draw(this.screenManager.batch); //1
            this.gameTimeText.Draw(this.screenManager.batch); //2
            this.gameScoreText.Draw(this.screenManager.batch);//3
            this.gameLevelText.Draw(this.screenManager.batch);//4
            this.gameLinesText.Draw(this.screenManager.batch);//5

            //depending on which piece is next
            switch (this.tetrisSession.NextTetrisPiece().Type)
            {
                //draw the image of that piece
                case TetrisPieces.IBlock: this.screenManager.batch.Draw(this.IPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.JBlock: this.screenManager.batch.Draw(this.JPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.LBlock: this.screenManager.batch.Draw(this.LPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.OBlock: this.screenManager.batch.Draw(this.OPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.SBlock: this.screenManager.batch.Draw(this.SPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.TBlock: this.screenManager.batch.Draw(this.TPieceTexture, new Vector2(870, 350), Color.White); break;
                case TetrisPieces.ZBlock: this.screenManager.batch.Draw(this.ZPieceTexture, new Vector2(870, 350), Color.White); break;
                default: throw new NotImplementedException();
            }
            //end draw
            this.screenManager.batch.End();
        }
    }
}
