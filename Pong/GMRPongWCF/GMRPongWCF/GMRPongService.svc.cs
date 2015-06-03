﻿using System;
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

        #region Joining Game Methods

        public void AddGame(string name, int width, int height)
        {
            games.Add(name, new Game(new Ball(new Speed(0,0), new Position(width/2, height/2), 55), width, height, GameMode.Classical));//new Game(name));
        }

        public void RemoveGame(string name)
        {
            games.Remove(name);
        }

        public void SetGameMode(string name, GameMode mode)
        {
            games[name].GameMode = mode; 
        }

        public void JoinGame(string name)
        {
            games[name].IsGameReady = true;
        }

        public bool IsFull(string name)
        {
            return games[name].IsGameReady;
        }

        #endregion Joining Game Methods

        /// <summary>
        /// Awesome Player Position Setter made by Jessica P. & Kevin K.
        /// </summary>
        /// <param name="name">Name of gameroom</param>
        /// <param name="player">Number 1 or 2</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void SetPlayerPosition(string name, int player, int x, int y)
        {
            if (player == 1)
            {
                games[name].paddle1Position = new Position(x, y);
            }
            else
            {
                games[name].paddle2Position = new Position(x, y);
            }
        }

        public void LeaveGame(string name)
        {
            throw new NotImplementedException();
            //games[name].setConnection(playerNumber, false);
        }

        public bool CheckActiveGame(string name)
        {
            return games.ContainsKey(name);
        }

        public ICollection<string> GetGameNames() //List<string>
        {
            return games.Keys;
        }

        public Game GetGame(string name)
        {
            return games[name];
        }

        public void MoveBall(string name, int speedX, int speedY)
        {
            games[name].MoveBall(speedX, speedY);
        }
    }
}
