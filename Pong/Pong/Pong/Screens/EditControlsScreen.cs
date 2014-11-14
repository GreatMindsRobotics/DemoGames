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
using System.Xml.Linq;

namespace Pong.Screens
{
    class EditControlsScreen : BaseScreen
    {
        DropInFont titleDropInFont;

        FadingFont rightUpDisp;
        FadingFont rightDownDisp;
        FadingFont leftUpDisp;
        FadingFont leftDownDisp;

        ControlScreenState state;
        int buttonthatwaspressed = 0;

        Button changeRightUpBtn;
        Button changeRightDownBtn;
        Button changeLeftUpBtn;
        Button changeLeftDownBtn;

        Button backBtn;

        Vector2 dropSpeed = new Vector2(0, 45);

        KeyboardState keyboard;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            state = ControlScreenState.SelectingControl;

            titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts//JingJingTitle"), new Vector2(_viewPort.Width / 2, 50), new Vector2(_viewPort.Width / 2, 50), dropSpeed, "Controls",Color.White);
            titleDropInFont.IsVisible = true;
            titleDropInFont.SetCenterAsOrigin();
            titleDropInFont.EnableShadow = false;
            titleDropInFont.TintColor = Color.Black;
            titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            titleDropInFont.ShadowColor = Color.Gray;

            changeRightUpBtn = new Button(Content.Load<Texture2D>("temp right up button"), new Vector2(0, 0), Color.White);
            changeRightUpBtn.SetCenterAsOrigin();
            changeRightUpBtn.Position = new Vector2(_viewPort.Width / 2 + 200, _viewPort.Height / 2 - 100);

            changeRightDownBtn = new Button(Content.Load<Texture2D>("temp right down button"), new Vector2(0, 0), Color.White);
            changeRightDownBtn.SetCenterAsOrigin();
            changeRightDownBtn.Position = new Vector2(_viewPort.Width / 2 + 200, _viewPort.Height / 2 + 50);

            changeLeftUpBtn = new Button(Content.Load<Texture2D>("temp left up button"), new Vector2(0, 0), Color.White);
            changeLeftUpBtn.SetCenterAsOrigin();
            changeLeftUpBtn.Position = new Vector2(_viewPort.Width / 2 - 200, _viewPort.Height / 2 - 100);

            changeLeftDownBtn = new Button(Content.Load<Texture2D>("temp left down button"), new Vector2(0, 0), Color.White);
            changeLeftDownBtn.SetCenterAsOrigin();
            changeLeftDownBtn.Position = new Vector2(_viewPort.Width / 2 - 200, _viewPort.Height / 2 + 50);

            rightUpDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.RightPlayer.UpKey.ToString(), Color.White, true);
            rightUpDisp.EnableShadow = false;
            rightUpDisp.Position = new Vector2(changeRightUpBtn.Position.X - rightUpDisp.Size.X / 2, changeRightUpBtn.Bottom);

            rightDownDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.RightPlayer.DownKey.ToString(), Color.White, true);
            rightDownDisp.EnableShadow = false;
            rightDownDisp.Position = new Vector2(changeRightDownBtn.Position.X - rightDownDisp.Size.X / 2, changeRightDownBtn.Bottom);

            leftUpDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.LeftPlayer.UpKey.ToString(), Color.White, true);
            leftUpDisp.EnableShadow = false;
            leftUpDisp.Position = new Vector2(changeLeftUpBtn.Position.X - leftUpDisp.Size.X / 2, changeLeftUpBtn.Bottom);

            leftDownDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.LeftPlayer.DownKey.ToString(), Color.White, true);
            leftDownDisp.EnableShadow = false;
            leftDownDisp.Position = new Vector2(changeLeftDownBtn.Position.X - leftDownDisp.Size.X / 2, changeLeftDownBtn.Bottom);

            backBtn = new Button(Content.Load<Texture2D>("temp back button"), new Vector2(10, 10), Color.CornflowerBlue);
            backBtn.SetCenterAsOrigin();
            backBtn.Position = backBtn.Origin;

            _sprites.Add(titleDropInFont);
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
            if (state == ControlScreenState.WaitingForKey)
            {
                if (changeRightUpBtn.IsClicked && buttonthatwaspressed == 1)
                {
                    rightUpDisp.TintColor = Color.White;

                    state = ControlScreenState.SelectingControl;
                }
                if (changeRightDownBtn.IsClicked && buttonthatwaspressed == 2)
                {
                    rightDownDisp.TintColor = Color.White;

                    state = ControlScreenState.SelectingControl;
                }
                if (changeLeftUpBtn.IsClicked && buttonthatwaspressed == 3)
                {
                    leftUpDisp.TintColor = Color.White;

                    state = ControlScreenState.SelectingControl;
                }
                if (changeLeftDownBtn.IsClicked && buttonthatwaspressed == 4)
                {
                    leftDownDisp.TintColor = Color.White;

                    state = ControlScreenState.SelectingControl;
                }
                Keys[] pressedKeys = keyboard.GetPressedKeys();
                if (pressedKeys.Length > 0)
                {
                    if (buttonthatwaspressed == 1 && pressedKeys[0] != Global.RightPlayer.DownKey && pressedKeys[0] != Global.LeftPlayer.DownKey && pressedKeys[0] != Global.LeftPlayer.UpKey)
                    {
                        rightUpDisp.TintColor = Color.White;

                        Global.RightPlayer.UpKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        rightUpDisp.Text.Clear();
                        rightUpDisp.Text.Append(Global.RightPlayer.UpKey.ToString());
                        rightUpDisp.Position = new Vector2(changeRightUpBtn.Position.X - rightUpDisp.Size.X / 2, changeRightUpBtn.Bottom);
                    }
                    if (buttonthatwaspressed == 2 && pressedKeys[0] != Global.RightPlayer.UpKey && pressedKeys[0] != Global.LeftPlayer.DownKey && pressedKeys[0] != Global.LeftPlayer.UpKey)
                    {
                        rightDownDisp.TintColor = Color.White;

                        Global.RightPlayer.DownKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        rightDownDisp.Text.Clear();
                        rightDownDisp.Text.Append(Global.RightPlayer.DownKey.ToString());
                        rightDownDisp.Position = new Vector2(changeRightDownBtn.Position.X - rightDownDisp.Size.X / 2, changeRightDownBtn.Bottom);
                    }
                    if (buttonthatwaspressed == 3 && pressedKeys[0] != Global.RightPlayer.DownKey && pressedKeys[0] != Global.LeftPlayer.DownKey && pressedKeys[0] != Global.RightPlayer.UpKey)
                    {
                        leftUpDisp.TintColor = Color.White;

                        Global.LeftPlayer.UpKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        leftUpDisp.Text.Clear();
                        leftUpDisp.Text.Append(Global.LeftPlayer.UpKey.ToString());
                        leftUpDisp.Position = new Vector2(changeLeftUpBtn.Position.X - leftUpDisp.Size.X / 2, changeLeftUpBtn.Bottom);
                    }
                    if (buttonthatwaspressed == 4 && pressedKeys[0] != Global.RightPlayer.DownKey && pressedKeys[0] != Global.RightPlayer.UpKey && pressedKeys[0] != Global.LeftPlayer.UpKey)
                    {
                        leftDownDisp.TintColor = Color.White;

                        Global.LeftPlayer.DownKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        leftDownDisp.Text.Clear();
                        leftDownDisp.Text.Append(Global.LeftPlayer.DownKey.ToString());
                        leftDownDisp.Position = new Vector2(changeLeftDownBtn.Position.X - leftDownDisp.Size.X / 2, changeLeftDownBtn.Bottom);
                    }
                }

            }
            else if (state == ControlScreenState.SelectingControl)
            {
                if (changeRightUpBtn.IsClicked)
                {        
                    rightUpDisp.TintColor = Color.Yellow;

                    state = ControlScreenState.WaitingForKey;
                    buttonthatwaspressed = 1;
                }
                else if (changeRightDownBtn.IsClicked)
                {
                    rightDownDisp.TintColor = Color.Yellow;

                    state = ControlScreenState.WaitingForKey;
                    buttonthatwaspressed = 2;
                }
                else if (changeLeftUpBtn.IsClicked)
                {
                    leftUpDisp.TintColor = Color.Yellow;

                    state = ControlScreenState.WaitingForKey;
                    buttonthatwaspressed = 3;
                }
                else if (changeLeftDownBtn.IsClicked)
                {
                    leftDownDisp.TintColor = Color.Yellow;

                    state = ControlScreenState.WaitingForKey;
                    buttonthatwaspressed = 4;
                }
            }

            if (backBtn.IsClicked)
            {
                //set the keys to the document
   


                XDocument optionsXml = XDocument.Load(@"XML\Options.xml");

                XElement player1 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Player")).ToList()[0];
                XElement player2 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Player")).ToList()[1];

                player1.Element(XName.Get("Up")).Value = Convert.ToInt32(Global.LeftPlayer.UpKey).ToString();
                player1.Element(XName.Get("Down")).Value = Convert.ToInt32(Global.LeftPlayer.DownKey).ToString();
                player2.Element(XName.Get("Up")).Value = Convert.ToInt32(Global.RightPlayer.UpKey).ToString();
                player2.Element(XName.Get("Down")).Value = Convert.ToInt32(Global.RightPlayer.DownKey).ToString();


                optionsXml.Save(@"XML\Options.xml");
                ScreenManager.Back();
            }

            //rightUpDisp.Text.Clear();
            //rightUpDisp.Text.Append(Global.Player2.UpKey.ToString());

            base.Update(gameTime);
        }

        public override void Reset()
        {
            rightUpDisp.Reset();
            rightDownDisp.Reset();
            leftUpDisp.Reset();
            leftDownDisp.Reset();
            base.Reset();
        }

    }
}
