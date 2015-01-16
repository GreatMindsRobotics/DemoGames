using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Pong.CoreTypes
{
    public static class InputManager
    {
        static KeyboardState keyboard;
        static KeyboardState lastkeyboard;

        public  static Keys[] PressedKeys
        {
            get
            {
                return keyboard.GetPressedKeys();
            }
        }

        public static void Update()
        {
            lastkeyboard = keyboard;
            keyboard = Keyboard.GetState();
        }

        public static bool JustPressed(Keys key)
        {
            return keyboard.IsKeyDown(key) && lastkeyboard.IsKeyUp(key);
        }

        public static bool IsDown(Keys key)
        {
            return keyboard.IsKeyDown(key);
        }

    }
}
