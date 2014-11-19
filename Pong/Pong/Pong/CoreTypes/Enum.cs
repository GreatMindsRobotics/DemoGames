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

    public enum Mode
    {
        SinglePlayer,
        MultiPlayer
    }

    public enum Difficulty
    { 
        Easy,
        Medium,
        Hard
    }

    public enum GameMode
    { 
        Classical,
        PingPong
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
        EditControls,
        Pause,
        Error,
        GameMode
    }

    public enum ControlScreenState
    {
        SelectingControl,
        WaitingForKey
    }
}
