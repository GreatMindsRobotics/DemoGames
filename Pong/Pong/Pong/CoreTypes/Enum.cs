﻿using System;
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
        Game = 3
    }
}
