using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris3D
{

    //TODO: WORK
    public class HighScoreManager
    {
        public String[] highscoreEntries = new String[10];

        public HighScoreManager(TetrisModes mode)
        {
            String fileLocation;

            switch (mode)
            {
                case TetrisModes.Classic: fileLocation = @"HighScore\ClassicTetris.txt"; break;
                case TetrisModes.Marathon: fileLocation = @"HighScore\Marathon.txt"; break;
                case TetrisModes.TimeTrial: fileLocation = @"HighScore\TimeTrial.txt"; break;
                case TetrisModes.Challenge1: fileLocation = @"HighScore\Challenge1.txt"; break;
                case TetrisModes.Challenge2: fileLocation = @"HighScore\Challenge2.txt"; break;
                case TetrisModes.Challenge3: fileLocation = @"HighScore\Challenge3.txt"; break;
                case TetrisModes.Challenge4: fileLocation = @"HighScore\Challenge4.txt"; break;
                default: throw new NotImplementedException();
            }

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(fileLocation))
                {
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    for (int i = 0; i < 10; i++)
                    {
                        this.highscoreEntries[i] = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}
