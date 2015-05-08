﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GMRPongWCF
{
    [ServiceContract()]
    public interface IGMRPongService
    {
        [OperationContract]
        bool AddGame(string name);

        [OperationContract]
        Position getPlayerPosition(string name, int player);

        [OperationContract]
        void setPlayerPosition(string name, int player, Position position);

        [OperationContract]
        Position GetBallPosition(string name);

        [OperationContract]
        Score GetScore(string name);

        [OperationContract]
        void SetGameType(string name, GameType gameType);

        [OperationContract]
        GameType GetGameType(string name);
    }
}
