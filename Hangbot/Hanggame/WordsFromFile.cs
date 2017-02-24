using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hangman_Game
{
    class WordsFromFile
    {
        private Dictionary<string, List<string>> dictionary;

        private void loadDictionary()
        {
            try
            {
                dictionary = new Dictionary<string, List<string>>(); // **field to hold your data. Could be List<> or somethin
                using (StreamReader sr = new StreamReader("\\word_rus.txt")) // **Opening file, create reader
                {

                    //**in my dictionary words were separated by "\"
                    string[] arr = sr.ReadLine().Split('\\'); // **read first line

                    

                }
            }
            catch (Exception e)
            {

                chat.Output("Loaded");

            }
            
        }
    }
}



