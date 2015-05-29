using System;
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
        #region Joining Game Methods
        [OperationContract]
        void AddGame(string name, int width, int height);

        [OperationContract]
        void JoinGame(string name, int playerNumber);

        [OperationContract]
        void SetGameMode(string name, GameMode mode);

        [OperationContract]
        void LeaveGame(string name, int playerNumber);

        [OperationContract]
        bool CheckActiveGame(string name);

        [OperationContract]
        void RemoveGame(string name);

        [OperationContract]
        ICollection<string> GetGameNames();

        [OperationContract]
        bool IsFull(string name);

        #endregion Joining Game Methods



    }
}
