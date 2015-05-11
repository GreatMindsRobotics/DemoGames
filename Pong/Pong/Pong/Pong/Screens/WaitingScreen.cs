using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FontEffectsLib.FontTypes;
using Pong.CoreTypes;
using FontEffectsLib.SpriteTypes;

namespace Pong.Screens
{
    class WaitingScreen : BaseScreen
    {
        Vector2 dropSpeed = new Vector2(0, 45);

        FadingFont inputFont;

        Button backBtn;

        GamePadMapper gamePad;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            gamePad = new GamePadMapper(PlayerIndex.One);

            //GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\Waiting"), Vector2.Zero, Color.White);
            //background.Scale = Global.Scale;



            inputFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(840, 600), 0.1f, 1.0f, 0.01f, 1.0f, "Waiting", Color.White, false);
            inputFont.SetCenterAsOrigin();
            inputFont.EnableShadow = false;


            backBtn = new Button(Content.Load<Texture2D>("Buttons//Back"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 159, 169), new Rectangle(0, 0, 159, 149));
            backBtn.Origin = new Vector2(backBtn.Texture.Width / 2, 169);
            backBtn.Position = new Vector2(177, 907 + backBtn.SourceRectangle.Value.Height / 2);


            // _sprites.Add(background);
            _sprites.Add(inputFont);
            _sprites.Add(backBtn);

            if (!Global.UsingKeyboard)
            {
                backBtn.IsPressed = true;
                WebServiceConnection.Client.LeaveGame(WebServiceConnection.GameName, WebServiceConnection.PlayerNumber);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (WebServiceConnection.Client.IsFull(WebServiceConnection.GameName))
            {
                ScreenManager.Change(ScreenState.Game);
            }

            if (Global.UsingKeyboard)
            {

                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Back();
                    WebServiceConnection.Client.LeaveGame(WebServiceConnection.GameName, WebServiceConnection.PlayerNumber);
                }

                if (backBtn.IsClicked)
                {
                    ScreenManager.Back();
                    WebServiceConnection.Client.LeaveGame(WebServiceConnection.GameName, WebServiceConnection.PlayerNumber);
                }

            }
            else
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                    WebServiceConnection.Client.LeaveGame(WebServiceConnection.GameName, WebServiceConnection.PlayerNumber);
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {             
                    if (backBtn.IsPressed)
                    {
                        ScreenManager.Back();
                        WebServiceConnection.Client.LeaveGame(WebServiceConnection.GameName, WebServiceConnection.PlayerNumber);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
