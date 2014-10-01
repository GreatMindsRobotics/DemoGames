using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.Screens;

namespace Pong.CoreTypes
{
    public static class ScreenManager
    {
        private Dictionary<ScreenState, BaseScreen> _screens = new Dictionary<ScreenState, BaseScreen>();

        private ScreenState _currentScreen = ScreenState.None;
    }
}
