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
    /// Game result enumeration
    /// </summary>
    public enum GAMERESULT
    {
        WIN,// Game is finished and player won the game
        LOSE,// Game is finished and player lose the game
        CONTINUE,// Continue play
    }
}
