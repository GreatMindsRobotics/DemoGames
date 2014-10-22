using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.CoreTypes;
using Microsoft.Xna.Framework.Input;

namespace Pong.Screens
{
    class EditControlsScreen : BaseScreen
    {

        Button changeRightUpBtn;
        Button changeRightDownBtn;
        Button changeLeftUpBtn;
        Button changeLeftDownBtn;

        Button backBtn;

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            changeRightUpBtn = new Button(Content.Load<Texture2D>("temp right up button"), new Vector2(0, 0), Color.White);
            changeRightUpBtn.SetCenterAsOrigin();
            changeRightUpBtn.Position = new Vector2(_viewPort.Width / 2, 100);

            changeRightDownBtn = new Button(Content.Load<Texture2D>("temp right down button"), new Vector2(0, 0), Color.White);
            changeRightDownBtn.SetCenterAsOrigin();
            changeRightDownBtn.Position = new Vector2(_viewPort.Width / 2, 200);

            changeLeftUpBtn = new Button(Content.Load<Texture2D>("temp left up button"), new Vector2(0, 0), Color.White);
            changeLeftUpBtn.SetCenterAsOrigin();
            changeLeftUpBtn.Position = new Vector2(_viewPort.Width / 2, 300);

            changeLeftDownBtn = new Button(Content.Load<Texture2D>("temp left down button"), new Vector2(0, 0), Color.White);
            changeLeftDownBtn.SetCenterAsOrigin();
            changeLeftDownBtn.Position = new Vector2(_viewPort.Width / 2, 400);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;


            _sprites.Add(changeRightUpBtn);
            _sprites.Add(changeRightDownBtn);
            _sprites.Add(changeLeftUpBtn);
            _sprites.Add(changeLeftDownBtn);

            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            if (changeRightUpBtn.IsClicked)
            {
                //Keys[] pressedKeys = keyboard.GetPressedKeys();
                //if (pressedKeys.Length > 0)
                //{
                //    rightPaddle.UpKey = pressedKeys[0];
                //}
                
            }
            else if (changeRightDownBtn.IsClicked)
            {
                
            }
            else if (changeLeftUpBtn.IsClicked)
            {

            }
            else if (changeLeftDownBtn.IsClicked)
            {
                
            }
            else if (backBtn.IsClicked)
            {
                ScreenManager.Back();
            }

            base.Update(gameTime);
        }

    }
}
