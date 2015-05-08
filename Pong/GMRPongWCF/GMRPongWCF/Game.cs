using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GMRPongWCF
{
    public enum GameType
    {
        Classic,
        PingPong
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        Position[] players;

        [DataMember]
        Position ball;

        [DataMember]
        Score score;

        [DataMember]
        GameType gameType;

        string name;

        public Game(string name)
        {
            this.name = name;
            players = new Position[2];
            for(int i = 0; i < 2; i++)
            {
                players[i] = new Position();
            }
            ball = new Position();
            score = new Score();
        }

        [OperationContract]
        public Position getPlayerPosition(int player)
        {
            return players[player];
        }

        [OperationContract]
        public void setPlayerPosition(int player, Position position)
        {
            players[player] = position;
        }

        [OperationContract]
        public Position getBallPosition()
        {
            return ball;
        }

        [OperationContract]
        public Score getScore()
        {
            return score;
        }

        [OperationContract]
        public void setGameType(GameType gameType)
        {
            this.gameType = gameType;
        }

        [OperationContract]
        public GameType getGameType()
        {
            return gameType;
        }
    }
}