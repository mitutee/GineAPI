//|---------------------------------------------------------------|
//|                       HANGMAN GAME                            |
//|         Developed by Yewondwossen Tadesse(Wonde)              |  
//|                                 Version 1.0.0.0               |
//|                                 Copyright ©  2010             |
//|---------------------------------------------------------------|
//|                       HANGMAN GAME                            |
//|---------------------------------------------------------------|
using Hangbot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hangman_Game
{
    class BaseRect
    {
        private int _w;
        private int _h;


        public int W
        {
            get
            {
                return _w;
            }

            set
            {
                if (value < H) throw new ArgumentException();
                _w = value;
            }
        }

        public int H
        {
            get
            {
                return _h;
            }

            set
            {
                if (value > W) throw new ArgumentException();
                _h = value;
            }
        }

        public BaseRect(int a, int b)
        {
            H = a > b ? b : a;
            W = a <= b ? b : a;
        }
    }
    /// <summary>
    /// Main Program class
    /// </summary>



    class Program
    {
        public chat chat;
        public void Main()
        {

            chat.Output("Добро пожаловать в Висельницу. У вас есть 7 попыток, чтобы угадать слово.");
            string yesNo = string.Empty;
            do
            {
                
                yesNo = playGame();
            } while (yesNo.ToUpper().Equals("Д"));
        }

        /// <summary>
        /// Make text to be blink
        /// </summary>
        /// <param name="text">Text to be blinked</param>
        /// <param name="delay">Deley time value</param>


        /// <summary>
        /// Write blinking text
        /// </summary>
        /// <param name="text">Text to be blinked</param>
        /// <param name="delay">Delay time value</param>
        /// <param name="visible">Set visiblity of the text</param>


        /// <summary>
        /// Play game
        /// </summary>
        private string playGame()
        {
            
                Words words = new Words();
                Word pickedWord = words.Pick;
                PlayHangman playHangman = new PlayHangman();
                playHangman.PickedWord = pickedWord;

            string temp = "";
                for (int i = 0; i < pickedWord.WordLength; i++)
                {
                    temp += " _ ";
                }
            chat.Output(temp);

            while (playHangman.Result() == GAMERESULT.CONTINUE)
            {
                chat.Output("Напиши букву --> ");
                char guessedLetter = chat.Input()[0];
                if (playHangman.AddGuessedLetters(guessedLetter))
                    playHangman.Play();
            }
            if (playHangman.Result() == GAMERESULT.LOSE)
            {
                chat.Output("Вы проиграли :(");
                chat.Output("Загаданное слово было: '" + pickedWord.Content.ToUpper() + "'");
                chat.Output("Вы ходите сыграть еще раз ? Д/Н");

                return chat.Input();
            }
            else
            {
                chat.Output("Вы выйграли !");
                chat.Output("Вы ходите сыграть еще раз ? Д/Н");

                return chat.Input();
            }
        }
    }
}