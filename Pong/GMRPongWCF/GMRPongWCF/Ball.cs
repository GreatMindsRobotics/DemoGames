using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GMRPongWCF
{
    [DataContract]
    public class Ball
    {
        [DataMember]
        private Speed _speed;

        [DataMember]
        private Position _position;

        [DataMember]
        private int _r;

        public Speed Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Position Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public int R
        {
            get
            {
                return _r;
            }
        }

        public bool IsMoving
        {
            get
            {
                return _speed.X == 0 && _speed.Y == 0;
            }
        }

        public Ball(Speed speed, Position position, int r)
        {
            _speed = speed;
            _position = position;
            _r = r;
        }

        public void Update()
        {
            _position.X += _speed.X;
            _position.Y += _speed.Y;
        }
    }
}