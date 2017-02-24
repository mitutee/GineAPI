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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Hangman_Game
{
    /// <summary>
    /// Class which holds a collection of words for the hangman game
    /// </summary>
    public class Words : List<Word>
    {
        
        /// <summary>
        /// Class which holds a collection of words for the hangman game 
        /// </summary>
        public Words()
        {
            Random r = new Random();
            string[] string_array = File.ReadAllLines("word_rus.txt");


            random:
            string random_string = string_array[r.Next(0, string_array.Length)];
            Regex reg = new Regex("^[а-я]{1,7}$");
            if(!reg.IsMatch(random_string))
            {
                goto random;
            }
            this.Add(new Word() { Content = random_string });
        }

        /// <summary>
        /// Pick a random word
        /// </summary>
        /// <returns></returns>
        public Word Pick
        {
            get
            {
                //Random RandomPick = new Random();
                int index = 0; //(int)(RandomPick.NextDouble() * this.Count);
                Word word = this[index];
                word.Content = word.Content.ToUpper();
                return word;
            }
        }
    }
}
