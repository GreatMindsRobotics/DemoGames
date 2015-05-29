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
        private int score1 = 0, score2 = 0;



        [DataMember]
        private Ball _ball;

        [DataMember]
        private int _w;

        [DataMember]
        private int _h;

        [DataMember]
        private GameMode _gameMode;


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


        [DataMember]
        public Position paddle1Position { get; set; }


        [DataMember]
        public Position paddle2Position { get; set; }

        public Ball Ball
        {
            get
            {
                return _ball;
            }
        }

        public Game(Ball ball, int w, int h, GameMode gameMode)
        {
            _ball = ball;
            _w = w;
            _h = h;
            _gameMode = gameMode;
            gameTimer.Elapsed += new ElapsedEventHandler(gameTimer_Elapsed);
            paddle1Position = new Position(0, 0);
            paddle2Position = new Position(0, 0);
        }

        public void Start()
        {
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
                else if ((_ball.Speed.X < 0 && (_ball.Position.X - _ball.R) <= 0))
                {
                    _ball.Position = new Position(_w / 2, _h / 2);
                    _ball.Speed = new Speed(0, 0);
                    score1++;
                }
                else if ((_ball.Speed.X > 0 && (_ball.Position.X + _ball.R) >= _w))
                {
                    _ball.Position = new Position(_w / 2, _h / 2);
                    _ball.Speed = new Speed(0, 0);
                    score2++;
                }
            }
        }

        public void MoveBall()
        {
            if (_ball.IsMoving)
            {
                _ball.Speed = new Speed(5, 5);
            }
        }



    }
}