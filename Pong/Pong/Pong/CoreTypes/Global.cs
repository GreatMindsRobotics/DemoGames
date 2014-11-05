using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;

namespace Pong.CoreTypes
{
    public static class Global
    {
        public static Paddle Player1;
        public static Paddle Player2;

        public static bool isOnline;
        public static Mode Mode;
        public static Difficulty Difficulty;
    }
}
