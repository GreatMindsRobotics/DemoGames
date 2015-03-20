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

namespace Pong.Screens
{
    public class GameModeScreen : BaseScreen
    {

        //DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button classicalBtn;
        Button pingPongBtn;
        Button backBtn;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\GameMode"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, 50), dropSpeed, "Game Mode", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            classicalBtn = new Button(Content.Load<Texture2D>("Buttons//Classical"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            classicalBtn.Origin = new Vector2(classicalBtn.Texture.Width / 2, 169);
            classicalBtn.Position = new Vector2(960, 469 + classicalBtn.SourceRectangle.Value.Height / 2);

            pingPongBtn = new Button(Content.Load<Texture2D>("Buttons//PingPong"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            pingPongBtn.Origin = new Vector2(pingPongBtn.Texture.Width / 2, 169);
            pingPongBtn.Position = new Vector2(960, 735 + pingPongBtn.SourceRectangle.Value.Height / 2);


            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);

            //_sprites.Add(titleDropInFont);
            _sprites.Add(background);
            _sprites.Add(classicalBtn);
            _sprites.Add(pingPongBtn);
            _sprites.Add(backBtn);

            if (!Global.UsingKeyboard)
            {
                classicalBtn.IsPressed = true;
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
            }
            else 
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
                {
                    if (classicalBtn.IsPressed)
                    {
                        classicalBtn.IsPressed = false;
                        pingPongBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
                {
                    if (pingPongBtn.IsPressed)
                    {
                        pingPongBtn.IsPressed = false;
                        classicalBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (classicalBtn.IsPressed)
                    {
                        classicalBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                    else if (pingPongBtn.IsPressed)
                    {
                        pingPongBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (backBtn.IsPressed)
                    {
                        backBtn.IsPressed = false;
                        classicalBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (classicalBtn.IsPressed)
                    {
                        Global.GameMode = GameMode.Classical;
                        if (Global.Mode == Mode.SinglePlayer)
                        {
                            ScreenManager.Change(ScreenState.OnePlayerSelect);
                        }
                        else
                        {
                            ScreenManager.Change(ScreenState.Game);
                        }
                    }
                    else if (pingPongBtn.IsPressed)
                    {
                        Global.GameMode = GameMode.PingPong;
                        ScreenManager.Change(ScreenState.TwoPlayerSelect);
                        if (Global.Mode == Mode.SinglePlayer)
                        {
                            ScreenManager.Change(ScreenState.OnePlayerSelect);
                        }
                        else
                        {
                            ScreenManager.Change(ScreenState.Game);
                        }
                    }
                    else if (backBtn.IsPressed)
                    {
                        ScreenManager.Back();
                    }
                }
            }

            if (classicalBtn.IsClicked)
            {
                Global.GameMode = GameMode.Classical;

                switch (Global.Mode)
                {
                    case Mode.SinglePlayer:
                        ScreenManager.Change(ScreenState.OnePlayerSelect);
                        break;

                    case Mode.MultiPlayer:
                        ScreenManager.Change(ScreenState.Game);
                        break;

                    default:
                        break;
                }
            }
            else if (pingPongBtn.IsClicked)
            {
                Global.GameMode = GameMode.PingPong;

                switch (Global.Mode)
                {
                    case Mode.SinglePlayer:
                        ScreenManager.Change(ScreenState.OnePlayerSelect);
                        break;

                    case Mode.MultiPlayer:
                        ScreenManager.Change(ScreenState.Game);
                        break;

                    default:
                        break;
                }
            }
            else if(backBtn.IsClicked)
            {
                ScreenManager.Back();
            }

            Global.Reset = true;

            base.Update(gameTime);
        }

        public override void Reset()
        {
            //titleDropInFont.Reset();
            base.Reset();
        }
    }
}
