using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris3D
{
   class Shape
   {
      public Shape()
      {
         this.blocks = new Cube[4];
      }

      public ShapeType getShapeType
      {
         get { return name; }
         set { name = value; }
      }

      public Cube[] getBlock
      {
         get { return blocks; }
         set { blocks = value; }
      }

      private ShapeType name;
      protected Cube[] blocks;

      protected Vector2[] txAqua =
        {
            new Vector2(.26f, 0f),   
            new Vector2(.375f, 0f), 
            new Vector2(.375f, 1f), 
            new Vector2(.26f, 1f),
            new Vector2(.26f, 0f),   
            new Vector2(.375f, 0f), 
            new Vector2(.375f, 1f), 
            new Vector2(.26f, 1f),
            new Vector2(.26f, 0f),   
            new Vector2(.375f, 0f), 
            new Vector2(.375f, 1f), 
            new Vector2(.26f, 1f),
            new Vector2(.26f, 0f),   
            new Vector2(.375f, 0f), 
            new Vector2(.375f, 1f), 
            new Vector2(.26f, 1f),
            new Vector2(.26f, 0f),   
            new Vector2(.375f, 0f), 
            new Vector2(.375f, 1f), 
            new Vector2(.26f, 1f),
            new Vector2(.26f, 0f),   
            new Vector2(.375f, 0f), 
            new Vector2(.375f, 1f), 
            new Vector2(.26f, 1f)
        };

      protected Vector2[] txOrange =
      {
            new Vector2(.13f, 0f),   
            new Vector2(.25f, 0f), 
            new Vector2(.25f, 1f), 
            new Vector2(.13f, 1f),
            new Vector2(.13f, 0f),   
            new Vector2(.25f, 0f), 
            new Vector2(.25f, 1f), 
            new Vector2(.13f, 1f),
            new Vector2(.13f, 0f),   
            new Vector2(.25f, 0f), 
            new Vector2(.25f, 1f), 
            new Vector2(.13f, 1f),
            new Vector2(.13f, 0f),   
            new Vector2(.25f, 0f), 
            new Vector2(.25f, 1f), 
            new Vector2(.13f, 1f),
            new Vector2(.13f, 0f),   
            new Vector2(.25f, 0f), 
            new Vector2(.25f, 1f), 
            new Vector2(.13f, 1f),
            new Vector2(.13f, 0f),   
            new Vector2(.25f, 0f), 
            new Vector2(.25f, 1f), 
            new Vector2(.13f, 1f)
      };

      protected Vector2[] txBlue =
      {
			   new Vector2(0f, 0f),   
            new Vector2(.1f, 0f), 
            new Vector2(.1f, 1f), 
            new Vector2(0f, 1f),
            new Vector2(0f, 0f),   
            new Vector2(.1f, 0f), 
            new Vector2(.1f, 1f), 
            new Vector2(0f, 1f),
            new Vector2(0f, 0f),   
            new Vector2(.1f, 0f), 
            new Vector2(.1f, 1f), 
            new Vector2(0f, 1f),
            new Vector2(0f, 0f),   
            new Vector2(.1f, 0f), 
            new Vector2(.1f, 1f), 
            new Vector2(0f, 1f),
            new Vector2(0f, 0f),   
            new Vector2(.1f, 0f), 
            new Vector2(.1f, 1f), 
            new Vector2(0f, 1f),
            new Vector2(0f, 0f),   
            new Vector2(.1f, 0f), 
            new Vector2(.1f, 1f), 
            new Vector2(0f, 1f)
      };


      protected Vector2[] txYellow =
        {
            new Vector2(.38f, 0f),   
            new Vector2(.5f, 0f), 
            new Vector2(.5f, 1f), 
            new Vector2(.38f, 1f),
            new Vector2(.38f, 0f),   
            new Vector2(.5f, 0f), 
            new Vector2(.5f, 1f), 
            new Vector2(.38f, 1f),
            new Vector2(.38f, 0f),   
            new Vector2(.5f, 0f), 
            new Vector2(.5f, 1f), 
            new Vector2(.38f, 1f),
            new Vector2(.38f, 0f),   
            new Vector2(.5f, 0f), 
            new Vector2(.5f, 1f), 
            new Vector2(.38f, 1f),
            new Vector2(.38f, 0f),   
            new Vector2(.5f, 0f), 
            new Vector2(.5f, 1f), 
            new Vector2(.38f, 1f),
            new Vector2(.38f, 0f),   
            new Vector2(.5f, 0f), 
            new Vector2(.5f, 1f), 
            new Vector2(.38f, 1f),
        };

      protected Vector2[] txGreen =
        {
            new Vector2(.51f, 0f),   
            new Vector2(.625f, 0f), 
            new Vector2(.625f, 1f), 
            new Vector2(.51f, 1f),
            new Vector2(.51f, 0f),   
            new Vector2(.625f, 0f), 
            new Vector2(.625f, 1f), 
            new Vector2(.51f, 1f),
            new Vector2(.51f, 0f),   
            new Vector2(.625f, 0f), 
            new Vector2(.625f, 1f), 
            new Vector2(.51f, 1f),
            new Vector2(.51f, 0f),   
            new Vector2(.625f, 0f), 
            new Vector2(.625f, 1f), 
            new Vector2(.51f, 1f),
            new Vector2(.51f, 0f),   
            new Vector2(.625f, 0f), 
            new Vector2(.625f, 1f), 
            new Vector2(.51f, 1f),
            new Vector2(.51f, 0f),   
            new Vector2(.625f, 0f), 
            new Vector2(.625f, 1f), 
            new Vector2(.51f, 1f),
        };

      protected Vector2[] txRed =
        {
            new Vector2(.63f, 0f),   
            new Vector2(.75f, 0f), 
            new Vector2(.75f, 1f), 
            new Vector2(.63f, 1f),
            new Vector2(.63f, 0f),   
            new Vector2(.75f, 0f), 
            new Vector2(.75f, 1f), 
            new Vector2(.63f, 1f),
            new Vector2(.63f, 0f),   
            new Vector2(.75f, 0f), 
            new Vector2(.75f, 1f), 
            new Vector2(.63f, 1f),
            new Vector2(.63f, 0f),   
            new Vector2(.75f, 0f), 
            new Vector2(.75f, 1f), 
            new Vector2(.63f, 1f),
            new Vector2(.63f, 0f),   
            new Vector2(.75f, 0f), 
            new Vector2(.75f, 1f), 
            new Vector2(.63f, 1f),
            new Vector2(.63f, 0f),   
            new Vector2(.75f, 0f), 
            new Vector2(.75f, 1f), 
            new Vector2(.63f, 1f),
        };

      protected Vector2[] txPink =
        {
            new Vector2(.76f, 0f),   
            new Vector2(.875f, 0f), 
            new Vector2(.875f, 1f), 
            new Vector2(.76f, 1f),
            new Vector2(.76f, 0f),   
            new Vector2(.875f, 0f), 
            new Vector2(.875f, 1f), 
            new Vector2(.76f, 1f),
            new Vector2(.76f, 0f),   
            new Vector2(.875f, 0f), 
            new Vector2(.875f, 1f), 
            new Vector2(.76f, 1f),
            new Vector2(.76f, 0f),   
            new Vector2(.875f, 0f), 
            new Vector2(.875f, 1f), 
            new Vector2(.76f, 1f),
            new Vector2(.76f, 0f),   
            new Vector2(.875f, 0f), 
            new Vector2(.875f, 1f), 
            new Vector2(.76f, 1f),
            new Vector2(.76f, 0f),   
            new Vector2(.875f, 0f), 
            new Vector2(.875f, 1f), 
            new Vector2(.76f, 1f),
        };
   }
}
