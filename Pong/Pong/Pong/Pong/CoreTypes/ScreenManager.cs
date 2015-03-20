using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.Screens;

namespace Pong.CoreTypes
{
    public static class ScreenManager
    {
        private static Dictionary<ScreenState, BaseScreen> _screenList = new Dictionary<ScreenState, BaseScreen>();

        private static Stack<BaseScreen> _screenStack = new Stack<BaseScreen>();

        public static void AddScreen(ScreenState state, BaseScreen screen)
        {
            if (_screenList.ContainsKey(state))
            {
                throw new ArgumentException("This state has already been added to the manager class");
            }
            

            _screenList.Add(state, screen);  
        }

        public static void Update(GameTime gameTime)
        {
            //update the top of the stack
            //HINT: _screenStack.Peek()

            _screenStack.Peek().Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //todo: draw the top of the stack
            //HINT: _screenStack.Peek()

            _screenStack.Peek().Draw(spriteBatch);
        }

        public static void Change(ScreenState state)
        {
            if (!_screenList.ContainsKey(state))
            {
                throw new ArgumentException("This state has not been added to the manager class");
            }
            
            _screenStack.Push(_screenList[state]);
            _screenList[state].Reset();
        }

        public static void Back()
        {
            _screenStack.Pop();
            _screenStack.Peek().Reset();
        }

        //private ScreenState _currentScreen = ScreenState.None;
    }
}
