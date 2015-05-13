using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Screens;
using FontEffectsLib.SpriteTypes;
using FontEffectsLib.FontTypes;

namespace Pong.CoreTypes
{
    class OnlineJoinScreen : BaseScreen
    {
        Button backBtn;
        TextButton doneBtn;

        FadingFont codeLabel;

        FadingFont takenLabel;

        GamePadMapper gamePad;

        //string code;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\EnterCode"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //connectBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Join", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            //connectBtn.Origin = new Vector2(connectBtn.Texture.Width / 2, 169);
            //connectBtn.Position = new Vector2(960, 735 + connectBtn.SourceRectangle.Value.Height / 2);


            codeLabel = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, Global.onlineCode, Color.LightGoldenrodYellow, false);
            codeLabel.EnableShadow = false;
            codeLabel.SetCenterAsOrigin();

            takenLabel = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 + 100), 0.1f, 1.0f, 0.01f, 1.0f,"That name is taken", Color.Red, false);
            takenLabel.EnableShadow = false;
            takenLabel.SetCenterAsOrigin();
            takenLabel.IsVisible = false;

            doneBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Done", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            doneBtn.Scale = new Vector2(doneBtn.Scale.X + 0.02f, doneBtn.Scale.Y);
            doneBtn.Origin = new Vector2(doneBtn.Texture.Width / 2, 137);
            doneBtn.Position = new Vector2(960, 735 + doneBtn.SourceRectangle.Value.Height / 2);


            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);

            _sprites.Add(background);
            _sprites.Add(codeLabel);
            _sprites.Add(takenLabel);
            _sprites.Add(backBtn);

            _sprites.Add(doneBtn);




            if (!Global.UsingKeyboard)
            {

                doneBtn.IsPressed = true;

            }
        }
        //Abdurrahman
        public override void Update(GameTime gameTime)
        {

            if (!Global.IsHost)
            {
                doneBtn.Text = "Join";
            }

            if (Global.UsingKeyboard)
            {
                if (doneBtn.IsClicked)
                {
                    string name = codeLabel.Text.ToString();
                    if (Global.IsHost)
                    {
                        //add game to wcf


                        if (name.Trim() != "" && !WebServiceConnection.Client.CheckActiveGame(name))
                        {

                            takenLabel.IsVisible = false;
                            WebServiceConnection.Client.AddGame(name);
                            WebServiceConnection.GameName = name;
                            WebServiceConnection.PlayerNumber = 0;
                            ScreenManager.Change(ScreenState.GameMode);
                        }
                        else
                        {
                            takenLabel.IsVisible = true;
                        }
                    }
                    else
                    {
                        //try to connect
                        //remember gamepad!!!!!!!!!
                        if (name.Trim() != "" && WebServiceConnection.Client.CheckActiveGame(name))
                        {
                            if (!WebServiceConnection.Client.IsFull(name))
                            {
                                WebServiceConnection.Client.JoinGame(name, 1);
                                WebServiceConnection.GameName = name;
                                WebServiceConnection.PlayerNumber = 1;
                                ScreenManager.Change(ScreenState.Waiting);
                            }
                        }
                    }
                }

                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Back();
                }

                else if (backBtn.IsClicked)
                {
                    ScreenManager.Back();
                }

                if (InputManager.PressedKeys.Length > 0)
                {
                    if (InputManager.JustPressed(Keys.Back) && Global.onlineCode.Length > 0)
                    {
                        Global.onlineCode = Global.onlineCode.Substring(0, Global.onlineCode.Length - 1);
                    }
                    else if ((InputManager.PressedKeys.Contains(Keys.LeftShift) || InputManager.PressedKeys.Contains(Keys.RightShift)) && InputManager.JustPressed(Keys.D3))
                    {
                        Global.onlineCode += ("#");
                    }
                    else if ((InputManager.PressedKeys.Contains(Keys.LeftShift) || InputManager.PressedKeys.Contains(Keys.RightShift)) && InputManager.JustPressed(Keys.D7))
                    {
                        Global.onlineCode += ("&");
                    }
                    else if ((InputManager.PressedKeys.Contains(Keys.LeftShift) || InputManager.PressedKeys.Contains(Keys.RightShift)) && InputManager.JustPressed(Keys.D8))
                    {
                        Global.onlineCode += ("*");
                    }
                    else if (InputManager.JustPressed(Keys.OemPeriod))
                    {
                        Global.onlineCode += (".");
                    }
                    else if ((InputManager.PressedKeys.Contains(Keys.LeftShift) || InputManager.PressedKeys.Contains(Keys.RightShift)) && InputManager.JustPressed(Keys.OemMinus))
                    {
                        Global.onlineCode += ("_");
                    }
                    else if (InputManager.JustPressed(Keys.OemMinus))
                    {
                        Global.onlineCode += ("-");
                    }
                    else if ((InputManager.PressedKeys.Contains(Keys.LeftShift) || InputManager.PressedKeys.Contains(Keys.RightShift)) && InputManager.JustPressed(Keys.OemQuestion))
                    {
                        Global.onlineCode += ("?");
                    }
                    else if ((InputManager.PressedKeys.Contains(Keys.LeftShift) || InputManager.PressedKeys.Contains(Keys.RightShift)) && InputManager.JustPressed(Keys.D1))
                    {
                        Global.onlineCode += ("!");
                    }
                    else
                    {
                        for (int i = 48; i <= 57; i++)
                        {
                            if (InputManager.JustPressed((Keys)i))
                            {
                                Global.onlineCode += ((Keys)i).ToString()[1];
                            }
                        }

                        for (int i = 65; i <= 90; i++)
                        {
                            if (InputManager.JustPressed((Keys)i))
                            {
                                Global.onlineCode += ((Keys)i).ToString();
                            }
                        }
                    }
                    codeLabel.Text.Clear();
                    codeLabel.Text.Append(Global.onlineCode);
                    codeLabel.SetCenterAsOrigin();
                }
            }
            else
            {
                //IS NOT FOCUSED FOR SOME REASON


                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                }


                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (doneBtn.IsPressed)
                    {
                        doneBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }


                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (backBtn.IsPressed)
                    {
                        backBtn.IsPressed = false;

                        doneBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (doneBtn.IsPressed)
                    {
                        string name = codeLabel.Text.ToString();
                        if (Global.IsHost)
                        {
                            //add game to wcf


                            if (name.Trim() != "" && !WebServiceConnection.Client.CheckActiveGame(name))
                            {

                                takenLabel.IsVisible = false;
                                WebServiceConnection.Client.AddGame(name);
                                WebServiceConnection.GameName = name;
                                WebServiceConnection.PlayerNumber = 0;
                                ScreenManager.Change(ScreenState.GameMode);
                            }
                            else
                            {
                                takenLabel.IsVisible = true;
                            }
                        }
                        else
                        {
                            //try to connect
                            //remember gamepad!!!!!!!!!
                            if (name.Trim() != "" && WebServiceConnection.Client.CheckActiveGame(name))
                            {
                                if (!WebServiceConnection.Client.IsFull(name))
                                {
                                    WebServiceConnection.Client.JoinGame(name, 1);
                                    WebServiceConnection.GameName = name;
                                    WebServiceConnection.PlayerNumber = 1;
                                    ScreenManager.Change(ScreenState.Waiting);
                                }
                            }
                        }
                    }

                    if (backBtn.IsPressed)
                    {
                        ScreenManager.Back();
                    }
                }



                //if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.X))
                //{
                //    code += "X";
                //}
                //else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Y))
                //{
                //    code += "Y";
                //}
                //else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.B))
                //{
                //    code += "B";
                //}

                codeLabel.Text.Clear();

                codeLabel.Text.Append(Global.onlineCode);
                codeLabel.SetCenterAsOrigin();

            }
            base.Update(gameTime);
        }
    }
}
