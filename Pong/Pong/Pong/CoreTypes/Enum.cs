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

    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }

    public enum ScreenState
    {
        None = 0,
        Title = 1,
        MainMenu = 2,
        Game = 3,
        EditControls = 4,
        GameOver = 5,
        OnePlayerSelect = 6,
        Options = 7,
        PlayerSelect = 8,
        TwoPlayerSelect = 9
    }
}
