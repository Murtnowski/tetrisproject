#region File Description
//-----------------------------------------------------------------------------
// ScrollingBackground.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// http://msdn.microsoft.com/en-us/library/bb203868(v=XNAGameStudio.31).aspx
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris3D
{
    /// <summary>
    /// This represents a scroll 2D background
    /// </summary>
    public class ScrollingBackground
    {
        private Vector2 screenPosition, origin, textureSize;

        /// <summary>
        /// The 2D Texture to draw
        /// </summary>
        private Texture2D myTexture;

        private int screenHeight;

        /// <summary>
        /// Constructs a new scrolling background
        /// </summary>
        public ScrollingBackground()
        {
        }

        /// <summary>
        /// Loads a new 2D Texture onto the scrolling background
        /// </summary>
        /// <param name="device">The graphics device the 2D texture will scroll on</param>
        /// <param name="backgroundTexture">The 2D texture to scroll</param>
        public void Load( GraphicsDevice device, Texture2D backgroundTexture )
        {
            myTexture = backgroundTexture;
            screenHeight = device.Viewport.Height;
            int screenwidth = device.Viewport.Width;
            // Set the origin so that we're drawing from the 
            // center of the top edge.
            origin = new Vector2( myTexture.Width / 2, 0 );
            // Set the screen position to the center of the screen.
            screenPosition = new Vector2( screenwidth / 2, screenHeight / 2 );
            // Offset to draw the second texture, when necessary.
            textureSize = new Vector2( 0, myTexture.Height );
        }

        /// <summary>
        /// Used to update the scrolling textures position based on time
        /// </summary>
        /// <param name="deltaY">The time offset for moving the 2D texture</param>
        public void Update( float deltaY )
        {
            screenPosition.Y += deltaY;
            screenPosition.Y = screenPosition.Y % myTexture.Height;
        }
        /// <summary>
        /// Draws the scrolling background
        /// </summary>
        /// <param name="batch">The spritebatch used to draw the 2D Textures</param>
        public void Draw( SpriteBatch batch )
        {
            // Draw the texture, if it is still onscreen.
            if (screenPosition.Y < screenHeight)
            {
                batch.Draw( myTexture, screenPosition, null,
                     Color.White, 0, origin, 1, SpriteEffects.None, 1f );
                //set layer depth to be 1f to NOT mess up the 3d
            }
            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            batch.Draw( myTexture, screenPosition - textureSize, null,
                 Color.White, 0, origin, 1, SpriteEffects.None, 1f );
        }
    }
}
