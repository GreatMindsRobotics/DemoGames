using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class GamePadMapper
    {

        private GamePadState _gamePad;

        private PlayerIndex _player;


        public bool A
        {
            get
            {
                return _gamePad.Buttons.A == ButtonState.Pressed;
            }
        }
        public bool B
        {
            get
            {
                return _gamePad.Buttons.B == ButtonState.Pressed;
            }
        }
        public bool X
        {
            get
            {
                return _gamePad.Buttons.X == ButtonState.Pressed;
            }
        }
        public bool Y
        {
            get
            {
                return _gamePad.Buttons.Y == ButtonState.Pressed;
            }
        }

        public bool LeftJoyUp
        {
            get
            {
                return _gamePad.ThumbSticks.Left.Y == 1;
            }
        }
        public bool LeftJoyDown
        {
            get
            {
                return _gamePad.ThumbSticks.Left.Y == -1;
            }
        }
        public bool LeftJoyLeft
        {
            get
            {
                return _gamePad.ThumbSticks.Left.X == 1;
            }
        }
        public bool LeftJoyRight
        {
            get
            {
                return _gamePad.ThumbSticks.Left.X == -1;
            }
        }

        public bool RightJoyUp
        {
            get
            {
                return _gamePad.ThumbSticks.Right.Y == 1;
            }
        }
        public bool RightJoyDown
        {
            get
            {
                return _gamePad.ThumbSticks.Right.Y == -1;
            }
        }
        public bool RightJoyLeft
        {
            get
            {
                return _gamePad.ThumbSticks.Right.X == 1;
            }
        }
        public bool RightJoyRight
        {
            get
            {
                return _gamePad.ThumbSticks.Right.X == -1;
            }
        }

        public bool DPadUp
        {
            get
            {
                return _gamePad.DPad.Up == ButtonState.Pressed;
            }
        }
        public bool DPadDown
        {
            get
            {
                return _gamePad.DPad.Down == ButtonState.Pressed;
            }
        }
        public bool DPadLeft
        {
            get
            {
                return _gamePad.DPad.Left == ButtonState.Pressed;
            }
        }
        public bool DPadRight
        {
            get
            {
                return _gamePad.DPad.Right == ButtonState.Pressed;
            }
        }

        public bool LeftTrigger
        {
            get
            {
                return _gamePad.Triggers.Left == 1;
            }
        }
        public bool RightTrigger
        {
            get
            {
                return _gamePad.Triggers.Right == 1;
            }
        }

        public bool LeftBumper
        {
            get
            {
                return _gamePad.Buttons.LeftStick == ButtonState.Pressed;
            }
        }
        public bool RightBumper
        {
            get
            {
                return _gamePad.Buttons.RightStick == ButtonState.Pressed;
            }
        }

        public bool Back
        {
            get
            {
                return _gamePad.Buttons.Back == ButtonState.Pressed;
            }
        }
        public bool Start
        {
            get
            {
                return _gamePad.Buttons.Start == ButtonState.Pressed;
            }
        }

        public void Update()
        {
            _gamePad = GamePad.GetState(_player);
        }

        public GamePadMapper(PlayerIndex player)
        {
            _player = player;
        }

        public enum GamePadButtons
        {
            A=0,
            B,
            X,
            Y,
            LeftJoyUp,
            LeftJoyDown,
            LeftJoyLeft,
            LeftJoyRight,
            RightJoyUp,
            RightJoyDown,
            RightJoyLeft,
            RightJoyRight,
            DPadUp,
            DPadDown,
            DPadLeft,
            DPadRight,
            LeftTrigger,
            RightTrigger,
            LeftBumper,
            RightBumper,
            Back,
            Start
        }

        public GamePadButtons[] GetPressedButtons()
        {
            List<GamePadButtons> buttonsPressed = new List<GamePadButtons>();
            for (int i = 0; i < 22; i++)
            {
                if (IsButtonDown((GamePadButtons)i))
                {
                    buttonsPressed.Add((GamePadButtons)i);
                }
            }
            return buttonsPressed.ToArray();
        }

        public bool IsButtonDown(GamePadButtons button)
        {
            if (button == GamePadButtons.A)
            {
                return A;
            }
            if (button == GamePadButtons.B)
            {
                return B;
            }
            if (button == GamePadButtons.X)
            {
                return X;
            }
            if (button == GamePadButtons.Y)
            {
                return Y;
            }
            if (button == GamePadButtons.LeftJoyUp)
            {
                return LeftJoyUp;
            }
            if (button == GamePadButtons.LeftJoyDown)
            {
                return LeftJoyDown;
            }
            if (button == GamePadButtons.LeftJoyLeft)
            {
                return LeftJoyLeft;
            }
            if (button == GamePadButtons.LeftJoyRight)
            {
                return LeftJoyRight;
            }
            if (button == GamePadButtons.RightJoyUp)
            {
                return RightJoyUp;
            }
            if (button == GamePadButtons.RightJoyDown)
            {
                return RightJoyDown;
            }
            if (button == GamePadButtons.RightJoyLeft)
            {
                return RightJoyLeft;
            }
            if (button == GamePadButtons.RightJoyRight)
            {
                return RightJoyRight;
            }
            if (button == GamePadButtons.DPadUp)
            {
                return DPadUp;
            }
            if (button == GamePadButtons.DPadDown)
            {
                return DPadDown;
            }
            if (button == GamePadButtons.DPadLeft)
            {
                return DPadLeft;
            }
            if (button == GamePadButtons.DPadRight)
            {
                return DPadRight;
            }
            if (button == GamePadButtons.LeftTrigger)
            {
                return LeftTrigger;
            }
            if (button == GamePadButtons.RightTrigger)
            {
                return RightTrigger;
            }
            if (button == GamePadButtons.LeftBumper)
            {
                return LeftBumper;
            }
            if (button == GamePadButtons.RightBumper)
            {
                return RightBumper;
            }
            if (button == GamePadButtons.Back)
            {
                return Back;
            }
            if (button == GamePadButtons.Start)
            {
                return Start;
            }
            return false;
        }
    }
}
