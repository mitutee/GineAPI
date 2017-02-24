//|---------------------------------------------------------------|
//|                       HANGMAN GAME                            |
//|         Developed by Yewondwossen Tadesse(Wonde)              |  
//|                                 Version 1.0.0.0               |
//|                                 Copyright ©  2010             |
//|---------------------------------------------------------------|
//|                       HANGMAN GAME                            |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman_Game
{
    /// <summary>
    /// Class play hangman game
    /// </summary>
    public class PlayHangman
    {       
        /// <summary>
        /// Get or set the random picked word
        /// </summary>       
        public Word PickedWord {
            get;
            set;
        }
        
        /// <summary>
        /// Variable used to hold guessed and found letters
        /// </summary>
        private List<string> guessed_FoundLetters;

        /// <summary>
        /// Variable used to hold user guessed letters
        /// </summary>
        private List<string> GuessedLetters;

        /// <summary>
        /// Variable used to hold user missed words from the word
        /// </summary>
        private List<string> MissedLetters;

        /// <summary>
        /// Class play hang man game
        /// </summary>
        public PlayHangman()
        {
            guessed_FoundLetters = new List<string>();
            GuessedLetters = new List<string>();
            MissedLetters = new List<string>();
        }

        /// <summary>
        /// Process the user guessed letter against the random picked word
        /// </summary>
        public void Play()
        {
            guessed_FoundLetters = new List<string>();

            for (int i = 0; i < PickedWord.WordLength; i++) // Add underscore to the guessed and found string collection
            {
                guessed_FoundLetters.Add(" _ ");
            }

            for (int i = 0; i < PickedWord.WordLength; i++)
            {
                string letter = PickedWord.Content.Substring(i, 1);
                if (GuessedLetters.Count > 0)
                {
                    foreach (string guessedLetter in this.GuessedLetters)
                    {
                        if (letter.Equals(guessedLetter.Trim().ToUpper())) // If the guessed letter is found from the picked word then replace underscore with the letter
                        {
                            guessed_FoundLetters.RemoveAt(i);
                            guessed_FoundLetters.Insert(i, " " + letter + " ");
                        }
                    }
                }
            }
            drawHangMan();
            chat.Output(buildString(guessed_FoundLetters, false));

        }

        /// <summary>
        /// Add user guessed letter
        /// </summary>
        /// <param name="letter">Letter guessed by the user</param>
        /// <returns>true/false</returns>
        public bool AddGuessedLetters(char letter)
        {
            if (char.IsDigit(letter))  // Check if the letter is a digit
            {

                chat.Output("'" + letter.ToString().ToUpper() + "' Не подходящий тип буквы");
                return false;
            }
            else if (!this.GuessedLetters.Contains(letter.ToString().ToUpper())) // Add guessed letter iif it is not exists
            {
                this.GuessedLetters.Add(letter.ToString().ToUpper());
                chat.Output("Написанные буквы : " + buildString(GuessedLetters, true));
                return true;
            }
            else // letter is exists
            {
                chat.Output("Вы уже писали эту вукву '" + letter.ToString().ToUpper() + "'");
            }
            return false;
        }

        /// <summary>
        /// Check a letter is found from the random picked word
        /// </summary>
        /// <param name="letter">Letter to be checked</param>
        /// <returns>true/false</returns>
        private bool checkLetter(string letter)
        {
            for (int i = 0; i < PickedWord.WordLength; i++)
            {
                string splitedletter = PickedWord.Content.Substring(i, 1).ToUpper();
                if (splitedletter.Equals(letter.Trim().ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Build a string from string collection
        /// </summary>
        /// <param name="inPutString">Collection of strings to be build</param>        
        /// <param name="space">Space to be appended</param>
        /// <returns>an appended string</returns>
        private string buildString(List<string> inPutString, bool space)
        {
            StringBuilder outPut = new StringBuilder();
            foreach (object item in inPutString)
            {
                if (space)
                    outPut = outPut.Append(item.ToString().ToUpper() + " ");
                else
                    outPut = outPut.Append(item.ToString().ToUpper());

            }
            return outPut.ToString();
        }

        /// <summary>
        /// Draw hang man based on the missed letters from the used guessed letters
        /// </summary>
        private void drawHangMan()
        {

            MissedLetters = new List<string>();
            foreach (string item in GuessedLetters)
            {
                if (!checkLetter(item))
                {
                    MissedLetters.Add(item);
                }
            }

            if (MissedLetters.Count == 1)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("__|__");
            }
            else if (MissedLetters.Count == 2)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |     |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("__|__");
            }
            else if (MissedLetters.Count == 3)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |    \\|");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("__|__");
            }
            else if (MissedLetters.Count == 4)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |    \\|/");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("__|__");

            }
            else if (MissedLetters.Count == 5)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |    \\|/");
                chat.Output("  |     |");
                chat.Output("  |");
                chat.Output("  |");
                chat.Output("__|__");
            }
            else if (MissedLetters.Count == 6)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |    \\|/");
                chat.Output("  |     |");
                chat.Output("  |    /");
                chat.Output("  |");
                chat.Output("__|__");
            }
            else if (MissedLetters.Count == 7)
            {
                chat.Output("   _____");
                chat.Output("  |     |");
                chat.Output("  |     O");
                chat.Output("  |    \\|/");
                chat.Output("  |     |");
                chat.Output("  |    / \\");
                chat.Output("  |");
                chat.Output("__|__");
            }


        }

        /// <summary>
        /// Process win lose the game
        /// </summary>
        /// <returns>returns an enumeration of win, lose or proceed</returns>
        public GAMERESULT Result()
        {
            if (MissedLetters.Count == 7)//If full hang man is built then, you lose the game
            {
                return GAMERESULT.LOSE;
            }
            else if (PickedWord.Content.ToUpper().Equals(buildString(guessed_FoundLetters, false).Replace(" ",""))) // If the guessed letters match all the letters from random picked word, then you win the game
            {
                return GAMERESULT.WIN;
            }
            else
                return GAMERESULT.CONTINUE; // Else play the game
        }
    }
}

