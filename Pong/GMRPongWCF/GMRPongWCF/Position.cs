using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace GMRPongWCF
{
    [DataContract]
    public class Position
    {
        [DataMember]
        float x;

        [DataMember]
        float y;

        public Position()
            : this(0) { }

        public Position(float xy)
            : this(xy, xy) { }

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        [OperationContract]
        public void SetX(float x)
        {
            this.x = x;
        }

        [OperationContract]
        public void SetY(float y)
        {
            this.y = y;
        }

        [OperationContract]
        public float GetX()
        {
            return x;
        }

        [OperationContract]
        public float GetY()
        {
            return y;
        }
    }
}