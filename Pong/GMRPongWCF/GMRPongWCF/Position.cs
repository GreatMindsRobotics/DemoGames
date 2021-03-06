﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GMRPongWCF
{
    [DataContract]
    public class Position
    {
        [DataMember]
        private float _x;

        [DataMember]
        private float _y;


        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public Position(float x, float y)
        {
            _x = x;
            _y = y;
        }

    }
}