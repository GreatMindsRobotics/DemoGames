using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public enum BallState
    {
        Rested,
        Moving
    }

    public enum ScreenState
    {
        None = 0,
        Title = 1,
        MainMenu = 2,
        Game = 3,
        GameOver=4,
        PlayerSelect=5,
        OnePlayerSelect=6,
        TwoPlayerSelect=7
    }   
}
