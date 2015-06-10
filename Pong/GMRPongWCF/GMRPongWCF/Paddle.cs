using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace GMRPongWCF
{
    [DataContract]
    public class Paddle
    { [DataMember]
        private Speed _speed;

        [DataMember]
        private Position _position;

        [DataMember]
        private int _w;
        
        [DataMember]
        private int _h;

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

        public int W
        {
            get
            {
                return _w;
            }
        }

        public int H
        {
            get
            {
                return _h;
            }
        }


        public bool IsMoving
        {
            get
            {
                return _speed.X != 0 || _speed.Y != 0;
            }
        }

        public Paddle(Speed speed, Position position, int w, int h)
        {
            _speed = speed;
            _position = position;
            _w = w;
            _h = h;
        }

        public void Update()
        {
            _position.X += _speed.X;
            _position.Y += _speed.Y;
        }
    }
}