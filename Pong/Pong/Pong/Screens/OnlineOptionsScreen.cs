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

namespace Pong.CoreTypes
{
    class OnlineOptionsScreen : BaseScreen
    {
        Vector2 dropSpeed = new Vector2(0, 45);

        Button hostBtn;
        Button joinBtn;
        Button backBtn;

        //TextButton hostBtn;
        //TextButton joinBtn;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\2Players"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            hostBtn = new Button(Content.Load<Texture2D>("Buttons//Host"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            hostBtn.Origin = new Vector2(hostBtn.Texture.Width / 2, 169);
            hostBtn.Position = new Vector2(960, 469 + hostBtn.SourceRectangle.Value.Height / 2);

            joinBtn = new Button(Content.Load<Texture2D>("Buttons//Join"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            joinBtn.Origin = new Vector2(joinBtn.Texture.Width / 2, 169);
            joinBtn.Position = new Vector2(960, 735 + joinBtn.SourceRectangle.Value.Height / 2);

            //hostBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Host", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            //hostBtn.Origin = new Vector2(hostBtn.Texture.Width / 2, 169);
            //hostBtn.Position = new Vector2(960, 469 + hostBtn.SourceRectangle.Value.Height / 2);


            //joinBtn = new TextButton(Content.Load<Texture2D>("Buttons//Blank"), new Vector2(0, 0), Color.White, Content.Load<SpriteFont>("Fonts\\BigOutage"), Color.White, "Join", new Rectangle(0, 117, 404, 137), new Rectangle(0, 0, 404, 117));
            //joinBtn.Origin = new Vector2(joinBtn.Texture.Width / 2, 169);
            //joinBtn.Position = new Vector2(960, 735 + joinBtn.SourceRectangle.Value.Height / 2);
            

            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);


            _sprites.Add(background);
            _sprites.Add(hostBtn);
            _sprites.Add(joinBtn);
            _sprites.Add(backBtn);

            if (!Global.UsingKeyboard)
            {
                hostBtn.IsPressed = true;
            }
        }

        public override void Update(GameTime gameTime)
        {

            if (Global.UsingKeyboard)
            {
                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Back();
                }

                if (hostBtn.IsClicked)
                {
                    Global.IsHost = true;
                    ScreenManager.Change(ScreenState.GameMode);
                }
                else if (joinBtn.IsClicked)
                {
                    Global.IsHost = false;
                    ScreenManager.Change(ScreenState.OnlineJoinScreen);
                }
                else if (backBtn.IsClicked)
                {
                    ScreenManager.Back();
                }
            }
            else
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
                {
                    if (hostBtn.IsPressed)
                    {
                        hostBtn.IsPressed = false;
                        joinBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
                {
                    if (joinBtn.IsPressed)
                    {
                        joinBtn.IsPressed = false;
                        hostBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (hostBtn.IsPressed)
                    {
                        hostBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                    else if (joinBtn.IsPressed)
                    {
                        joinBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (backBtn.IsPressed)
                    {
                        backBtn.IsPressed = false;
                        hostBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (hostBtn.IsPressed)
                    {
                        Global.IsHost = true;
                        ScreenManager.Change(ScreenState.GameMode);
                    }
                    else if (joinBtn.IsPressed)
                    {
                        Global.IsHost = false;
                        ScreenManager.Change(ScreenState.OnlineJoinScreen);
                    }
                    else if (backBtn.IsPressed)
                    {
                        ScreenManager.Back();
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Reset()
        {
            //titleDropInFont.Reset();
            base.Reset();
        }
    }
}
