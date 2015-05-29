using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GMRPongWCF
{
    [DataContract]
    public class Speed
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
        }

        public float Y
        {
            get
            {
                return _y;
            }
        }

        public Speed(float x, float y)
        {
            _x = x;
            _y = y;
        }
    }
}