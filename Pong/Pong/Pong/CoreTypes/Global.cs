using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;

namespace Pong.CoreTypes
{
    public static class Global
    {
        public static Paddle LeftPlayer;
        public static Paddle RightPlayer;

        public static bool isOnline;
        public static Mode Mode;
        public static Difficulty Difficulty;
        public static GameMode GameMode;
    }
}
