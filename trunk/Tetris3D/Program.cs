using System;

namespace Tetris3D
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      static void Main(string[] args)
      {
         using (Game game = new Game())
         {
            game.Run();
         }
      }
   }
}

