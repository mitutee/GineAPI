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
    /// Class Word
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Class word
        /// </summary>
        public Word()
        {
        }

        /// <summary>
        /// Class word
        /// </summary>
        /// <param name="content">Content of the word</param>
        public Word(string content)
        {
            this.Content = content;
        }

        /// <summary>
        /// Get or set word content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set word length
        /// </summary>
        public int WordLength
        {
            get { return this.Content.Length; }
        }
    }
}
