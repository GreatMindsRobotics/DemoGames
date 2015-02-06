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



        private GamePadState _lastGamePad;

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
            _lastGamePad = _gamePad;

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

        public bool IsButtonTapped(GamePadButtons button)
        {
            if (button == GamePadButtons.A)
            {
                return A && _lastGamePad.Buttons.A != ButtonState.Pressed;
            }
            if (button == GamePadButtons.B)
            {
                return B && _lastGamePad.Buttons.B != ButtonState.Pressed;
            }
            if (button == GamePadButtons.X)
            {
                return X && _lastGamePad.Buttons.X != ButtonState.Pressed;
            }
            if (button == GamePadButtons.Y)
            {
                return Y && _lastGamePad.Buttons.Y != ButtonState.Pressed;
            }
            if (button == GamePadButtons.LeftJoyUp)
            {
                return LeftJoyUp && _lastGamePad.ThumbSticks.Left.Y <= 0;
            }
            if (button == GamePadButtons.LeftJoyDown)
            {
                return LeftJoyDown && _lastGamePad.ThumbSticks.Left.Y >= 0;
            }
            if (button == GamePadButtons.LeftJoyLeft)
            {
                return LeftJoyLeft && _lastGamePad.ThumbSticks.Left.X >= 0; ;
            }
            if (button == GamePadButtons.LeftJoyRight)
            {
                return LeftJoyRight && _lastGamePad.ThumbSticks.Left.X <= 0; ;
            }
            if (button == GamePadButtons.RightJoyUp)
            {
                return RightJoyUp && _lastGamePad.ThumbSticks.Right.Y <= 0;
            }
            if (button == GamePadButtons.RightJoyDown)
            {
                return RightJoyDown && _lastGamePad.ThumbSticks.Right.Y >= 0;
            }
            if (button == GamePadButtons.RightJoyLeft)
            {
                return RightJoyLeft && _lastGamePad.ThumbSticks.Right.X >= 0;
            }
            if (button == GamePadButtons.RightJoyRight)
            {
                return RightJoyRight && _lastGamePad.ThumbSticks.Right.X <= 0;
            }
            if (button == GamePadButtons.DPadUp)
            {
                return DPadUp && _lastGamePad.DPad.Up != ButtonState.Pressed;
            }
            if (button == GamePadButtons.DPadDown)
            {
                return DPadDown && _lastGamePad.DPad.Down != ButtonState.Pressed;
            }
            if (button == GamePadButtons.DPadLeft)
            {
                return DPadLeft && _lastGamePad.DPad.Left != ButtonState.Pressed;
            }
            if (button == GamePadButtons.DPadRight)
            {
                return DPadRight && _lastGamePad.DPad.Right != ButtonState.Pressed;
            }
            if (button == GamePadButtons.LeftTrigger)
            {
                return LeftTrigger && _lastGamePad.Triggers.Left == 0;
            }
            if (button == GamePadButtons.RightTrigger)
            {
                return RightTrigger && _lastGamePad.Triggers.Right == 0;
            }
            if (button == GamePadButtons.LeftBumper)
            {
                return LeftBumper && _lastGamePad.Buttons.LeftShoulder != ButtonState.Pressed;
            }
            if (button == GamePadButtons.RightBumper)
            {
                return RightBumper && _lastGamePad.Buttons.RightShoulder != ButtonState.Pressed;
            }
            if (button == GamePadButtons.Back)
            {
                return Back && _lastGamePad.Buttons.Back != ButtonState.Pressed;
            }
            if (button == GamePadButtons.Start)
            {
                return Start && _lastGamePad.Buttons.Start != ButtonState.Pressed;
            }
            return false;
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
