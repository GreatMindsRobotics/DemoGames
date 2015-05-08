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

        public bool AddGame(string name)
        {
            try
            {
                games.Add(name, new Game(name));
            }
            catch
            {
                return false;
            }
            return true;
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

        public void SetGameType(string name, GameType gameType)
        {
            games[name].setGameType(gameType);
        }

        public GameType GetGameType(string name)
        {
            return games[name].getGameType();
        }
    }
}
