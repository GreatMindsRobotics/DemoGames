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
using System.Xml.Linq;
using FontEffectsLib.SpriteTypes;


namespace Pong.Screens
{
    class GameOverScreen : BaseScreen
    {

        FadingFont player1WinsFont;
        FadingFont player2WinsFont;

        Button mainMenuButton;

        GamePadMapper gamePad;

        int backgroundIndex = 0;

        TimeSpan elapsedBackground = TimeSpan.Zero;
        TimeSpan BackgroundSwitch = TimeSpan.FromSeconds(1);

        Texture2D[] backgrounds = new Texture2D[5];
        
        GameSprite background;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            backgrounds[0] = Content.Load<Texture2D>("Background\\Victory01");
            backgrounds[1] = Content.Load<Texture2D>("Background\\Victory02");
            backgrounds[2] = Content.Load<Texture2D>("Background\\Victory03");
            backgrounds[3] = Content.Load<Texture2D>("Background\\Victory04");
            backgrounds[4] = Content.Load<Texture2D>("Background\\Victory05");

            background = new GameSprite(Content.Load<Texture2D>("Background\\Victory01"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;


            gamePad = new GamePadMapper(PlayerIndex.One);

            player1WinsFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 - 150), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player 1 Wins"), Color.White, false);
            player1WinsFont.EnableShadow = false;
            player1WinsFont.SetCenterAsOrigin();
            player1WinsFont.IsVisible = false;

            player2WinsFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\BigOutage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 - 150), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player 2 Wins"), Color.White, false);
            player2WinsFont.EnableShadow = false;
            player2WinsFont.SetCenterAsOrigin();
            player2WinsFont.IsVisible = false;


            mainMenuButton = new Button(Content.Load<Texture2D>("Buttons//Menu"), new Vector2(0, 0), Color.White, new Rectangle(0, 149, 707, 169), new Rectangle(0, 0, 707, 149));
            mainMenuButton.Origin = new Vector2(mainMenuButton.Texture.Width / 2, 169);
            mainMenuButton.Position = new Vector2(960, 800 + mainMenuButton.SourceRectangle.Value.Height / 2);

            _sprites.Add(background);
            _sprites.Add(player1WinsFont);
            _sprites.Add(player2WinsFont);
            _sprites.Add(mainMenuButton);

            if (!Global.UsingKeyboard)
            {
                mainMenuButton.IsPressed = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            elapsedBackground += gameTime.ElapsedGameTime;
            if (elapsedBackground >= BackgroundSwitch)
            {
                elapsedBackground = TimeSpan.Zero;
                backgroundIndex++;
                backgroundIndex %= backgrounds.Length;
                background.Texture = backgrounds[backgroundIndex];
            }
            if (Global.UsingKeyboard)
            {
                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Change(ScreenState.MainMenu);
                }
                if (GameScreen.player1Won)
                {
                    player1WinsFont.IsVisible = true;
                }
                else
                {
                    player2WinsFont.IsVisible = true;
                }
                if (mainMenuButton.IsClicked)
                {
                    ScreenManager.Change(ScreenState.MainMenu);
                }
            }
            else 
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Back();
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A))
                {
                    ScreenManager.Change(ScreenState.MainMenu);
                }

            }
            base.Update(gameTime);
        }

    }
}
