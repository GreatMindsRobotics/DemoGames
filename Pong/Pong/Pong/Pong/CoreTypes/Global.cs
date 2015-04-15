using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;

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

        public static Vector2 Scale;

        public static bool UsingKeyboard = false;

        public static bool IsHost = true;

        public static bool Reset = false;
        public static bool Close = false;

        public static string onlineCode;

        public static Pong.WebService.IGMRPongService Webservice;
    }
}
