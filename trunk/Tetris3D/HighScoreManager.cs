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
        public class HighScoreEntry
        {
            private string scoreText;
            private int scoreInt;
            private string nameText;

            public HighScoreEntry(String line)
            {
                this.Name = line.Substring(0, 3);
                this.ScoreText = line.Substring(3);
            }

            public HighScoreEntry(String name, String value)
            {
                this.Name = name;
                this.ScoreText = value;
            }

            public HighScoreEntry(String name, int value)
            {
                this.Name = name;
                this.ScoreInt = value;
            }

            public string ScoreText
            {
                get
                {
                    return this.scoreText;
                }

                set
                {
                    this.scoreText = value;
                    this.scoreInt = Convert.ToInt32(value);
                }
            }

            public int ScoreInt
            {
                get
                {
                    return this.scoreInt;
                }

                set
                {
                    this.scoreInt = value;
                    this.scoreText = this.scoreInt.ToString();
                }
            }

            public string Name
            {
                get
                {
                    return this.nameText;
                }

                set
                {
                    this.nameText = value;
                }
            }
        }

        public HighScoreEntry[] highscoreEntries = new HighScoreEntry[10];

        private String fileLocation;

        public String HighScoreInitialText
        {
            get
            {
                String text = "";

                for (int i = 0; i < this.highscoreEntries.Length; i++)
                {
                    text += this.highscoreEntries[i].Name + "\n";
                }

                return text;
            }
        }

        public String HighScoreScoreText
        {
            get
            {
                String text = "";

                for (int i = 0; i < this.highscoreEntries.Length; i++)
                {
                    text += this.highscoreEntries[i].ScoreText + "\n";
                }

                return text;
            }
        }


        public HighScoreManager(TetrisModes mode)
        {
            switch (mode)
            {
                case TetrisModes.Classic: this.fileLocation = @"Content\HighScore\ClassicTetris.txt"; break;
                case TetrisModes.Marathon: this.fileLocation = @"Content\HighScore\Marathon.txt"; break;
                case TetrisModes.TimeTrial: this.fileLocation = @"Content\HighScore\TimeTrial.txt"; break;
                case TetrisModes.Challenge1: this.fileLocation = @"Content\HighScore\Challenge1.txt"; break;
                case TetrisModes.Challenge2: this.fileLocation = @"Content\HighScore\Challenge2.txt"; break;
                case TetrisModes.Challenge3: this.fileLocation = @"Content\HighScore\Challenge3.txt"; break;
                case TetrisModes.Challenge4: this.fileLocation = @"Content\HighScore\Challenge4.txt"; break;
                default: throw new NotImplementedException();
            }

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(fileLocation))
                {
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    for (int i = 0; i < 10; i++)
                    {
                        this.highscoreEntries[i] = new HighScoreEntry(sr.ReadLine());
                    }

                    sr.Close();
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        public void Publish()
        {
            try
            {
                // create a writer and open the file
                StreamWriter sw = new StreamWriter(this.fileLocation);

                // write a line of text to the file
                for (int i = 0; i < this.highscoreEntries.Length; i++)
                {
                    sw.WriteLine(this.highscoreEntries[i].Name + this.highscoreEntries[i].ScoreText);
                }

                // close the stream
                sw.Close();
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void Insert(string initals, int score)
        {
            //           #    for(int i = n;i>0 && x[i-1]> t;i--)//using obsolete binding at 'i'     
            //#      x[i]=x[i-1];
            //#      x[i]= t;//name lookup of i changed for new ISO 'for' scoping

            for (int i = this.highscoreEntries.Length - 1; i >= 0; i--)
            {
                if (i == 0 || this.highscoreEntries[i - 1].ScoreInt > score)
                {
                    this.highscoreEntries[i] = new HighScoreEntry(initals, score);
                    i = 0;
                }
                else
                {
                    this.highscoreEntries[i] = this.highscoreEntries[i - 1];
                }
            }
        }
    }
}
