using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GMRPongWCF
{
    [DataContract]
    public class Score
    {
        [DataMember]
        int leftScore;

        [DataMember]
        int rightScore;

        public Score()
            : this(0, 0) { }

        public Score(int leftScore, int rightScore)
        {
            this.leftScore = leftScore;
            this.rightScore = rightScore;
        }

        [OperationContract]
        public void setleftScore(int score)
        {
            leftScore = score;
        }

        [OperationContract]
        public void getrightScore(int score)
        {
            rightScore = score;
        }

        [OperationContract]
        public int getleftScore()
        {
            return leftScore;
        }

        [OperationContract]
        public int getrightScore()
        {
            return rightScore;
        }
    }
}