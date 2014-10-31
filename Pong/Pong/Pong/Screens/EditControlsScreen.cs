using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.CoreTypes;
using Microsoft.Xna.Framework.Input;
using FontEffectsLib.FontTypes;

namespace Pong.Screens
{
    class EditControlsScreen : BaseScreen
    {
        FadingFont rightUpDisp;
        FadingFont rightDownDisp;
        FadingFont leftUpDisp;
        FadingFont leftDownDisp;

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

            rightUpDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(changeRightUpBtn.Right, changeRightUpBtn.Top), 0.1f, 1.0f, 0.01f, 1.0f, Global.Player2.UpKey.ToString(), Color.White, true);
            rightUpDisp.EnableShadow = false;

            rightDownDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(changeRightDownBtn.Right, changeRightDownBtn.Top), 0.1f, 1.0f, 0.01f, 1.0f, Global.Player2.DownKey.ToString(), Color.White, true);
            rightDownDisp.EnableShadow = false;

            leftUpDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(changeLeftUpBtn.Right, changeLeftUpBtn.Top), 0.1f, 1.0f, 0.01f, 1.0f, Global.Player1.UpKey.ToString(), Color.White, true);
            leftUpDisp.EnableShadow = false;
            
            leftDownDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(changeLeftDownBtn.Right, changeLeftDownBtn.Top), 0.1f, 1.0f, 0.01f, 1.0f, Global.Player1.DownKey.ToString(), Color.White, true);
            leftDownDisp.EnableShadow = false;

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;


            _sprites.Add(changeRightUpBtn);
            _sprites.Add(changeRightDownBtn);
            _sprites.Add(changeLeftUpBtn);
            _sprites.Add(changeLeftDownBtn);
            _sprites.Add(rightUpDisp);
            _sprites.Add(rightDownDisp);
            _sprites.Add(leftUpDisp);
            _sprites.Add(leftDownDisp);

            _sprites.Add(backBtn);
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            if (changeRightUpBtn.IsClicked)
            {
                Keys[] pressedKeys = keyboard.GetPressedKeys();
                if (pressedKeys.Length > 0)
                {
                    Global.Player1.UpKey = pressedKeys[0];
                }
                
            }
            else if (changeRightDownBtn.IsClicked)
            {
                Keys[] pressedKeys = keyboard.GetPressedKeys();
                if (pressedKeys.Length > 0)
                {
                    Global.Player1.DownKey = pressedKeys[0];
                }
            }
            else if (changeLeftUpBtn.IsClicked)
            {
                Keys[] pressedKeys = keyboard.GetPressedKeys();
                if (pressedKeys.Length > 0)
                {
                    Global.Player2.UpKey = pressedKeys[0];
                }

            }
            else if (changeLeftDownBtn.IsClicked)
            {
                Keys[] pressedKeys = keyboard.GetPressedKeys();
                if (pressedKeys.Length > 0)
                {
                    Global.Player2.DownKey = pressedKeys[0];
                }
                
            }
            else if (backBtn.IsClicked)
            {
                ScreenManager.Back();
            }

            base.Update(gameTime);
        }

    }
}
