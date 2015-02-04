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
        static GamePadMapper player1Pad = new GamePadMapper(Microsoft.Xna.Framework.PlayerIndex.One);
        static GamePadMapper player2Pad = new GamePadMapper(Microsoft.Xna.Framework.PlayerIndex.Two);

        public static GamePadMapper PressedKeysPlayer1
        {
            get
            {
                return player1Pad;
            }
        }
        public static GamePadMapper.GamePadButtons[] PressedKeysPlayer2
        {
            get
            {
                return player2Pad.GetPressedButtons();
            }
        }
        public  static Keys[] PressedKeys
        {
            get
            {
                return keyboard.GetPressedKeys();
            }
        }

        public static void Update()
        {
            player1Pad.Update();
            player2Pad.Update();
            
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
