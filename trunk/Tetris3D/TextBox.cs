/*
 * Project: Tetris Project
 * Author: Matthew Urtnowski
 * Date: Fall 2010
 * Class: CECS 491
 * Instructor: Alvaro Monge
 * School: California State University Long Beach - Computer Science
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris3D
{
    public class TextBox
    {
        public GraphicsDevice graphicsDevice;
        public Texture2D backTexture;
        public SpriteFont spriteFont;
        public Color ForeColor = Color.Black;
        public TextAlignOption TextAlign = TextAlignOption.TopLeft;
        public String Text = "";
        public Vector2 Location = Vector2.Zero;
        public Vector2 Size = Vector2.Zero;
        public Vector4 Margins = Vector4.Zero;
        public float Rotation = 0.0f;
        public Vector2 Scale = Vector2.One;
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        public float LayerDepth = 0.0f;
        public bool Wrap;

        protected Color backColor = Color.TransparentWhite;

        public enum TextAlignOption { TopLeft, TopCenter, TopRight, MiddleLeft, MiddleCenter, MiddleRight, BottomLeft, BottomRight, BottomCenter };

        public Color BackColor
        {
            get  //get accessor method
            {
                return this.backColor;
            }
            set   //set accessor method
            {
                this.backColor = value;

                this.backTexture = new Texture2D(this.graphicsDevice, 1, 1);
                Color[] color = { this.backColor };
                this.backTexture.SetData<Color>(color);
            }
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, String fontLocation, String text)
        {
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = owner.content.Load<SpriteFont>(fontLocation);
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, String fontLocation, String text, bool wrap)
        {
            this.Wrap = wrap;
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = owner.content.Load<SpriteFont>(fontLocation);
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, String fontLocation, String text, bool wrap, Vector4 margins)
        {
            this.Margins = margins;
            this.Wrap = wrap;
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = owner.content.Load<SpriteFont>(fontLocation);
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, String fontLocation, String text, Vector4 margins)
        {
            this.Margins = margins;
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = owner.content.Load<SpriteFont>(fontLocation);
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, SpriteFont font, String text)
        {
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = font;
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, SpriteFont font, String text, bool wrap)
        {
            this.Wrap = wrap;
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = font;
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, SpriteFont font, String text, bool wrap, Vector4 margins)
        {
            this.Margins = margins;
            this.Wrap = wrap;
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = font;
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public TextBox(GameScreen owner, Vector2 location, Vector2 size, SpriteFont font, String text, Vector4 margins)
        {
            this.Margins = margins;
            this.Location = location;
            this.Size = size;
            this.graphicsDevice = owner.screenManager.GraphicsDevice;
            this.spriteFont = font;
            this.Text = text;
            this.BackColor = this.backColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.ExecuteDraw(spriteBatch);
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = new SpriteBatch(this.graphicsDevice);
            spriteBatch.Begin();
            this.ExecuteDraw(spriteBatch);
            spriteBatch.End();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        private void ExecuteDraw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here


            spriteBatch.Draw(this.backTexture, new Rectangle((int)this.Location.X, (int)this.Location.Y, (int)(this.Size.X + this.Margins.W + this.Margins.X), (int)(this.Size.Y + this.Margins.Z + this.Margins.Y)), null, Color.White, this.Rotation, Vector2.Zero, this.SpriteEffect, this.LayerDepth);
            if (this.Wrap)
            {
                switch (this.TextAlign)
                {
                    case TextAlignOption.TopLeft: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2(-this.Margins.W, -this.Margins.Z), this.Scale, SpriteEffects.None, this.LayerDepth); break;
                    case TextAlignOption.TopCenter: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2((int)((this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).X - this.Size.X) / 2) - this.Margins.W, -this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.TopRight: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2((int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).X - this.Size.X) - this.Margins.W, -this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.MiddleLeft: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2(-this.Margins.W, (int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).Y - this.Size.Y) / 2 - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.MiddleCenter: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2((int)((this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).X - this.Size.X) / 2) - this.Margins.W, (int)((this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).Y - this.Size.Y) / 2) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.MiddleRight: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2((int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).X - this.Size.X) - this.Margins.W, (int)((this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).Y - this.Size.Y) / 2) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.BottomLeft: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2(-this.Margins.W, (int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).Y - this.Size.Y) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.BottomCenter: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2((int)((this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).X - this.Size.X) / 2) - this.Margins.W, (int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).Y - this.Size.Y) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.BottomRight: spriteBatch.DrawString(this.spriteFont, StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X), this.Location, this.ForeColor, this.Rotation, new Vector2((int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).X - this.Size.X) - this.Margins.W, (int)(this.spriteFont.MeasureString(StringUtilities.getWrapText(this.spriteFont, this.Text, this.Size.X)).Y - this.Size.Y) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    default: break;
                }
            }
            else
            {
                switch (this.TextAlign)
                {
                    case TextAlignOption.TopLeft: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2(-this.Margins.W, this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.TopCenter: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2((int)((this.spriteFont.MeasureString(this.Text).X - this.Size.X) / 2) - this.Margins.W, -this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.TopRight: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2((int)(this.spriteFont.MeasureString(this.Text).X - this.Size.X) - this.Margins.W, -this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.MiddleLeft: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2(-this.Margins.W, (int)((this.spriteFont.MeasureString(this.Text).Y - this.Size.Y) / 2) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.MiddleCenter: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2((int)((this.spriteFont.MeasureString(this.Text).X - this.Size.X) / 2) - this.Margins.W, (int)((this.spriteFont.MeasureString(this.Text).Y - this.Size.Y) / 2) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.MiddleRight: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2((int)(this.spriteFont.MeasureString(this.Text).X - this.Size.X) - this.Margins.W, (int)((this.spriteFont.MeasureString(this.Text).Y - this.Size.Y) / 2) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.BottomLeft: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2(-this.Margins.W, (int)(this.spriteFont.MeasureString(this.Text).Y - this.Size.Y) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.BottomCenter: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2((int)((this.spriteFont.MeasureString(this.Text).X - this.Size.X) / 2) - this.Margins.W, (this.spriteFont.MeasureString(this.Text).Y - this.Size.Y) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    case TextAlignOption.BottomRight: spriteBatch.DrawString(this.spriteFont, this.Text, this.Location, this.ForeColor, this.Rotation, new Vector2((int)(this.spriteFont.MeasureString(this.Text).X - this.Size.X) - this.Margins.W, (this.spriteFont.MeasureString(this.Text).Y - this.Size.Y) - this.Margins.Z), this.Scale, this.SpriteEffect, this.LayerDepth); break;
                    default: break;
                }
            }
        }
    }
}