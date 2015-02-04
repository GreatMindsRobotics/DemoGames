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
    class TwoPlayerSelectScreen : BaseScreen
    {

        //DropInFont titleDropInFont;
        Vector2 dropSpeed = new Vector2(0, 45);

        Button onlineBtn;
        Button localBtn;
        Button backBtn;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\2Players"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            //titleDropInFont = new DropInFont(Content.Load<SpriteFont>("Fonts\\JingJingTitle"), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), new Vector2(_viewPort.Width / 2, _viewPort.Height * 0.1f), dropSpeed, "Multiplayer", Color.CornflowerBlue);
            //titleDropInFont.IsVisible = true;
            //titleDropInFont.SetCenterAsOrigin();
            //titleDropInFont.EnableShadow = false;
            //titleDropInFont.TintColor = Color.Black;
            //titleDropInFont.ShadowPosition = new Vector2(titleDropInFont.Position.X - 4, titleDropInFont.Position.Y + 4);
            //titleDropInFont.ShadowColor = Color.Gray;

            onlineBtn = new Button(Content.Load<Texture2D>("Buttons//Online"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            onlineBtn.Origin = new Vector2(onlineBtn.Texture.Width / 2, 169);
            onlineBtn.Position = new Vector2(960, 469 + onlineBtn.SourceRectangle.Value.Height / 2);

            localBtn = new Button(Content.Load<Texture2D>("Buttons//Local"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            localBtn.Origin = new Vector2(localBtn.Texture.Width / 2, 169);
            localBtn.Position = new Vector2(960, 735 + localBtn.SourceRectangle.Value.Height / 2);


            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);


            //_sprites.Add(titleDropInFont);
            _sprites.Add(background);
            _sprites.Add(onlineBtn);
            _sprites.Add(localBtn);
            _sprites.Add(backBtn);

            if (!Global.UsingKeyboard)
            {
                onlineBtn.IsPressed = true;
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

                if (onlineBtn.IsClicked)
                {
                    Global.isOnline = true;
                    ScreenManager.Change(ScreenState.Error);
                }
                else if (localBtn.IsClicked)
                {
                    Global.isOnline = false;
                    ScreenManager.Change(ScreenState.GameMode);
                }
                else if (backBtn.IsClicked)
                {
                    ScreenManager.Back();
                }
            }
            else 
            {
                if (InputManager.PressedKeysPlayer1.Back)
                {
                    ScreenManager.Back();
                }

                if (InputManager.PressedKeysPlayer1.DPadDown)
                {
                    if (onlineBtn.IsPressed)
                    {
                        onlineBtn.IsPressed = false;
                        localBtn.IsPressed = true;
                    }
                }
                else if (InputManager.PressedKeysPlayer1.DPadUp)
                {
                    if (localBtn.IsPressed)
                    {
                        localBtn.IsPressed = false;
                        onlineBtn.IsPressed = true;
                    }
                }
                else if (InputManager.PressedKeysPlayer1.DPadLeft)
                {
                    if (onlineBtn.IsPressed)
                    {
                        onlineBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                    else if (localBtn.IsPressed)
                    {
                        localBtn.IsPressed = false;
                        backBtn.IsPressed = true;
                    }
                }
                else if (InputManager.PressedKeysPlayer1.DPadRight)
                {
                    if (backBtn.IsPressed)
                    {
                        backBtn.IsPressed = false;
                        onlineBtn.IsPressed = true;
                    }
                }
                else if (InputManager.PressedKeysPlayer1.A)
                {
                    if (onlineBtn.IsPressed)
                    {
                        Global.Mode = Mode.SinglePlayer;
                        ScreenManager.Change(ScreenState.GameMode);
                    }
                    else if (localBtn.IsPressed)
                    {
                        Global.Mode = Mode.MultiPlayer;
                        ScreenManager.Change(ScreenState.TwoPlayerSelect);
                    }
                    else if (backBtn.IsPressed)
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
