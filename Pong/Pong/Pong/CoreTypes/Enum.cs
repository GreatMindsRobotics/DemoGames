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
        None,
        Title,
        MainMenu,
        Game,
        GameOver,
        PlayerSelect,
        OnePlayerSelect,
        TwoPlayerSelect,
        Options,
        EditControls
    }
}
