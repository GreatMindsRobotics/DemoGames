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
    class MainMenuScreen : BaseScreen
    {
        ScreenState screenState;

        //DropInFont titleDropInFont;

        Vector2 dropSpeed = new Vector2(0, 45);

        FadingFont inputFont;

        Button singlePlayerBtn;
        Button multiPlayerBtn;
        Button optionsBtn;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\MainMenu"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;


            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, 50), dropSpeed, "Main Menu", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            singlePlayerBtn = new Button(Content.Load<Texture2D>("Buttons//1Player"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            singlePlayerBtn.Origin = new Vector2(singlePlayerBtn.Texture.Width / 2, 169);
            singlePlayerBtn.Position = new Vector2(960, 469 + singlePlayerBtn.SourceRectangle.Value.Height / 2);

            multiPlayerBtn = new Button(Content.Load<Texture2D>("Buttons//2Players"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            multiPlayerBtn.Origin = new Vector2(multiPlayerBtn.Texture.Width / 2, 169);
            multiPlayerBtn.Position = new Vector2(960, 735 + multiPlayerBtn.SourceRectangle.Value.Height / 2);


            optionsBtn = new Button(Content.Load<Texture2D>("Buttons//Settings"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            optionsBtn.Origin = new Vector2(optionsBtn.Texture.Width / 2, 169);
            optionsBtn.Position = new Vector2(1743, 907 + optionsBtn.SourceRectangle.Value.Height / 2);


            inputFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(1610, 987), 0.1f, 1.0f, 0.01f, 1.0f, "", Color.White, false);
            inputFont.SetCenterAsOrigin();
            inputFont.EnableShadow = false;

            _sprites.Add(background);

            //_sprites.Add(titleDropInFont);
            _sprites.Add(singlePlayerBtn);
            _sprites.Add(multiPlayerBtn);
            _sprites.Add(optionsBtn);

            _sprites.Add(inputFont);

            if (!Global.UsingKeyboard)
            {
                singlePlayerBtn.IsPressed = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Global.UsingKeyboard = !InputManager.PressedKeysPlayer1.IsConnected;

            if (!Global.UsingKeyboard && !multiPlayerBtn.IsPressed && !optionsBtn.IsPressed)
            {
                singlePlayerBtn.IsPressed = true;
            }


            //#if XBOX
            //                if (controllerDisconnected)
            //                {

            //                }
            //                else
            //                {

            //                }
            //#endif


            if (Global.UsingKeyboard)
            {
                inputFont.Text.Clear();
                inputFont.Text.Append("Using Keyboard");

                if (InputManager.JustPressed(Keys.Escape))
                {
                    Global.Close = true;
                }
                if (singlePlayerBtn.IsClicked)
                {
                    Global.Mode = Mode.SinglePlayer;
                    ScreenManager.Change(ScreenState.OnePlayerSelect);
                }
                else if (multiPlayerBtn.IsClicked)
                {
                    Global.Mode = Mode.MultiPlayer;
                    ScreenManager.Change(ScreenState.TwoPlayerSelect);
                }
                else if (optionsBtn.IsClicked)
                {
                    ScreenManager.Change(ScreenState.Options);
                }
            }
            else
            {
                inputFont.Text.Clear();
                inputFont.Text.Append("Using GamePad");

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    Global.Close = true;
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadDown))
                {
                    if (singlePlayerBtn.IsPressed)
                    {
                        singlePlayerBtn.IsPressed = false;
                        multiPlayerBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadUp))
                {
                    if (multiPlayerBtn.IsPressed)
                    {
                        multiPlayerBtn.IsPressed = false;
                        singlePlayerBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadRight))
                {
                    if (singlePlayerBtn.IsPressed)
                    {
                        singlePlayerBtn.IsPressed = false;
                        optionsBtn.IsPressed = true;
                    }
                    else if (multiPlayerBtn.IsPressed)
                    {
                        multiPlayerBtn.IsPressed = false;
                        optionsBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.DPadLeft))
                {
                    if (optionsBtn.IsPressed)
                    {
                        optionsBtn.IsPressed = false;
                        singlePlayerBtn.IsPressed = true;
                    }
                }
                else if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    if (singlePlayerBtn.IsPressed)
                    {
                        Global.Mode = Mode.SinglePlayer;
                        ScreenManager.Change(ScreenState.OnePlayerSelect);
                    }
                    else if (multiPlayerBtn.IsPressed)
                    {
                        Global.Mode = Mode.MultiPlayer;
                        ScreenManager.Change(ScreenState.TwoPlayerSelect);
                    }
                    else if (optionsBtn.IsPressed)
                    {
                        ScreenManager.Change(ScreenState.Options);
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
