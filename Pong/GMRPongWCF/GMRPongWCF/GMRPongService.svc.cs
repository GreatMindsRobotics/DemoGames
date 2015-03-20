using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GMRPongWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GMRPongService : IGMRPongService
    {
        Dictionary<string, Game> games = new Dictionary<string, Game>();

        public void AddGame(string name)
        {
            games.Add(name, new Game(name));
        }

        public Position getPlayerPosition(string name, int player)
        {
            return games[name].getPlayerPosition(player);
        }

        public void setPlayerPosition(string name, int player, Position position)
        {
            games[name].setPlayerPosition(player, position);
        }

        public Position GetBallPosition(string name)
        {
            return games[name].getBallPosition();
        }

        public Score GetScore(string name)
        {
            return games[name].getScore();
        }
    }
}
