using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Timers;

namespace GMRPongWCF
{
    [DataContract]
    public class Game
    {
        Timer gameTimer = new Timer(50/3.0);

        [DataMember]
        public int score1 = 0, score2 = 0;

        [DataMember]
        private Ball _ball;

        [DataMember]
        private Paddle _leftPaddle;

        [DataMember]
        private Paddle _rightPaddle;

        [DataMember]
        private int _w;

        [DataMember]
        private int _h;

        [DataMember]
        private int paddleW = 51;

        [DataMember]
        private int paddleH = 355;

        [DataMember]
        private GameMode _gameMode;

        public bool IsGameReady;

        public GameMode GameMode
        {
            get
            {
                return _gameMode;
            }
            set
            {
                _gameMode = value;
            }
        }

        public Ball Ball
        {
            get
            {
                return _ball;
            }
        }

        public Paddle LeftPaddle
        {
            get
            {
                return _leftPaddle;
            }
        }

        public Paddle RightPaddle
        {
            get
            {
                return _rightPaddle;
            }
        }

        public Game(Paddle leftPaddle, Paddle rightPaddle, Ball ball, int w, int h, GameMode gameMode)
        {
            _leftPaddle = leftPaddle;
            _rightPaddle = rightPaddle;
            _ball = ball;
            _w = w;
            _h = h;
            _gameMode = gameMode;
            gameTimer.Elapsed += new ElapsedEventHandler(gameTimer_Elapsed);
            IsGameReady = false;
            gameTimer.Start();
        }

        void gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            _ball.Update();
            if (_gameMode == GMRPongWCF.GameMode.Classical)
            {
                if ((_ball.Speed.Y < 0 && (_ball.Position.Y - _ball.R) <= 0) || (_ball.Speed.Y > 0 && (_ball.Position.Y + _ball.R) >= _h))
                {
                    float newSpeedY = _ball.Speed.Y * -1;
                    _ball.Speed = new Speed(_ball.Speed.X, newSpeedY);
                }
                else if (((_ball.Position.Y + _ball.R) >= (_leftPaddle.Position.Y - (paddleH / 2)) && 
                    (_ball.Position.Y - _ball.R) <= (_leftPaddle.Position.Y + (paddleH / 2))) && 
                    (_ball.Position.X - _ball.R <= _leftPaddle.Position.X + _leftPaddle.W / 2))
                {
                    float newSpeedX = Math.Abs(_ball.Speed.X);
                    _ball.Speed = new Speed(newSpeedX, _ball.Speed.Y);
                }

                else if (((_ball.Position.Y + _ball.R) >= (_rightPaddle.Position.Y - (paddleH / 2)) && 
                    (_ball.Position.Y + _ball.R) <= (_rightPaddle.Position.Y + (paddleH / 2))) && 
                    (_ball.Position.X + _ball.R >= _rightPaddle.Position.X - _rightPaddle.W / 2))
                {
                    float newSpeedX = Math.Abs(_ball.Speed.X) * -1;
                    _ball.Speed = new Speed(newSpeedX, _ball.Speed.Y);
                }

                else if ((_ball.Speed.X < 0 && (_ball.Position.X - _ball.R) <= 0))
                {
                    _ball.Position = new Position(_w / 2, _h / 2);
                    _ball.Speed = new Speed(0, 0);
                    score2++;
                    
                }
                else if ((_ball.Speed.X > 0 && (_ball.Position.X + _ball.R) >= _w))
                {
                    _ball.Position = new Position(_w / 2, _h / 2);
                    _ball.Speed = new Speed(0, 0);
                    score1++;
                }
            }

            // if (_ball.Position.X - _ball.R < leftPaddle.Right && ball.Bottom > Global.LeftPlayer.Top && ball.Top < Global.LeftPlayer.Bottom)
        }




        public void MoveBall(int speedX, int speedY)
        {
            if (!_ball.IsMoving)
            {
                _ball.Speed = new Speed(speedX, speedY);
            }
        }

        public void MovePaddle(int playerIndex, int newYValue)
        {
            if (playerIndex == 1)
            {
                _leftPaddle.Position.Y = newYValue;
            }
            else 
            {
                _rightPaddle.Position.Y = newYValue;
            }
        }
    }
}