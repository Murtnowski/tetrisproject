using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris3D
{
    public class StringUtilities
    {
        public static string getWrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    if (word == words[words.Length - 1])
                    {
                        sb.Append(word);
                    }
                    else
                    {
                        sb.Append(word + " ");
                    }
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    if (word == words[words.Length - 1])
                    {
                        sb.Append("\n" + word);
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                    }
                    lineWidth = size.X + spaceWidth;
                }
            }
            return sb.ToString();
        }
    }
}
