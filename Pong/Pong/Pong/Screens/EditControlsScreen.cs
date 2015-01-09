using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.CoreTypes;
using FontEffectsLib;
using FontEffectsLib.CoreTypes;
using FontEffectsLib.FontTypes;
using FontEffectsLib.SpriteTypes;
using System.Xml.Linq;


namespace Pong.Screens
{
    class EditControlsScreen : BaseScreen
    {
        //DropInFont titleDropInFont;

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

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\Keys"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts//JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "Controls", Color.White);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            changeRightUpBtn = new Button(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            changeRightUpBtn.Origin = new Vector2(changeRightUpBtn.Texture.Width / 2, 137);
            changeRightUpBtn.Position = new Vector2(1267, 453 + changeRightUpBtn.SourceRectangle.Value.Height / 2);

            changeRightDownBtn = new Button(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            changeRightDownBtn.Origin = new Vector2(changeRightDownBtn.Texture.Width / 2, 137);
            changeRightDownBtn.Position = new Vector2(1267, 718 + changeRightDownBtn.SourceRectangle.Value.Height / 2);

            changeLeftUpBtn = new Button(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            changeLeftUpBtn.Origin = new Vector2(changeLeftUpBtn.Texture.Width / 2, 137);
            changeLeftUpBtn.Position = new Vector2(635, 453 + changeLeftUpBtn.SourceRectangle.Value.Height / 2);

            changeLeftDownBtn = new Button(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            changeLeftDownBtn.Origin = new Vector2(changeLeftDownBtn.Texture.Width / 2, 137);
            changeLeftDownBtn.Position = new Vector2(635, 718 + changeLeftDownBtn.SourceRectangle.Value.Height / 2);
            
            rightUpDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.RightPlayer.UpKey.ToString(), Color.White, true);
            rightUpDisp.EnableShadow = false;
            rightUpDisp.Position = new Vector2(changeRightUpBtn.Position.X - rightUpDisp.Size.X / 2, changeRightUpBtn.Bottom - 120);

            rightDownDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.RightPlayer.DownKey.ToString(), Color.White, true);
            rightDownDisp.EnableShadow = false;
            rightDownDisp.Position = new Vector2(changeRightDownBtn.Position.X - rightDownDisp.Size.X / 2, changeRightDownBtn.Bottom - 120);

            leftUpDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.LeftPlayer.UpKey.ToString(), Color.White, true);
            leftUpDisp.EnableShadow = false;
            leftUpDisp.Position = new Vector2(changeLeftUpBtn.Position.X - leftUpDisp.Size.X / 2, changeLeftUpBtn.Bottom - 120);

            leftDownDisp = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), Vector2.Zero, 0.1f, 1.0f, 0.1f, 1.0f, Global.LeftPlayer.DownKey.ToString(), Color.White, true);
            leftDownDisp.EnableShadow = false;
            leftDownDisp.Position = new Vector2(changeLeftDownBtn.Position.X - leftDownDisp.Size.X / 2, changeLeftDownBtn.Bottom - 120);
            
            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);

            //_sprites.Add(titleDropInFont);

            _sprites.Add(background);
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
                        rightUpDisp.Position = new Vector2(changeRightUpBtn.Position.X - rightUpDisp.Size.X / 2, changeRightUpBtn.Bottom - 120);
                    }
                    if (buttonthatwaspressed == 2 && pressedKeys[0] != Global.RightPlayer.UpKey && pressedKeys[0] != Global.LeftPlayer.DownKey && pressedKeys[0] != Global.LeftPlayer.UpKey)
                    {
                        rightDownDisp.TintColor = Color.White;

                        Global.RightPlayer.DownKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        rightDownDisp.Text.Clear();
                        rightDownDisp.Text.Append(Global.RightPlayer.DownKey.ToString());
                        rightDownDisp.Position = new Vector2(changeRightDownBtn.Position.X - rightDownDisp.Size.X / 2, changeRightDownBtn.Bottom - 120);
                    }
                    if (buttonthatwaspressed == 3 && pressedKeys[0] != Global.RightPlayer.DownKey && pressedKeys[0] != Global.LeftPlayer.DownKey && pressedKeys[0] != Global.RightPlayer.UpKey)
                    {
                        leftUpDisp.TintColor = Color.White;

                        Global.LeftPlayer.UpKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        leftUpDisp.Text.Clear();
                        leftUpDisp.Text.Append(Global.LeftPlayer.UpKey.ToString());
                        leftUpDisp.Position = new Vector2(changeLeftUpBtn.Position.X - leftUpDisp.Size.X / 2, changeLeftUpBtn.Bottom - 120);
                    }
                    if (buttonthatwaspressed == 4 && pressedKeys[0] != Global.RightPlayer.DownKey && pressedKeys[0] != Global.RightPlayer.UpKey && pressedKeys[0] != Global.LeftPlayer.UpKey)
                    {
                        leftDownDisp.TintColor = Color.White;

                        Global.LeftPlayer.DownKey = pressedKeys[0];
                        state = ControlScreenState.SelectingControl;

                        leftDownDisp.Text.Clear();
                        leftDownDisp.Text.Append(Global.LeftPlayer.DownKey.ToString());
                        leftDownDisp.Position = new Vector2(changeLeftDownBtn.Position.X - leftDownDisp.Size.X / 2, changeLeftDownBtn.Bottom - 120);
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
            
            //titleDropInFont.Reset();
            base.Reset();
        }

    }
}
